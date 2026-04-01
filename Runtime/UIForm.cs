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

using System;
using System.Collections.Generic;
using GameFrameX.Event.Runtime;
using GameFrameX.Localization.Runtime;
using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面。
    /// </summary>
    /// <remarks>
    /// UI form base class.
    /// </remarks>
    [Serializable]
    [UnityEngine.Scripting.Preserve]
    public abstract class UIForm : MonoBehaviour, IUIForm
    {
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_Available = false;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_Visible = false;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_IsInit = false;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_IsDisableRecycling = false;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_IsCenter = false;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_IsDisableClosing = false;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private int m_SerialId;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private int m_OriginalLayer = 0;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private string m_UIFormAssetName;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private string m_AssetPath;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private int m_DepthInUIGroup;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_PauseCoveredUIForm;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private string m_FullName;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_EnableShowAnimation;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private string m_ShowAnimationName;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private bool m_EnableHideAnimation;
        [UnityEngine.Scripting.Preserve]
        [SerializeField] private string m_HideAnimationName;
        [UnityEngine.Scripting.Preserve]
        private IUIGroup m_UIGroup;
        [UnityEngine.Scripting.Preserve]
        private UIEventSubscriber m_EventSubscriber = null;
        [UnityEngine.Scripting.Preserve]
        private object m_UserData = null;

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        /// <remarks>
        /// Gets the user custom data.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public object UserData
        {
            get { return m_UserData; }
        }

        /// <summary>
        /// 获取界面事件订阅器。
        /// </summary>
        /// <remarks>
        /// Gets the UI form event subscriber.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected UIEventSubscriber EventSubscriber
        {
            get { return m_EventSubscriber; }
        }

        /// <summary>
        /// 获取界面是否来自对象池。
        /// </summary>
        /// <remarks>
        /// Gets whether the UI form is from the object pool.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected bool IsFromPool { get; set; }

        /// <summary>
        /// 获取界面是否已被销毁。
        /// </summary>
        /// <remarks>
        /// Gets whether the UI form has been disposed.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected bool IsDisposed { get; set; }

        /// <summary>
        /// 界面回收开始时间
        /// </summary>
        /// <remarks>
        /// The start time of UI form recycling.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public DateTime ReleaseStartTime { get; private set; } = DateTime.MaxValue;

        /// <summary>
        /// 获取界面序列编号。
        /// </summary>
        /// <remarks>
        /// Gets the UI form serial number.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int SerialId
        {
            get { return m_SerialId; }
        }

        /// <summary>
        /// 获取界面完整名称。
        /// </summary>
        /// <remarks>
        /// Gets the UI form full name.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public string FullName
        {
            get { return m_FullName; }
        }

        /// <summary>
        /// 获取或设置界面名称。
        /// </summary>
        /// <remarks>
        /// Gets or sets the UI form name.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public string Name
        {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }

        /// <summary>
        /// 获取界面是否可用。
        /// </summary>
        /// <remarks>
        /// Gets whether the UI form is available.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool Available
        {
            get { return m_Available; }
        }

        /// <summary>
        /// 是否启用显示动画
        /// </summary>
        /// <remarks>
        /// Whether to enable show animation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool EnableShowAnimation
        {
            get { return m_EnableShowAnimation; }
            set { m_EnableShowAnimation = value; }
        }

        /// <summary>
        /// 显示动画名称
        /// </summary>
        /// <remarks>
        /// The name of the show animation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public string ShowAnimationName
        {
            get { return m_ShowAnimationName; }
            set { m_ShowAnimationName = value; }
        }

        /// <summary>
        /// 是否启用隐藏动画
        /// </summary>
        /// <remarks>
        /// Whether to enable hide animation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool EnableHideAnimation
        {
            get { return m_EnableHideAnimation; }
            set { m_EnableHideAnimation = value; }
        }

        /// <summary>
        /// 隐藏动画名称
        /// </summary>
        /// <remarks>
        /// The name of the hide animation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public string HideAnimationName
        {
            get { return m_HideAnimationName; }
            set { m_HideAnimationName = value; }
        }

        /// <summary>
        /// 获取或设置界面是否可见。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether the UI form is visible.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual bool Visible
        {
            get { return m_Available && m_Visible; }
            [UnityEngine.Scripting.Preserve]
            protected set
            {
                if (!m_Available)
                {
                    Log.Warning("UI form '{0}' is not available.", Name);
                    return;
                }

                if (m_Visible == value)
                {
                    return;
                }

                m_Visible = value;
                InternalSetVisible(value);
            }
        }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        /// <remarks>
        /// Gets the UI form asset name.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public string UIFormAssetName
        {
            get { return m_UIFormAssetName; }
        }

        /// <summary>
        /// 是否禁用回收，禁用回收的界面不会被回收
        /// </summary>
        /// <remarks>
        /// Whether recycling is disabled. UI forms with recycling disabled will not be recycled.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsDisableRecycling
        {
            get { return m_IsDisableRecycling; }
            [UnityEngine.Scripting.Preserve]
            protected set { m_IsDisableRecycling = value; }
        }

        /// <summary>
        /// 是否禁用关闭，禁用关闭的界面不会被关闭
        /// </summary>
        /// <remarks>
        /// Whether closing is disabled. UI forms with closing disabled will not be closed.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsDisableClosing
        {
            get { return m_IsDisableClosing; }
            [UnityEngine.Scripting.Preserve]
            protected set { m_IsDisableClosing = value; }
        }

        /// <summary>
        /// 是否可以回收，true:界面可以被回收，false:界面不可以被回收
        /// </summary>
        /// <remarks>
        /// Whether the UI form can be recycled. true: the UI form can be recycled, false: the UI form cannot be recycled.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsCanRecycle
        {
            get
            {
                if (m_IsDisableRecycling)
                {
                    return false;
                }

                return (DateTime.Now - ReleaseStartTime).TotalSeconds >= RecycleInterval;
            }
        }

        /// <summary>
        /// 界面回收间隔，单位：秒
        /// </summary>
        /// <remarks>
        /// UI form recycling interval in seconds.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int RecycleInterval { get; private set; }

        /// <summary>
        /// 是否开启组件居中，true:组件生成后默认父组件居中
        /// </summary>
        /// <remarks>
        /// Whether to enable component centering. true: components are centered to parent component after creation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsCenter
        {
            get { return m_IsCenter; }
            [UnityEngine.Scripting.Preserve]
            protected set { m_IsCenter = value; }
        }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        /// <remarks>
        /// Gets the UI form asset path.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public string AssetPath
        {
            get { return m_AssetPath; }
            [UnityEngine.Scripting.Preserve]
            protected set { m_AssetPath = value; }
        }

        /// <summary>
        /// 获取界面实例。
        /// </summary>
        /// <remarks>
        /// Gets the UI form instance handle.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public object Handle
        {
            get { return gameObject; }
        }

        /// <summary>
        /// 获取界面所属的界面组。
        /// </summary>
        /// <remarks>
        /// Gets the UI group that the UI form belongs to.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual IUIGroup UIGroup
        {
            get { return m_UIGroup; }
            set { m_UIGroup = value; }
        }

        /// <summary>
        /// 获取界面深度。
        /// </summary>
        /// <remarks>
        /// Gets the depth of the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int DepthInUIGroup
        {
            get { return m_DepthInUIGroup; }
        }

        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        /// <remarks>
        /// Gets whether to pause covered UI forms.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool PauseCoveredUIForm
        {
            get { return m_PauseCoveredUIForm; }
        }

        /// <summary>
        /// 获取界面是否已唤醒。
        /// </summary>
        /// <remarks>
        /// Gets whether the UI form has been awakened.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsAwake { get; private set; }

        /// <summary>
        /// 界面初始化前执行
        /// </summary>
        /// <remarks>
        /// Executed before UI form initialization.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnAwake()
        {
            IsAwake = true;
        }

        /// <summary>
        /// Unity生命周期Awake方法
        /// 在此处初始化事件订阅器并订阅本地化语言变更事件
        /// </summary>
        /// <remarks>
        /// Unity lifecycle Awake method.
        /// Initializes event subscriber and subscribes to localization language change events here.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        private void Awake()
        {
            m_EventSubscriber = UIEventSubscriber.Create(this);
            m_EventSubscriber.CheckSubscribe(LocalizationLanguageChangeEventArgs.EventId, OnLocalizationLanguageChanged);
            OnAwake();
        }

        /// <summary>
        /// Unity生命周期OnEnable方法
        /// 在界面启用时触发事件订阅
        /// </summary>
        /// <remarks>
        /// Unity lifecycle OnEnable method.
        /// Triggers event subscription when the UI form is enabled.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        private void OnEnable()
        {
            OnEventSubscribe();
        }

        /// <summary>
        /// Unity生命周期OnDisable方法
        /// 在界面禁用时取消事件订阅,忽略本地化语言变更事件的订阅
        /// </summary>
        /// <remarks>
        /// Unity lifecycle OnDisable method.
        /// Unsubscribes events when the UI form is disabled, ignoring localization language change event subscription.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        private void OnDisable()
        {
            OnEventUnSubscribe();
            m_EventSubscriber?.UnSubscribeAll(new List<string>() { LocalizationLanguageChangeEventArgs.EventId });
        }

        /// <summary>
        /// Unity生命周期OnDestroy方法
        /// 在界面销毁时执行清理操作
        /// </summary>
        /// <remarks>
        /// Unity lifecycle OnDestroy method.
        /// Performs cleanup operations when the UI form is destroyed.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        private void OnDestroy()
        {
            Dispose();
        }

        /// <summary>
        /// 订阅事件时调用
        /// 在界面启用(OnEnable)时触发,可在此处订阅界面所需的事件
        /// 继承类通过重写此方法来注册自己需要的事件
        /// </summary>
        /// <remarks>
        /// Called when subscribing to events.
        /// Triggered when the UI form is enabled (OnEnable). Subscribe to required events here.
        /// Derived classes override this method to register their own events.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected virtual void OnEventSubscribe()
        {
        }

        /// <summary>
        /// 取消订阅事件时调用
        /// 在界面禁用(OnDisable)时触发,可在此处取消订阅界面的事件
        /// 继承类通过重写此方法来取消注册自己的事件
        /// </summary>
        /// <remarks>
        /// Called when unsubscribing from events.
        /// Triggered when the UI form is disabled (OnDisable). Unsubscribe from UI form events here.
        /// Derived classes override this method to unregister their own events.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected virtual void OnEventUnSubscribe()
        {
        }

        /// <summary>
        /// 界面初始化。
        /// </summary>
        /// <remarks>
        /// UI form initialization.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected virtual void InitView()
        {
        }

        /// <summary>
        /// 初始化界面。
        /// </summary>
        /// <remarks>
        /// Initializes the UI form.
        /// </remarks>
        /// <param name="serialId">界面序列编号。 / The UI form serial number.</param>
        /// <param name="uiFormAssetPath">界面资源路径。 / The UI form asset path.</param>
        /// <param name="uiFormAssetName">界面资源名称。 / The UI form asset name.</param>
        /// <param name="uiGroup">界面所处的界面组。 / The UI group that the form belongs to.</param>
        /// <param name="onInitAction">初始化界面前的委托。 / The delegate to invoke before initializing the UI form.</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。 / Whether to pause covered UI forms.</param>
        /// <param name="isNewInstance">是否是新实例。 / Whether this is a new instance.</param>
        /// <param name="userData">用户自定义数据。 / The user custom data.</param>
        /// <param name="recycleInterval">回收间隔（秒）。 / The recycle interval in seconds.</param>
        /// <param name="isFullScreen">是否全屏。 / Whether the UI form is full screen.</param>
        [UnityEngine.Scripting.Preserve]
        public void Init(int serialId, string uiFormAssetPath, string uiFormAssetName, IUIGroup uiGroup, Action<IUIForm> onInitAction, bool pauseCoveredUIForm, bool isNewInstance, object userData, int recycleInterval, bool isFullScreen = false)
        {
            RecycleInterval = recycleInterval;
            ReleaseStartTime = DateTime.MaxValue;
            m_UserData = userData;
            if (serialId >= 0)
            {
                m_SerialId = serialId;
            }

            m_PauseCoveredUIForm = pauseCoveredUIForm;
            m_UIGroup = uiGroup;
            if (m_IsInit)
            {
                return;
            }

            m_AssetPath = uiFormAssetPath;
            m_UIFormAssetName = uiFormAssetName;
            m_FullName = GetType().FullName;
            m_DepthInUIGroup = 0;
            m_OriginalLayer = gameObject.layer;
            if (!isNewInstance)
            {
                return;
            }

            try
            {
                onInitAction?.Invoke(this);
                InitView();
                if (isFullScreen)
                {
                    MakeFullScreen();
                }

                OnInit();
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnInit with exception '{2}'.", m_SerialId, m_UIFormAssetName, exception);
            }

            m_IsInit = true;
        }

        /// <summary>
        /// 初始化界面。
        /// </summary>
        /// <remarks>
        /// Initializes the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnInit()
        {
        }

        [UnityEngine.Scripting.Preserve]
        private void OnLocalizationLanguageChanged(object sender, GameEventArgs e)
        {
            UpdateLocalization();
        }

        /// <summary>
        /// 界面回收。
        /// </summary>
        /// <remarks>
        /// Recycles the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnRecycle()
        {
            m_DepthInUIGroup = 0;
            m_PauseCoveredUIForm = true;
        }

        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <remarks>
        /// Opens the UI form.
        /// </remarks>
        /// <param name="userData">用户自定义数据。 / The user custom data.</param>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnOpen(object userData)
        {
            m_Available = true;
            Visible = true;
            m_UserData = userData;
        }


        /// <summary>
        /// 绑定事件
        /// </summary>
        /// <remarks>
        /// Binds events.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void BindEvent()
        {
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <remarks>
        /// Loads data.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void LoadData()
        {
        }

        /// <summary>
        /// 界面更新本地化。
        /// </summary>
        /// <remarks>
        /// Updates UI form localization.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void UpdateLocalization()
        {
        }

        /// <summary>
        /// 界面显示。
        /// </summary>
        /// <remarks>
        /// Shows the UI form.
        /// </remarks>
        /// <param name="handler">界面显示处理接口。 / The UI form show handler interface.</param>
        /// <param name="complete">完成回调。 / The completion callback.</param>
        [UnityEngine.Scripting.Preserve]
        public abstract void Show(IUIFormShowHandler handler, Action complete);

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <remarks>
        /// Closes the UI form.
        /// </remarks>
        /// <param name="isShutdown">是否是关闭界面管理器时触发。 / Whether this is triggered when closing the UI manager.</param>
        /// <param name="userData">用户自定义数据。 / The user custom data.</param>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnClose(bool isShutdown, object userData)
        {
            if (gameObject != null)
            {
                gameObject.SetLayerRecursively(m_OriginalLayer);
            }

            m_Available = false;
            Visible = false;
            if (m_IsDisableRecycling)
            {
                return;
            }

            ReleaseStartTime = DateTime.Now;
        }


        /// <summary>
        /// 界面隐藏。
        /// </summary>
        /// <remarks>
        /// Hides the UI form.
        /// </remarks>
        /// <param name="handler">界面隐藏处理接口。 / The UI form hide handler interface.</param>
        /// <param name="complete">完成回调。 / The completion callback.</param>
        [UnityEngine.Scripting.Preserve]
        public abstract void Hide(IUIFormHideHandler handler, Action complete);

        /// <summary>
        /// 界面暂停。
        /// </summary>
        /// <remarks>
        /// Pauses the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnPause()
        {
            m_Available = false;
            Visible = false;
        }

        /// <summary>
        /// 界面暂停恢复。
        /// </summary>
        /// <remarks>
        /// Resumes the UI form from pause.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnResume()
        {
            m_Available = true;
            Visible = true;
        }

        /// <summary>
        /// 界面遮挡。
        /// </summary>
        /// <remarks>
        /// Called when the UI form is covered.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnCover()
        {
        }

        /// <summary>
        /// 界面遮挡恢复。
        /// </summary>
        /// <remarks>
        /// Called when the UI form is revealed after being covered.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnReveal()
        {
        }

        /// <summary>
        /// 界面激活。
        /// </summary>
        /// <remarks>
        /// Activates the UI form.
        /// </remarks>
        /// <param name="userData">用户自定义数据。 / The user custom data.</param>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnRefocus(object userData)
        {
        }

        /// <summary>
        /// 界面轮询。
        /// </summary>
        /// <remarks>
        /// Updates the UI form.
        /// </remarks>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。 / The logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。 / The real elapsed time in seconds.</param>
        [UnityEngine.Scripting.Preserve]
        public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <remarks>
        /// Called when the UI form depth changes.
        /// </remarks>
        /// <param name="uiGroupDepth">界面组深度。 / The UI group depth.</param>
        /// <param name="depthInUIGroup">界面在界面组中的深度。 / The depth of the UI form within the UI group.</param>
        [UnityEngine.Scripting.Preserve]
        public void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            m_DepthInUIGroup = depthInUIGroup;
        }

        /// <summary>
        /// 销毁界面.
        /// </summary>
        /// <remarks>
        /// Disposes the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            if (m_EventSubscriber != null)
            {
                m_EventSubscriber.UnSubscribeAll();
                ReferencePool.Release(m_EventSubscriber);
            }

            m_EventSubscriber = null;
            IsDisposed = true;
        }

        /// <summary>
        /// 设置界面的可见性。
        /// </summary>
        /// <remarks>
        /// Sets the visibility of the UI form.
        /// </remarks>
        /// <param name="visible">界面的可见性。 / The visibility of the UI form.</param>
        [UnityEngine.Scripting.Preserve]
        protected abstract void InternalSetVisible(bool visible);

        /// <summary>
        /// 设置界面为全屏
        /// </summary>
        /// <remarks>
        /// Sets the UI form to full screen.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected internal abstract void MakeFullScreen();
    }
}