// ==========================================================================================
//   GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//   GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//   均受中华人民共和国及相关国际法律法规保护。
//   are protected by the laws of the People's Republic of China and relevant international regulations.
//   使用本项目须严格遵守相应法律法规及开源许可证之规定。
//   Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
//   本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//   This project is dual-licensed under the MIT License and Apache License 2.0,
//   完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//   please refer to the LICENSE file in the root directory of the source code for the full license text.
//   禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//   It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//   侵犯他人合法权益等法律法规所禁止的行为！
//   or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//   因基于本项目二次开发所产生的一切法律纠纷与责任，
//   Any legal disputes and liabilities arising from secondary development based on this project
//   本项目组织与贡献者概不承担。
//   shall be borne solely by the developer; the project organization and contributors assume no responsibility.
//   GitHub 仓库：https://github.com/GameFrameX
//   GitHub Repository: https://github.com/GameFrameX
//   Gitee  仓库：https://gitee.com/GameFrameX
//   Gitee Repository:  https://gitee.com/GameFrameX
//   CNB  仓库：https://cnb.cool/GameFrameX
//   CNB Repository:  https://cnb.cool/GameFrameX
//   官方文档：https://gameframex.doc.alianblank.com/
//   Official Documentation: https://gameframex.doc.alianblank.com/
//  ==========================================================================================

using System.Collections.Generic;
using GameFrameX.Asset.Runtime;
using GameFrameX.Event.Runtime;
using GameFrameX.ObjectPool;
using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面组件。
    /// </summary>
    /// <remarks>
    /// UI component for managing UI forms.
    /// </remarks>
    [DisallowMultipleComponent]
    [AddComponentMenu("GameFrameX/UI")]
    [UnityEngine.Scripting.Preserve]
    public partial class UIComponent : GameFrameworkComponent
    {
        [UnityEngine.Scripting.Preserve]
        private const int DefaultPriority = 0;

        [UnityEngine.Scripting.Preserve]
        private IUIManager m_UIManager = null;
        [UnityEngine.Scripting.Preserve]
        private EventComponent m_EventComponent = null;

        [UnityEngine.Scripting.Preserve]
        private readonly List<IUIForm> m_InternalUIFormResults = new List<IUIForm>();

        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_EnableOpenUIFormSuccessEvent = true;

        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_EnableOpenUIFormFailureEvent = true;

        // [SerializeField] private bool m_EnableOpenUIFormUpdateEvent = false;
        //
        // [SerializeField] private bool m_EnableOpenUIFormDependencyAssetEvent = false;
        /// <summary>
        /// 是否自动回收 UI。
        /// </summary>
        /// <remarks>
        /// Whether to enable auto release of UI forms.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_EnableAutoReleaseUIForm = true;

        /// <summary>
        /// 是否在关闭 UI 时触发完成事件。
        /// </summary>
        /// <remarks>
        /// Whether to trigger completion event when closing UI forms.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_EnableCloseUIFormCompleteEvent = true;

        /// <summary>
        /// 是否启用 UI 显示动画。
        /// </summary>
        /// <remarks>
        /// Whether to enable UI show animation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_IsEnableUIShowAnimation = false;

        /// <summary>
        /// 是否启用 UI 隐藏动画。
        /// </summary>
        /// <remarks>
        /// Whether to enable UI hide animation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_IsEnableUIHideAnimation = false;

        /// <summary>
        /// UI 自动回收间隔时间/秒。
        /// </summary>
        /// <remarks>
        /// UI auto release interval time in seconds.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private float m_InstanceAutoReleaseInterval = 60f;

        /// <summary>
        /// UI 实例容量。
        /// </summary>
        /// <remarks>
        /// UI instance capacity.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private int m_InstanceCapacity = 16;

        /// <summary>
        /// UI 实例过期时间/秒。
        /// </summary>
        /// <remarks>
        /// UI instance expire time in seconds.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private float m_InstanceExpireTime = 60f;

        /// <summary>
        /// UI 回收间隔时间/秒。
        /// </summary>
        /// <remarks>
        /// UI recycle interval time in seconds.
        /// </remarks>
        [Tooltip("UI 回收间隔时间/秒")] [SerializeField]
        [UnityEngine.Scripting.Preserve]
        private int m_RecycleInterval = 60;

        // [SerializeField] private int m_InstancePriority = 0;

        /// <summary>
        /// UI 实例根节点。
        /// </summary>
        /// <remarks>
        /// UGUI root transform.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private Transform m_InstanceUGUIRoot = null;

        /// <summary>
        /// FairyGUI 实例根节点。
        /// </summary>
        /// <remarks>
        /// FairyGUI root transform.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private Transform m_InstanceFairyGUIRoot = null;

        /// <summary>
        /// UI 表单帮助器类型名。
        /// </summary>
        /// <remarks>
        /// UI form helper type name.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private string m_UIFormHelperTypeName = "GameFrameX.UI.FairyGUI.Runtime.FairyGUIFormHelper";

        /// <summary>
        /// 自定义 UI 表单帮助器。
        /// </summary>
        /// <remarks>
        /// Custom UI form helper.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private UIFormHelperBase m_CustomUIFormHelper = null;

        /// <summary>
        /// UI 组帮助器类型名。
        /// </summary>
        /// <remarks>
        /// UI group helper type name.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private string m_UIGroupHelperTypeName = "GameFrameX.UI.FairyGUI.Runtime.FairyGUIUIGroupHelper";

        /// <summary>
        /// 自定义 UI 组帮助器。
        /// </summary>
        /// <remarks>
        /// Custom UI group helper.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private UIGroupHelperBase m_CustomUIGroupHelper = null;

        /// <summary>
        /// UI 组数组。
        /// </summary>
        /// <remarks>
        /// Array of UI groups.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private UIGroup[] m_UIGroups = new UIGroup[]
        {
            new UIGroup(UIGroupConstants.Hidden.Depth, UIGroupConstants.Hidden.Name),
            new UIGroup(UIGroupConstants.Background.Depth, UIGroupConstants.Background.Name),
            new UIGroup(UIGroupConstants.Scene.Depth, UIGroupConstants.Scene.Name),
            new UIGroup(UIGroupConstants.World.Depth, UIGroupConstants.World.Name),
            new UIGroup(UIGroupConstants.Battle.Depth, UIGroupConstants.Battle.Name),
            new UIGroup(UIGroupConstants.Hud.Depth, UIGroupConstants.Hud.Name),
            new UIGroup(UIGroupConstants.Map.Depth, UIGroupConstants.Map.Name),
            new UIGroup(UIGroupConstants.Floor.Depth, UIGroupConstants.Floor.Name),
            new UIGroup(UIGroupConstants.Normal.Depth, UIGroupConstants.Normal.Name),
            new UIGroup(UIGroupConstants.Fixed.Depth, UIGroupConstants.Fixed.Name),
            new UIGroup(UIGroupConstants.Window.Depth, UIGroupConstants.Window.Name),
            new UIGroup(UIGroupConstants.Tip.Depth, UIGroupConstants.Tip.Name),
            new UIGroup(UIGroupConstants.Guide.Depth, UIGroupConstants.Guide.Name),
            new UIGroup(UIGroupConstants.BlackBoard.Depth, UIGroupConstants.BlackBoard.Name),
            new UIGroup(UIGroupConstants.Dialogue.Depth, UIGroupConstants.Dialogue.Name),
            new UIGroup(UIGroupConstants.Loading.Depth, UIGroupConstants.Loading.Name),
            new UIGroup(UIGroupConstants.Notify.Depth, UIGroupConstants.Notify.Name),
            new UIGroup(UIGroupConstants.System.Depth, UIGroupConstants.System.Name),
        };

        /// <summary>
        /// 获取 UGUI 根节点变换组件。
        /// </summary>
        /// <remarks>
        /// Gets the UGUI root transform.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public Transform UGUIRoot
        {
            get { return m_InstanceUGUIRoot; }
        }

        /// <summary>
        /// 获取 FairyGUI 根节点变换组件。
        /// </summary>
        /// <remarks>
        /// Gets the FairyGUI root transform.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public Transform FairyGUIRoot
        {
            get { return m_InstanceFairyGUIRoot; }
        }

        /// <summary>
        /// 获取或设置是否启用自动回收界面。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether auto release of UI forms is enabled.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool EnableAutoReleaseUIForm
        {
            get { return m_EnableAutoReleaseUIForm; }
            set { m_EnableAutoReleaseUIForm = value; }
        }

        /// <summary>
        /// 获取是否启用界面显示动画。
        /// </summary>
        /// <remarks>
        /// Gets whether UI show animation is enabled.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsEnableUIShowAnimation
        {
            get { return m_UIManager.IsEnableUIShowAnimation; }
        }

        /// <summary>
        /// 获取是否启用界面隐藏动画。
        /// </summary>
        /// <remarks>
        /// Gets whether UI hide animation is enabled.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsEnableUIHideAnimation
        {
            get { return m_UIManager.IsEnableUIHideAnimation; }
        }

        /// <summary>
        /// 获取界面组数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of UI groups.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int UIGroupCount
        {
            get { return m_UIManager.UIGroupCount; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池自动回收可回收对象的间隔秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the auto recycle interval in seconds for the UI form instance pool.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int RecycleInterval
        {
            get { return m_UIManager.RecycleInterval; }
            set { m_UIManager.RecycleInterval = m_RecycleInterval = value; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the auto release interval in seconds for the UI form instance pool.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public float InstanceAutoReleaseInterval
        {
            get { return m_UIManager.InstanceAutoReleaseInterval; }
            set { m_UIManager.InstanceAutoReleaseInterval = m_InstanceAutoReleaseInterval = value; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池的容量。
        /// </summary>
        /// <remarks>
        /// Gets or sets the capacity of the UI form instance pool.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int InstanceCapacity
        {
            get { return m_UIManager.InstanceCapacity; }
            set { m_UIManager.InstanceCapacity = m_InstanceCapacity = value; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池对象过期秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the expire time in seconds for the UI form instance pool.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public float InstanceExpireTime
        {
            get { return m_UIManager.InstanceExpireTime; }
            set { m_UIManager.InstanceExpireTime = m_InstanceExpireTime = value; }
        }

        /*
        /// <summary>
        /// 获取或设置界面实例对象池的优先级。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int InstancePriority
        {
            get { return m_UIManager.InstancePriority; }
            set { m_UIManager.InstancePriority = m_InstancePriority = value; }
        }*/

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        /// <remarks>
        /// Initializes the game framework component.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected override void Awake()
        {
            ImplementationComponentType = Utility.Assembly.GetType(componentType);
            InterfaceComponentType = typeof(IUIManager);
            base.Awake();
            var namespaceName = ImplementationComponentType.Namespace;

#if ENABLE_UI_FAIRYGUI
            if (!namespaceName.StartsWithFast("GameFrameX.UI.FairyGUI.Runtime"))
            {
                Debug.LogError("UI组件的 ComponentType 设置错误。请设置和 UI 系统一致的组件.");
                return;
            }

            if (m_InstanceFairyGUIRoot == null)
            {
                Debug.LogError("UI组件的 FAIRY GUI Root 设置错误。请设置");
                return;
            }

            m_InstanceFairyGUIRoot.gameObject.SetActive(true);
            if (m_InstanceUGUIRoot != null)
            {
                m_InstanceUGUIRoot.gameObject.SetActive(false);
            }

#elif ENABLE_UI_UGUI
            if (!namespaceName.StartsWithFast("GameFrameX.UI.UGUI.Runtime"))
            {
                Debug.LogError("UI组件的 ComponentType 设置错误。请设置和 UI 系统一致的组件.");
                return;
            }

            if (m_InstanceFairyGUIRoot != null)
            {
                m_InstanceFairyGUIRoot.gameObject.SetActive(false);
            }

            if (m_InstanceUGUIRoot == null)
            {
                Debug.LogError("UI组件的 UGUI Root 设置错误。请设置");
                return;
            }

            m_InstanceUGUIRoot.gameObject.SetActive(true);
#endif
            if (!m_UIFormHelperTypeName.StartsWithFast(namespaceName))
            {
                Debug.LogError("UI组件的 UI Form Helper 设置错误。请设置和 ComponentType 类型 一致.");
                return;
            }

            if (!m_UIGroupHelperTypeName.StartsWithFast(namespaceName))
            {
                Debug.LogError("UI组件的 UI Group Helper 设置错误。请设置和 ComponentType 类型 一致.");
                return;
            }

            m_UIManager = GameFrameworkEntry.GetModule<IUIManager>();
            if (m_UIManager == null)
            {
                Debug.LogError("UI manager is invalid.");
                return;
            }

            if (m_EnableOpenUIFormSuccessEvent)
            {
                m_UIManager.OpenUIFormSuccess += OnOpenUIFormSuccess;
            }

            m_UIManager.OpenUIFormFailure += OnOpenUIFormFailure;
            /*
            if (m_EnableOpenUIFormUpdateEvent)
            {
                m_UIManager.OpenUIFormUpdate += OnOpenUIFormUpdate;
            }

            if (m_EnableOpenUIFormDependencyAssetEvent)
            {
                m_UIManager.OpenUIFormDependencyAsset += OnOpenUIFormDependencyAsset;
            }*/

            if (m_EnableCloseUIFormCompleteEvent)
            {
                m_UIManager.CloseUIFormComplete += OnCloseUIFormComplete;
            }
        }

        [UnityEngine.Scripting.Preserve]
        private void Start()
        {
            BaseComponent baseComponent = GameEntry.GetComponent<BaseComponent>();
            if (baseComponent == null)
            {
                Log.Fatal("Base component is invalid.");
                return;
            }

            m_EventComponent = GameEntry.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            m_UIManager.SetResourceManager(GameFrameworkEntry.GetModule<IAssetManager>());
            m_UIManager.SetObjectPoolManager(GameFrameworkEntry.GetModule<IObjectPoolManager>());
            m_UIManager.InstanceAutoReleaseInterval = m_InstanceAutoReleaseInterval;
            m_UIManager.InstanceCapacity = m_InstanceCapacity;
            m_UIManager.InstanceExpireTime = m_InstanceExpireTime;
            m_UIManager.RecycleInterval = m_RecycleInterval;
            m_UIManager.IsEnableUIHideAnimation = m_IsEnableUIHideAnimation;
            m_UIManager.IsEnableUIShowAnimation = m_IsEnableUIShowAnimation;
            // m_UIManager.InstancePriority = m_InstancePriority;

            m_CustomUIGroupHelper = Helper.CreateHelper(m_UIGroupHelperTypeName, m_CustomUIGroupHelper);
            if (m_CustomUIGroupHelper == null)
            {
                Log.Error("Can not create UI Group helper.");
                return;
            }

            m_CustomUIGroupHelper.name = "UI Group Helper";
            Transform transform = m_CustomUIGroupHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;


            UIFormHelperBase uiFormHelper = Helper.CreateHelper(m_UIFormHelperTypeName, m_CustomUIFormHelper);
            if (uiFormHelper == null)
            {
                Log.Error("Can not create UI form helper.");
                return;
            }

            uiFormHelper.name = "UI Form Helper";
            transform = uiFormHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            m_UIManager.SetUIFormHelper(uiFormHelper);
#if ENABLE_UI_UGUI
            if (m_InstanceUGUIRoot == null)
            {
                m_InstanceUGUIRoot = new GameObject("UI Form Instances").transform;
                m_InstanceUGUIRoot.SetParent(gameObject.transform);
                m_InstanceUGUIRoot.localScale = Vector3.one;
            }

            m_InstanceUGUIRoot.gameObject.layer = LayerMask.NameToLayer("UI");
#endif
            for (int i = 0; i < m_UIGroups.Length; i++)
            {
                var uiGroup = m_UIGroups[i];
                if (!AddUIGroup(uiGroup.Name, uiGroup.Depth))
                {
                    Log.Warning("Add UI group '{0}' failure.", uiGroup.Name);
                    continue;
                }
            }
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <remarks>
        /// Checks if a UI form exists.
        /// </remarks>
        /// <param name="serialId">界面序列编号。 / The UI form serial ID.</param>
        /// <returns>是否存在界面。 / Whether the UI form exists.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool HasUIForm(int serialId)
        {
            return m_UIManager.HasUIForm(serialId);
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <remarks>
        /// Checks if a UI form exists.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。 / The UI form asset name.</param>
        /// <returns>是否存在界面。 / Whether the UI form exists.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool HasUIForm(string uiFormAssetName)
        {
            return m_UIManager.HasUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <remarks>
        /// Checks if a UI form is loading.
        /// </remarks>
        /// <param name="serialId">界面序列编号。 / The UI form serial ID.</param>
        /// <returns>是否正在加载界面。 / Whether the UI form is loading.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool IsLoadingUIForm(int serialId)
        {
            return m_UIManager.IsLoadingUIForm(serialId);
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <remarks>
        /// Checks if a UI form is loading.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。 / The UI form asset name.</param>
        /// <returns>是否正在加载界面。 / Whether the UI form is loading.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool IsLoadingUIForm(string uiFormAssetName)
        {
            return m_UIManager.IsLoadingUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 是否是合法的界面。
        /// </summary>
        /// <remarks>
        /// Checks if a UI form is valid.
        /// </remarks>
        /// <param name="uiForm">界面。 / The UI form.</param>
        /// <returns>界面是否合法。 / Whether the UI form is valid.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool IsValidUIForm(IUIForm uiForm)
        {
            return m_UIManager.IsValidUIForm(uiForm);
        }


        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <remarks>
        /// Refocuses a UI form.
        /// </remarks>
        /// <param name="uiForm">要激活的界面。 / The UI form to refocus.</param>
        [UnityEngine.Scripting.Preserve]
        public void RefocusUIForm(UIForm uiForm)
        {
            m_UIManager.RefocusUIForm(uiForm);
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <remarks>
        /// Refocuses a UI form.
        /// </remarks>
        /// <param name="uiForm">要激活的界面。 / The UI form to refocus.</param>
        /// <param name="userData">用户自定义数据。 / User custom data.</param>
        [UnityEngine.Scripting.Preserve]
        public void RefocusUIForm(UIForm uiForm, object userData)
        {
            m_UIManager.RefocusUIForm(uiForm, userData);
        }

        /// <summary>
        /// 设置界面是否被加锁。
        /// </summary>
        /// <remarks>
        /// Sets whether the UI form instance is locked.
        /// </remarks>
        /// <param name="uiForm">要设置是否被加锁的界面。 / The UI form to set locked state.</param>
        /// <param name="locked">界面是否被加锁。 / Whether the UI form is locked.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetUIFormInstanceLocked(UIForm uiForm, bool locked)
        {
            if (uiForm == null)
            {
                Log.Warning("UI form is invalid.");
                return;
            }

            m_UIManager.SetUIFormInstanceLocked(uiForm.gameObject, locked);
        }

        [UnityEngine.Scripting.Preserve]
        private void OnOpenUIFormSuccess(object sender, OpenUIFormSuccessEventArgs e)
        {
            m_EventComponent.Fire(this, e);
        }

        [UnityEngine.Scripting.Preserve]
        private void OnOpenUIFormFailure(object sender, OpenUIFormFailureEventArgs e)
        {
            Log.Warning("Open UI form failure, asset name '{0}', pause covered UI form '{1}', error message '{2}'.", e.UIFormAssetName, e.PauseCoveredUIForm, e.ErrorMessage);
            if (m_EnableOpenUIFormFailureEvent)
            {
                m_EventComponent.Fire(this, e);
            }
        }

        [UnityEngine.Scripting.Preserve]
        public void SetShowUIFormHandler(IUIFormShowHandler uiFormShowHandler)
        {
            m_UIManager.SetUIFormShowHandler(uiFormShowHandler);
        }

        [UnityEngine.Scripting.Preserve]
        public void SetHideUIFormHandler(IUIFormHideHandler uiFormHideHandler)
        {
            m_UIManager.SetUIFormHideHandler(uiFormHideHandler);
        }

        /*
        [UnityEngine.Scripting.Preserve]
        private void OnOpenUIFormUpdate(object sender, OpenUIFormUpdateEventArgs e)
        {
            m_EventComponent.Fire(this, e);
        }

        [UnityEngine.Scripting.Preserve]
        private void OnOpenUIFormDependencyAsset(object sender, OpenUIFormDependencyAssetEventArgs e)
        {
            m_EventComponent.Fire(this, e);
        }*/

        [UnityEngine.Scripting.Preserve]
        private void OnCloseUIFormComplete(object sender, CloseUIFormCompleteEventArgs e)
        {
            m_EventComponent.Fire(this, e);
        }
    }
}
