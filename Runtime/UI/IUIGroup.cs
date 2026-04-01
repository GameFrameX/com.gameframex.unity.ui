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

using System.Collections.Generic;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面组接口。
    /// </summary>
    /// <remarks>
    /// UI group interface.
    /// </remarks>
    public interface IUIGroup
    {
        /// <summary>
        /// 获取界面组名称。
        /// </summary>
        /// <remarks>
        /// Gets the UI group name.
        /// </remarks>
        string Name { get; }

        /// <summary>
        /// 获取或设置界面组深度。
        /// </summary>
        /// <remarks>
        /// Gets or sets the UI group depth.
        /// </remarks>
        int Depth { get; set; }

        /// <summary>
        /// 获取或设置界面组是否暂停。
        /// </summary>
        /// <remarks>
        /// Gets or sets whether the UI group is paused.
        /// </remarks>
        bool Pause { get; set; }

        /// <summary>
        /// 获取界面组中界面数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of UI forms in the UI group.
        /// </remarks>
        int UIFormCount { get; }

        /// <summary>
        /// 获取当前界面。
        /// </summary>
        /// <remarks>
        /// Gets the current UI form.
        /// </remarks>
        IUIForm CurrentUIForm { get; }

        /// <summary>
        /// 获取界面组辅助器。
        /// </summary>
        /// <remarks>
        /// Gets the UI group helper.
        /// </remarks>
        IUIGroupHelper Helper { get; }

        /// <summary>
        /// 界面组中是否存在界面。
        /// </summary>
        /// <remarks>
        /// Checks whether a UI form exists in the UI group.
        /// </remarks>
        /// <param name="serialId">界面序列编号。/ The UI form serial ID.</param>
        /// <returns>界面组中是否存在界面。/ Whether the UI form exists in the UI group.</returns>
        bool HasUIForm(int serialId);

        /// <summary>
        /// 界面组中是否存在界面。
        /// </summary>
        /// <remarks>
        /// Checks whether a UI form exists in the UI group.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The UI form asset name.</param>
        /// <returns>界面组中是否存在界面。/ Whether the UI form exists in the UI group.</returns>
        bool HasUIForm(string uiFormAssetName);

        /// <summary>
        /// 从界面组中获取界面。
        /// </summary>
        /// <remarks>
        /// Gets a UI form from the UI group.
        /// </remarks>
        /// <param name="serialId">界面序列编号。/ The UI form serial ID.</param>
        /// <returns>要获取的界面。/ The UI form to get.</returns>
        IUIForm GetUIForm(int serialId);

        /// <summary>
        /// 从界面组中获取界面。
        /// </summary>
        /// <remarks>
        /// Gets a UI form from the UI group.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The UI form asset name.</param>
        /// <returns>要获取的界面。/ The UI form to get.</returns>
        IUIForm GetUIForm(string uiFormAssetName);

        /// <summary>
        /// 从界面组中获取界面。
        /// </summary>
        /// <remarks>
        /// Gets UI forms from the UI group.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The UI form asset name.</param>
        /// <returns>要获取的界面。/ The UI forms to get.</returns>
        IUIForm[] GetUIForms(string uiFormAssetName);

        /// <summary>
        /// 从界面组中获取界面。
        /// </summary>
        /// <remarks>
        /// Gets UI forms from the UI group.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The UI form asset name.</param>
        /// <param name="results">要获取的界面。/ The UI forms to get.</param>
        void GetUIForms(string uiFormAssetName, List<IUIForm> results);

        /// <summary>
        /// 从界面组中获取所有界面。
        /// </summary>
        /// <remarks>
        /// Gets all UI forms from the UI group.
        /// </remarks>
        /// <returns>界面组中的所有界面。/ All UI forms in the UI group.</returns>
        IUIForm[] GetAllUIForms();

        /// <summary>
        /// 从界面组中获取所有界面。
        /// </summary>
        /// <remarks>
        /// Gets all UI forms from the UI group.
        /// </remarks>
        /// <param name="results">界面组中的所有界面。/ All UI forms in the UI group.</param>
        void GetAllUIForms(List<IUIForm> results);

        /// <summary>
        /// 检查界面组中是否存在指定界面。
        /// </summary>
        /// <remarks>
        /// Checks whether a specified UI form exists in the UI group.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。/ The UI form asset name.</param>
        /// <param name="uiForm">要检查的界面。/ The UI form to check.</param>
        /// <returns>是否存在指定界面。/ Whether the specified UI form exists.</returns>
        bool InternalHasInstanceUIForm(string uiFormAssetName, IUIForm uiForm);

        /// <summary>
        /// 往界面组增加界面。
        /// </summary>
        /// <remarks>
        /// Adds a UI form to the UI group.
        /// </remarks>
        /// <param name="uiForm">要增加的界面。/ The UI form to add.</param>
        void AddUIForm(IUIForm uiForm);

        /// <summary>
        /// 刷新界面组。
        /// </summary>
        /// <remarks>
        /// Refreshes the UI group.
        /// </remarks>
        void Refresh();
    }
}
