// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面接口。
    /// </summary>
    public interface IUIForm
    {
        /// <summary>
        /// 获取界面序列编号。
        /// </summary>
        int SerialId { get; }

        /// <summary>
        /// 获取界面完整名称。
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        string UIFormAssetName { get; }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        string AssetPath { get; }

        /// <summary>
        /// 是否禁用回收，禁用回收的界面不会被回收
        /// </summary>
        bool IsDisableRecycling { get; }

        /// <summary>
        /// 获取界面实例。
        /// </summary>
        object Handle { get; }

        /// <summary>
        /// 获取界面是否可用。
        /// </summary>
        bool Available { get; }

        /// <summary>
        /// 获取界面是否可见。
        /// </summary>
        bool Visible { get; }

        /// <summary>
        /// 获取界面所属的界面组。
        /// </summary>
        IUIGroup UIGroup { get; }

        /// <summary>
        /// 获取界面在界面组中的深度。
        /// </summary>
        int DepthInUIGroup { get; }

        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        bool PauseCoveredUIForm { get; }

        /// <summary>
        /// 获取是否唤醒过
        /// </summary>
        bool IsAwake { get; }

        /// <summary>
        /// 界面初始化前执行
        /// </summary>
        void OnAwake();

        /// <summary>
        /// 初始化界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroup">界面所属的界面组。</param>
        /// <param name="onInitAction">初始化界面前的委托。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isNewInstance">是否是新实例。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isFullScreen">是否全屏</param>
        void Init(int serialId, string uiFormAssetName, IUIGroup uiGroup, Action<IUIForm> onInitAction, bool pauseCoveredUIForm, bool isNewInstance, object userData, bool isFullScreen = false);

        /// <summary>
        /// 界面初始化。
        /// </summary>
        void OnInit();

        /// <summary>
        /// 界面回收。
        /// </summary>
        void OnRecycle();

        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        void OnOpen(object userData);

        /// <summary>
        /// 界面更新本地化。
        /// </summary>
        void UpdateLocalization();

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <param name="isShutdown">是否是关闭界面管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        void OnClose(bool isShutdown, object userData);

        /// <summary>
        /// 界面暂停。
        /// </summary>
        void OnPause();

        /// <summary>
        /// 界面暂停恢复。
        /// </summary>
        void OnResume();

        /// <summary>
        /// 界面遮挡。
        /// </summary>
        void OnCover();

        /// <summary>
        /// 界面遮挡恢复。
        /// </summary>
        void OnReveal();

        /// <summary>
        /// 界面激活。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        void OnRefocus(object userData);

        /// <summary>
        /// 界面轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        void OnUpdate(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <param name="uiGroupDepth">界面组深度。</param>
        /// <param name="depthInUIGroup">界面在界面组中的深度。</param>
        void OnDepthChanged(int uiGroupDepth, int depthInUIGroup);
    }
}