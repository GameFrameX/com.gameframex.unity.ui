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
    /// 打开界面时加载依赖资源事件。
    /// </summary>
    /// <remarks>
    /// Event arguments for loading dependency assets when opening a UI form.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public sealed class OpenUIFormDependencyAssetEventArgs : GameEventArgs
    {
        /// <summary>
        /// 打开界面时加载依赖资源事件编号。
        /// </summary>
        /// <remarks>
        /// The event ID for loading dependency assets when opening a UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static readonly string EventId = typeof(OpenUIFormDependencyAssetEventArgs).FullName;

        /// <summary>
        /// 初始化打开界面时加载依赖资源事件的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="OpenUIFormDependencyAssetEventArgs"/> class.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public OpenUIFormDependencyAssetEventArgs()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }

        /// <summary>
        /// 获取打开界面时加载依赖资源事件编号。
        /// </summary>
        /// <remarks>
        /// Gets the event ID for loading dependency assets when opening a UI form.
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
        /// 获取被加载的依赖资源名称。
        /// </summary>
        /// <remarks>
        /// Gets the name of the dependency asset being loaded.
        /// </remarks>
        /// <value>被加载的依赖资源名称 / Name of the dependency asset being loaded</value>
        [UnityEngine.Scripting.Preserve]
        public string DependencyAssetName { get; private set; }

        /// <summary>
        /// 获取当前已加载依赖资源数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of dependency assets currently loaded.
        /// </remarks>
        /// <value>当前已加载依赖资源数量 / Number of dependency assets currently loaded</value>
        [UnityEngine.Scripting.Preserve]
        public int LoadedCount { get; private set; }

        /// <summary>
        /// 获取总共加载依赖资源数量。
        /// </summary>
        /// <remarks>
        /// Gets the total number of dependency assets to load.
        /// </remarks>
        /// <value>总共加载依赖资源数量 / Total number of dependency assets to load</value>
        [UnityEngine.Scripting.Preserve]
        public int TotalCount { get; private set; }

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
        /// 创建打开界面时加载依赖资源事件。
        /// </summary>
        /// <remarks>
        /// Creates an event for loading dependency assets when opening a UI form.
        /// </remarks>
        /// <param name="serialId">界面序列编号 / UI form serial ID</param>
        /// <param name="uiFormAssetName">界面资源名称 / UI form asset name</param>
        /// <param name="uiGroupName">界面组名称 / UI group name</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面 / Whether to pause covered UI forms</param>
        /// <param name="dependencyAssetName">依赖资源名称 / Dependency asset name</param>
        /// <param name="loadedCount">已加载依赖资源数量 / Number of dependency assets loaded</param>
        /// <param name="totalCount">总共加载依赖资源数量 / Total number of dependency assets to load</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        /// <returns>创建的打开界面时加载依赖资源事件 / The created dependency asset loading event</returns>
        [UnityEngine.Scripting.Preserve]
        public static OpenUIFormDependencyAssetEventArgs Create(int serialId, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, string dependencyAssetName, int loadedCount, int totalCount, object userData)
        {
            OpenUIFormDependencyAssetEventArgs openUIFormDependencyAssetEventArgs = ReferencePool.Acquire<OpenUIFormDependencyAssetEventArgs>();
            openUIFormDependencyAssetEventArgs.SerialId = serialId;
            openUIFormDependencyAssetEventArgs.UIFormAssetName = uiFormAssetName;
            openUIFormDependencyAssetEventArgs.UIGroupName = uiGroupName;
            openUIFormDependencyAssetEventArgs.PauseCoveredUIForm = pauseCoveredUIForm;
            openUIFormDependencyAssetEventArgs.DependencyAssetName = dependencyAssetName;
            openUIFormDependencyAssetEventArgs.LoadedCount = loadedCount;
            openUIFormDependencyAssetEventArgs.TotalCount = totalCount;
            openUIFormDependencyAssetEventArgs.UserData = userData;
            return openUIFormDependencyAssetEventArgs;
        }

        /// <summary>
        /// 清理打开界面时加载依赖资源事件。
        /// </summary>
        /// <remarks>
        /// Clears the dependency asset loading event.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }
    }
}*/