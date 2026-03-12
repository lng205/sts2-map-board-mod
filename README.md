# STS2 Map Board Mod

An agent-native `Slay the Spire 2` mod, designed in conversation and shipped as a tiny co-op quality-of-life hack.

The mod adds a fixed board overlay to the map screen so players can use the game's built-in map drawing tools for quick gomoku or go-style sketch games while waiting on teammates.

## Why this repo exists

This project is not just "a mod repo."

It is also a small example of a different way to build software:

- start from a vague game-side idea
- refine it through dialogue
- let an agent inspect the target game, set up the toolchain, scaffold the project, implement a first pass, and publish the repo

The build story matters here, so the repo includes both the code and the conversation-shaped project narrative.

Read the talk here: [docs/THE_TALK.md](docs/THE_TALK.md)

## What the mod does

- Injects a 15x15 board into the map scene at runtime.
- Keeps the board behind the native map drawing layer, so the game's draw and erase tools still work on top.
- Uses a fixed placement tuned for the map layout instead of a movable overlay.
- Avoids asset-pipeline complexity by drawing the board in code instead of relying on a static image.

## Agent-built workflow

This repo came together in one collaborative loop:

1. The idea started as "put a gomoku or go board in the blank map space."
2. The implementation was narrowed to "a lightweight board overlay instead of a full minigame."
3. The modding guide was recovered and matched to the actual local game install.
4. The map screen classes in `sts2.dll` were inspected to find a clean injection point.
5. The environment was installed: `.NET 9 SDK`, `Godot 4.5.1 Mono`, and `GitHub CLI`.
6. The project was scaffolded, built, exported, committed, and pushed to GitHub.

## Tech notes

- Runtime patching: `Harmony`
- Game integration target: `MegaCrit.Sts2.Core.Nodes.Screens.Map.NMapScreen`
- Overlay UI: `Godot Control`
- Build output: DLL plus exported `.pck`

## Local setup

1. Copy `local.props.example` to `local.props`.
2. Update `STS2GamePath` and `GodotExePath` in `local.props`.
3. Run `dotnet build`.

On success, the build copies the DLL and exports the Godot pack into:

`[Slay the Spire 2]\\mods\\Sts2MapBoardMod\\`

## Repo notes

- `local.props` is ignored because it contains machine-specific paths.
- `godot/project.godot`, `godot/mod_manifest.json`, and `godot/export_presets.cfg` are generated during build.
- The current version intentionally keeps the board fixed and understated so it feels closer to part of the map background than a floating tool window.
