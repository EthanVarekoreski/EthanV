using HarmonyLib;
using Verse;

namespace EthanV.AutoRebuildFence
{
    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        static HarmonyInit()
        {
            var harmony = new Harmony("EthanV.AutoRebuildFence");
            harmony.PatchAll();
            if (AutoRebuildSettings.DebugMode)
                Log.Message("[AutoRebuildFence] Harmony patches applied (DebugMode ON)");
        }
    }
}
