// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 用于标记 UI 配置的特性类，支持 FairyGUI 和 UGUI 两种 UI 框架。
    /// 通过指定包名或路径，实现 UI 资源的自动定位和加载。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OptionUIConfigAttribute : Attribute
    {
        /// <summary>
        /// FairyGUI 使用的包名。用于定位 FairyGUI 的 UI 资源包。
        /// </summary>
        public string PackageName { get; private set; }

        /// <summary>
        /// UGUI 使用的资源路径。用于定位 UGUI 的 UI 预制体或资源。
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// 构造 UI 配置特性。
        /// </summary>
        /// <param name="packageName">FairyGUI 使用的包名，若为 null 则不使用 FairyGUI。</param>
        /// <param name="path">UGUI 使用的资源路径，若为 null 则不使用 UGUI。</param>
        /// <exception cref="Exception">当 packageName 和 path 均为 null 或空字符串时抛出异常。</exception>
        public OptionUIConfigAttribute(string packageName = null, string path = null)
        {
            PackageName = packageName;
            Path = path;
            if (string.IsNullOrEmpty(PackageName) && string.IsNullOrEmpty(Path))
            {
                throw new Exception("PackageName or Path is null");
            }
        }
    }
}