// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Threading.Tasks;
using GameFrameX.Asset.Runtime;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面管理器。
    /// </summary>
    public partial class BaseUIManager
    {
        protected EventHandler<OpenUIFormSuccessEventArgs> m_OpenUIFormSuccessEventHandler;

        protected EventHandler<OpenUIFormFailureEventArgs> m_OpenUIFormFailureEventHandler;

        // private EventHandler<OpenUIFormUpdateEventArgs> m_OpenUIFormUpdateEventHandler;
        // private EventHandler<OpenUIFormDependencyAssetEventArgs> m_OpenUIFormDependencyAssetEventHandler;

        /// <summary>
        /// 打开界面成功事件。
        /// </summary>
        public event EventHandler<OpenUIFormSuccessEventArgs> OpenUIFormSuccess
        {
            add { m_OpenUIFormSuccessEventHandler += value; }
            remove { m_OpenUIFormSuccessEventHandler -= value; }
        }

        /// <summary>
        /// 打开界面失败事件。
        /// </summary>
        public event EventHandler<OpenUIFormFailureEventArgs> OpenUIFormFailure
        {
            add { m_OpenUIFormFailureEventHandler += value; }
            remove { m_OpenUIFormFailureEventHandler -= value; }
        }

        /*
        /// <summary>
        /// 打开界面更新事件。
        /// </summary>
        public event EventHandler<OpenUIFormUpdateEventArgs> OpenUIFormUpdate
        {
            add { m_OpenUIFormUpdateEventHandler += value; }
            remove { m_OpenUIFormUpdateEventHandler -= value; }
        }

        /// <summary>
        /// 打开界面时加载依赖资源事件。
        /// </summary>
        public event EventHandler<OpenUIFormDependencyAssetEventArgs> OpenUIFormDependencyAsset
        {
            add { m_OpenUIFormDependencyAssetEventHandler += value; }
            remove { m_OpenUIFormDependencyAssetEventHandler -= value; }
        }*/

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormType">界面逻辑类型。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <returns>界面的序列编号。</returns>
        public async Task<IUIForm> OpenUIFormAsync(string uiFormAssetPath, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen = false)
        {
            GameFrameworkGuard.NotNull(m_AssetManager, nameof(m_AssetManager));
            GameFrameworkGuard.NotNull(m_UIFormHelper, nameof(m_UIFormHelper));
            GameFrameworkGuard.NotNull(uiFormType, nameof(uiFormType));

            return await InnerOpenUIFormAsync(uiFormAssetPath, uiFormType, pauseCoveredUIForm, userData, isFullScreen);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="isMultiple">是否创建新界面</param>
        /// <returns>界面的序列编号。</returns>
        public Task<IUIForm> OpenUIFormAsync<T>(string uiFormAssetPath, bool pauseCoveredUIForm, object userData, bool isFullScreen = false, bool isMultiple = false) where T : class, IUIForm
        {
            return InnerOpenUIFormAsync(uiFormAssetPath, typeof(T), pauseCoveredUIForm, userData, isFullScreen, isMultiple);
        }

        public async Task<IUIForm> OpenUIFormAsync(string uiFormAssetPath, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen = false, bool isMultiple = false)
        {
            return await InnerOpenUIFormAsync(uiFormAssetPath, uiFormType, pauseCoveredUIForm, userData, isFullScreen, isMultiple);
        }

        protected abstract Task<IUIForm> InnerOpenUIFormAsync(string uiFormAssetPath, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen = false, bool isMultiple = false);


        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        public void RefocusUIForm(IUIForm uiForm)
        {
            RefocusUIForm(uiForm, null);
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void RefocusUIForm(IUIForm uiForm, object userData)
        {
            GameFrameworkGuard.NotNull(uiForm, nameof(uiForm));
            GameFrameworkGuard.NotNull(uiForm.UIGroup, nameof(uiForm.UIGroup));
            UIGroup uiGroup = (UIGroup)uiForm.UIGroup;
            uiGroup.RefocusUIForm(uiForm, userData);
            uiGroup.Refresh();
            uiForm.OnRefocus(userData);
        }

        /// <summary>
        /// 设置界面实例是否被加锁。
        /// </summary>
        /// <param name="uiFormInstance">要设置是否被加锁的界面实例。</param>
        /// <param name="locked">界面实例是否被加锁。</param>
        public void SetUIFormInstanceLocked(object uiFormInstance, bool locked)
        {
            GameFrameworkGuard.NotNull(uiFormInstance, nameof(uiFormInstance));

            m_InstancePool.SetLocked(uiFormInstance, locked);
        }

        /// <summary>
        /// 设置界面实例的优先级。
        /// </summary>
        /// <param name="uiFormInstance">要设置优先级的界面实例。</param>
        /// <param name="priority">界面实例优先级。</param>
        public void SetUIFormInstancePriority(object uiFormInstance, int priority)
        {
            GameFrameworkGuard.NotNull(uiFormInstance, nameof(uiFormInstance));

            m_InstancePool.SetPriority(uiFormInstance, priority);
        }
    }
}