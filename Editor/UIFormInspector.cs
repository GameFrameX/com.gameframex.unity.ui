// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using GameFrameX.Editor;
using GameFrameX.UI.Runtime;
using UnityEditor;

namespace GameFrameX.UI.Editor
{
    /// <summary>
    /// UI 表单检查器。
    /// </summary>
    [CustomEditor(typeof(UIForm), true)]
    internal sealed class UIFormInspector : GameFrameworkInspector
    {
        private SerializedProperty m_Available = null;
        private SerializedProperty m_Visible = null;
        private SerializedProperty m_IsInit = null;
        private SerializedProperty m_IsDisableRecycling = null;
        private SerializedProperty m_IsDisableClosing = null;
        private SerializedProperty m_SerialId = null;
        private SerializedProperty m_OriginalLayer = null;
        private SerializedProperty m_UIFormAssetName = null;
        private SerializedProperty m_AssetPath = null;
        private SerializedProperty m_DepthInUIGroup = null;
        private SerializedProperty m_PauseCoveredUIForm = null;
        private SerializedProperty m_FullName = null;
        private SerializedProperty m_EnableShowAnimation = null;
        private SerializedProperty m_ShowAnimationName = null;
        private SerializedProperty m_EnableHideAnimation = null;
        private SerializedProperty m_HideAnimationName = null;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                EditorGUILayout.PropertyField(m_FullName);
                EditorGUILayout.PropertyField(m_SerialId);
                EditorGUILayout.PropertyField(m_IsInit);
                EditorGUILayout.PropertyField(m_IsDisableRecycling);
                EditorGUILayout.PropertyField(m_IsDisableClosing);
                EditorGUILayout.PropertyField(m_OriginalLayer);
                EditorGUILayout.PropertyField(m_UIFormAssetName);
                EditorGUILayout.PropertyField(m_Available);
                EditorGUILayout.PropertyField(m_Visible);
                EditorGUILayout.PropertyField(m_AssetPath);
                EditorGUILayout.PropertyField(m_DepthInUIGroup);
                EditorGUILayout.PropertyField(m_PauseCoveredUIForm);
                EditorGUILayout.PropertyField(m_EnableShowAnimation);
                EditorGUILayout.PropertyField(m_ShowAnimationName);
                EditorGUILayout.PropertyField(m_EnableHideAnimation);
                EditorGUILayout.PropertyField(m_HideAnimationName);
            }
            EditorGUI.EndDisabledGroup();

            Repaint();
        }


        private void OnEnable()
        {
            m_Available = serializedObject.FindProperty("m_Available");
            m_Visible = serializedObject.FindProperty("m_Visible");
            m_IsInit = serializedObject.FindProperty("m_IsInit");
            m_IsDisableRecycling = serializedObject.FindProperty("m_IsDisableRecycling");
            m_IsDisableClosing = serializedObject.FindProperty("m_IsDisableClosing");
            m_SerialId = serializedObject.FindProperty("m_SerialId");
            m_OriginalLayer = serializedObject.FindProperty("m_OriginalLayer");
            m_UIFormAssetName = serializedObject.FindProperty("m_UIFormAssetName");
            m_AssetPath = serializedObject.FindProperty("m_AssetPath");
            m_DepthInUIGroup = serializedObject.FindProperty("m_DepthInUIGroup");
            m_PauseCoveredUIForm = serializedObject.FindProperty("m_PauseCoveredUIForm");
            m_FullName = serializedObject.FindProperty("m_FullName");
            m_EnableShowAnimation = serializedObject.FindProperty("m_EnableShowAnimation");
            m_ShowAnimationName = serializedObject.FindProperty("m_ShowAnimationName");
            m_EnableHideAnimation = serializedObject.FindProperty("m_EnableHideAnimation");
            m_HideAnimationName = serializedObject.FindProperty("m_HideAnimationName");
        }
    }
}