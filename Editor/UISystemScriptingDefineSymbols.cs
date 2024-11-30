﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFrameX.Editor;
using UnityEditor;

namespace GameFrameX.UI.Editor
{
    /// <summary>
    /// UI系统脚本宏定义。
    /// </summary>
    public static class UISystemScriptingDefineSymbols
    {
        private const string UGUIScriptingDefineSymbol = "ENABLE_UI_UGUI";
        private const string FairyGUIScriptingDefineSymbol = "ENABLE_UI_FAIRYGUI";

        /// <summary>
        /// 开启UGUI UI脚本宏定义。
        /// </summary>
        [MenuItem("GameFrameX/UI Scripting Define Symbols/Enable UGUI", false, 10)]
        public static void DisableForceWebSocketNetwork()
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(FairyGUIScriptingDefineSymbol);
            ScriptingDefineSymbols.AddScriptingDefineSymbol(UGUIScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启FairyGUI UI脚本宏定义。
        /// </summary>
        [MenuItem("GameFrameX/UI Scripting Define Symbols/Enable FairyGUI", false, 11)]
        public static void EnableForceWebSocketNetwork()
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(UGUIScriptingDefineSymbol);
            ScriptingDefineSymbols.AddScriptingDefineSymbol(FairyGUIScriptingDefineSymbol);
        }
    }
}