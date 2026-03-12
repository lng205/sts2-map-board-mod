using Godot;

namespace Sts2MapBoardMod.Ui;

public partial class MapBoardOverlay : Control
{
    public const string NodeName = "CodexMapBoardOverlay";

    public static readonly Vector2 DefaultPosition = new(606f, 920f);

    private static readonly Color BoardColor = new(0.95f, 0.84f, 0.64f, 0.88f);
    private static readonly Color BorderColor = new(0.28f, 0.20f, 0.10f, 0.95f);
    private static readonly Color GridColor = new(0.17f, 0.12f, 0.05f, 0.78f);
    private static readonly Color HandleColor = new(0.34f, 0.24f, 0.11f, 0.92f);
    private static readonly Vector2 BoardPixelSize = new(420f, 420f);

    private const int LineCount = 15;
    private const float Padding = 28f;
    private const float HandleHeight = 18f;
    private const float BorderWidth = 3f;
    private const float GridWidth = 2f;
    private const float StarPointRadius = 4.5f;

    private bool _isDragging;

    public override void _Ready()
    {
        SetAnchorsPreset(LayoutPreset.TopLeft);
        Size = BoardPixelSize;
        CustomMinimumSize = BoardPixelSize;
        MouseFilter = MouseFilterEnum.Pass;
        MouseDefaultCursorShape = CursorShape.PointingHand;
        QueueRedraw();
    }

    public override void _Draw()
    {
        var boardRect = new Rect2(Vector2.Zero, Size);
        DrawRect(boardRect, BoardColor, true);
        DrawRect(boardRect, BorderColor, false, BorderWidth);

        var handleRect = new Rect2(0f, 0f, Size.X, HandleHeight);
        DrawRect(handleRect, HandleColor, true);

        var playArea = new Rect2(
            Padding,
            Padding + 6f,
            Size.X - (Padding * 2f),
            Size.Y - (Padding * 2f) - 6f);

        var stepX = playArea.Size.X / (LineCount - 1);
        var stepY = playArea.Size.Y / (LineCount - 1);

        for (var i = 0; i < LineCount; i++)
        {
            var x = playArea.Position.X + (stepX * i);
            var y = playArea.Position.Y + (stepY * i);

            DrawLine(
                new Vector2(x, playArea.Position.Y),
                new Vector2(x, playArea.End.Y),
                GridColor,
                GridWidth);

            DrawLine(
                new Vector2(playArea.Position.X, y),
                new Vector2(playArea.End.X, y),
                GridColor,
                GridWidth);
        }

        foreach (var starIndex in new[] { 3, 7, 11 })
        {
            var starPoint = new Vector2(
                playArea.Position.X + (stepX * starIndex),
                playArea.Position.Y + (stepY * starIndex));

            DrawCircle(starPoint, StarPointRadius, BorderColor);
        }
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton
            && mouseButton.ButtonIndex == MouseButton.Left
            && mouseButton.Pressed
            && mouseButton.CtrlPressed)
        {
            _isDragging = true;
            MouseDefaultCursorShape = CursorShape.Drag;
            AcceptEvent();
            return;
        }

        if (@event is InputEventMouseMotion mouseMotion && _isDragging)
        {
            Position += mouseMotion.Relative;
            AcceptEvent();
            return;
        }

        if (@event is InputEventMouseButton releaseButton
            && releaseButton.ButtonIndex == MouseButton.Left
            && !releaseButton.Pressed
            && _isDragging)
        {
            _isDragging = false;
            MouseDefaultCursorShape = CursorShape.PointingHand;
            AcceptEvent();
        }
    }
}
