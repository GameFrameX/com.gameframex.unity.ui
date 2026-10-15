// ==========================================================================================
//   GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//   GameFrameX organization and its derivative projects' copyrights, trademarks, patents and other related rights
//   均受中华人民共和国及相关国际法律法规保护。
//   are protected by the laws of the People's Republic of China and other international regulations.
//   使用本项目须严格遵守相应法律法规与开源许可证之规定。
//   Usage of this project must strictly comply with applicable laws, regulations and open-source licenses.
//   本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//   This project is dual-licensed under the MIT License and Apache License 2.0,
//   complete license text please see the LICENSE file in the source code root directory.
//   禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//   It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//   侵犯他人合法权益等法律法规所禁止的行为！
//   or infringe any legal rights and interests of others as prohibited by laws and regulations!
//   因基于本项目二次开发所产生的一切法律纠纷与责任，
//   Any legal disputes or liabilities arising from second development based on this project
//   shall be borne solely by the developer of the project; the project organization and contributors assume no responsibility.
//   GitHub 仓库：https://github.com/GameFrameX
//   GitHub Repository: https://github.com/GameFrameX
//   Gitee  仓库：https://gitee.com/GameFrameX
//   Gitee Repository:  https://gitee.com/GameFrameX
//   CNB 仓库：https://cnb.cool/GameFrameX
//   CNB Repository:  https://cnb.cool/GameFrameX
//   官方文档：https://gameframex.doc.alianblank.com/
//   Official Documentation: https://gameframex.doc.alianblank.com/
//  ==========================================================================================

using System;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面模块启动配置（模块级 Options）。
    /// </summary>
    /// <remarks>
    /// 默认值即开箱值（与原 UIComponent 序列化字段默认值同源）；游戏在首场景加载前（BeforeSceneLoad）经 <c>GameFrameXOptions.Configure(new UIOptions{...})</c> 注入差异值，
    /// 由 <see cref="UIComponent"/> 在 Awake 中一次性消费（事件开关与实例池参数为实时型，运行期可经组件属性修改）。
    /// </remarks>
    public sealed class UIOptions
    {
        /// <summary>
        /// 是否启用打开界面成功事件（默认开启）。
        /// </summary>
        /// <remarks>实时型：由组件在 Awake 订阅事件时消费。</remarks>
        public bool EnableOpenUIFormSuccessEvent = true;

        /// <summary>
        /// 是否启用打开界面失败事件（默认开启）。
        /// </summary>
        /// <remarks>实时型：由组件在 Awake 订阅事件时消费。</remarks>
        public bool EnableOpenUIFormFailureEvent = true;

        /// <summary>
        /// 是否启用打开界面更新事件（默认关闭）。
        /// </summary>
        /// <remarks>实时型：当前组件未订阅该事件，预留与原注释字段同源。</remarks>
        public bool EnableOpenUIFormUpdateEvent = false;

        /// <summary>
        /// 是否启用打开界面时加载依赖资源事件（默认关闭）。
        /// </summary>
        /// <remarks>实时型：当前组件未订阅该事件，预留与原注释字段同源。</remarks>
        public bool EnableOpenUIFormDependencyAssetEvent = false;

        /// <summary>
        /// 是否启用自动回收界面（默认开启）。
        /// </summary>
        /// <remarks>实时型：运行期可经 <see cref="UIComponent.EnableAutoReleaseUIForm"/> 修改。</remarks>
        public bool EnableAutoReleaseUIForm = true;

        /// <summary>
        /// 是否启用关闭界面完成事件（默认开启）。
        /// </summary>
        /// <remarks>实时型：由组件在 Awake 订阅事件时消费。</remarks>
        public bool EnableCloseUIFormCompleteEvent = true;

        /// <summary>
        /// 是否启用界面显示动画（默认关闭）。
        /// </summary>
        /// <remarks>实时型：由组件在 Start 写入界面管理器。</remarks>
        public bool IsEnableUIShowAnimation = false;

        /// <summary>
        /// 是否启用界面隐藏动画（默认关闭）。
        /// </summary>
        /// <remarks>实时型：由组件在 Start 写入界面管理器。</remarks>
        public bool IsEnableUIHideAnimation = false;

        /// <summary>
        /// 界面实例对象池自动释放可释放对象的间隔秒数（默认 60 秒）。
        /// </summary>
        /// <remarks>实时型：运行期可经 <see cref="UIComponent.InstanceAutoReleaseInterval"/> 修改。</remarks>
        public float InstanceAutoReleaseInterval = 60f;

        /// <summary>
        /// 界面实例对象池的容量（默认 16）。
        /// </summary>
        /// <remarks>实时型：运行期可经 <see cref="UIComponent.InstanceCapacity"/> 修改。</remarks>
        public int InstanceCapacity = 16;

        /// <summary>
        /// 界面实例对象池对象过期秒数（默认 60 秒）。
        /// </summary>
        /// <remarks>实时型：运行期可经 <see cref="UIComponent.InstanceExpireTime"/> 修改。</remarks>
        public float InstanceExpireTime = 60f;

        /// <summary>
        /// 界面实例对象池的优先级（默认 0）。
        /// </summary>
        /// <remarks>实时型：与原注释字段同源，当前界面管理器未暴露该属性。</remarks>
        public int InstancePriority = 0;

        /// <summary>
        /// 界面表单辅助器工厂。默认值来自 UI 后端包（FairyGUI / UGUI）启动期自注册，因此为 null；
        /// 未显式配置时组件按当前实现组件命名空间解析后端默认工厂。
        /// </summary>
        /// <remarks>启动期型：仅在组件 Start 创建帮助器时消费，运行期不可修改。</remarks>
        [GameFrameXStartupOnly]
        public Func<UIFormHelperBase> UIFormHelperFactory = null;

        /// <summary>
        /// 界面组辅助器工厂。默认值来自 UI 后端包（FairyGUI / UGUI）启动期自注册，因此为 null；
        /// 未显式配置时组件按当前实现组件命名空间解析后端默认工厂。
        /// </summary>
        /// <remarks>启动期型：仅在组件创建界面组时消费，运行期不可修改。</remarks>
        [GameFrameXStartupOnly]
        public Func<UIGroupHelperBase> UIGroupHelperFactory = null;
    }
}
