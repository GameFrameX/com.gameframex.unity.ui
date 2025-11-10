// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

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
        protected readonly Dictionary<int, string> m_UIFormsBeingLoaded = new Dictionary<int, string>();

        /// <summary>
        /// 需要释放的界面实例对象池。
        /// </summary>
        protected readonly Dictionary<int, IUIForm> m_UIFormsToReleaseOnLoad = new Dictionary<int, IUIForm>();

        /// <summary>
        /// 界面实例对象池回收间隔秒数。
        /// </summary>
        protected int m_RecycleInterval = 60;

        /// <summary>
        /// 界面实例对象池回收时间。
        /// </summary>
        protected float m_RecycleTime = 0;

        protected int m_Serial;

        /// <summary>
        /// 资源管理器。
        /// </summary>
        protected IAssetManager m_AssetManager;

        /// <summary>
        /// 界面辅助器。
        /// </summary>
        protected IUIFormHelper m_UIFormHelper;

        /// <summary>
        /// 对象池管理器。
        /// </summary>
        protected IObjectPoolManager m_ObjectPoolManager;

        /// <summary>
        /// 获取或设置界面实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        public float InstanceAutoReleaseInterval
        {
            get { return m_InstancePool.AutoReleaseInterval; }
            set { m_InstancePool.AutoReleaseInterval = value; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池的回收间隔秒数。
        /// </summary>
        public int RecycleInterval
        {
            get { return m_RecycleInterval; }
            set { m_RecycleInterval = value; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池的容量。
        /// </summary>
        public int InstanceCapacity
        {
            get { return m_InstancePool.Capacity; }
            set { m_InstancePool.Capacity = value; }
        }

        /// <summary>
        /// 获取或设置界面实例对象池对象过期秒数。
        /// </summary>
        public float InstanceExpireTime
        {
            get { return m_InstancePool.ExpireTime; }
            set { m_InstancePool.ExpireTime = value; }
        }

        protected IObjectPool<UIFormInstanceObject> m_InstancePool = null;
        protected bool m_IsShutdown = false;

        /// <summary>
        /// 界面管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected override void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_RecycleTime += elapseSeconds;
            if (m_RecycleTime >= m_RecycleInterval)
            {
                m_RecycleTime = 0;
                if (m_UIFormsToReleaseOnLoad.Count > 0)
                {
                    foreach (var keyValuePair in m_UIFormsToReleaseOnLoad)
                    {
                        var uiForm = keyValuePair.Value;
                        if (uiForm != null)
                        {
                            RecycleUIForm(uiForm, true);
                        }
                    }

                    m_UIFormsToReleaseOnLoad.Clear();
                }
            }

            foreach (KeyValuePair<string, UIGroup> uiGroup in m_UIGroups)
            {
                uiGroup.Value.Update(elapseSeconds, realElapseSeconds);
            }
        }

        /// <summary>
        /// 关闭并清理界面管理器。
        /// </summary>
        protected override void Shutdown()
        {
            m_IsShutdown = true;
            CloseAllLoadedUIForms();
            m_UIGroups.Clear();
            m_UIFormsBeingLoaded.Clear();
            m_UIFormsToReleaseOnLoad.Clear();
        }

        /// <summary>
        /// 设置对象池管理器。
        /// </summary>
        /// <param name="objectPoolManager">对象池管理器。</param>
        public void SetObjectPoolManager(IObjectPoolManager objectPoolManager)
        {
            GameFrameworkGuard.NotNull(objectPoolManager, nameof(objectPoolManager));

            m_ObjectPoolManager = objectPoolManager;
            m_InstancePool = m_ObjectPoolManager.CreateMultiSpawnObjectPool<UIFormInstanceObject>("UI Instance Pool");
        }

        /// <summary>
        /// 设置资源管理器。
        /// </summary>
        /// <param name="assetManager">资源管理器。</param>
        public virtual void SetResourceManager(IAssetManager assetManager)
        {
            GameFrameworkGuard.NotNull(assetManager, nameof(assetManager));

            m_AssetManager = assetManager;
        }


        /// <summary>
        /// 设置界面辅助器。
        /// </summary>
        /// <param name="uiFormHelper">界面辅助器。</param>
        public void SetUIFormHelper(IUIFormHelper uiFormHelper)
        {
            GameFrameworkGuard.NotNull(uiFormHelper, nameof(uiFormHelper));

            m_UIFormHelper = uiFormHelper;
        }
    }
}