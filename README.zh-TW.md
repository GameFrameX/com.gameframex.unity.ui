<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" height="160" />

# Game Frame X UI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/releases)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

[文檔](https://gameframex.doc.alianblank.com) | [快速開始](https://gameframex.doc.alianblank.com) | [QQ群](https://qm.qq.com/q/urKenB9AU)

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

**Game Frame X UI** 是 GameFrameX 框架的 UI 組件，提供了完整的 UI 管理解決方案，支持 UGUI 和 FairyGUI 兩種 UI 系統。

## 功能特性

- 統一的 UI 管理接口 - 支持 UGUI 和 FairyGUI
- 分層 UI 系統 - 預定義的 UI 組層級管理
- 對象池管理 - 自動回收和復用 UI 實例
- 事件驅動 - 完整的 UI 生命週期事件
- 異步加載 - 支持異步 UI 加載和依賴管理
- 靈活配置 - 可配置的 UI 組和輔助器
- 編輯器支持 - 完整的 Unity 編輯器集成

## 安裝

### 方式一：Package Manager (推薦)

1. 打開 Unity Package Manager
2. 點擊 "+" 按鈕，選擇 "Add package from git URL"
3. 輸入以下 URL：
   ```
   https://github.com/gameframex/com.gameframex.unity.ui.git
   ```

### 方式二：manifest.json

在項目的 `Packages/manifest.json` 文件中添加：

```json
{
  "dependencies": {
    "com.gameframex.unity.ui": "https://github.com/gameframex/com.gameframex.unity.ui.git"
  }
}
```

### 方式三：本地安裝

1. 下載或克隆此倉庫
2. 將文件夾放置到項目的 `Packages` 目錄下
3. Unity 會自動識別並加載包

## 快速開始

### 1. 添加 UI 組件

在場景中創建一個 GameObject 並添加 `UIComponent` 組件：

```csharp
// 獲取 UI 組件
var uiComponent = GameEntry.GetComponent<UIComponent>();
```

### 2. 創建 UI 窗體

```csharp
// 繼承 UIForm 創建自定義 UI
public class MainMenuUI : UIForm
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        // 初始化 UI 邏輯
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        // UI 打開時的邏輯
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        // UI 關閉時的邏輯
    }
}
```

### 3. 打開和關閉 UI

```csharp
// 打開 UI
uiComponent.OpenUIForm("MainMenuUI", "UI/MainMenu");

// 異步打開 UI
await uiComponent.OpenUIFormAsync("MainMenuUI", "UI/MainMenu");

// 關閉 UI
uiComponent.CloseUIForm("MainMenuUI");

// 關閉所有 UI
uiComponent.CloseAllLoadedUIForms();
```

## 核心概念

### UI 管理器 (IUIManager)

UI 管理器是整個 UI 系統的核心，負責：
- UI 窗體的生命週期管理
- UI 組的管理和層級控制
- 對象池的管理和回收
- 事件的分發和處理

### UI 窗體 (UIForm)

UI 窗體是所有 UI 界面的基類，提供：
- 標準的生命週期方法
- 可見性控制
- 暫停和恢復功能
- 用戶數據傳遞

### UI 組 (UIGroup)

UI 組用於管理 UI 的層級關係，每個組有不同的深度值：
- 深度值越小，顯示層級越高
- 支持組內 UI 的排序和管理
- 可配置組的輔助器

## API 文檔

### UIComponent 主要方法

#### 打開 UI 窗體

```csharp
public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority = 0, bool pauseCoveredUIForm = true, object userData = null)

public Task<IUIForm> OpenUIFormAsync(string uiFormAssetName, string uiGroupName, int priority = 0, bool pauseCoveredUIForm = true, object userData = null)
```

#### 關閉 UI 窗體

```csharp
public void CloseUIForm(int serialId, object userData = null)

public void CloseUIForm(IUIForm uiForm, object userData = null)
```

#### 獲取 UI 窗體

```csharp
public IUIForm GetUIForm(int serialId)

public IUIForm GetUIForm(string uiFormAssetName)
```

### UIForm 生命週期方法

```csharp
protected virtual void OnInit(object userData) { }
protected virtual void OnRecycle() { }
protected virtual void OnOpen(object userData) { }
protected virtual void OnClose(bool isShutdown, object userData) { }
protected virtual void OnPause() { }
protected virtual void OnResume() { }
```

## UI 組層級

框架預定義了以下 UI 組層級（按深度值排序）：

| 組名 | 深度值 | 描述 |
|------|--------|------|
| System | -35 | 系統頂級界面 |
| Notify | -30 | 通知界面 |
| Loading | -25 | 加載界面 |
| Dialogue | -23 | 對話界面 |
| BlackBoard | -22 | 黑板界面 |
| Guide | -20 | 引導界面 |
| Tip | -15 | 提示界面 |
| Window | -10 | 窗口界面 |
| Fixed | 0 | 固定界面 |
| Normal | 10 | 普通界面 |
| Floor | 15 | 底板界面 |
| Map | 20 | 地圖界面 |
| Hud | 22 | 頭頂界面 |
| Battle | 25 | 戰鬥界面 |
| World | 27 | 世界界面 |
| Scene | 30 | 場景界面 |
| Background | 35 | 背景界面 |
| Hidden | 40 | 隱藏界面 |

## 事件系統

框架提供了完整的 UI 事件系統：

### 事件類型

- `OpenUIFormSuccessEventArgs` - UI 打開成功事件
- `OpenUIFormFailureEventArgs` - UI 打開失敗事件
- `CloseUIFormCompleteEventArgs` - UI 關閉完成事件
- `UIFormVisibleChangedEventArgs` - UI 可見性改變事件

### 事件訂閱示例

```csharp
// 訂閱 UI 打開成功事件
GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
{
    var args = (OpenUIFormSuccessEventArgs)e;
    Debug.Log($"UI {args.UIForm.UIFormAssetName} 打開成功");
}
```

## 最佳實踐

### 1. UI 資源命名規範

```csharp
// 推薦的命名方式
"UI/MainMenu"      // 主選單
"UI/Battle/HUD"    // 戰鬥 HUD
"UI/Shop/ItemList" // 商店物品列表
```

### 2. UI 數據傳遞

```csharp
public class ShopUIData
{
    public int PlayerId { get; set; }
    public List<Item> Items { get; set; }
}

var shopData = new ShopUIData { PlayerId = 123, Items = itemList };
uiComponent.OpenUIForm("ShopUI", "Normal", userData: shopData);

protected override void OnOpen(object userData)
{
    var shopData = userData as ShopUIData;
    if (shopData != null)
    {
        // 使用數據初始化 UI
    }
}
```

### 3. UI 性能優化

```csharp
uiComponent.InstanceCapacity = 16;        // 對象池容量
uiComponent.InstanceExpireTime = 60f;     // 對象過期時間
uiComponent.RecycleInterval = 60;         // 回收間隔
```

## 依賴項

此包依賴以下 GameFrameX 組件：

- `com.gameframex.unity` (>= 1.1.1) - 核心框架
- `com.gameframex.unity.asset` (>= 1.0.6) - 資源管理
- `com.gameframex.unity.event` (>= 1.0.0) - 事件系統
- `com.gameframex.unity.localization` (>= 1.0.0) - 本地化支持

## 更新日誌

### 2.0.0 (2025-06-12)
- 重大版本更新
- 優化 UI 管理器架構
- 改進對象池性能

### 1.2.7 (2025-06-11)
- 修復 UI 回收後參數重置問題
- 優化異步加載性能

查看完整的 [CHANGELOG.md](CHANGELOG.md) 了解詳細的版本歷史。

## 開源協議

本項目採用 MIT 許可證 - 查看 [LICENSE.md](LICENSE.md) 文件了解詳情。
