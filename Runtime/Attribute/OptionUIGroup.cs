// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 用于为类指定 UI 选项分组的特性。
    /// 只能应用于类，分组名称不能为空。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OptionUIGroup : Attribute
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// 构造函数，初始化分组名称
        /// </summary>
        /// <param name="groupName">分组名称，不能为空</param>
        /// <exception cref="Exception">分组名称为空时抛出异常</exception>
        public OptionUIGroup(string groupName)
        {
            GroupName = groupName;
            if (string.IsNullOrEmpty(GroupName))
            {
                throw new Exception("GroupName is null");
            }
        }
    }
}