/*// ==========================================================================================
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

using GameFrameX.Event.Runtime;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 打开界面更新事件。
    /// </summary>
    /// <remarks>
    /// Event arguments for UI form open update.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public sealed class OpenUIFormUpdateEventArgs : GameEventArgs
    {
        /// <summary>
        /// 打开界面更新事件编号。
        /// </summary>
        /// <remarks>
        /// The event ID for UI form open update.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static readonly string EventId = typeof(OpenUIFormUpdateEventArgs).FullName;

        /// <summary>
        /// 初始化打开界面更新事件的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="OpenUIFormUpdateEventArgs"/> class.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public OpenUIFormUpdateEventArgs()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            Progress = 0f;
            UserData = null;
        }

        /// <summary>
        /// 获取打开界面更新事件编号。
        /// </summary>
        /// <remarks>
        /// Gets the event ID for UI form open update.
        /// </remarks>
        /// <value>事件编号 / Event ID</value>
        [UnityEngine.Scripting.Preserve]
        public override string Id
        {
            get { return EventId; }
        }

        /// <summary>
        /// 获取界面序列编号。
        /// </summary>
        /// <remarks>
        /// Gets the serial ID of the UI form.
        /// </remarks>
        /// <value>界面序列编号 / UI form serial ID</value>
        [UnityEngine.Scripting.Preserve]
        public int SerialId { get; private set; }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        /// <remarks>
        /// Gets the asset name of the UI form.
        /// </remarks>
        /// <value>界面资源名称 / UI form asset name</value>
        [UnityEngine.Scripting.Preserve]
        public string UIFormAssetName { get; private set; }

        /// <summary>
        /// 获取界面组名称。
        /// </summary>
        /// <remarks>
        /// Gets the UI group name.
        /// </remarks>
        /// <value>界面组名称 / UI group name</value>
        [UnityEngine.Scripting.Preserve]
        public string UIGroupName { get; private set; }

        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        /// <remarks>
        /// Gets whether to pause covered UI forms.
        /// </remarks>
        /// <value>是否暂停被覆盖的界面 / Whether to pause covered UI forms</value>
        [UnityEngine.Scripting.Preserve]
        public bool PauseCoveredUIForm { get; private set; }

        /// <summary>
        /// 获取打开界面进度。
        /// </summary>
        /// <remarks>
        /// Gets the progress of opening the UI form.
        /// </remarks>
        /// <value>打开界面进度（0-1） / Progress of opening the UI form (0-1)</value>
        [UnityEngine.Scripting.Preserve]
        public float Progress { get; private set; }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        /// <remarks>
        /// Gets the user custom data.
        /// </remarks>
        /// <value>用户自定义数据 / User custom data</value>
        [UnityEngine.Scripting.Preserve]
        public object UserData { get; private set; }

        /// <summary>
        /// 创建打开界面更新事件。
        /// </summary>
        /// <remarks>
        /// Creates a UI form open update event.
        /// </remarks>
        /// <param name="serialId">界面序列编号 / UI form serial ID</param>
        /// <param name="uiFormAssetName">界面资源名称 / UI form asset name</param>
        /// <param name="uiGroupName">界面组名称 / UI group name</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面 / Whether to pause covered UI forms</param>
        /// <param name="progress">打开界面进度 / Progress of opening the UI form</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        /// <returns>创建的打开界面更新事件 / The created open UI form update event</returns>
        [UnityEngine.Scripting.Preserve]
        public static OpenUIFormUpdateEventArgs Create(int serialId, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, float progress, object userData)
        {
            OpenUIFormUpdateEventArgs openUIFormUpdateEventArgs = ReferencePool.Acquire<OpenUIFormUpdateEventArgs>();
            openUIFormUpdateEventArgs.SerialId = serialId;
            openUIFormUpdateEventArgs.UIFormAssetName = uiFormAssetName;
            openUIFormUpdateEventArgs.UIGroupName = uiGroupName;
            openUIFormUpdateEventArgs.PauseCoveredUIForm = pauseCoveredUIForm;
            openUIFormUpdateEventArgs.Progress = progress;
            openUIFormUpdateEventArgs.UserData = userData;
            return openUIFormUpdateEventArgs;
        }

        /// <summary>
        /// 清理打开界面更新事件。
        /// </summary>
        /// <remarks>
        /// Clears the open UI form update event.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            Progress = 0f;
            UserData = null;
        }
    }
}*/