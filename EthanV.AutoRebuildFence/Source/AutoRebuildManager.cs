using System.Collections.Generic;
using Verse;
using RimWorld;

namespace EthanV.AutoRebuildFence
{
    public class AutoRebuildManager : MapComponent
    {
        private readonly List<(IntVec3 pos, ThingDef def, ThingDef stuff)> rebuildQueue = new();
        private bool active;
        private int nextCheckTick;

        public AutoRebuildManager(Map map) : base(map) { }

        public void RegisterDestroyed(Thing destroyed)
        {
            if (destroyed?.def == null) return;
            if (!AutoRebuildSettings.IsTargetDef(destroyed.def)) return;

            rebuildQueue.Add((destroyed.Position, destroyed.def, destroyed.Stuff));

            if (AutoRebuildSettings.DebugMode)
                Log.Message($"[AutoRebuildFence] Registered rebuild for {destroyed.def.defName} at {destroyed.Position}");

            if (!active)
            {
                active = true;
                nextCheckTick = Find.TickManager.TicksGame + 300; // 5秒後
            }
        }

        public override void MapComponentTick()
        {
            // map が破棄済みまたは未初期化なら安全にスキップ
            if (map == null || map.fogGrid == null || map.floodFiller == null)
                return;

            if (!active) return;

            int ticks = Find.TickManager.TicksGame;
            if (ticks < nextCheckTick) return;
            nextCheckTick = ticks + 300;

            if (rebuildQueue.Count == 0)
            {
                active = false;
                return;
            }

            for (int i = rebuildQueue.Count - 1; i >= 0; i--)
            {
                var (pos, def, stuff) = rebuildQueue[i];

                if (!pos.InBounds(map) || map.fogGrid.IsFogged(pos))
                    continue;

                var canPlace = GenConstruct.CanPlaceBlueprintAt(def, pos, Rot4.North, map, false, null);
                if (!canPlace.Accepted)
                    continue;

                ThingDef stuffToUse = def.MadeFromStuff ? (stuff ?? GenStuff.DefaultStuffFor(def)) : null;

                try
                {
                    GenConstruct.PlaceBlueprintForBuild(def, pos, map, Rot4.North, Faction.OfPlayer, stuffToUse);

                    if (AutoRebuildSettings.DebugMode)
                        Log.Message($"[AutoRebuildFence] Blueprint placed for {def.defName} at {pos} ({stuffToUse?.defName ?? "no stuff"})");

                    rebuildQueue.RemoveAt(i);
                }
                catch (System.Exception ex)
                {
                    Log.Error($"[AutoRebuildFence] Exception while placing blueprint for {def?.defName ?? "null"} at {pos}: {ex}");
                    rebuildQueue.RemoveAt(i);
                }
            }

            if (rebuildQueue.Count == 0)
                active = false;
        }
    }
}
