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
    [CustomEditor(typeof(UIComponent))]
    internal sealed class UIComponentInspector : ComponentTypeComponentInspector
    {
        private SerializedProperty m_EnableOpenUIFormSuccessEvent = null;

        private SerializedProperty m_EnableOpenUIFormFailureEvent = null;

        // private SerializedProperty m_EnableOpenUIFormUpdateEvent = null;
        // private SerializedProperty m_EnableOpenUIFormDependencyAssetEvent = null;
        private SerializedProperty m_EnableCloseUIFormCompleteEvent = null;
        private SerializedProperty m_InstanceAutoReleaseInterval = null;
        private SerializedProperty m_InstanceCapacity = null;
        private SerializedProperty m_IsEnableUIShowAnimation = null;
        private SerializedProperty m_IsEnableUIHideAnimation = null;

        private SerializedProperty m_InstanceExpireTime = null;
        private SerializedProperty m_RecycleInterval = null;

        // private SerializedProperty m_InstancePriority = null;
        private SerializedProperty m_InstanceUGUIRoot = null;
        private SerializedProperty m_InstanceFairyGUIRoot = null;
        private SerializedProperty m_UIGroups = null;

        private HelperInfo<UIFormHelperBase> m_UIFormHelperInfo = new HelperInfo<UIFormHelperBase>("UIForm");
        private HelperInfo<UIGroupHelperBase> m_UIGroupHelperInfo = new HelperInfo<UIGroupHelperBase>("UIGroup");

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            UIComponent t = (UIComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_UIFormHelperInfo.Draw();
                m_UIGroupHelperInfo.Draw();

                EditorGUILayout.HelpBox("以上的组件前缀的命名空间必须设置为一致，否则将会初始化失败", MessageType.Warning);

                EditorGUILayout.PropertyField(m_EnableOpenUIFormSuccessEvent);
                EditorGUILayout.PropertyField(m_EnableOpenUIFormFailureEvent);
                // EditorGUILayout.PropertyField(m_EnableOpenUIFormUpdateEvent);
                // EditorGUILayout.PropertyField(m_EnableOpenUIFormDependencyAssetEvent);
                EditorGUILayout.PropertyField(m_EnableCloseUIFormCompleteEvent);
                EditorGUILayout.IntSlider(m_RecycleInterval, 10, 600, "Recycle Interval");
                EditorGUILayout.PropertyField(m_IsEnableUIShowAnimation);
                EditorGUILayout.PropertyField(m_IsEnableUIHideAnimation);
            }
            EditorGUI.EndDisabledGroup();

            float instanceAutoReleaseInterval = EditorGUILayout.DelayedFloatField("Instance Auto Release Interval", m_InstanceAutoReleaseInterval.floatValue);
            if (instanceAutoReleaseInterval != m_InstanceAutoReleaseInterval.floatValue)
            {
                if (EditorApplication.isPlaying)
                {
                    t.InstanceAutoReleaseInterval = instanceAutoReleaseInterval;
                }
                else
                {
                    m_InstanceAutoReleaseInterval.floatValue = instanceAutoReleaseInterval;
                }
            }

            int instanceCapacity = EditorGUILayout.DelayedIntField("Instance Capacity", m_InstanceCapacity.intValue);
            if (instanceCapacity != m_InstanceCapacity.intValue)
            {
                if (EditorApplication.isPlaying)
                {
                    t.InstanceCapacity = instanceCapacity;
                }
                else
                {
                    m_InstanceCapacity.intValue = instanceCapacity;
                }
            }

            float instanceExpireTime = EditorGUILayout.DelayedFloatField("Instance Expire Time", m_InstanceExpireTime.floatValue);
            if (instanceExpireTime != m_InstanceExpireTime.floatValue)
            {
                if (EditorApplication.isPlaying)
                {
                    t.InstanceExpireTime = instanceExpireTime;
                }
                else
                {
                    m_InstanceExpireTime.floatValue = instanceExpireTime;
                }
            }

            /*
            int instancePriority = EditorGUILayout.DelayedIntField("Instance Priority", m_InstancePriority.intValue);
            if (instancePriority != m_InstancePriority.intValue)
            {
                if (EditorApplication.isPlaying)
                {
                    t.InstancePriority = instancePriority;
                }
                else
                {
                    m_InstancePriority.intValue = instancePriority;
                }
            }*/

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                EditorGUILayout.HelpBox("设置为UGUI的根节点", MessageType.Info);
                EditorGUILayout.PropertyField(m_InstanceUGUIRoot);
                EditorGUILayout.HelpBox("设置为FairyGUI的根节点", MessageType.Info);
                EditorGUILayout.PropertyField(m_InstanceFairyGUIRoot);

                if (m_UIGroups.arraySize <= 0)
                {
                    EditorGUILayout.HelpBox("必须要设置至少一个UIGroup", MessageType.Error);
                }

                EditorGUILayout.HelpBox("强烈推荐不要设置为同一个Depth(深度)", MessageType.Info);
                EditorGUILayout.PropertyField(m_UIGroups, true);
            }
            EditorGUI.EndDisabledGroup();

            if (EditorApplication.isPlaying && IsPrefabInHierarchy(t.gameObject))
            {
                EditorGUILayout.LabelField("UI Group Count", t.UIGroupCount.ToString());
            }

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }

        protected override void Enable()
        {
            m_EnableOpenUIFormSuccessEvent = serializedObject.FindProperty("m_EnableOpenUIFormSuccessEvent");
            m_EnableOpenUIFormFailureEvent = serializedObject.FindProperty("m_EnableOpenUIFormFailureEvent");
            // m_EnableOpenUIFormUpdateEvent = serializedObject.FindProperty("m_EnableOpenUIFormUpdateEvent");
            // m_EnableOpenUIFormDependencyAssetEvent = serializedObject.FindProperty("m_EnableOpenUIFormDependencyAssetEvent");
            m_EnableCloseUIFormCompleteEvent = serializedObject.FindProperty("m_EnableCloseUIFormCompleteEvent");
            m_InstanceAutoReleaseInterval = serializedObject.FindProperty("m_InstanceAutoReleaseInterval");
            m_InstanceCapacity = serializedObject.FindProperty("m_InstanceCapacity");
            m_InstanceExpireTime = serializedObject.FindProperty("m_InstanceExpireTime");
            m_RecycleInterval = serializedObject.FindProperty("m_RecycleInterval");
            // m_InstancePriority = serializedObject.FindProperty("m_InstancePriority");
            m_InstanceUGUIRoot = serializedObject.FindProperty("m_InstanceUGUIRoot");
            m_InstanceFairyGUIRoot = serializedObject.FindProperty("m_InstanceFairyGUIRoot");
            m_IsEnableUIShowAnimation = serializedObject.FindProperty("m_IsEnableUIShowAnimation");
            m_IsEnableUIHideAnimation = serializedObject.FindProperty("m_IsEnableUIHideAnimation");
            m_UIGroups = serializedObject.FindProperty("m_UIGroups");

            m_UIFormHelperInfo.Init(serializedObject);
            m_UIGroupHelperInfo.Init(serializedObject);

            RefreshTypeNames();
        }

        protected override void RefreshTypeNames()
        {
            RefreshComponentTypeNames(typeof(IUIManager));
            m_UIFormHelperInfo.Refresh();
            m_UIGroupHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }
    }
}