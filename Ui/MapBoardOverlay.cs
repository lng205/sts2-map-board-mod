using Godot;

namespace Sts2MapBoardMod.Ui;

public partial class MapBoardOverlay : Control
{
    public const string NodeName = "CodexMapBoardOverlay";
    private const string VisualRootName = "VisualRoot";

    private static readonly Color BoardColor = new(0.80f, 0.72f, 0.54f, 0.42f);
    private static readonly Color BorderColor = new(0.25f, 0.19f, 0.11f, 0.38f);
    private static readonly Color GridColor = new(0.20f, 0.16f, 0.10f, 0.50f);
    private static readonly Color StarPointColor = new(0.18f, 0.14f, 0.09f, 0.62f);
    private static readonly Vector2 BoardPixelSize = new(455f, 455f);

    private const int LineCount = 15;
    private const float Padding = 24f;
    private const float BorderWidth = 2f;
    private const float GridWidth = 1.5f;
    private const float StarPointRadius = 3.5f;
    private const float DefaultLeftMargin = 92f;
    private const float DefaultTopMargin = 156f;
    private const float MinMargin = 24f;

    private Vector2 _lastViewportSize;

    public override void _Ready()
    {
        PrepareForInjection();
        CallDeferred(nameof(RefreshDefaultPosition));
        QueueRedraw();
    }

    public override void _Process(double delta)
    {
        var viewportSize = GetViewportRect().Size;
        if (viewportSize != _lastViewportSize)
        {
            RefreshDefaultPosition();
        }
    }

    public void PrepareForInjection()
    {
        SetAnchorsPreset(LayoutPreset.TopLeft);
        AnchorLeft = 0f;
        AnchorTop = 0f;
        AnchorRight = 0f;
        AnchorBottom = 0f;
        TopLevel = false;
        Visible = true;
        Size = BoardPixelSize;
        CustomMinimumSize = BoardPixelSize;
        MouseFilter = MouseFilterEnum.Ignore;
        TooltipText = string.Empty;
        ZIndex = 0;
        Scale = Vector2.One;
        EnsureVisuals();
        QueueRedraw();
    }

    public void RefreshDefaultPosition()
    {
        var viewportSize = GetViewportRect().Size;
        if (viewportSize.X <= 0f || viewportSize.Y <= 0f)
        {
            return;
        }

        if (GetParent() is not Control parent)
        {
            return;
        }

        var maxX = Mathf.Max(MinMargin, viewportSize.X - Size.X - MinMargin);
        var maxY = Mathf.Max(MinMargin, viewportSize.Y - Size.Y - MinMargin);
        var desiredScreenPosition = new Vector2(
            Mathf.Clamp(DefaultLeftMargin, MinMargin, maxX),
            Mathf.Clamp(DefaultTopMargin, MinMargin, maxY));

        var inverseCanvasTransform = parent.GetGlobalTransformWithCanvas().Inverse();

        Position = inverseCanvasTransform * desiredScreenPosition;

        _lastViewportSize = viewportSize;
    }

    public override void _Draw()
    {
        // Visuals are built from child ColorRects for more reliable rendering in-game.
    }

    private void EnsureVisuals()
    {
        var visualRoot = GetNodeOrNull<Control>(VisualRootName);
        if (visualRoot is null)
        {
            visualRoot = new Control
            {
                Name = VisualRootName,
                MouseFilter = MouseFilterEnum.Ignore,
            };
            AddChild(visualRoot);
            MoveChild(visualRoot, 0);
        }

        visualRoot.Position = Vector2.Zero;
        visualRoot.Size = BoardPixelSize;
        visualRoot.CustomMinimumSize = BoardPixelSize;

        RebuildVisualChildren(visualRoot);
    }

    private void RebuildVisualChildren(Control visualRoot)
    {
        foreach (var child in visualRoot.GetChildren())
        {
            if (child is Node node)
            {
                node.QueueFree();
            }
        }

        AddRect(visualRoot, "Background", Vector2.Zero, BoardPixelSize, BoardColor);
        AddRect(visualRoot, "BorderTop", Vector2.Zero, new Vector2(BoardPixelSize.X, BorderWidth), BorderColor);
        AddRect(visualRoot, "BorderBottom", new Vector2(0f, BoardPixelSize.Y - BorderWidth), new Vector2(BoardPixelSize.X, BorderWidth), BorderColor);
        AddRect(visualRoot, "BorderLeft", Vector2.Zero, new Vector2(BorderWidth, BoardPixelSize.Y), BorderColor);
        AddRect(visualRoot, "BorderRight", new Vector2(BoardPixelSize.X - BorderWidth, 0f), new Vector2(BorderWidth, BoardPixelSize.Y), BorderColor);

        var playArea = new Rect2(
            Padding,
            Padding,
            BoardPixelSize.X - (Padding * 2f),
            BoardPixelSize.Y - (Padding * 2f));

        var stepX = playArea.Size.X / (LineCount - 1);
        var stepY = playArea.Size.Y / (LineCount - 1);

        for (var i = 0; i < LineCount; i++)
        {
            var x = playArea.Position.X + (stepX * i);
            var y = playArea.Position.Y + (stepY * i);

            AddRect(
                visualRoot,
                $"VLine{i}",
                new Vector2(x - (GridWidth * 0.5f), playArea.Position.Y),
                new Vector2(GridWidth, playArea.Size.Y),
                GridColor);

            AddRect(
                visualRoot,
                $"HLine{i}",
                new Vector2(playArea.Position.X, y - (GridWidth * 0.5f)),
                new Vector2(playArea.Size.X, GridWidth),
                GridColor);
        }

        foreach (var starIndex in new[] { 3, 7, 11 })
        {
            var starPoint = new Vector2(
                playArea.Position.X + (stepX * starIndex),
                playArea.Position.Y + (stepY * starIndex));

            AddRect(
                visualRoot,
                $"Star{starIndex}",
                starPoint - new Vector2(StarPointRadius, StarPointRadius),
                new Vector2(StarPointRadius * 2f, StarPointRadius * 2f),
                StarPointColor);
        }
    }

    private static void AddRect(Control parent, string name, Vector2 position, Vector2 size, Color color)
    {
        var rect = new ColorRect
        {
            Name = name,
            Position = position,
            Size = size,
            Color = color,
            MouseFilter = MouseFilterEnum.Ignore,
        };

        parent.AddChild(rect);
    }
}
