using Verse;
using HarmonyLib;

namespace EthanV.AutoRebuildFence
{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        static Startup()
        {
            new Harmony("EthanV.AutoRebuildFence").PatchAll();
            Log.Message("[AutoRebuildFence] Harmony patches applied.");
        }
    }
}
