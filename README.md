# STS2 Map Board Mod

中文说明: [README.zh-CN.md](README.zh-CN.md)
English narrative: [docs/THE_TALK.md](docs/THE_TALK.md)
中文过程记录: [docs/THE_TALK.zh-CN.md](docs/THE_TALK.zh-CN.md)

An agent-native `Slay the Spire 2` mod, designed in conversation and shipped as a small co-op quality-of-life hack.

The mod adds a fixed 15x15 board overlay to the map screen so players can use the game's built-in map drawing tools for quick gomoku or go-style sketch games while waiting on teammates.

## What the mod does

- Injects a 15x15 board into the map scene at runtime.
- Keeps the board behind the native map drawing layer, so the game's draw and erase tools still work on top.
- Uses a fixed placement in the upper-left area of the map instead of a movable overlay.
- Draws the board in code, so the mod does not depend on extra image assets.

## Install

1. Download the latest release zip from GitHub Releases.
2. Extract the `Sts2MapBoardMod` folder from the archive.
3. Copy that folder into `[Slay the Spire 2]\\mods\\`.
4. Start the game and choose `Load Mods`.
5. Open the map screen. The board appears in the upper-left area.

To uninstall, delete:

`[Slay the Spire 2]\\mods\\Sts2MapBoardMod\\`

## Build locally

1. Copy `local.props.example` to `local.props`.
2. Update `STS2GamePath` and `GodotExePath` in `local.props`.
3. Run `dotnet build`.

On success, the build copies the DLL and exports the Godot pack into:

`[Slay the Spire 2]\\mods\\Sts2MapBoardMod\\`

## Tech notes

- Runtime patching: `Harmony`
- Game integration target: `MegaCrit.Sts2.Core.Nodes.Screens.Map.NMapScreen`
- Overlay UI: `Godot Control`
- Build output: DLL plus exported `.pck`

## Why this repo exists

This repo is both a mod project and a record of an agent-centered build workflow:

1. Start from a vague gameplay idea.
2. Refine the scope through dialogue.
3. Inspect the game, set up the toolchain, scaffold the project, implement the mod, and publish it.

The narrative part is intentional. If you want the full build story, read:

- [docs/THE_TALK.md](docs/THE_TALK.md)
- [docs/THE_TALK.zh-CN.md](docs/THE_TALK.zh-CN.md)

## Repo notes

- `local.props` is ignored because it contains machine-specific paths.
- `godot/project.godot`, `godot/mod_manifest.json`, and `godot/export_presets.cfg` are generated during build.
- The current version keeps the board fixed and understated so it feels closer to part of the map background than a floating tool window.
