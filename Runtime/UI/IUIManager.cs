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
using System.Threading.Tasks;
using GameFrameX.Asset.Runtime;
using GameFrameX.ObjectPool;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面管理器接口。
    /// </summary>
    /// <remarks>
    /// UI manager interface.
    /// </remarks>
    public interface IUIManager
    {
        /// <summary>
        /// 获取界面组数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of UI groups.
        /// </remarks>
        int UIGroupCount { get; }

        /// <summary>
        /// 获取或设置界面实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the interval in seconds for the UI instance object pool to automatically release releasable objects.
        /// </remarks>
        float InstanceAutoReleaseInterval { get; set; }

        /// <summary>
        /// 获取或设置界面实例对象池的容量。
        /// </summary>
        /// <remarks>
        /// Gets or sets the capacity of the UI instance object pool.
        /// </remarks>
        int InstanceCapacity { get; set; }

        /// <summary>
        /// 获取或设置是否启用界面显示动画。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether to enable UI show animation.
        /// </remarks>
        bool IsEnableUIShowAnimation { get; set; }

        /// <summary>
        /// 获取或设置是否启用界面隐藏动画。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether to enable UI hide animation.
        /// </remarks>
        bool IsEnableUIHideAnimation { get; set; }

        /// <summary>
        /// 获取或设置是否启用界面单实例打开模式。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether singleton mode is enabled when opening UI forms.
        /// </remarks>
        bool EnableUIFormSingleton { get; set; }

        /// <summary>
        /// 获取或设置界面实例对象池对象过期秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the expiration time in seconds for objects in the UI instance object pool.
        /// </remarks>
        float InstanceExpireTime { get; set; }

        /// <summary>
        /// 获取或设置界面实例对象池的回收间隔秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the recycle interval in seconds for the UI instance object pool.
        /// </remarks>
        int RecycleInterval { get; set; }

        /*/// <summary>
        /// 获取或设置界面实例对象池的优先级。
        /// </summary>
        int InstancePriority { get; set; }*/

        /// <summary>
        /// 打开界面成功事件。
        /// </summary>
        /// <remarks>
        /// Event triggered when a UI form is opened successfully.
        /// </remarks>
        event EventHandler<OpenUIFormSuccessEventArgs> OpenUIFormSuccess;

        /// <summary>
        /// 打开界面失败事件。
        /// </summary>
        /// <remarks>
        /// Event triggered when a UI form fails to open.
        /// </remarks>
        event EventHandler<OpenUIFormFailureEventArgs> OpenUIFormFailure;

        /*
        /// <summary>
        /// 打开界面更新事件。
        /// </summary>
        event EventHandler<OpenUIFormUpdateEventArgs> OpenUIFormUpdate;

        /// <summary>
        /// 打开界面时加载依赖资源事件。
        /// </summary>
        event EventHandler<OpenUIFormDependencyAssetEventArgs> OpenUIFormDependencyAsset;*/

        /// <summary>
        /// 关闭界面完成事件。
        /// </summary>
        /// <remarks>
        /// Event triggered when a UI form is closed completely.
        /// </remarks>
        event EventHandler<CloseUIFormCompleteEventArgs> CloseUIFormComplete;

        /// <summary>
        /// 设置对象池管理器。
        /// </summary>
        /// <remarks>
        /// Sets the object pool manager.
        /// </remarks>
        /// <param name="objectPoolManager">对象池管理器。/ The object pool manager.</param>
        void SetObjectPoolManager(IObjectPoolManager objectPoolManager);

        /// <summary>
        /// 设置资源管理器。
        /// </summary>
        /// <remarks>
        /// Sets the asset manager.
        /// </remarks>
        /// <param name="assetManager">资源管理器。/ The asset manager.</param>
        void SetResourceManager(IAssetManager assetManager);

        /// <summary>
        /// 设置界面辅助器。
        /// </summary>
        /// <remarks>
        /// Sets the UI form helper.
        /// </remarks>
        /// <param name="uiFormHelper">界面辅助器。/ The UI form helper.</param>
        void SetUIFormHelper(IUIFormHelper uiFormHelper);

        /// <summary>
        /// 是否存在界面组。
        /// </summary>
        /// <remarks>
        /// Checks whether a UI group exists.
        /// </remarks>
        /// <param name="uiGroupName">界面组名称。/ The name of the UI group.</param>
        /// <returns>是否存在界面组。/ Whether the UI group exists.</returns>
        bool HasUIGroup(string uiGroupName);

        /// <summary>
        /// 获取界面组。
        /// </summary>
        /// <remarks>
        /// Gets a UI group by name.
        /// </remarks>
        /// <param name="uiGroupName">界面组名称。/ The name of the UI group.</param>
        /// <returns>要获取的界面组。/ The UI group to retrieve.</returns>
        IUIGroup GetUIGroup(string uiGroupName);

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <remarks>
        /// Gets all UI groups.
        /// </remarks>
        /// <returns>所有界面组。/ All UI groups.</returns>
        IUIGroup[] GetAllUIGroups();

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <remarks>
        /// Gets all UI groups.
        /// </remarks>
        /// <param name="results">所有界面组。/ All UI groups.</param>
        void GetAllUIGroups(List<IUIGroup> results);

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <remarks>
        /// Adds a UI group.
        /// </remarks>
        /// <param name="uiGroupName">界面组名称。/ The name of the UI group.</param>
        /// <param name="uiGroupHelper">界面组辅助器。/ The UI group helper.</param>
        /// <returns>是否增加界面组成功。/ Whether the UI group was added successfully.</returns>
        bool AddUIGroup(string uiGroupName, IUIGroupHelper uiGroupHelper);

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <remarks>
        /// Adds a UI group with specified depth.
        /// </remarks>
        /// <param name="uiGroupName">界面组名称。/ The name of the UI group.</param>
        /// <param name="uiGroupDepth">界面组深度。/ The depth of the UI group.</param>
        /// <param name="uiGroupHelper">界面组辅助器。/ The UI group helper.</param>
        /// <returns>是否增加界面组成功。/ Whether the UI group was added successfully.</returns>
        bool AddUIGroup(string uiGroupName, int uiGroupDepth, IUIGroupHelper uiGroupHelper);

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <remarks>
        /// Checks whether a UI form exists by serial ID.
        /// </remarks>
        /// <param name="serialId">界面序列编号。/ The serial ID of the UI form.</param>
        /// <returns>是否存在界面。/ Whether the UI form exists.</returns>
        bool HasUIForm(int serialId);

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <remarks>
        /// Checks whether a UI form exists by asset name.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The asset name of the UI form.</param>
        /// <returns>是否存在界面。/ Whether the UI form exists.</returns>
        bool HasUIForm(string uiFormAssetName);

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <remarks>
        /// Gets a UI form by serial ID.
        /// </remarks>
        /// <param name="serialId">界面序列编号。/ The serial ID of the UI form.</param>
        /// <returns>要获取的界面。/ The UI form to retrieve.</returns>
        IUIForm GetUIForm(int serialId);

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <remarks>
        /// Gets a UI form by asset name.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The asset name of the UI form.</param>
        /// <returns>要获取的界面。/ The UI form to retrieve.</returns>
        IUIForm GetUIForm(string uiFormAssetName);

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <remarks>
        /// Gets all UI forms by asset name.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The asset name of the UI form.</param>
        /// <returns>要获取的界面。/ The UI forms to retrieve.</returns>
        IUIForm[] GetUIForms(string uiFormAssetName);

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <remarks>
        /// Gets all UI forms by asset name.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The asset name of the UI form.</param>
        /// <param name="results">要获取的界面。/ The UI forms to retrieve.</param>
        void GetUIForms(string uiFormAssetName, List<IUIForm> results);

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <remarks>
        /// Gets all loaded UI forms.
        /// </remarks>
        /// <returns>所有已加载的界面。/ All loaded UI forms.</returns>
        IUIForm[] GetAllLoadedUIForms();

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <remarks>
        /// Gets all loaded UI forms.
        /// </remarks>
        /// <param name="results">所有已加载的界面。/ All loaded UI forms.</param>
        void GetAllLoadedUIForms(List<IUIForm> results);

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <remarks>
        /// Gets the serial IDs of all loading UI forms.
        /// </remarks>
        /// <returns>所有正在加载界面的序列编号。/ The serial IDs of all loading UI forms.</returns>
        int[] GetAllLoadingUIFormSerialIds();

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <remarks>
        /// Gets the serial IDs of all loading UI forms.
        /// </remarks>
        /// <param name="results">所有正在加载界面的序列编号。/ The serial IDs of all loading UI forms.</param>
        void GetAllLoadingUIFormSerialIds(List<int> results);

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <remarks>
        /// Checks whether a UI form is being loaded by serial ID.
        /// </remarks>
        /// <param name="serialId">界面序列编号。/ The serial ID of the UI form.</param>
        /// <returns>是否正在加载界面。/ Whether the UI form is being loaded.</returns>
        bool IsLoadingUIForm(int serialId);

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <remarks>
        /// Checks whether a UI form is being loaded by asset name.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The asset name of the UI form.</param>
        /// <returns>是否正在加载界面。/ Whether the UI form is being loaded.</returns>
        bool IsLoadingUIForm(string uiFormAssetName);

        /// <summary>
        /// 是否是合法的界面。
        /// </summary>
        /// <remarks>
        /// Checks whether a UI form is valid.
        /// </remarks>
        /// <param name="uiForm">界面。/ The UI form.</param>
        /// <returns>界面是否合法。/ Whether the UI form is valid.</returns>
        bool IsValidUIForm(IUIForm uiForm);

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <remarks>
        /// Opens a UI form asynchronously.
        /// </remarks>
        /// <param name="uiFormAssetPath">界面所在路径。/ The asset path of the UI form.</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。/ Whether to pause the covered UI form.</param>
        /// <param name="userData">用户自定义数据。/ User-defined data.</param>
        /// <param name="isFullScreen">是否全屏。/ Whether the UI form is full screen.</param>
        /// <returns>界面的序列编号。/ The UI form instance.</returns>
        Task<IUIForm> OpenUIFormAsync<T>(string uiFormAssetPath, bool pauseCoveredUIForm, object userData, bool isFullScreen = false) where T : class, IUIForm;

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <remarks>
        /// Opens a UI form asynchronously with specified type.
        /// </remarks>
        /// <param name="uiFormAssetPath">界面所在路径。/ The asset path of the UI form.</param>
        /// <param name="uiFormType">界面逻辑类型。/ The logic type of the UI form.</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。/ Whether to pause the covered UI form.</param>
        /// <param name="userData">用户自定义数据。/ User-defined data.</param>
        /// <param name="isFullScreen">是否全屏。/ Whether the UI form is full screen.</param>
        /// <returns>界面的序列编号。/ The UI form instance.</returns>
        Task<IUIForm> OpenUIFormAsync(string uiFormAssetPath, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen = false);

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <remarks>
        /// Closes a UI form by serial ID.
        /// </remarks>
        /// <param name="serialId">要关闭界面的序列编号。/ The serial ID of the UI form to close.</param>
        /// <param name="isNowRecycle">是否立即回收界面，默认是否。/ Whether to recycle the UI form immediately, default is false.</param>
        void CloseUIForm(int serialId, bool isNowRecycle = false);

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <remarks>
        /// Closes a UI form by serial ID with user data.
        /// </remarks>
        /// <param name="serialId">要关闭界面的序列编号。/ The serial ID of the UI form to close.</param>
        /// <param name="userData">用户自定义数据。/ User-defined data.</param>
        /// <param name="isNowRecycle">是否立即回收界面，默认是否。/ Whether to recycle the UI form immediately, default is false.</param>
        void CloseUIForm(int serialId, object userData, bool isNowRecycle = false);

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <remarks>
        /// Closes a UI form by instance.
        /// </remarks>
        /// <param name="uiForm">要关闭的界面。/ The UI form to close.</param>
        /// <param name="isNowRecycle">是否立即回收界面，默认是否。/ Whether to recycle the UI form immediately, default is false.</param>
        void CloseUIForm(IUIForm uiForm, bool isNowRecycle = false);

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <remarks>
        /// Closes a UI form of specified type.
        /// </remarks>
        /// <param name="userData">用户自定义数据。/ User-defined data.</param>
        /// <param name="isNowRecycle">是否立即回收界面，默认是否。/ Whether to recycle the UI form immediately, default is false.</param>
        /// <typeparam name="T">界面类型。/ The type of the UI form.</typeparam>
        void CloseUIForm<T>(object userData, bool isNowRecycle = false) where T : IUIForm;

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <remarks>
        /// Closes a UI form by instance with user data.
        /// </remarks>
        /// <param name="uiForm">要关闭的界面。/ The UI form to close.</param>
        /// <param name="userData">用户自定义数据。/ User-defined data.</param>
        /// <param name="isNowRecycle">是否立即回收界面，默认是否。/ Whether to recycle the UI form immediately, default is false.</param>
        void CloseUIForm(IUIForm uiForm, object userData = null, bool isNowRecycle = false);

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        /// <remarks>
        /// Closes all loaded UI forms.
        /// </remarks>
        /// <param name="isNowRecycle">是否立即回收界面，默认是否。/ Whether to recycle UI forms immediately, default is false.</param>
        void CloseAllLoadedUIForms(bool isNowRecycle = false);

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        /// <remarks>
        /// Closes all loaded UI forms with user data.
        /// </remarks>
        /// <param name="userData">用户自定义数据。/ User-defined data.</param>
        /// <param name="isNowRecycle">是否立即回收界面，默认是否。/ Whether to recycle UI forms immediately, default is false.</param>
        void CloseAllLoadedUIForms(object userData, bool isNowRecycle = false);

        /// <summary>
        /// 关闭所有正在加载的界面。
        /// </summary>
        /// <remarks>
        /// Closes all loading UI forms.
        /// </remarks>
        void CloseAllLoadingUIForms();

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <remarks>
        /// Refocuses a UI form.
        /// </remarks>
        /// <param name="uiForm">要激活的界面。/ The UI form to refocus.</param>
        void RefocusUIForm(IUIForm uiForm);

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <remarks>
        /// Refocuses a UI form with user data.
        /// </remarks>
        /// <param name="uiForm">要激活的界面。/ The UI form to refocus.</param>
        /// <param name="userData">用户自定义数据。/ User-defined data.</param>
        void RefocusUIForm(IUIForm uiForm, object userData);

        /// <summary>
        /// 设置界面实例是否被加锁。
        /// </summary>
        /// <remarks>
        /// Sets whether a UI form instance is locked.
        /// </remarks>
        /// <param name="uiFormInstance">要设置是否被加锁的界面实例。/ The UI form instance to set locked state.</param>
        /// <param name="locked">界面实例是否被加锁。/ Whether the UI form instance is locked.</param>
        void SetUIFormInstanceLocked(object uiFormInstance, bool locked);

        /// <summary>
        /// 关闭界面组。
        /// </summary>
        /// <remarks>
        /// Closes a UI group.
        /// </remarks>
        /// <param name="uiGroupName">界面组名称。/ The name of the UI group.</param>
        /// <param name="userData">用户自定义数据。/ User-defined data.</param>
        void CloseUIGroup(string uiGroupName, object userData);

        /// <summary>
        /// 释放所有已加载的界面。
        /// </summary>
        /// <remarks>
        /// Releases all loaded UI forms.
        /// </remarks>
        /// <param name="userData">用户自定义数据。/ User-defined data.</param>
        /// <param name="isNowRecycle">是否立即回收界面，默认是否。/ Whether to recycle UI forms immediately, default is false.</param>
        void ReleaseAllLoadedUIForms(bool isNowRecycle = false, object userData = null);

        /// <summary>
        /// 设置界面显示处理接口。
        /// </summary>
        /// <remarks>
        /// Sets the UI form show handler.
        /// </remarks>
        /// <param name="handler">界面显示处理接口。/ The UI form show handler.</param>
        void SetUIFormShowHandler(IUIFormShowHandler handler);

        /// <summary>
        /// 设置界面隐藏处理接口。
        /// </summary>
        /// <remarks>
        /// Sets the UI form hide handler.
        /// </remarks>
        /// <param name="handler">界面隐藏处理接口。/ The UI form hide handler.</param>
        void SetUIFormHideHandler(IUIFormHideHandler handler);
    }
}
