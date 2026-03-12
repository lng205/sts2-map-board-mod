using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Screens.Map;
using Sts2MapBoardMod.Ui;

namespace Sts2MapBoardMod.Patches;

[HarmonyPatch(typeof(NMapScreen), nameof(NMapScreen._Ready))]
public static class MapBoardOverlayPatch
{
    private const string MapContainerPath = "TheMap";

    static void Postfix(NMapScreen __instance)
    {
        var mapContainer = __instance.GetNodeOrNull<Control>(MapContainerPath);
        if (mapContainer is null)
        {
            return;
        }

        if (mapContainer.GetNodeOrNull<Node>(MapBoardOverlay.NodeName) is not null)
        {
            return;
        }

        var overlay = new MapBoardOverlay
        {
            Name = MapBoardOverlay.NodeName,
            Position = MapBoardOverlay.DefaultPosition,
        };

        mapContainer.AddChild(overlay);
        mapContainer.MoveChild(overlay, 0);
    }
}
