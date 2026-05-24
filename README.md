<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" height="160" />

# Game Frame X UI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/releases)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

[Documentation](https://gameframex.doc.alianblank.com) | [Quick Start](https://gameframex.doc.alianblank.com) | [QQ Group](https://qm.qq.com/q/urKenB9AU)

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

**Game Frame X UI** is the UI component of the GameFrameX framework, providing a complete UI management solution that supports both UGUI and FairyGUI UI systems.

## Features

- Unified UI management interface - supports UGUI and FairyGUI
- Layered UI system - predefined UI group hierarchy management
- Object pool management - automatic recycling and reuse of UI instances
- Event-driven - complete UI lifecycle events
- Asynchronous loading - supports async UI loading and dependency management
- Flexible configuration - configurable UI groups and helpers
- Editor support - complete Unity editor integration

## Installation

### Option 1: Package Manager (Recommended)

1. Open Unity Package Manager
2. Click the "+" button and select "Add package from git URL"
3. Enter the following URL:
   ```
   https://github.com/gameframex/com.gameframex.unity.ui.git
   ```

### Option 2: manifest.json

Add to your project's `Packages/manifest.json` file:

```json
{
  "dependencies": {
    "com.gameframex.unity.ui": "https://github.com/gameframex/com.gameframex.unity.ui.git"
  }
}
```

### Option 3: Local Installation

1. Download or clone this repository
2. Place the folder in your project's `Packages` directory
3. Unity will automatically recognize and load the package

## Quick Start

### 1. Add UI Component

Create a GameObject in the scene and add the `UIComponent` component:

```csharp
// Get the UI component
var uiComponent = GameEntry.GetComponent<UIComponent>();
```

### 2. Create a UI Form

```csharp
// Inherit from UIForm to create a custom UI
public class MainMenuUI : UIForm
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        // Initialize UI logic
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        // Logic when UI opens
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        // Logic when UI closes
    }
}
```

### 3. Open and Close UI

```csharp
// Open UI
uiComponent.OpenUIForm("MainMenuUI", "UI/MainMenu");

// Open UI asynchronously
await uiComponent.OpenUIFormAsync("MainMenuUI", "UI/MainMenu");

// Close UI
uiComponent.CloseUIForm("MainMenuUI");

// Close all UI
uiComponent.CloseAllLoadedUIForms();
```

## Core Concepts

### UI Manager (IUIManager)

The UI Manager is the core of the entire UI system, responsible for:
- UI form lifecycle management
- UI group management and hierarchy control
- Object pool management and recycling
- Event dispatching and handling

### UI Form (UIForm)

The UI Form is the base class for all UI interfaces, providing:
- Standard lifecycle methods
- Visibility control
- Pause and resume functionality
- User data passing

### UI Group (UIGroup)

UI Groups manage the hierarchy of UIs, each with a different depth value:
- Lower depth values mean higher display priority
- Supports sorting and management of UIs within groups
- Configurable group helpers

## API Documentation

### UIComponent Main Methods

#### Open UI Form

```csharp
public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority = 0, bool pauseCoveredUIForm = true, object userData = null)

public Task<IUIForm> OpenUIFormAsync(string uiFormAssetName, string uiGroupName, int priority = 0, bool pauseCoveredUIForm = true, object userData = null)
```

#### Close UI Form

```csharp
public void CloseUIForm(int serialId, object userData = null)

public void CloseUIForm(IUIForm uiForm, object userData = null)
```

#### Get UI Form

```csharp
public IUIForm GetUIForm(int serialId)

public IUIForm GetUIForm(string uiFormAssetName)
```

### UIForm Lifecycle Methods

```csharp
protected virtual void OnInit(object userData) { }
protected virtual void OnRecycle() { }
protected virtual void OnOpen(object userData) { }
protected virtual void OnClose(bool isShutdown, object userData) { }
protected virtual void OnPause() { }
protected virtual void OnResume() { }
```

## UI Group Hierarchy

The framework predefines the following UI group hierarchy (sorted by depth value):

| Group Name | Depth | Description |
|------------|-------|-------------|
| System | -35 | System top-level interface |
| Notify | -30 | Notification interface |
| Loading | -25 | Loading interface |
| Dialogue | -23 | Dialogue interface |
| BlackBoard | -22 | Blackboard interface |
| Guide | -20 | Guide interface |
| Tip | -15 | Tip interface |
| Window | -10 | Window interface |
| Fixed | 0 | Fixed interface |
| Normal | 10 | Normal interface |
| Floor | 15 | Floor interface |
| Map | 20 | Map interface |
| Hud | 22 | Head-up display interface |
| Battle | 25 | Battle interface |
| World | 27 | World interface |
| Scene | 30 | Scene interface |
| Background | 35 | Background interface |
| Hidden | 40 | Hidden interface |

## Event System

The framework provides a complete UI event system:

### Event Types

- `OpenUIFormSuccessEventArgs` - UI open success event
- `OpenUIFormFailureEventArgs` - UI open failure event
- `CloseUIFormCompleteEventArgs` - UI close complete event
- `UIFormVisibleChangedEventArgs` - UI visibility changed event

### Event Subscription Example

```csharp
// Subscribe to UI open success event
GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
{
    var args = (OpenUIFormSuccessEventArgs)e;
    Debug.Log($"UI {args.UIForm.UIFormAssetName} opened successfully");
}
```

## Best Practices

### 1. UI Resource Naming Convention

```csharp
// Recommended naming
"UI/MainMenu"      // Main menu
"UI/Battle/HUD"    // Battle HUD
"UI/Shop/ItemList" // Shop item list
```

### 2. UI Data Passing

```csharp
// Use strongly typed data class
public class ShopUIData
{
    public int PlayerId { get; set; }
    public List<Item> Items { get; set; }
}

// Pass data when opening UI
var shopData = new ShopUIData { PlayerId = 123, Items = itemList };
uiComponent.OpenUIForm("ShopUI", "Normal", userData: shopData);

// Receive data in UI
protected override void OnOpen(object userData)
{
    var shopData = userData as ShopUIData;
    if (shopData != null)
    {
        // Initialize UI with data
    }
}
```

### 3. UI Performance Optimization

```csharp
// Configure object pool parameters
uiComponent.InstanceCapacity = 16;        // Object pool capacity
uiComponent.InstanceExpireTime = 60f;     // Object expiration time
uiComponent.RecycleInterval = 60;         // Recycle interval
```

## Usage Examples

### Complete UI Form Example

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

        // Bind button events
        startButton.onClick.AddListener(OnStartButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);

        // Play open animation
        PlayOpenAnimation();
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);

        // Cleanup resources
        CleanupResources();
    }

    private void OnStartButtonClick()
    {
        // Close current UI and open game UI
        UIComponent.CloseUIForm(this);
        UIComponent.OpenUIForm("GameUI", "Normal");
    }

    private void OnSettingsButtonClick()
    {
        // Open settings UI
        UIComponent.OpenUIForm("SettingsUI", "Window");
    }

    private void OnExitButtonClick()
    {
        // Exit game
        Application.Quit();
    }

    private void PlayOpenAnimation()
    {
        // Implement open animation
    }

    private void CleanupResources()
    {
        // Cleanup resources
    }
}
```

### UI Manager Usage Example

```csharp
public class UIManager : MonoBehaviour
{
    private UIComponent uiComponent;

    private void Start()
    {
        uiComponent = GameEntry.GetComponent<UIComponent>();

        // Configure UI component
        ConfigureUIComponent();

        // Open main menu
        OpenMainMenu();
    }

    private void ConfigureUIComponent()
    {
        // Set object pool parameters
        uiComponent.InstanceCapacity = 20;
        uiComponent.InstanceExpireTime = 120f;
        uiComponent.RecycleInterval = 60;

        // Subscribe to events
        GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnUIFormOpenSuccess);
        GameEntry.Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnUIFormOpenFailure);
    }

    private async void OpenMainMenu()
    {
        try
        {
            var mainMenuUI = await uiComponent.OpenUIFormAsync("MainMenuUI", "Normal");
            Debug.Log("Main menu opened successfully");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to open main menu: {ex.Message}");
        }
    }

    private void OnUIFormOpenSuccess(object sender, GameEventArgs e)
    {
        var args = (OpenUIFormSuccessEventArgs)e;
        Debug.Log($"UI opened successfully: {args.UIForm.UIFormAssetName}");
    }

    private void OnUIFormOpenFailure(object sender, GameEventArgs e)
    {
        var args = (OpenUIFormFailureEventArgs)e;
        Debug.LogError($"Failed to open UI: {args.UIForm.UIFormAssetName}, Error: {args.ErrorMessage}");
    }
}
```

## Dependencies

This package depends on the following GameFrameX components:

- `com.gameframex.unity` (>= 1.1.1) - Core framework
- `com.gameframex.unity.asset` (>= 1.0.6) - Asset management
- `com.gameframex.unity.event` (>= 1.0.0) - Event system
- `com.gameframex.unity.localization` (>= 1.0.0) - Localization support

## Changelog

### 2.0.0 (2025-06-12)
- Major version update
- Optimized UI manager architecture
- Improved object pool performance

### 1.2.7 (2025-06-11)
- Fixed UI parameter reset after recycling
- Optimized async loading performance

See the full [CHANGELOG.md](CHANGELOG.md) for detailed version history.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
