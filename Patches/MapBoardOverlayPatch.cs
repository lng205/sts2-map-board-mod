using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.Screens.Map;
using Sts2MapBoardMod.Ui;

namespace Sts2MapBoardMod.Patches;

[HarmonyPatch(typeof(NMapScreen), nameof(NMapScreen._Ready))]
public static class MapBoardOverlayPatch
{
    static void Postfix(NMapScreen __instance)
    {
        EnsureOverlay(__instance);
    }

    [HarmonyPatch(typeof(NMapScreen), nameof(NMapScreen.Open))]
    [HarmonyPostfix]
    private static void OnMapOpen(NMapScreen __instance)
    {
        EnsureOverlay(__instance);
    }

    private static void EnsureOverlay(NMapScreen mapScreen)
    {
        var drawings = mapScreen.Drawings;
        if (drawings is null)
        {
            return;
        }

        var overlay = drawings.GetNodeOrNull<MapBoardOverlay>(MapBoardOverlay.NodeName);
        if (overlay is null)
        {
            overlay = new MapBoardOverlay
            {
                Name = MapBoardOverlay.NodeName,
            };

            drawings.AddChild(overlay);
            drawings.MoveChild(overlay, 0);
        }

        overlay.PrepareForInjection();
        overlay.RefreshDefaultPosition();
    }
}
