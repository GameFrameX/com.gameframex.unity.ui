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
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 打开界面的信息。
    /// </summary>
    /// <remarks>
    /// Information for opening a UI form.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public sealed class OpenUIFormInfo : IReference
    {
        [UnityEngine.Scripting.Preserve]
        private int m_SerialId = 0;
        [UnityEngine.Scripting.Preserve]
        private bool m_PauseCoveredUIForm = false;
        [UnityEngine.Scripting.Preserve]
        private object m_UserData = null;
        [UnityEngine.Scripting.Preserve]
        private Type m_FormType;
        [UnityEngine.Scripting.Preserve]
        private object m_AssetHandle;
        [UnityEngine.Scripting.Preserve]
        private bool m_IsFullScreen = false;
        [UnityEngine.Scripting.Preserve]
        private string m_AssetPath;
        [UnityEngine.Scripting.Preserve]
        private string m_AssetName;

        /// <summary>
        /// 获取界面是否全屏。
        /// </summary>
        /// <remarks>
        /// Gets whether the UI form is full screen.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsFullScreen
        {
            get { return m_IsFullScreen; }
        }

        /// <summary>
        /// 获取界面资源路径。
        /// </summary>
        /// <remarks>
        /// Gets the asset path of the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public string AssetPath
        {
            get { return m_AssetPath; }
        }

        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        /// <remarks>
        /// Gets the asset name of the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public string AssetName
        {
            get { return m_AssetName; }
        }

        /// <summary>
        /// 获取界面类型。
        /// </summary>
        /// <remarks>
        /// Gets the type of the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public Type FormType
        {
            get { return m_FormType; }
        }

        /// <summary>
        /// 获取界面序列编号。
        /// </summary>
        /// <remarks>
        /// Gets the serial ID of the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public int SerialId
        {
            get { return m_SerialId; }
        }

        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        /// <remarks>
        /// Gets whether to pause the covered UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool PauseCoveredUIForm
        {
            get { return m_PauseCoveredUIForm; }
        }

        /// <summary>
        /// 获取用户自定义数据。
        /// </summary>
        /// <remarks>
        /// Gets the user custom data.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public object UserData
        {
            get { return m_UserData; }
        }

        /// <summary>
        /// 获取界面资源句柄。
        /// </summary>
        /// <remarks>
        /// Gets the asset handle of the UI form.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public object AssetHandle
        {
            get { return m_AssetHandle; }
        }

        /// <summary>
        /// 设置界面资源句柄。
        /// </summary>
        /// <remarks>
        /// Sets the asset handle of the UI form.
        /// </remarks>
        /// <param name="assetHandle">界面资源句柄 / The asset handle of the UI form.</param>
        [UnityEngine.Scripting.Preserve]
        public void SetAssetHandle(object assetHandle)
        {
            m_AssetHandle = assetHandle;
        }

        /// <summary>
        /// 创建打开界面的信息。
        /// </summary>
        /// <remarks>
        /// Creates information for opening a UI form.
        /// </remarks>
        /// <param name="serialId">界面序列编号 / The serial ID of the UI form.</param>
        /// <param name="assetPath">界面资源路径 / The asset path of the UI form.</param>
        /// <param name="assetName">界面资源名称 / The asset name of the UI form.</param>
        /// <param name="uiFormType">界面类型 / The type of the UI form.</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面 / Whether to pause the covered UI form.</param>
        /// <param name="userData">用户自定义数据 / The user custom data.</param>
        /// <param name="isFullScreen">界面是否全屏 / Whether the UI form is full screen.</param>
        /// <returns>创建的打开界面的信息 / The created open UI form information.</returns>
        [UnityEngine.Scripting.Preserve]
        public static OpenUIFormInfo Create(int serialId, string assetPath, string assetName, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen)
        {
            OpenUIFormInfo openUIFormInfo = ReferencePool.Acquire<OpenUIFormInfo>();
            openUIFormInfo.m_SerialId = serialId;
            openUIFormInfo.m_PauseCoveredUIForm = pauseCoveredUIForm;
            openUIFormInfo.m_UserData = userData;
            openUIFormInfo.m_AssetPath = assetPath;
            openUIFormInfo.m_AssetName = assetName;
            openUIFormInfo.m_FormType = uiFormType;
            openUIFormInfo.m_IsFullScreen = isFullScreen;
            return openUIFormInfo;
        }

        /// <summary>
        /// 清理打开界面的信息。
        /// </summary>
        /// <remarks>
        /// Clears the open UI form information.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public void Clear()
        {
            m_SerialId = default;
            m_PauseCoveredUIForm = default;
            m_UserData = default;
            m_FormType = default;
            m_AssetHandle = default;
            m_IsFullScreen = default;
            m_AssetPath = default;
            m_AssetName = default;
        }
    }
}