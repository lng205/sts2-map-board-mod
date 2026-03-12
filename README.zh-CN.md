# STS2 Map Board Mod

English README: [README.md](README.md)
Docs index: [docs/README.md](docs/README.md)
English narrative: [docs/THE_TALK.md](docs/THE_TALK.md)
中文过程记录: [docs/THE_TALK.zh-CN.md](docs/THE_TALK.zh-CN.md)

这是一个通过对话协作完成的 `Slay the Spire 2` 小模组。

它会在地图界面左上区域加入一个固定的棋盘覆盖层，玩家可以直接用游戏原生的地图绘图功能，在等待队友时下五子棋、围棋，或者随手做一些战术标记。

## 这个仓库为什么存在

它不只是一个模组仓库。

它同时也是一个很小但很完整的例子，展示了另一种做项目的方式：

- 从一个模糊的游戏灵感开始
- 通过对话不断收敛实现方向
- 让 Agent 去检查目标游戏、搭环境、建项目、实现首版、发布仓库

所以这里既保存代码，也保存项目是怎样被“聊出来”的。

## 当前功能

- 在地图界面运行时注入一个 `15x15` 棋盘。
- 棋盘位于地图绘图层之下，所以游戏自带的画线和擦除依然可以直接在棋盘上使用。
- 棋盘固定摆在地图左上区域，不再支持拖动。
- 棋盘直接通过代码绘制，不依赖额外图片资源，方便继续微调。

## 项目工作流

这个仓库是在一轮完整的协作里搭出来的：

1. 想法从“把一个五子棋/围棋棋盘放到地图空白处”开始。
2. 实现被收敛成“轻量覆盖层”，而不是完整小游戏。
3. 通过本地游戏文件和 mod 指南确认技术路线。
4. 检查 `sts2.dll` 里的地图界面类型，找到注入点。
5. 安装 `.NET 9 SDK`、`Godot 4.5.1 Mono`、`GitHub CLI`。
6. 搭项目、构建、导出、提交、推送到 GitHub。

## 技术说明

- 运行时补丁: `Harmony`
- 游戏注入目标: `MegaCrit.Sts2.Core.Nodes.Screens.Map.NMapScreen`
- 覆盖层 UI: `Godot Control`
- 构建产物: DLL + `.pck`

## 本地使用

1. 把 `local.props.example` 复制为 `local.props`
2. 修改 `local.props` 里的 `STS2GamePath` 和 `GodotExePath`
3. 运行 `dotnet build`

构建成功后，模组会被自动导出到：

`[Slay the Spire 2]\\mods\\Sts2MapBoardMod\\`

## 仓库说明

- `local.props` 被忽略，因为它包含本机路径。
- `godot/project.godot`、`godot/mod_manifest.json`、`godot/export_presets.cfg` 会在构建时自动生成。
- 当前版本刻意保持固定位置和低干扰风格，让棋盘更像地图背景的一部分，而不是一个漂浮工具面板。
