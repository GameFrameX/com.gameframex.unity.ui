## [2.3.1](https://github.com/gameframex/com.gameframex.unity.ui/compare/2.3.0...2.3.1) (2026-01-08)


### Bug Fixes

* **UIForm:** 在OnClose方法中添加gameObject空检查 ([de70843](https://github.com/gameframex/com.gameframex.unity.ui/commit/de70843de677947729e48d70608eeeaebf727e9b))

# [2.3.0](https://github.com/gameframex/com.gameframex.unity.ui/compare/2.2.0...2.3.0) (2026-01-05)


### Features

* **UI组件:** 添加根据类型获取已加载UI的方法 ([21b63ac](https://github.com/gameframex/com.gameframex.unity.ui/commit/21b63ac515fff5217115e4afbe1f7fda8b3b56a6))

# [2.2.0](https://github.com/gameframex/com.gameframex.unity.ui/compare/2.1.1...2.2.0) (2025-12-23)


### Bug Fixes

* **UIForm:** 添加OnAwake调用以确保初始化完成 ([5122c3d](https://github.com/gameframex/com.gameframex.unity.ui/commit/5122c3d3d0960375f00ae182c89460571ab96e04))
* **UI:** 修复关闭UI表单时未从待释放列表移除的问题 ([848fc83](https://github.com/gameframex/com.gameframex.unity.ui/commit/848fc837dac196d42126f3c0ea731469664e55b6))
* **UI:** 初始化时添加缺失的成员变量默认值 ([f1835ef](https://github.com/gameframex/com.gameframex.unity.ui/commit/f1835ef8926bc3eec5bfbf3dba5a0089a4db90cb))
* **UI:** 将异常抛出改为日志记录并添加提前返回 ([f24cb5f](https://github.com/gameframex/com.gameframex.unity.ui/commit/f24cb5f6dc7e38eb31603cac10e064b995242367))
* **UI:** 添加异常处理以防止UI表单回收时崩溃 ([e895fcf](https://github.com/gameframex/com.gameframex.unity.ui/commit/e895fcf34c481bc17f16f744da46e88cb189d609))
* 修正获取OptionUIConfigAttribute时的类型错误 ([231c2cf](https://github.com/gameframex/com.gameframex.unity.ui/commit/231c2cf63e4348f3a4949f7aa96d04dcd22a9f43))


### Features

* **UI:** 为动画属性添加简化构造函数 ([68b651a](https://github.com/gameframex/com.gameframex.unity.ui/commit/68b651af8aa551089f2fbcaae3e0573000bf5f71))
* **UI:** 为界面释放功能添加资源句柄参数 ([06dfb80](https://github.com/gameframex/com.gameframex.unity.ui/commit/06dfb80ec5d43639ad5bf4344d709e239c934623))
* **UI:** 为界面释放功能添加资源句柄参数 ([4d1446c](https://github.com/gameframex/com.gameframex.unity.ui/commit/4d1446c186c50b3ce1fe07c3180e014e55817f35))
* **UI:** 添加UIFormLoadingObject类用于管理界面加载 ([f751a76](https://github.com/gameframex/com.gameframex.unity.ui/commit/f751a76d1e95f2586e5d3f7060a31f24b7fa03e0))
* **UI:** 添加UI开启动画和关闭动画特性属性 ([7536e6a](https://github.com/gameframex/com.gameframex.unity.ui/commit/7536e6aefea371ce1a6f75fa92587d430b49e1ad))
* **UI:** 添加回收界面到实例池的方法 ([a53e0f0](https://github.com/gameframex/com.gameframex.unity.ui/commit/a53e0f097629c24a06c60b4198f84e3ac35b128e))
* **UI:** 添加界面回收功能 ([0144068](https://github.com/gameframex/com.gameframex.unity.ui/commit/014406837389e4c9d6580730e967ebd3443bf2fa))
* **UI:** 添加界面实例回收至对象池的配置选项 ([aed194a](https://github.com/gameframex/com.gameframex.unity.ui/commit/aed194a9017ee86e353007efe1501700384264cf))
* **UI:** 添加界面显示和隐藏动画支持 ([e3c83c0](https://github.com/gameframex/com.gameframex.unity.ui/commit/e3c83c0c217d207ddf035a3a0adb75129686f33c))
* **UI:** 添加界面显示和隐藏动画的开关功能 ([31d6bf9](https://github.com/gameframex/com.gameframex.unity.ui/commit/31d6bf926aacf9e94418ef8a4685d67cecf9719c))
* **UI:** 添加选项UI分组和配置特性 ([8655d72](https://github.com/gameframex/com.gameframex.unity.ui/commit/8655d72a0108e7dd85b7b3623cd9877ac5c14a18))
* **UI:** 添加释放所有已加载界面的功能 ([9aa4d9b](https://github.com/gameframex/com.gameframex.unity.ui/commit/9aa4d9b26fd991582453ac6b70f3013fdd367d20))
* **UI:** 添加释放所有已加载界面的功能 ([03643ab](https://github.com/gameframex/com.gameframex.unity.ui/commit/03643ab7737109a0a6718e2f75c31afec18ece0a))
* **UI组件:** 为关闭所有界面方法添加立即回收参数 ([a00ecc3](https://github.com/gameframex/com.gameframex.unity.ui/commit/a00ecc38b3b3efee6255feb0abe249695b44d69c))

# Changelog

## [2.1.1](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/2.1.1) (2025-10-27)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/2.1.0...2.1.1)

## [2.1.0](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/2.1.0) (2025-09-17)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/2.0.0...2.1.0)

## [2.0.0](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/2.0.0) (2025-06-12)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.2.7...2.0.0)

## [1.2.7](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.2.7) (2025-06-11)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.2.6...1.2.7)

## [1.2.6](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.2.6) (2025-06-11)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.2.5...1.2.6)

## [1.2.5](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.2.5) (2025-05-31)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.2.4...1.2.5)

## [1.2.4](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.2.4) (2025-05-30)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.2.3...1.2.4)

## [1.2.3](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.2.3) (2025-04-15)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.2.2...1.2.3)

## [1.2.2](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.2.2) (2025-04-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.2.1...1.2.2)

**Closed issues:**

- UIComponent.Open的OpenAsync能否开放pauseCoveredUIForm参数 [\#2](https://github.com/GameFrameX/com.gameframex.unity.ui/issues/2)
- UI回收后从对象池再次打开时 m\_SerialId等参数没有重置 [\#1](https://github.com/GameFrameX/com.gameframex.unity.ui/issues/1)

## [1.2.1](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.2.1) (2025-03-14)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.2.0...1.2.1)

## [1.2.0](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.2.0) (2025-03-14)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.1.0...1.2.0)

## [1.1.0](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.1.0) (2025-02-05)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.0.8...1.1.0)

## [1.0.8](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.0.8) (2025-01-14)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.0.7...1.0.8)

## [1.0.7](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.0.7) (2025-01-13)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.0.6...1.0.7)

## [1.0.6](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.0.6) (2024-12-16)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.0.5...1.0.6)

## [1.0.5](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.0.5) (2024-12-07)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.0.4...1.0.5)

## [1.0.4](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.0.4) (2024-11-08)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.0.3...1.0.4)

## [1.0.3](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.0.3) (2024-10-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.0.2...1.0.3)

## [1.0.2](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.0.2) (2024-10-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.0.1...1.0.2)

## [1.0.1](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.0.1) (2024-09-27)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/1.0.0...1.0.1)

## [1.0.0](https://github.com/GameFrameX/com.gameframex.unity.ui/tree/1.0.0) (2024-09-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.ui/compare/4903db5fbd4e15c81798c0a4bb9f84936d302860...1.0.0)



\* *This Changelog was automatically generated by [github_changelog_generator](https://github.com/github-changelog-generator/github-changelog-generator)*
