<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X UI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#quick-start) · [QQグループ](https://qm.qq.com/q/urKenB9AU)

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>
## 機能特性

- 統一された UI 管理インターフェース - UGUI と FairyGUI をサポート
- レイヤー化された UI システム - 定義済みの UI グループ階層管理
- オブジェクトプール管理 - UI インスタンスの自動回収と再利用
- イベント駆動 - 完全な UI ライフサイクルイベント
- 非同期読み込み - 非同期 UI 読み込みと依存関係管理をサポート
- 柔軟な設定 - 設定可能な UI グループとヘルパー
- エディタサポート - 完全な Unity エディタ統合

## インストール

### 方法 1: Package Manager (推奨)

1. Unity Package Manager を開く
2. "+" ボタンをクリックし、「Add package from git URL」を選択
3. 以下の URL を入力：
   ```
   https://github.com/gameframex/com.gameframex.unity.ui.git
   ```

### 方法 2: manifest.json

プロジェクトの `Packages/manifest.json` ファイルに追加：

```json
{
  "dependencies": {
    "com.gameframex.unity.ui": "https://github.com/gameframex/com.gameframex.unity.ui.git"
  }
}
```

### 方法 3: ローカルインストール

1. このリポジトリをダウンロードまたはクローン
2. フォルダをプロジェクトの `Packages` ディレクトリに配置
3. Unity が自動的にパッケージを認識して読み込みます

## クイックスタート

### 1. UI コンポーネントの追加

シーンに GameObject を作成し、`UIComponent` コンポーネントを追加します：

```csharp
// UI コンポーネントを取得
var uiComponent = GameEntry.GetComponent<UIComponent>();
```

### 2. UI フォームの作成

```csharp
// UIForm を継承してカスタム UI を作成
public class MainMenuUI : UIForm
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        // UI ロジックの初期化
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        // UI オープン時のロジック
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        // UI クローズ時のロジック
    }
}
```

### 3. UI のオープンとクローズ

```csharp
// UI を開く
uiComponent.OpenUIForm("MainMenuUI", "UI/MainMenu");

// 非同期で UI を開く
await uiComponent.OpenUIFormAsync("MainMenuUI", "UI/MainMenu");

// UI を閉じる
uiComponent.CloseUIForm("MainMenuUI");

// すべての UI を閉じる
uiComponent.CloseAllLoadedUIForms();
```

## コアコンセプト

### UI マネージャー (IUIManager)

UI マネージャーは UI システム全体の中核で、以下を担当します：
- UI フォームのライフサイクル管理
- UI グループの管理と階層制御
- オブジェクトプールの管理と回収
- イベントのディスパッチと処理

### UI フォーム (UIForm)

UI フォームはすべての UI インターフェースの基底クラスで、以下を提供します：
- 標準的なライフサイクルメソッド
- 可視性制御
- 一時停止と再開機能
- ユーザーデータの受け渡し

### UI グループ (UIGroup)

UI グループは UI の階層関係を管理し、各グループには異なる深度値があります：
- 深度値が小さいほど表示優先度が高い
- グループ内の UI の並べ替えと管理をサポート
- グループヘルパーの設定が可能

## 依存関係

このパッケージは以下の GameFrameX コンポーネントに依存しています：

- `com.gameframex.unity` (>= 1.1.1) - コアフレームワーク
- `com.gameframex.unity.asset` (>= 1.0.6) - アセット管理
- `com.gameframex.unity.event` (>= 1.0.0) - イベントシステム
- `com.gameframex.unity.localization` (>= 1.0.0) - ローカライゼーションサポート

## 変更履歴

### 2.0.0 (2025-06-12)
- メジャーバージョンアップデート
- UI マネージャーアーキテクチャの最適化
- オブジェクトプールパフォーマンスの改善

### 1.2.7 (2025-06-11)
- UI リサイクル後のパラメータリセット問題を修正
- 非同期読み込みパフォーマンスの最適化

詳細なバージョン履歴は [CHANGELOG.md](CHANGELOG.md) を参照してください。

## ライセンス

このプロジェクトは MIT ライセンスの下で公開されています - 詳細は [LICENSE.md](LICENSE.md) ファイルを参照してください。
