using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;

[ModInitializer("Initialize")]
public static class ModEntry
{
    public static void Initialize()
    {
        var harmony = new Harmony("sts2.mapboardmod.patch");
        harmony.PatchAll();
        Log.Info("STS2 Map Board Mod initialized.");
    }
}
