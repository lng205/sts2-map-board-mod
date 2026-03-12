# STS2 Map Board Mod

`Sts2MapBoardMod` is a small `Slay the Spire 2` mod that adds a draggable board overlay to the map screen so players can use the built-in map drawing tools for quick gomoku/go-style sketch games while waiting in co-op.

## What it does

- Injects a 15x15 board into the map scene at runtime.
- Keeps the board behind the native map drawing layer, so the game's draw/erase tools still work on top.
- Lets you reposition the board with `Ctrl + Left Mouse Drag` if it lands in an awkward spot.

## Prerequisites

- `.NET 9 SDK`
- `Godot 4.5.1 Mono / .NET`
- `Slay the Spire 2`

## Local setup

1. Copy `local.props.example` to `local.props`.
2. Update `STS2GamePath` and `GodotExePath` in `local.props`.
3. Run `dotnet build`.

On success, the build copies the DLL and exports the Godot pack into:

`[Slay the Spire 2]\\mods\\Sts2MapBoardMod\\`

## Notes

- `local.props` is intentionally ignored because it contains machine-specific paths.
- The first implementation draws the board in code instead of loading an image asset, which keeps the repo lightweight and makes size tweaks easier.
