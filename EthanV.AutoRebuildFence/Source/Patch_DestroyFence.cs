using HarmonyLib;
using Verse;

namespace EthanV.AutoRebuildFence
{
    [HarmonyPatch(typeof(Thing), nameof(Thing.DeSpawn))]
    public static class Patch_DeSpawnFence
    {
        [HarmonyPrefix]
        public static void Prefix(Thing __instance, DestroyMode mode)
        {
            // 解体や撤去は対象外
            if (mode == DestroyMode.Deconstruct) return;
            if (__instance == null || __instance.Map == null) return;

            var mgr = __instance.Map.GetComponent<AutoRebuildManager>();
            mgr?.RegisterDestroyed(__instance);
        }
    }
}
