<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X UI

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.ui)](https://github.com/GameFrameX/com.gameframex.unity.ui/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#quick-start) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>

## 기능 특징

- 통합 UI 관리 인터페이스 - UGUI 및 FairyGUI 지원
- 계층형 UI 시스템 - 사전 정의된 UI 그룹 계층 관리
- 오브젝트 풀 관리 - UI 인스턴스의 자동 회수 및 재사용
- 이벤트 기반 - 완전한 UI 수명 주기 이벤트
- 비동기 로딩 - 비동기 UI 로딩 및 종속성 관리 지원
- 유연한 구성 - 구성 가능한 UI 그룹 및 헬퍼
- 에디터 지원 - 완전한 Unity 에디터 통합

## 설치

### 방법 1: Package Manager (권장)

1. Unity Package Manager 열기
2. "+" 버튼을 클릭하고 "Add package from git URL" 선택
3. 다음 URL 입력:
   ```
   https://github.com/gameframex/com.gameframex.unity.ui.git
   ```

### 방법 2: manifest.json

프로젝트의 `Packages/manifest.json` 파일에 추가:

```json
{
  "dependencies": {
    "com.gameframex.unity.ui": "https://github.com/gameframex/com.gameframex.unity.ui.git"
  }
}
```

### 방법 3: 로컬 설치

1. 이 저장소를 다운로드하거나 클론
2. 폴더를 프로젝트의 `Packages` 디렉토리에 배치
3. Unity가 자동으로 패키지를 인식하고 로드합니다

## 빠른 시작

### 1. UI 컴포넌트 추가

장면에 GameObject를 만들고 `UIComponent` 컴포넌트를 추가합니다:

```csharp
// UI 컴포넌트 가져오기
var uiComponent = GameEntry.GetComponent<UIComponent>();
```

### 2. UI 폼 만들기

```csharp
// UIForm을 상속하여 커스텀 UI 만들기
public class MainMenuUI : UIForm
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        // UI 로직 초기화
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        // UI 열기 시 로직
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        // UI 닫기 시 로직
    }
}
```

### 3. UI 열기 및 닫기

```csharp
// UI 열기
uiComponent.OpenUIForm("MainMenuUI", "UI/MainMenu");

// 비동기로 UI 열기
await uiComponent.OpenUIFormAsync("MainMenuUI", "UI/MainMenu");

// UI 닫기
uiComponent.CloseUIForm("MainMenuUI");

// 모든 UI 닫기
uiComponent.CloseAllLoadedUIForms();
```

## 핵심 개념

### UI 매니저 (IUIManager)

UI 매니저는 전체 UI 시스템의 핵심으로, 다음을 담당합니다:
- UI 폼 수명 주기 관리
- UI 그룹 관리 및 계층 제어
- 오브젝트 풀 관리 및 회수
- 이벤트 디스패치 및 처리

### UI 폼 (UIForm)

UI 폼은 모든 UI 인터페이스의 기본 클래스로, 다음을 제공합니다:
- 표준 수명 주기 메서드
- 가시성 제어
- 일시 정지 및 재개 기능
- 사용자 데이터 전달

### UI 그룹 (UIGroup)

UI 그룹은 UI의 계층 관계를 관리하며, 각 그룹은 서로 다른 깊이 값을 가집니다:
- 깊이 값이 작을수록 표시 우선순위가 높음
- 그룹 내 UI 정렬 및 관리 지원
- 그룹 헬퍼 구성 가능

## 종속성

이 패키지는 다음 GameFrameX 컴포넌트에 종속됩니다:

- `com.gameframex.unity` (>= 1.1.1) - 코어 프레임워크
- `com.gameframex.unity.asset` (>= 1.0.6) - 에셋 관리
- `com.gameframex.unity.event` (>= 1.0.0) - 이벤트 시스템
- `com.gameframex.unity.localization` (>= 1.0.0) - 로컬라이제이션 지원

## 변경 로그

### 2.0.0 (2025-06-12)
- 주요 버전 업데이트
- UI 매니저 아키텍처 최적화
- 오브젝트 풀 성능 개선

### 1.2.7 (2025-06-11)
- UI 회수 후 매개변수 재설정 문제 수정
- 비동기 로딩 성능 최적화

자세한 버전 기록은 [CHANGELOG.md](CHANGELOG.md)를 참조하세요.

## 라이선스

이 프로젝트는 MIT 라이선스에 따라 배포됩니다 - 자세한 내용은 [LICENSE.md](LICENSE.md) 파일을 참조하세요.
