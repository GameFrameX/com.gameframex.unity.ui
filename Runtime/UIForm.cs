// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

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
    [UnityEngine.Scripting.Preserve]
    [Serializable]
    public abstract class UIForm : MonoBehaviour, IUIForm
    {
        [SerializeField] private bool m_Available = false;
        [SerializeField] private bool m_Visible = false;
        [SerializeField] private bool m_IsInit = false;
        [SerializeField] private bool m_IsDisableRecycling = false;
        [SerializeField] private bool m_IsCenter = false;
        [SerializeField] private bool m_IsDisableClosing = false;
        [SerializeField] private int m_SerialId;
        [SerializeField] private int m_OriginalLayer = 0;
        [SerializeField] private string m_UIFormAssetName;
        [SerializeField] private string m_AssetPath;
        [SerializeField] private int m_DepthInUIGroup;
        [SerializeField] private bool m_PauseCoveredUIForm;
        [SerializeField] private string m_FullName;
        [SerializeField] private bool m_EnableShowAnimation;
        [SerializeField] private string m_ShowAnimationName;
        [SerializeField] private bool m_EnableHideAnimation;
        [SerializeField] private string m_HideAnimationName;
        private IUIGroup m_UIGroup;
        private UIEventSubscriber m_EventSubscriber = null;
        private object m_UserData = null;

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        public object UserData
        {
            get { return m_UserData; }
        }

        /// <summary>
        /// 获取界面事件订阅器。
        /// </summary>
        protected UIEventSubscriber EventSubscriber
        {
            get { return m_EventSubscriber; }
        }

        /// <summary>
        /// 获取界面是否来自对象池。
        /// </summary>
        protected bool IsFromPool { get; set; }

        /// <summary>
        /// 获取界面是否已被销毁。
        /// </summary>
        protected bool IsDisposed { get; set; }

        /// <summary>
        /// 获取界面序列编号。
        /// </summary>
        public int SerialId
        {
            get { return m_SerialId; }
        }

        /// <summary>
        /// 获取界面完整名称。
        /// </summary>
        public string FullName
        {
            get { return m_FullName; }
        }

        /// <summary>
        /// 获取或设置界面名称。
        /// </summary>
        public string Name
        {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }

        /// <summary>
        /// 获取界面是否可用。
        /// </summary>
        public bool Available
        {
            get { return m_Available; }
        }

        /// <summary>
        /// 是否启用显示动画
        /// </summary>
        public bool EnableShowAnimation
        {
            get { return m_EnableShowAnimation; }
            set { m_EnableShowAnimation = value; }
        }

        /// <summary>
        /// 显示动画名称
        /// </summary>
        public string ShowAnimationName
        {
            get { return m_ShowAnimationName; }
            set { m_ShowAnimationName = value; }
        }

        /// <summary>
        /// 是否启用隐藏动画
        /// </summary>
        public bool EnableHideAnimation
        {
            get { return m_EnableHideAnimation; }
            set { m_EnableHideAnimation = value; }
        }

        /// <summary>
        /// 隐藏动画名称
        /// </summary>
        public string HideAnimationName
        {
            get { return m_HideAnimationName; }
            set { m_HideAnimationName = value; }
        }

        /// <summary>
        /// 获取或设置界面是否可见。
        /// </summary>
        public virtual bool Visible
        {
            get { return m_Available && m_Visible; }
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
        public string UIFormAssetName
        {
            get { return m_UIFormAssetName; }
        }

        /// <summary>
        /// 是否禁用回收，禁用回收的界面不会被回收
        /// </summary>
        public bool IsDisableRecycling
        {
            get { return m_IsDisableRecycling; }
            protected set { m_IsDisableRecycling = value; }
        }

        /// <summary>
        /// 是否禁用关闭，禁用关闭的界面不会被关闭
        /// </summary>
        public bool IsDisableClosing
        {
            get { return m_IsDisableClosing; }
            protected set { m_IsDisableClosing = value; }
        }

        /// <summary>
        /// 是否开启组件居中，true:组件生成后默认父组件居中
        /// </summary>
        public bool IsCenter
        {
            get { return m_IsCenter; }
            protected set { m_IsCenter = value; }
        }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        public string AssetPath
        {
            get { return m_AssetPath; }
            protected set { m_AssetPath = value; }
        }

        /// <summary>
        /// 获取界面实例。
        /// </summary>
        public object Handle
        {
            get { return gameObject; }
        }

        /// <summary>
        /// 获取界面所属的界面组。
        /// </summary>
        public virtual IUIGroup UIGroup
        {
            get { return m_UIGroup; }
            set { m_UIGroup = value; }
        }

        /// <summary>
        /// 获取界面深度。
        /// </summary>
        public int DepthInUIGroup
        {
            get { return m_DepthInUIGroup; }
        }

        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        public bool PauseCoveredUIForm
        {
            get { return m_PauseCoveredUIForm; }
        }

        public bool IsAwake { get; private set; }

        /// <summary>
        /// 界面初始化前执行
        /// </summary>
        public virtual void OnAwake()
        {
            IsAwake = true;
        }

        /// <summary>
        /// Unity生命周期Awake方法
        /// 在此处初始化事件订阅器并订阅本地化语言变更事件
        /// </summary>
        private void Awake()
        {
            m_EventSubscriber = UIEventSubscriber.Create(this);
            m_EventSubscriber.CheckSubscribe(LocalizationLanguageChangeEventArgs.EventId, OnLocalizationLanguageChanged);
        }

        /// <summary>
        /// Unity生命周期OnEnable方法
        /// 在界面启用时触发事件订阅
        /// </summary>
        private void OnEnable()
        {
            OnEventSubscribe();
        }

        /// <summary>
        /// Unity生命周期OnDisable方法
        /// 在界面禁用时取消事件订阅,忽略本地化语言变更事件的订阅
        /// </summary>
        private void OnDisable()
        {
            OnEventUnSubscribe();
            m_EventSubscriber?.UnSubscribeAll(new List<string>() { LocalizationLanguageChangeEventArgs.EventId });
        }

        /// <summary>
        /// Unity生命周期OnDestroy方法
        /// 在界面销毁时执行清理操作
        /// </summary>
        private void OnDestroy()
        {
            Dispose();
        }

        /// <summary>
        /// 订阅事件时调用
        /// 在界面启用(OnEnable)时触发,可在此处订阅界面所需的事件
        /// 继承类通过重写此方法来注册自己需要的事件
        /// </summary>
        protected virtual void OnEventSubscribe()
        {
        }

        /// <summary>
        /// 取消订阅事件时调用
        /// 在界面禁用(OnDisable)时触发,可在此处取消订阅界面的事件
        /// 继承类通过重写此方法来取消注册自己的事件
        /// </summary>
        protected virtual void OnEventUnSubscribe()
        {
        }

        /// <summary>
        /// 界面初始化。
        /// </summary>
        protected virtual void InitView()
        {
        }

        /// <summary>
        /// 初始化界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroup">界面所处的界面组。</param>
        /// <param name="onInitAction">初始化界面前的委托。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isNewInstance">是否是新实例。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isFullScreen">是否全屏</param>
        public void Init(int serialId, string uiFormAssetName, IUIGroup uiGroup, Action<IUIForm> onInitAction, bool pauseCoveredUIForm, bool isNewInstance, object userData, bool isFullScreen = false)
        {
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
        public virtual void OnInit()
        {
        }

        private void OnLocalizationLanguageChanged(object sender, GameEventArgs e)
        {
            UpdateLocalization();
        }

        /// <summary>
        /// 界面回收。
        /// </summary>
        public virtual void OnRecycle()
        {
            m_DepthInUIGroup = 0;
            m_PauseCoveredUIForm = true;
        }

        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public virtual void OnOpen(object userData)
        {
            m_Available = true;
            Visible = true;
            m_UserData = userData;
        }


        /// <summary>
        /// 绑定事件
        /// </summary>
        public virtual void BindEvent()
        {
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public virtual void LoadData()
        {
        }

        /// <summary>
        /// 界面更新本地化。
        /// </summary>
        public virtual void UpdateLocalization()
        {
        }

        /// <summary>
        /// 界面显示。
        /// </summary>
        /// <param name="handler">界面显示处理接口</param>
        /// <param name="complete">完成回调</param>
        public abstract void Show(IUIFormShowHandler handler, Action complete);

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <param name="isShutdown">是否是关闭界面管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        public virtual void OnClose(bool isShutdown, object userData)
        {
            gameObject.SetLayerRecursively(m_OriginalLayer);
            m_Available = false;
            Visible = false;
        }


        /// <summary>
        /// 界面隐藏。
        /// </summary>
        /// <param name="handler">界面隐藏处理接口</param>
        /// <param name="complete">完成回调</param>
        public abstract void Hide(IUIFormHideHandler handler, Action complete);

        /// <summary>
        /// 界面暂停。
        /// </summary>
        public virtual void OnPause()
        {
            m_Available = false;
            Visible = false;
        }

        /// <summary>
        /// 界面暂停恢复。
        /// </summary>
        public virtual void OnResume()
        {
            m_Available = true;
            Visible = true;
        }

        /// <summary>
        /// 界面遮挡。
        /// </summary>
        public virtual void OnCover()
        {
        }

        /// <summary>
        /// 界面遮挡恢复。
        /// </summary>
        public virtual void OnReveal()
        {
        }

        /// <summary>
        /// 界面激活。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public virtual void OnRefocus(object userData)
        {
        }

        /// <summary>
        /// 界面轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <param name="uiGroupDepth">界面组深度。</param>
        /// <param name="depthInUIGroup">界面在界面组中的深度。</param>
        public void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
            m_DepthInUIGroup = depthInUIGroup;
        }

        /// <summary>
        /// 销毁界面.
        /// </summary>
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
        /// <param name="visible">界面的可见性。</param>
        protected abstract void InternalSetVisible(bool visible);

        /// <summary>
        /// 设置界面为全屏
        /// </summary>
        protected internal abstract void MakeFullScreen();
    }
}