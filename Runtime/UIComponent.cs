﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/UI")]
    [UnityEngine.Scripting.Preserve]
    public partial class UIComponent : GameFrameworkComponent
    {
        private const int DefaultPriority = 0;

        private IUIManager m_UIManager = null;
        private EventComponent m_EventComponent = null;

        private readonly List<IUIForm> m_InternalUIFormResults = new List<IUIForm>();

        [SerializeField] private bool m_EnableOpenUIFormSuccessEvent = true;

        [SerializeField] private bool m_EnableOpenUIFormFailureEvent = true;

        // [SerializeField] private bool m_EnableOpenUIFormUpdateEvent = false;
        //
        // [SerializeField] private bool m_EnableOpenUIFormDependencyAssetEvent = false;

        [SerializeField] private bool m_EnableCloseUIFormCompleteEvent = true;

        [SerializeField] private float m_InstanceAutoReleaseInterval = 60f;

        [SerializeField] private int m_InstanceCapacity = 16;

        [SerializeField] private float m_InstanceExpireTime = 60f;

        // [SerializeField] private int m_InstancePriority = 0;

        [SerializeField] private Transform m_InstanceRoot = null;

        [SerializeField] private string m_UIFormHelperTypeName = "GameFrameX.UI.Runtime.DefaultUIFormHelper";

        [SerializeField] private UIFormHelperBase m_CustomUIFormHelper = null;

        [SerializeField] private string m_UIGroupHelperTypeName = "GameFrameX.UI.Runtime.DefaultUIGroupHelper";

        [SerializeField] private UIGroupHelperBase m_CustomUIGroupHelper = null;

        [SerializeField] private UIGroup[] m_UIGroups = new UIGroup[]
        {
            new UIGroup(UIGroupConstants.Hidden.Depth, UIGroupConstants.Hidden.Name),
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
        /// 获取界面组数量。
        /// </summary>
        public int UIGroupCount
        {
            get { return m_UIManager.UIGroupCount; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public float InstanceAutoReleaseInterval
        {
            get { return m_UIManager.InstanceAutoReleaseInterval; }
            set { m_UIManager.InstanceAutoReleaseInterval = m_InstanceAutoReleaseInterval = value; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池的容量。
        /// </summary>
        public int InstanceCapacity
        {
            get { return m_UIManager.InstanceCapacity; }
            set { m_UIManager.InstanceCapacity = m_InstanceCapacity = value; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池对象过期秒数。
        /// </summary>
        public float InstanceExpireTime
        {
            get { return m_UIManager.InstanceExpireTime; }
            set { m_UIManager.InstanceExpireTime = m_InstanceExpireTime = value; }
        }

        /*
        /// <summary>
        /// 获取或设置界面实例对象池的优先级。
        /// </summary>
        public int InstancePriority
        {
            get { return m_UIManager.InstancePriority; }
            set { m_UIManager.InstancePriority = m_InstancePriority = value; }
        }*/

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected override void Awake()
        {
            ImplementationComponentType = Utility.Assembly.GetType(componentType);
            InterfaceComponentType = typeof(IUIManager);
            base.Awake();

            m_UIManager = GameFrameworkEntry.GetModule<IUIManager>();
            if (m_UIManager == null)
            {
                Log.Fatal("UI manager is invalid.");
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

            if (m_InstanceRoot == null)
            {
                m_InstanceRoot = new GameObject("UI Form Instances").transform;
                m_InstanceRoot.SetParent(gameObject.transform);
                m_InstanceRoot.localScale = Vector3.one;
            }

            m_InstanceRoot.gameObject.layer = LayerMask.NameToLayer("UI");

            for (int i = 0; i < m_UIGroups.Length; i++)
            {
                if (!AddUIGroup(m_UIGroups[i].Name, m_UIGroups[i].Depth))
                {
                    Log.Warning("Add UI group '{0}' failure.", m_UIGroups[i].Name);
                    continue;
                }
            }
        }

        /// <summary>
        /// 是否存在界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>是否存在界面组。</returns>
        public bool HasUIGroup(string uiGroupName)
        {
            return m_UIManager.HasUIGroup(uiGroupName);
        }

        /// <summary>
        /// 获取界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>要获取的界面组。</returns>
        public IUIGroup GetUIGroup(string uiGroupName)
        {
            return m_UIManager.GetUIGroup(uiGroupName);
        }

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <returns>所有界面组。</returns>
        public IUIGroup[] GetAllUIGroups()
        {
            return m_UIManager.GetAllUIGroups();
        }

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <param name="results">所有界面组。</param>
        public void GetAllUIGroups(List<IUIGroup> results)
        {
            m_UIManager.GetAllUIGroups(results);
        }

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>是否增加界面组成功。</returns>
        public bool AddUIGroup(string uiGroupName)
        {
            return AddUIGroup(uiGroupName, 0);
        }

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="depth">界面组深度。</param>
        /// <returns>是否增加界面组成功。</returns>
        public bool AddUIGroup(string uiGroupName, int depth)
        {
            if (m_UIManager.HasUIGroup(uiGroupName))
            {
                return false;
            }

            UIGroupHelperBase uiGroupHelper = (UIGroupHelperBase)m_CustomUIGroupHelper.Handler(m_InstanceRoot, uiGroupName, m_UIGroupHelperTypeName, m_CustomUIGroupHelper);

            if (uiGroupHelper == null)
            {
                Log.Error("Can not create UI group helper.");
                return false;
            }
            // UIGroupHelperBase uiGroupHelper = Helper.CreateHelper(m_UIGroupHelperTypeName, m_CustomUIGroupHelper);
            // if (uiGroupHelper == null)
            // {
            //     Log.Error("Can not create UI group helper.");
            //     return false;
            // }

            /*
            uiGroupHelper.name = Utility.Text.Format("UI Group - {0}", uiGroupName);
            uiGroupHelper.gameObject.layer = LayerMask.NameToLayer("UI");
            Transform transform = uiGroupHelper.transform;
            transform.SetParent(m_InstanceRoot);
            transform.localScale = Vector3.one;*/

            return m_UIManager.AddUIGroup(uiGroupName, depth, uiGroupHelper);
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>是否存在界面。</returns>
        public bool HasUIForm(int serialId)
        {
            return m_UIManager.HasUIForm(serialId);
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>是否存在界面。</returns>
        public bool HasUIForm(string uiFormAssetName)
        {
            return m_UIManager.HasUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>要获取的界面。</returns>
        public UIForm GetUIForm(int serialId)
        {
            return (UIForm)m_UIManager.GetUIForm(serialId);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public UIForm GetUIForm(string uiFormAssetName)
        {
            return (UIForm)m_UIManager.GetUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public UIForm[] GetUIForms(string uiFormAssetName)
        {
            IUIForm[] uiForms = m_UIManager.GetUIForms(uiFormAssetName);
            UIForm[] uiFormImpls = new UIForm[uiForms.Length];
            for (int i = 0; i < uiForms.Length; i++)
            {
                uiFormImpls[i] = (UIForm)uiForms[i];
            }

            return uiFormImpls;
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="results">要获取的界面。</param>
        public void GetUIForms(string uiFormAssetName, List<UIForm> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
                return;
            }

            results.Clear();
            m_UIManager.GetUIForms(uiFormAssetName, m_InternalUIFormResults);
            foreach (IUIForm uiForm in m_InternalUIFormResults)
            {
                results.Add((UIForm)uiForm);
            }
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <returns>所有已加载的界面。</returns>
        public UIForm[] GetAllLoadedUIForms()
        {
            IUIForm[] uiForms = m_UIManager.GetAllLoadedUIForms();
            UIForm[] uiFormImpls = new UIForm[uiForms.Length];
            for (int i = 0; i < uiForms.Length; i++)
            {
                uiFormImpls[i] = (UIForm)uiForms[i];
            }

            return uiFormImpls;
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <param name="results">所有已加载的界面。</param>
        public void GetAllLoadedUIForms(List<UIForm> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
                return;
            }

            results.Clear();
            m_UIManager.GetAllLoadedUIForms(m_InternalUIFormResults);
            foreach (IUIForm uiForm in m_InternalUIFormResults)
            {
                results.Add((UIForm)uiForm);
            }
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <returns>所有正在加载界面的序列编号。</returns>
        public int[] GetAllLoadingUIFormSerialIds()
        {
            return m_UIManager.GetAllLoadingUIFormSerialIds();
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <param name="results">所有正在加载界面的序列编号。</param>
        public void GetAllLoadingUIFormSerialIds(List<int> results)
        {
            m_UIManager.GetAllLoadingUIFormSerialIds(results);
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>是否正在加载界面。</returns>
        public bool IsLoadingUIForm(int serialId)
        {
            return m_UIManager.IsLoadingUIForm(serialId);
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>是否正在加载界面。</returns>
        public bool IsLoadingUIForm(string uiFormAssetName)
        {
            return m_UIManager.IsLoadingUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 是否是合法的界面。
        /// </summary>
        /// <param name="uiForm">界面。</param>
        /// <returns>界面是否合法。</returns>
        public bool IsValidUIForm(UIForm uiForm)
        {
            return m_UIManager.IsValidUIForm(uiForm);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="isFullScreen"></param>
        /// <returns>界面的序列编号。</returns>
        public Task<IUIForm> OpenUIFormAsync<T>(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, bool isFullScreen = false) where T : UIFormLogic
        {
            return OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), false, null, isFullScreen);
        }

        /// <summary>
        /// 打开全屏界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiGroupName"></param>
        /// <param name="userData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> OpenFullScreenUIFormAsync<T>(string uiFormAssetPath, string uiGroupName, object userData = null) where T : UIFormLogic
        {
            return await OpenUIFormAsync<T>(uiFormAssetPath, uiGroupName, userData, true);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiGroupName"></param>
        /// <param name="userData"></param>
        /// <param name="isFullScreen"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> OpenUIFormAsync<T>(string uiFormAssetPath, string uiGroupName, object userData = null, bool isFullScreen = false) where T : UIFormLogic
        {
            string uiFormAssetName = typeof(T).Name;
            var ui = await OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), false, userData, isFullScreen);
            if (ui != null)
            {
                var uiFormLogic = (ui.Handle as GameObject)?.GetComponent<T>();
                return uiFormLogic;
            }

            return null;
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <returns>界面的序列编号。</returns>
        public Task<IUIForm> OpenUIFormAsync<T>(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, bool isFullScreen = false) where T : UIFormLogic
        {
            return OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), pauseCoveredUIForm, null, isFullScreen);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <returns>界面的序列编号。</returns>
        public Task<IUIForm> OpenUIFormAsync<T>(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, object userData, bool isFullScreen = false) where T : UIFormLogic
        {
            return OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), false, userData, isFullScreen);
        }


        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="uiFormType">界面逻辑类型。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public async Task<IUIForm> OpenUIFormAsync(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen = false)
        {
            return await m_UIManager.OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, uiFormType, pauseCoveredUIForm, userData, isFullScreen);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        public void CloseUIForm(int serialId)
        {
            m_UIManager.CloseUIForm(serialId);
        }


        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUIForm(int serialId, object userData)
        {
            m_UIManager.CloseUIForm(serialId, userData);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        public void CloseUIForm(UIForm uiForm)
        {
            m_UIManager.CloseUIForm(uiForm);
        }

        /// <summary>
        /// 关闭界面。
        /// 该函数只适用于界面只有一个的情况.因为当找到一个目标对象之后就会立即终止
        /// </summary>
        /// <typeparam name="T">关闭界面的类型</typeparam>
        public void CloseUIForm<T>(object userData = null) where T : IUIForm
        {
            m_UIManager.CloseUIForm<T>(userData);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUIForm(UIForm uiForm, object userData)
        {
            m_UIManager.CloseUIForm(uiForm, userData);
        }

        /// <summary>
        /// 立即关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        public void CloseUIFormNow(int serialId)
        {
            m_UIManager.CloseUIFormNow(serialId);
        }

        /// <summary>
        /// 立即关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUIFormNow(int serialId, object userData)
        {
            m_UIManager.CloseUIFormNow(serialId, userData);
        }

        /// <summary>
        /// 立即关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        public void CloseUIFormNow(UIForm uiForm)
        {
            m_UIManager.CloseUIFormNow(uiForm);
        }

        /// <summary>
        /// 立即关闭界面。
        /// 该函数只适用于界面只有一个的情况.因为当找到一个目标对象之后就会立即终止
        /// </summary>
        /// <typeparam name="T">关闭界面的类型</typeparam>
        public void CloseUIFormNow<T>(object userData = null) where T : IUIForm
        {
            m_UIManager.CloseUIFormNow<T>(userData);
        }

        /// <summary>
        /// 立即关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseUIFormNow(UIForm uiForm, object userData)
        {
            m_UIManager.CloseUIFormNow(uiForm, userData);
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        public void CloseAllLoadedUIForms()
        {
            m_UIManager.CloseAllLoadedUIForms();
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseAllLoadedUIForms(object userData)
        {
            m_UIManager.CloseAllLoadedUIForms(userData);
        }

        /// <summary>
        /// 关闭所有正在加载的界面。
        /// </summary>
        public void CloseAllLoadingUIForms()
        {
            m_UIManager.CloseAllLoadingUIForms();
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        public void RefocusUIForm(UIForm uiForm)
        {
            m_UIManager.RefocusUIForm(uiForm);
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void RefocusUIForm(UIForm uiForm, object userData)
        {
            m_UIManager.RefocusUIForm(uiForm, userData);
        }

        /// <summary>
        /// 设置界面是否被加锁。
        /// </summary>
        /// <param name="uiForm">要设置是否被加锁的界面。</param>
        /// <param name="locked">界面是否被加锁。</param>
        public void SetUIFormInstanceLocked(UIForm uiForm, bool locked)
        {
            if (uiForm == null)
            {
                Log.Warning("UI form is invalid.");
                return;
            }

            m_UIManager.SetUIFormInstanceLocked(uiForm.gameObject, locked);
        }

        private void OnOpenUIFormSuccess(object sender, OpenUIFormSuccessEventArgs e)
        {
            m_EventComponent.Fire(this, e);
        }

        private void OnOpenUIFormFailure(object sender, OpenUIFormFailureEventArgs e)
        {
            Log.Warning("Open UI form failure, asset name '{0}', UI group name '{1}', pause covered UI form '{2}', error message '{3}'.", e.UIFormAssetName, e.UIGroupName, e.PauseCoveredUIForm, e.ErrorMessage);
            if (m_EnableOpenUIFormFailureEvent)
            {
                m_EventComponent.Fire(this, e);
            }
        }

        /*
        private void OnOpenUIFormUpdate(object sender, OpenUIFormUpdateEventArgs e)
        {
            m_EventComponent.Fire(this, e);
        }

        private void OnOpenUIFormDependencyAsset(object sender, OpenUIFormDependencyAssetEventArgs e)
        {
            m_EventComponent.Fire(this, e);
        }*/

        private void OnCloseUIFormComplete(object sender, CloseUIFormCompleteEventArgs e)
        {
            m_EventComponent.Fire(this, e);
        }
    }
}