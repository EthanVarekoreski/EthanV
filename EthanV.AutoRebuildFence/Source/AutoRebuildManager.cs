using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace EthanV.AutoRebuildFence
{
    public class AutoRebuildManager : MapComponent
    {
        private readonly List<(IntVec3 pos, ThingDef def, ThingDef stuff)> rebuildQueue = new();
        public bool Active;
        private int nextCheckTick;

        public AutoRebuildManager(Map map) : base(map) { }

        /// <summary>
        /// 他MOD含むフェンス・ゲート・動物出入口を判定
        /// </summary>
        public static bool IsTargetDef(ThingDef def)
        {
            if (def == null) return false;
            if (def.category != ThingCategory.Building) return false;

            string name = def.defName.ToLowerInvariant();
            string label = def.label?.ToLowerInvariant() ?? "";

            return name.Contains("fence")
                || name.Contains("gate")
                || name.Contains("animalflap")
                || label.Contains("フェンス")
                || label.Contains("ゲート")
                || label.Contains("出入口");
        }

        /// <summary>
        /// 壊れた建築物の登録（解体・消滅以外のみ）
        /// </summary>
        public void RegisterDestroyedStructure(IntVec3 pos, ThingDef def, ThingDef stuff, DestroyMode mode)
        {
            if (mode == DestroyMode.Deconstruct || mode == DestroyMode.Vanish) return;
            if (!IsTargetDef(def)) return;

            if (!rebuildQueue.Any(r => r.pos == pos))
            {
                rebuildQueue.Add((pos, def, stuff));
                Log.Message($"[AutoRebuildFence] Registered for rebuild: {def.defName} at {pos} (stuff={stuff?.defName ?? "none"}, mode={mode})");
            }

            if (!Active)
            {
                Active = true;
                nextCheckTick = Find.TickManager.TicksGame + 120; // 約2秒後
            }
        }

        public override void MapComponentTick()
        {
            if (!Active) return;
            if (map == null) { Active = false; return; }

            int ticks = Find.TickManager.TicksGame;
            if (ticks < nextCheckTick) return;
            nextCheckTick = ticks + 120;

            if (rebuildQueue.Count == 0)
            {
                Active = false;
                return;
            }

            rebuildQueue.RemoveAll(r => r.def == null);

            for (int i = rebuildQueue.Count - 1; i >= 0; i--)
            {
                var (pos, def, stuff) = rebuildQueue[i];
                if (!pos.InBounds(map) || !map.areaManager.Home[pos])
                {
                    rebuildQueue.RemoveAt(i);
                    continue;
                }

                if (pos.GetThingList(map).Any(t => t.def == def))
                {
                    rebuildQueue.RemoveAt(i);
                    continue;
                }

                if (!GenConstruct.CanPlaceBlueprintAt(def, pos, Rot4.North, map, false, null).Accepted)
                    continue;

                ThingDef useStuff = def.MadeFromStuff ? stuff ?? GenStuff.DefaultStuffFor(def) : null;

                GenConstruct.PlaceBlueprintForBuild(
                    def,
                    pos,
                    map,
                    Rot4.North,
                    Faction.OfPlayer,
                    useStuff
                );

                Log.Message($"[AutoRebuildFence] Placed rebuild blueprint for {def.defName} at {pos} (stuff={useStuff?.defName ?? "none"})");
                rebuildQueue.RemoveAt(i);
            }

            if (rebuildQueue.Count == 0)
            {
                Active = false;
                Log.Message("[AutoRebuildFence] Queue empty, stopping checks");
            }
        }
    }
}
