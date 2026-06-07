<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X UI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#quick-start) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 特性

- 统一的 UI 管理接口 - 支持 UGUI 和 FairyGUI
- 分层 UI 系统 - 预定义的 UI 组层级管理
- 对象池管理 - 自动回收和复用 UI 实例
- 事件驱动 - 完整的 UI 生命周期事件
- 异步加载 - 支持异步 UI 加载和依赖管理
- 灵活配置 - 可配置的 UI 组和辅助器
- 编辑器支持 - 完整的 Unity 编辑器集成

## 安装

### 方式一：Package Manager (推荐)

1. 打开 Unity Package Manager
2. 点击 "+" 按钮，选择 "Add package from git URL"
3. 输入以下 URL：
   ```
   https://github.com/gameframex/com.gameframex.unity.ui.git
   ```

### 方式二：manifest.json

在项目的 `Packages/manifest.json` 文件中添加：

```json
{
  "dependencies": {
    "com.gameframex.unity.ui": "https://github.com/gameframex/com.gameframex.unity.ui.git"
  }
}
```

### 方式三：本地安装

1. 下载或克隆此仓库
2. 将文件夹放置到项目的 `Packages` 目录下
3. Unity 会自动识别并加载包

## 快速开始

### 安装

编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

```json
{
  "scopedRegistries": [
    {
      "name": "GameFrameX",
      "url": "https://gameframex.upm.alianblank.uk",
      "scopes": [
        "com.gameframex"
      ]
    }
  ]
}
```

`scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.ui": "2.10.3"
  }
}
```

## 核心概念

### UI 管理器 (IUIManager)

UI 管理器是整个 UI 系统的核心，负责：
- UI 窗体的生命周期管理
- UI 组的管理和层级控制
- 对象池的管理和回收
- 事件的分发和处理

### UI 窗体 (UIForm)

UI 窗体是所有 UI 界面的基类，提供：
- 标准的生命周期方法
- 可见性控制
- 暂停和恢复功能
- 用户数据传递

### UI 组 (UIGroup)

UI 组用于管理 UI 的层级关系，每个组有不同的深度值：
- 深度值越小，显示层级越高
- 支持组内 UI 的排序和管理
- 可配置组的辅助器

## API 文档

### UIComponent 主要方法

#### 打开 UI 窗体

```csharp
/// <summary>
/// 打开界面。
/// </summary>
/// <param name="uiFormAssetName">界面资源名称。</param>
/// <param name="uiGroupName">界面组名称。</param>
/// <param name="priority">加载界面资源的优先级。</param>
/// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
/// <param name="userData">用户自定义数据。</param>
/// <returns>界面的序列编号。</returns>
public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority = 0, bool pauseCoveredUIForm = true, object userData = null)

/// <summary>
/// 异步打开界面。
/// </summary>
/// <param name="uiFormAssetName">界面资源名称。</param>
/// <param name="uiGroupName">界面组名称。</param>
/// <param name="priority">加载界面资源的优先级。</param>
/// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
/// <param name="userData">用户自定义数据。</param>
/// <returns>界面实例。</returns>
public Task<IUIForm> OpenUIFormAsync(string uiFormAssetName, string uiGroupName, int priority = 0, bool pauseCoveredUIForm = true, object userData = null)
```

#### 关闭 UI 窗体

```csharp
/// <summary>
/// 关闭界面。
/// </summary>
/// <param name="serialId">要关闭界面的序列编号。</param>
/// <param name="userData">用户自定义数据。</param>
public void CloseUIForm(int serialId, object userData = null)

/// <summary>
/// 关闭界面。
/// </summary>
/// <param name="uiForm">要关闭的界面。</param>
/// <param name="userData">用户自定义数据。</param>
public void CloseUIForm(IUIForm uiForm, object userData = null)
```

#### 获取 UI 窗体

```csharp
/// <summary>
/// 获取界面。
/// </summary>
/// <param name="serialId">要获取界面的序列编号。</param>
/// <returns>要获取的界面。</returns>
public IUIForm GetUIForm(int serialId)

/// <summary>
/// 获取界面。
/// </summary>
/// <param name="uiFormAssetName">要获取界面的资源名称。</param>
/// <returns>要获取的界面。</returns>
public IUIForm GetUIForm(string uiFormAssetName)
```

### UIForm 生命周期方法

```csharp
protected virtual void OnInit(object userData) { }
protected virtual void OnRecycle() { }
protected virtual void OnOpen(object userData) { }
protected virtual void OnClose(bool isShutdown, object userData) { }
protected virtual void OnPause() { }
protected virtual void OnResume() { }
```

## UI 组层级

框架预定义了以下 UI 组层级（按深度值排序）：

| 组名 | 深度值 | 描述 |
|------|--------|------|
| System | -35 | 系统顶级界面 |
| Notify | -30 | 通知界面 |
| Loading | -25 | 加载界面 |
| Dialogue | -23 | 对话界面 |
| BlackBoard | -22 | 黑板界面 |
| Guide | -20 | 引导界面 |
| Tip | -15 | 提示界面 |
| Window | -10 | 窗口界面 |
| Fixed | 0 | 固定界面 |
| Normal | 10 | 普通界面 |
| Floor | 15 | 底板界面 |
| Map | 20 | 地图界面 |
| Hud | 22 | 头顶界面 |
| Battle | 25 | 战斗界面 |
| World | 27 | 世界界面 |
| Scene | 30 | 场景界面 |
| Background | 35 | 背景界面 |
| Hidden | 40 | 隐藏界面 |

## 事件系统

框架提供了完整的 UI 事件系统：

### 事件类型

- `OpenUIFormSuccessEventArgs` - UI 打开成功事件
- `OpenUIFormFailureEventArgs` - UI 打开失败事件
- `CloseUIFormCompleteEventArgs` - UI 关闭完成事件
- `UIFormVisibleChangedEventArgs` - UI 可见性改变事件

### 事件订阅示例

```csharp
// 订阅 UI 打开成功事件
GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
{
    var args = (OpenUIFormSuccessEventArgs)e;
    Debug.Log($"UI {args.UIForm.UIFormAssetName} 打开成功");
}
```

## 最佳实践

### 1. UI 资源命名规范

```csharp
// 推荐的命名方式
"UI/MainMenu"      // 主菜单
"UI/Battle/HUD"    // 战斗 HUD
"UI/Shop/ItemList" // 商店物品列表
```

### 2. UI 数据传递

```csharp
// 使用强类型数据类
public class ShopUIData
{
    public int PlayerId { get; set; }
    public List<Item> Items { get; set; }
}

// 打开 UI 时传递数据
var shopData = new ShopUIData { PlayerId = 123, Items = itemList };
uiComponent.OpenUIForm("ShopUI", "Normal", userData: shopData);

// 在 UI 中接收数据
protected override void OnOpen(object userData)
{
    var shopData = userData as ShopUIData;
    if (shopData != null)
    {
        // 使用数据初始化 UI
    }
}
```

### 3. UI 性能优化

```csharp
// 配置对象池参数
uiComponent.InstanceCapacity = 16;        // 对象池容量
uiComponent.InstanceExpireTime = 60f;     // 对象过期时间
uiComponent.RecycleInterval = 60;         // 回收间隔
```

## 示例代码

### 完整的 UI 窗体示例

```csharp
using GameFrameX.UI.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIForm
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        // 绑定按钮事件
        startButton.onClick.AddListener(OnStartButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);

        // 播放打开动画
        PlayOpenAnimation();
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);

        // 清理资源
        CleanupResources();
    }

    private void OnStartButtonClick()
    {
        // 关闭当前 UI 并打开游戏 UI
        UIComponent.CloseUIForm(this);
        UIComponent.OpenUIForm("GameUI", "Normal");
    }

    private void OnSettingsButtonClick()
    {
        // 打开设置 UI
        UIComponent.OpenUIForm("SettingsUI", "Window");
    }

    private void OnExitButtonClick()
    {
        // 退出游戏
        Application.Quit();
    }

    private void PlayOpenAnimation()
    {
        // 实现打开动画
    }

    private void CleanupResources()
    {
        // 清理资源
    }
}
```

### UI 管理器使用示例

```csharp
public class UIManager : MonoBehaviour
{
    private UIComponent uiComponent;

    private void Start()
    {
        uiComponent = GameEntry.GetComponent<UIComponent>();

        // 配置 UI 组件
        ConfigureUIComponent();

        // 打开主菜单
        OpenMainMenu();
    }

    private void ConfigureUIComponent()
    {
        // 设置对象池参数
        uiComponent.InstanceCapacity = 20;
        uiComponent.InstanceExpireTime = 120f;
        uiComponent.RecycleInterval = 60;

        // 订阅事件
        GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnUIFormOpenSuccess);
        GameEntry.Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnUIFormOpenFailure);
    }

    private async void OpenMainMenu()
    {
        try
        {
            var mainMenuUI = await uiComponent.OpenUIFormAsync("MainMenuUI", "Normal");
            Debug.Log("主菜单打开成功");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"主菜单打开失败: {ex.Message}");
        }
    }

    private void OnUIFormOpenSuccess(object sender, GameEventArgs e)
    {
        var args = (OpenUIFormSuccessEventArgs)e;
        Debug.Log($"UI 打开成功: {args.UIForm.UIFormAssetName}");
    }

    private void OnUIFormOpenFailure(object sender, GameEventArgs e)
    {
        var args = (OpenUIFormFailureEventArgs)e;
        Debug.LogError($"UI 打开失败: {args.UIForm.UIFormAssetName}, 错误: {args.ErrorMessage}");
    }
}
```

## 依赖项

此包依赖以下 GameFrameX 组件：

- `com.gameframex.unity` (>= 1.1.1) - 核心框架
- `com.gameframex.unity.asset` (>= 1.0.6) - 资源管理
- `com.gameframex.unity.event` (>= 1.0.0) - 事件系统
- `com.gameframex.unity.localization` (>= 1.0.0) - 本地化支持

## 更新日志

### 2.0.0 (2025-06-12)
- 重大版本更新
- 优化 UI 管理器架构
- 改进对象池性能

### 1.2.7 (2025-06-11)
- 修复 UI 回收后参数重置问题
- 优化异步加载性能

查看完整的 [CHANGELOG.md](CHANGELOG.md) 了解详细的版本历史。

## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
