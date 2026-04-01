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

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面接口。
    /// </summary>
    /// <remarks>
    /// UI form interface.
    /// </remarks>
    public interface IUIForm
    {
        /// <summary>
        /// 界面回收开始时间
        /// </summary>
        /// <remarks>
        /// The start time of UI form recycling.
        /// </remarks>
        DateTime ReleaseStartTime { get; }

        /// <summary>
        /// 获取界面序列编号。
        /// </summary>
        /// <remarks>
        /// Gets the serial ID of the UI form.
        /// </remarks>
        int SerialId { get; }

        /// <summary>
        /// 获取界面完整名称。
        /// </summary>
        /// <remarks>
        /// Gets the full name of the UI form.
        /// </remarks>
        string FullName { get; }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        /// <remarks>
        /// Gets the asset name of the UI form.
        /// </remarks>
        string UIFormAssetName { get; }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        /// <remarks>
        /// Gets the asset path of the UI form.
        /// </remarks>
        string AssetPath { get; }

        /// <summary>
        /// 是否禁用回收，禁用回收的界面不会被回收
        /// </summary>
        /// <remarks>
        /// Whether recycling is disabled. UI forms with recycling disabled will not be recycled.
        /// </remarks>
        bool IsDisableRecycling { get; }

        /// <summary>
        /// 是否禁用关闭，禁用关闭的界面不会被关闭
        /// </summary>
        /// <remarks>
        /// Whether closing is disabled. UI forms with closing disabled will not be closed.
        /// </remarks>
        bool IsDisableClosing { get; }

        /// <summary>
        /// 是否可以回收，true:界面可以被回收，false:界面不可以被回收
        /// </summary>
        /// <remarks>
        /// Whether the UI form can be recycled. true: the UI form can be recycled, false: the UI form cannot be recycled.
        /// </remarks>
        bool IsCanRecycle { get; }

        /// <summary>
        /// 界面回收间隔，单位：秒
        /// </summary>
        /// <remarks>
        /// UI form recycling interval, in seconds.
        /// </remarks>
        int RecycleInterval { get; }

        /// <summary>
        /// 是否开启组件居中，true:组件生成后默认父组件居中
        /// </summary>
        /// <remarks>
        /// Whether to enable component centering. true: components are centered to the parent component after generation.
        /// </remarks>
        bool IsCenter { get; }

        /// <summary>
        /// 获取界面实例。
        /// </summary>
        /// <remarks>
        /// Gets the UI form instance.
        /// </remarks>
        object Handle { get; }

        /// <summary>
        /// 获取界面是否可用。
        /// </summary>
        /// <remarks>
        /// Gets whether the UI form is available.
        /// </remarks>
        bool Available { get; }

        /// <summary>
        /// 是否启用显示动画
        /// </summary>
        /// <remarks>
        /// Whether to enable show animation.
        /// </remarks>
        bool EnableShowAnimation { get; set; }

        /// <summary>
        /// 显示动画名称
        /// </summary>
        /// <remarks>
        /// The name of the show animation.
        /// </remarks>
        string ShowAnimationName { get; set; }

        /// <summary>
        /// 是否启用隐藏动画
        /// </summary>
        /// <remarks>
        /// Whether to enable hide animation.
        /// </remarks>
        bool EnableHideAnimation { get; set; }

        /// <summary>
        /// 隐藏动画名称
        /// </summary>
        /// <remarks>
        /// The name of the hide animation.
        /// </remarks>
        string HideAnimationName { get; set; }

        /// <summary>
        /// 获取界面是否可见。
        /// </summary>
        /// <remarks>
        /// Gets whether the UI form is visible.
        /// </remarks>
        bool Visible { get; }

        /// <summary>
        /// 获取界面所属的界面组。
        /// </summary>
        /// <remarks>
        /// Gets the UI group to which the UI form belongs.
        /// </remarks>
        IUIGroup UIGroup { get; set; }

        /// <summary>
        /// 获取界面在界面组中的深度。
        /// </summary>
        /// <remarks>
        /// Gets the depth of the UI form in the UI group.
        /// </remarks>
        int DepthInUIGroup { get; }

        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        /// <remarks>
        /// Gets whether to pause covered UI forms.
        /// </remarks>
        bool PauseCoveredUIForm { get; }

        /// <summary>
        /// 获取是否唤醒过
        /// </summary>
        /// <remarks>
        /// Gets whether the UI form has been awakened.
        /// </remarks>
        bool IsAwake { get; }

        /// <summary>
        /// 界面初始化前执行
        /// </summary>
        /// <remarks>
        /// Executed before UI form initialization.
        /// </remarks>
        void OnAwake();

        /// <summary>
        /// 初始化界面。
        /// </summary>
        /// <remarks>
        /// Initializes the UI form.
        /// </remarks>
        /// <param name="serialId">界面序列编号 / UI form serial ID</param>
        /// <param name="uiFormAssetPath">界面资源路径 / UI form asset path</param>
        /// <param name="uiFormAssetName">界面资源名称 / UI form asset name</param>
        /// <param name="uiGroup">界面所属的界面组 / The UI group to which the UI form belongs</param>
        /// <param name="onInitAction">初始化界面前的委托 / Delegate before UI form initialization</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面 / Whether to pause covered UI forms</param>
        /// <param name="isNewInstance">是否是新实例 / Whether it is a new instance</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        /// <param name="recycleInterval">界面回收间隔，单位：秒 / UI form recycling interval, in seconds</param>
        /// <param name="isFullScreen">是否全屏 / Whether it is full screen</param>
        void Init(int serialId, string uiFormAssetPath, string uiFormAssetName, IUIGroup uiGroup, Action<IUIForm> onInitAction, bool pauseCoveredUIForm, bool isNewInstance, object userData, int recycleInterval, bool isFullScreen = false);

        /// <summary>
        /// 界面初始化。
        /// </summary>
        /// <remarks>
        /// UI form initialization.
        /// </remarks>
        void OnInit();

        /// <summary>
        /// 界面回收。
        /// </summary>
        /// <remarks>
        /// UI form recycling.
        /// </remarks>
        void OnRecycle();

        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <remarks>
        /// UI form opening.
        /// </remarks>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        void OnOpen(object userData);

        /// <summary>
        /// 绑定事件
        /// </summary>
        /// <remarks>
        /// Bind events.
        /// </remarks>
        void BindEvent();

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <remarks>
        /// Load data.
        /// </remarks>
        void LoadData();

        /// <summary>
        /// 界面更新本地化。
        /// </summary>
        /// <remarks>
        /// UI form localization update.
        /// </remarks>
        void UpdateLocalization();

        /// <summary>
        /// 界面显示。
        /// </summary>
        /// <remarks>
        /// UI form showing.
        /// </remarks>
        /// <param name="handler">界面显示处理接口 / UI form show handler interface</param>
        /// <param name="complete">完成回调 / Completion callback</param>
        void Show(IUIFormShowHandler handler, Action complete);

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <remarks>
        /// UI form closing.
        /// </remarks>
        /// <param name="isShutdown">是否是关闭界面管理器时触发 / Whether triggered when closing the UI manager</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        void OnClose(bool isShutdown, object userData);

        /// <summary>
        /// 界面隐藏。
        /// </summary>
        /// <remarks>
        /// UI form hiding.
        /// </remarks>
        /// <param name="handler">界面隐藏处理接口 / UI form hide handler interface</param>
        /// <param name="complete">完成回调 / Completion callback</param>
        void Hide(IUIFormHideHandler handler, Action complete);

        /// <summary>
        /// 界面暂停。
        /// </summary>
        /// <remarks>
        /// UI form pausing.
        /// </remarks>
        void OnPause();

        /// <summary>
        /// 界面暂停恢复。
        /// </summary>
        /// <remarks>
        /// UI form resume from pause.
        /// </remarks>
        void OnResume();

        /// <summary>
        /// 界面遮挡。
        /// </summary>
        /// <remarks>
        /// UI form covered.
        /// </remarks>
        void OnCover();

        /// <summary>
        /// 界面遮挡恢复。
        /// </summary>
        /// <remarks>
        /// UI form revealed from cover.
        /// </remarks>
        void OnReveal();

        /// <summary>
        /// 界面激活。
        /// </summary>
        /// <remarks>
        /// UI form refocus.
        /// </remarks>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        void OnRefocus(object userData);

        /// <summary>
        /// 界面轮询。
        /// </summary>
        /// <remarks>
        /// UI form update polling.
        /// </remarks>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位 / Logic elapsed time, in seconds</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位 / Real elapsed time, in seconds</param>
        void OnUpdate(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <remarks>
        /// UI form depth changed.
        /// </remarks>
        /// <param name="uiGroupDepth">界面组深度 / UI group depth</param>
        /// <param name="depthInUIGroup">界面在界面组中的深度 / Depth of the UI form in the UI group</param>
        void OnDepthChanged(int uiGroupDepth, int depthInUIGroup);
    }
}
