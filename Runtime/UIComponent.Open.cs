// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Threading.Tasks;
using System;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    public partial class UIComponent
    {
        /// <summary>
        /// 异步打开全屏UI。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <param name="isMultiple">是否创建新界面</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenFullScreenAsync<T>(string uiFormAssetPath, object userData = null, bool isMultiple = false) where T : class, IUIForm
        {
            return await OpenUIFormAsync<T>(uiFormAssetPath, true, userData, true, isMultiple);
        }

        /// <summary>
        /// 异步打开全屏UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <param name="isMultiple">是否创建新界面</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenFullScreenAsync<T>(object userData = null, bool isMultiple = false) where T : class, IUIForm
        {
            var uiFormAssetName = typeof(T).Name;
            var uiFormAssetPath = Utility.Asset.Path.GetUIPath(uiFormAssetName);
            return await OpenFullScreenAsync<T>(uiFormAssetPath, userData, isMultiple);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormType">界面逻辑类型。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isMultiple">是否创建新界面</param>
        /// <returns>界面的序列编号。</returns>
        public async Task<IUIForm> OpenUIAsync(string uiFormAssetPath, Type uiFormType, bool pauseCoveredUIForm, object userData = null, bool isFullScreen = false, bool isMultiple = false)
        {
            return await m_UIManager.OpenUIFormAsync(uiFormAssetPath, uiFormType, pauseCoveredUIForm, userData, isFullScreen, isMultiple);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isMultiple">是否创建新界面</param>
        /// <returns>界面的序列编号。</returns>
        private async Task<T> OpenUIFormAsync<T>(string uiFormAssetPath, bool pauseCoveredUIForm, object userData = null, bool isFullScreen = false, bool isMultiple = false) where T : class, IUIForm
        {
            var ui = await m_UIManager.OpenUIFormAsync<T>(uiFormAssetPath, pauseCoveredUIForm, userData, isFullScreen, isMultiple);
            return ui as T;
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isMultiple">是否创建新界面</param>
        /// <returns>界面的序列编号。</returns>
        public async Task<T> OpenUIFormAsync<T>(bool pauseCoveredUIForm, object userData = null, bool isFullScreen = false, bool isMultiple = false) where T : class, IUIForm
        {
            var uiFormAssetName = typeof(T).Name;
            var uiFormAssetPath = Utility.Asset.Path.GetUIPath(uiFormAssetName);
            return await OpenUIFormAsync<T>(uiFormAssetPath, pauseCoveredUIForm, userData, isFullScreen, isMultiple);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isMultiple">是否创建新界面</param>
        /// <returns>界面的序列编号。</returns>
        public async Task<T> OpenAsync<T>(string uiFormAssetPath, object userData = null, bool isFullScreen = false, bool isMultiple = false) where T : class, IUIForm
        {
            return await OpenUIFormAsync<T>(uiFormAssetPath, false, userData, isFullScreen, isMultiple);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="pauseCoveredUIForm">是否暂停覆盖的UI</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isMultiple">是否创建新界面</param>
        /// <returns>界面的序列编号。</returns>
        public async Task<T> OpenAsync<T>(string uiFormAssetPath, bool pauseCoveredUIForm, object userData = null, bool isFullScreen = false, bool isMultiple = false) where T : class, IUIForm
        {
            return await OpenUIFormAsync<T>(uiFormAssetPath, pauseCoveredUIForm, userData, isFullScreen, isMultiple);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isMultiple">是否创建新界面</param>
        /// <returns>界面的序列编号。</returns>
        public async Task<T> OpenAsync<T>(object userData = null, bool isFullScreen = false, bool isMultiple = false) where T : class, IUIForm
        {
            var uiFormAssetName = typeof(T).Name;
            var uiFormAssetPath = Utility.Asset.Path.GetUIPath(uiFormAssetName);
            return await OpenAsync<T>(uiFormAssetPath, userData, isFullScreen, isMultiple);
        }
    }
}