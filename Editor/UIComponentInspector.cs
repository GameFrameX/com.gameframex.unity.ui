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
        private SerializedProperty m_RecycleInterval = null;
        private SerializedProperty m_InstanceUGUIRoot = null;
        private SerializedProperty m_InstanceFairyGUIRoot = null;
        private SerializedProperty m_DesignResolutionComponent = null;
        private SerializedProperty m_UIGroups = null;
        private SerializedProperty m_CustomUIFormHelper = null;
        private SerializedProperty m_CustomUIGroupHelper = null;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            UIComponent t = (UIComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                EditorGUILayout.PropertyField(m_CustomUIFormHelper);
                EditorGUILayout.PropertyField(m_CustomUIGroupHelper);

                EditorGUILayout.HelpBox("以上的两个自定义 Helper 的命名空间必须与组件 ComponentType 一致，否则将会初始化失败（Helper 类型名已迁移至 UIOptions）", MessageType.Warning);

                EditorGUILayout.HelpBox("界面回收间隔时间（秒）。设置对象池自动回收可回收对象的时间间隔，范围 10-600 秒。", MessageType.Info);
                EditorGUILayout.IntSlider(m_RecycleInterval, 10, 600, "Recycle Interval");
            }
            EditorGUI.EndDisabledGroup();



            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                EditorGUILayout.HelpBox("设置为UGUI的根节点", MessageType.Info);
                EditorGUILayout.PropertyField(m_InstanceUGUIRoot);
                EditorGUILayout.HelpBox("设置为FairyGUI的根节点", MessageType.Info);
                EditorGUILayout.PropertyField(m_InstanceFairyGUIRoot);
                EditorGUILayout.HelpBox("设置 UI 设计分辨率配置组件。为空时运行时会自动在 UI 节点上创建默认配置。", MessageType.Info);
                EditorGUILayout.PropertyField(m_DesignResolutionComponent);

                if (m_UIGroups.arraySize <= 0)
                {
                    EditorGUILayout.HelpBox("必须要设置至少一个UIGroup", MessageType.Error);
                }

                EditorGUILayout.HelpBox("强烈推荐不要设置为同一个Depth(深度)", MessageType.Info);
                EditorGUILayout.PropertyField(m_UIGroups, true);
            }
            EditorGUI.EndDisabledGroup();

            // C9 活视图：编辑态提示 UIOptions 入口；Play 态实时参数可改（优先经 UIComponent 公开属性写管理器+字段），
            // 两个 Helper 工厂为启动期型只读；RecycleInterval 无对应 Options 成员不在此绘制。
            GameFrameXOptionsLiveViewUtility.DrawLiveView(t, typeof(UIOptions));

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
            m_RecycleInterval = serializedObject.FindProperty("m_RecycleInterval");
            m_InstanceUGUIRoot = serializedObject.FindProperty("m_InstanceUGUIRoot");
            m_InstanceFairyGUIRoot = serializedObject.FindProperty("m_InstanceFairyGUIRoot");
            m_DesignResolutionComponent = serializedObject.FindProperty("m_DesignResolutionComponent");
            m_UIGroups = serializedObject.FindProperty("m_UIGroups");
            m_CustomUIFormHelper = serializedObject.FindProperty("m_CustomUIFormHelper");
            m_CustomUIGroupHelper = serializedObject.FindProperty("m_CustomUIGroupHelper");

            RefreshTypeNames();
        }

        protected override void RefreshTypeNames()
        {
            RefreshComponentTypeNames(typeof(IUIManager));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
