using System.Collections.Generic;
using Verse;
using RimWorld;

namespace EthanV.AutoRebuildFence
{
    public class AutoRebuildManager : MapComponent
    {
        private readonly List<(int dueTick, IntVec3 pos, ThingDef def, ThingDef stuff)> delayed = new();

        public AutoRebuildManager(Map map) : base(map) { }

        public void RegisterDestroyed(Thing destroyed)
        {
            if (destroyed?.def == null) return;
            if (destroyed.Faction != Faction.OfPlayer) return;  // 非プレイヤーのものは除外
            if (!AutoRebuildSettings.IsTargetDef(destroyed.def)) return;

            delayed.Add((Find.TickManager.TicksGame + 300, destroyed.Position, destroyed.def, destroyed.Stuff));

            if (AutoRebuildSettings.DebugMode)
                Log.Message($"[AutoRebuildFence] Registered rebuild for {destroyed.def.defName} at {destroyed.Position}");
        }

        public override void MapComponentTick()
        {
            if (delayed.Count == 0) return;
            int now = Find.TickManager.TicksGame;

            for (int i = delayed.Count - 1; i >= 0; i--)
            {
                var (due, pos, def, stuff) = delayed[i];
                if (now < due) continue; // まだ時期でない

                try
                {
                    if (!pos.InBounds(map) || map.fogGrid.IsFogged(pos))
                        continue;

                    var can = GenConstruct.CanPlaceBlueprintAt(def, pos, Rot4.North, map, false, null);
                    if (!can.Accepted)
                        continue;

                    ThingDef stuffToUse = def.MadeFromStuff ? (stuff ?? GenStuff.DefaultStuffFor(def)) : null;

                    GenConstruct.PlaceBlueprintForBuild(def, pos, map, Rot4.North, Faction.OfPlayer, stuffToUse);

                    if (AutoRebuildSettings.DebugMode)
                        Log.Message($"[AutoRebuildFence] Blueprint placed for {def.defName} at {pos} ({stuffToUse?.defName ?? "no stuff"})");
                }
                catch (System.Exception ex)
                {
                    Log.Error($"[AutoRebuildFence] Exception while placing blueprint for {def?.defName ?? "null"} at {pos}: {ex}");
                }
                finally
                {
                    delayed.RemoveAt(i);
                }
            }
        }
    }
}
