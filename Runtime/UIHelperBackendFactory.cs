// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents and other related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and other international regulations.
//
//  使用本项目须严格遵守相应法律法规与开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations and open-source licenses.
//
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory for the full license text.
//
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon any legal rights and interests of others as prohibited by laws and regulations.
//
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes or liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
//
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository:  https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  CNB 仓库：https://cnb.cool/GameFrameX
//  CNB Repository:  https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using System.Collections.Generic;
using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// UI 辅助器后端工厂注册表。UI 后端包（FairyGUI / UGUI）在启动阶段按命名空间自注册默认的表单/界面组辅助器工厂，
    /// <see cref="UIComponent"/> 在未通过 <see cref="UIOptions"/> 配置工厂时按当前实现组件命名空间解析默认工厂。
    /// </summary>
    /// <remarks>
    /// UI helper backend factory registry. UI backend packages (FairyGUI / UGUI) self-register their default form/group helper
    /// factories keyed by namespace during startup; <see cref="UIComponent"/> resolves the default factory by the active
    /// implementation component namespace when no factory was configured via <see cref="UIOptions"/>.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class UIHelperBackendFactory
    {
        private static readonly Dictionary<string, (Func<UIFormHelperBase> FormFactory, Func<UIGroupHelperBase> GroupFactory)> s_Backends =
            new Dictionary<string, (Func<UIFormHelperBase>, Func<UIGroupHelperBase>)>();

        /// <summary>
        /// 注册 UI 后端默认辅助器工厂。后端包通过 <c>RuntimeInitializeOnLoadMethod(SubsystemRegistration)</c> 自注册（引用即注册），
        /// 注册时机早于 <see cref="UIComponent"/> 首次创建辅助器。同一命名空间重复注册时保留首个并输出 Warning。
        /// </summary>
        /// <remarks>
        /// Registers a UI backend's default helper factories. Backend packages self-register via
        /// <c>RuntimeInitializeOnLoadMethod(SubsystemRegistration)</c> (reference implies registration), before <see cref="UIComponent"/>
        /// first creates helpers. Duplicate registrations for the same namespace keep the first and log a warning.
        /// </remarks>
        /// <param name="backendNamespace">后端命名空间（与后端 UIManager 命名空间一致）</param>
        /// <param name="formHelperFactory">表单辅助器工厂</param>
        /// <param name="groupHelperFactory">界面组辅助器工厂</param>
        /// <exception cref="GameFrameworkException">任一工厂为 null 时抛出</exception>
        [UnityEngine.Scripting.Preserve]
        public static void Register(string backendNamespace, Func<UIFormHelperBase> formHelperFactory, Func<UIGroupHelperBase> groupHelperFactory)
        {
            if (formHelperFactory == null || groupHelperFactory == null)
            {
                throw new GameFrameworkException("UI helper backend factory is invalid.");
            }

            if (s_Backends.ContainsKey(backendNamespace))
            {
                Log.Warning("UI helper backend for namespace '{0}' is already registered. The duplicate registration is ignored.", backendNamespace);
                return;
            }

            s_Backends[backendNamespace] = (formHelperFactory, groupHelperFactory);
        }

        /// <summary>
        /// 按后端命名空间解析默认表单辅助器工厂（未注册时为 null）。
        /// </summary>
        /// <param name="backendNamespace">后端命名空间</param>
        /// <returns>表单辅助器工厂</returns>
        [UnityEngine.Scripting.Preserve]
        public static Func<UIFormHelperBase> ResolveFormHelperFactory(string backendNamespace)
        {
            return backendNamespace != null && s_Backends.TryGetValue(backendNamespace, out var backend) ? backend.FormFactory : null;
        }

        /// <summary>
        /// 按后端命名空间解析默认界面组辅助器工厂（未注册时为 null）。
        /// </summary>
        /// <param name="backendNamespace">后端命名空间</param>
        /// <returns>界面组辅助器工厂</returns>
        [UnityEngine.Scripting.Preserve]
        public static Func<UIGroupHelperBase> ResolveUIGroupHelperFactory(string backendNamespace)
        {
            return backendNamespace != null && s_Backends.TryGetValue(backendNamespace, out var backend) ? backend.GroupFactory : null;
        }

        /// <summary>
        /// 重置已注册的后端工厂。仅供单元测试在用例间清理注册状态使用。
        /// </summary>
        internal static void ResetForTests()
        {
            s_Backends.Clear();
        }
    }
}
