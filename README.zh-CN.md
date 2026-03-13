# STS2 Map Board Mod

English README: [README.md](README.md)
English narrative: [docs/THE_TALK.md](docs/THE_TALK.md)
中文过程记录: [docs/THE_TALK.zh-CN.md](docs/THE_TALK.zh-CN.md)

这是一个通过对话协作完成的 `Slay the Spire 2` 小模组。

它会在地图界面左上区域加入一个固定的 `15x15` 棋盘覆盖层，玩家可以直接使用游戏自带的地图绘图功能，在等待队友时下五子棋、围棋，或者顺手做一些战术标记。

## 功能

- 在地图界面运行时注入一个 `15x15` 棋盘。
- 棋盘位于地图绘图层之下，所以游戏原生的画线和擦除仍然可以直接在棋盘上使用。
- 棋盘固定摆放在地图左上区域，不再支持拖动。
- 棋盘直接通过代码绘制，不依赖额外图片资源。

## 安装

1. 从 GitHub Releases 下载最新压缩包。
2. 解压后取得 `Sts2MapBoardMod` 文件夹。
3. 把这个文件夹复制到 `[Slay the Spire 2]\\mods\\`。
4. 启动游戏并选择 `Load Mods`。
5. 进入地图界面后，棋盘会显示在左上区域。

卸载时直接删除：

`[Slay the Spire 2]\\mods\\Sts2MapBoardMod\\`

## 本地构建

1. 把 `local.props.example` 复制为 `local.props`。
2. 修改 `local.props` 里的 `STS2GamePath` 和 `GodotExePath`。
3. 运行 `dotnet build`。

构建成功后，模组会被自动导出到：

`[Slay the Spire 2]\\mods\\Sts2MapBoardMod\\`

## 技术说明

- 运行时补丁：`Harmony`
- 游戏注入目标：`MegaCrit.Sts2.Core.Nodes.Screens.Map.NMapScreen`
- 覆盖层 UI：`Godot Control`
- 构建产物：DLL + `.pck`

## 为什么要保留这个仓库

这个仓库不只是一个模组仓库，也是一次以 Agent 为中心完成项目的记录：

1. 从一个模糊的游戏想法开始。
2. 通过对话不断收窄实现范围。
3. 由 Agent 检查游戏、搭环境、建项目、实现功能并发布。

如果你想看完整过程，可以继续读：

- [docs/THE_TALK.md](docs/THE_TALK.md)
- [docs/THE_TALK.zh-CN.md](docs/THE_TALK.zh-CN.md)

## 仓库说明

- `local.props` 被忽略，因为它包含本机路径。
- `godot/project.godot`、`godot/mod_manifest.json`、`godot/export_presets.cfg` 会在构建时自动生成。
- 当前版本刻意保持固定位置和低干扰风格，让棋盘更像地图背景的一部分，而不是一个漂浮工具窗。
