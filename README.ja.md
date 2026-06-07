<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X UI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#quick-start) · QQグループ: 467608841 / 233840761

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

### インストール

Unity プロジェクトの `Packages/manifest.json` を編集し、`scopedRegistries` セクションを追加してください：

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

`scopes` は、どのパッケージをこのレジストリから解決するかを制御します。`com.gameframex` で始まるパッケージのみがこのレジストリから取得されます。

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.ui": "2.10.3"
  }
}
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
