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

using GameFrameX.Event.Runtime;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面激活状态变化事件。
    /// </summary>
    /// <remarks>
    /// Event arguments for UI form visibility state change.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public sealed class UIFormVisibleChangedEventArgs : GameEventArgs
    {
        /// <summary>
        /// 界面激活状态变化事件编号。
        /// </summary>
        /// <remarks>
        /// The event ID for UI form visibility state change.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static readonly string EventId = typeof(UIFormVisibleChangedEventArgs).FullName;

        /// <summary>
        /// 初始化界面激活状态变化事件的新实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="UIFormVisibleChangedEventArgs"/> class.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public UIFormVisibleChangedEventArgs()
        {
            UIForm = null;
            Visible = false;
            UserData = null;
        }

        /// <summary>
        /// 获取界面激活状态变化事件编号。
        /// </summary>
        /// <remarks>
        /// Gets the event ID for UI form visibility state change.
        /// </remarks>
        /// <value>事件编号 / Event ID</value>
        [UnityEngine.Scripting.Preserve]
        public override string Id
        {
            get { return EventId; }
        }

        /// <summary>
        /// 获取激活状态变化的界面。
        /// </summary>
        /// <remarks>
        /// Gets the UI form whose visibility state changed.
        /// </remarks>
        /// <value>激活状态变化的界面 / The UI form whose visibility state changed</value>
        [UnityEngine.Scripting.Preserve]
        public IUIForm UIForm { get; private set; }

        /// <summary>
        /// 获取界面是否可见。
        /// </summary>
        /// <remarks>
        /// Gets whether the UI form is visible.
        /// </remarks>
        /// <value>界面是否可见 / Whether the UI form is visible</value>
        [UnityEngine.Scripting.Preserve]
        public bool Visible { get; private set; }

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
        /// 创建界面激活状态变化事件。
        /// </summary>
        /// <remarks>
        /// Creates a UI form visibility state change event.
        /// </remarks>
        /// <param name="uiForm">激活状态变化的界面 / The UI form whose visibility state changed</param>
        /// <param name="visible">显示状态 / Visibility state</param>
        /// <param name="userData">用户自定义数据 / User custom data</param>
        /// <returns>创建的界面激活状态变化事件 / The created visibility state change event</returns>
        [UnityEngine.Scripting.Preserve]
        public static UIFormVisibleChangedEventArgs Create(IUIForm uiForm, bool visible, object userData = null)
        {
            UIFormVisibleChangedEventArgs uiFormSuccessEventArgs = ReferencePool.Acquire<UIFormVisibleChangedEventArgs>();
            uiFormSuccessEventArgs.UIForm = uiForm;
            uiFormSuccessEventArgs.Visible = visible;
            uiFormSuccessEventArgs.UserData = userData;
            return uiFormSuccessEventArgs;
        }

        /// <summary>
        /// 清理界面激活状态变化事件。
        /// </summary>
        /// <remarks>
        /// Clears the UI form visibility state change event.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public override void Clear()
        {
            UIForm = null;
            Visible = false;
            UserData = null;
        }
    }
}