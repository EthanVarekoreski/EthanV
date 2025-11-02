using HarmonyLib;
using Verse;

namespace EthanV.AutoRebuildFence
{
    [HarmonyPatch(typeof(Thing), nameof(Thing.Destroy))]
    public static class Patch_DestroyFence
    {
        public static void Prefix(Thing __instance, DestroyMode mode)
        {
            if (__instance.def == null || __instance.Map == null)
                return;

            if (!AutoRebuildManager.IsTargetDef(__instance.def))
                return;

            var manager = __instance.Map.GetComponent<AutoRebuildManager>();
            manager?.RegisterDestroyedStructure(__instance.Position, __instance.def, __instance.Stuff, mode);
        }
    }
}
