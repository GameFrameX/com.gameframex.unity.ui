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

using System;
using System.Collections.Generic;
using GameFrameX.Asset.Runtime;
using GameFrameX.ObjectPool;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public abstract partial class BaseUIManager : GameFrameworkModule, IUIManager
    {
        /// <summary>
        /// 当前加载的界面实例对象池。
        /// </summary>
        /// <remarks>
        /// Dictionary of currently loading UI form instances.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected readonly Dictionary<int, string> m_UIFormsBeingLoaded = new Dictionary<int, string>();

        /// <summary>
        /// 需要释放的界面实例对象池。
        /// </summary>
        /// <remarks>
        /// HashSet of UI form instances to be released on load.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected readonly HashSet<int> m_UIFormsToReleaseOnLoad = new HashSet<int>();

        /// <summary>
        /// 待释放的界面实例队列。
        /// </summary>
        /// <remarks>
        /// Queue of UI form instances waiting to be recycled.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        private Queue<IUIForm> m_RecycleQueue = new Queue<IUIForm>();

        /// <summary>
        /// 界面实例对象池回收间隔秒数。
        /// </summary>
        /// <remarks>
        /// UI form instance pool recycle interval in seconds.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected int m_RecycleInterval = 60;

        /// <summary>
        /// 界面实例对象池回收时间。
        /// </summary>
        /// <remarks>
        /// UI form instance pool recycle time.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected float m_RecycleTime = 0;

        [UnityEngine.Scripting.Preserve]
        protected int m_Serial;

        /// <summary>
        /// 资源管理器。
        /// </summary>
        /// <remarks>
        /// The asset manager.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected IAssetManager m_AssetManager;

        /// <summary>
        /// 界面辅助器。
        /// </summary>
        /// <remarks>
        /// The UI form helper.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected IUIFormHelper m_UIFormHelper;

        /// <summary>
        /// 对象池管理器。
        /// </summary>
        /// <remarks>
        /// The object pool manager.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected IObjectPoolManager m_ObjectPoolManager;

        /// <summary>
        /// 获取或设置界面实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the auto release interval in seconds for the UI form instance pool.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public float InstanceAutoReleaseInterval
        {
            get { return m_InstancePool.AutoReleaseInterval; }
            set { m_InstancePool.AutoReleaseInterval = value; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池的回收间隔秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the recycle interval in seconds for the UI form instance pool.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int RecycleInterval
        {
            get { return m_RecycleInterval; }
            set { m_RecycleInterval = value; }
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
            get { return m_InstancePool.Capacity; }
            set { m_InstancePool.Capacity = value; }
        }

        [UnityEngine.Scripting.Preserve]
        private bool m_IsEnableUIHideAnimation = false;

        /// <summary>
        /// 获取或设置是否启用界面隐藏动画。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether to enable UI form hide animation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsEnableUIHideAnimation
        {
            get { return m_IsEnableUIHideAnimation; }
            set { m_IsEnableUIHideAnimation = value; }
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
            get { return m_InstancePool.ExpireTime; }
            set { m_InstancePool.ExpireTime = value; }
        }


        /// <summary>
        /// 获取或设置是否启用界面显示动画。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether to enable UI form show animation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        private bool m_IsEnableUIShowAnimation = false;

        /// <summary>
        /// 获取或设置是否启用界面显示动画。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether to enable UI form show animation.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsEnableUIShowAnimation
        {
            get { return m_IsEnableUIShowAnimation; }
            set { m_IsEnableUIShowAnimation = value; }
        }

        [UnityEngine.Scripting.Preserve]
        private bool m_EnableUIFormSingleton = true;

        /// <summary>
        /// 获取或设置是否启用界面单实例打开模式。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether singleton mode is enabled when opening UI forms.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool EnableUIFormSingleton
        {
            get { return m_EnableUIFormSingleton; }
            set { m_EnableUIFormSingleton = value; }
        }

        /// <summary>
        /// 是否对指定界面类型采用单实例打开模式。
        /// </summary>
        /// <remarks>
        /// Determines whether singleton open mode should be used for the specified UI form type.
        /// </remarks>
        /// <param name="uiFormType">界面类型。/ The UI form type.</param>
        /// <returns>是否采用单实例模式。/ Whether singleton mode should be used.</returns>
        [UnityEngine.Scripting.Preserve]
        protected bool UseSingletonOpenMode(Type uiFormType)
        {
            if (!EnableUIFormSingleton || uiFormType == null)
            {
                return false;
            }

            var allowMultiAttr = Attribute.GetCustomAttribute(uiFormType, typeof(OptionUIAllowMultiInstanceAttribute)) as OptionUIAllowMultiInstanceAttribute;
            return allowMultiAttr == null || !allowMultiAttr.Enable;
        }

        [UnityEngine.Scripting.Preserve]
        protected IObjectPool<UIFormInstanceObject> m_InstancePool = null;
        [UnityEngine.Scripting.Preserve]
        protected bool m_IsShutdown = false;
        [UnityEngine.Scripting.Preserve]
        protected IUIFormShowHandler m_UIFormShowHandler;
        [UnityEngine.Scripting.Preserve]
        private IUIFormHideHandler m_UIFormHideHandler;

        /// <summary>
        /// 界面管理器轮询。
        /// </summary>
        /// <remarks>
        /// Updates the UI manager.
        /// </remarks>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。 / The logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。 / The real elapsed time in seconds.</param>
        [UnityEngine.Scripting.Preserve]
        protected override void Update(float elapseSeconds, float realElapseSeconds)
        {
            while (m_RecycleQueue.Count > 0)
            {
                var uiForm = m_RecycleQueue.Dequeue();
                RecycleUIForm(uiForm);
            }

            foreach (var uiGroup in m_UIGroups)
            {
                uiGroup.Value.Update(elapseSeconds, realElapseSeconds);
            }
        }

        /// <summary>
        /// 关闭并清理界面管理器。
        /// </summary>
        /// <remarks>
        /// Shuts down and cleans up the UI manager.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        protected override void Shutdown()
        {
            m_IsShutdown = true;
            CloseAllLoadedUIForms();
            m_UIGroups.Clear();
            m_UIFormsBeingLoaded.Clear();
            m_UIFormsToReleaseOnLoad.Clear();
            m_RecycleQueue.Clear();
        }

        /// <summary>
        /// 设置对象池管理器。
        /// </summary>
        /// <remarks>
        /// Sets the object pool manager.
        /// </remarks>
        /// <param name="objectPoolManager">对象池管理器。 / The object pool manager.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetObjectPoolManager(IObjectPoolManager objectPoolManager)
        {
            GameFrameworkGuard.NotNull(objectPoolManager, nameof(objectPoolManager));

            m_ObjectPoolManager = objectPoolManager;
            m_InstancePool = m_ObjectPoolManager.CreateMultiSpawnObjectPool<UIFormInstanceObject>("UI Instance Pool");
        }

        /// <summary>
        /// 设置资源管理器。
        /// </summary>
        /// <remarks>
        /// Sets the asset manager.
        /// </remarks>
        /// <param name="assetManager">资源管理器。 / The asset manager.</param>
        [UnityEngine.Scripting.Preserve]
        public virtual void SetResourceManager(IAssetManager assetManager)
        {
            GameFrameworkGuard.NotNull(assetManager, nameof(assetManager));

            m_AssetManager = assetManager;
        }

        /// <summary>
        /// 设置界面辅助器。
        /// </summary>
        /// <remarks>
        /// Sets the UI form helper.
        /// </remarks>
        /// <param name="uiFormHelper">界面辅助器。 / The UI form helper.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetUIFormHelper(IUIFormHelper uiFormHelper)
        {
            GameFrameworkGuard.NotNull(uiFormHelper, nameof(uiFormHelper));

            m_UIFormHelper = uiFormHelper;
        }

        /// <summary>
        /// 设置界面显示处理接口。
        /// </summary>
        /// <remarks>
        /// Sets the UI form show handler.
        /// </remarks>
        /// <param name="handler">界面显示处理接口。 / The UI form show handler.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetUIFormShowHandler(IUIFormShowHandler handler)
        {
            m_UIFormShowHandler = handler;
        }

        /// <summary>
        /// 设置界面隐藏处理接口。
        /// </summary>
        /// <remarks>
        /// Sets the UI form hide handler.
        /// </remarks>
        /// <param name="handler">界面隐藏处理接口。 / The UI form hide handler.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetUIFormHideHandler(IUIFormHideHandler handler)
        {
            m_UIFormHideHandler = handler;
        }
    }
}
