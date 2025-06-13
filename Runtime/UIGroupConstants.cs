// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面组常量
    /// </summary>
    public static class UIGroupConstants
    {
        /// <summary>
        /// 隐藏
        /// </summary>
        public static readonly UIGroupDefine Hidden = new UIGroupDefine(40, "Hidden");

        /// <summary>
        /// 背景
        /// </summary>
        public static readonly UIGroupDefine Background = new UIGroupDefine(35, "Background");

        /// <summary>
        /// 场景
        /// </summary>
        public static readonly UIGroupDefine Scene = new UIGroupDefine(30, "Scene");

        /// <summary>
        /// 世界
        /// </summary>
        public static readonly UIGroupDefine World = new UIGroupDefine(27, "World");

        /// <summary>
        /// 战斗
        /// </summary>
        public static readonly UIGroupDefine Battle = new UIGroupDefine(25, "Battle");

        /// <summary>
        /// 头顶
        /// </summary>
        public static readonly UIGroupDefine Hud = new UIGroupDefine(22, "Hud");

        /// <summary>
        /// 地图
        /// </summary>
        public static readonly UIGroupDefine Map = new UIGroupDefine(20, "Map");

        /// <summary>
        /// 底板
        /// </summary>
        public static readonly UIGroupDefine Floor = new UIGroupDefine(15, "Floor");

        /// <summary>
        /// 正常
        /// </summary>
        public static readonly UIGroupDefine Normal = new UIGroupDefine(10, "Normal");

        /// <summary>
        /// 固定
        /// </summary>
        public static readonly UIGroupDefine Fixed = new UIGroupDefine(0, "Fixed");

        /// <summary>
        /// 窗口
        /// </summary>
        public static readonly UIGroupDefine Window = new UIGroupDefine(-10, "Window");

        /// <summary>
        /// 提示
        /// </summary>
        public static readonly UIGroupDefine Tip = new UIGroupDefine(-15, "Tip");

        /// <summary>
        /// 引导
        /// </summary>
        public static readonly UIGroupDefine Guide = new UIGroupDefine(-20, "Guide");

        /// <summary>
        /// 黑板
        /// </summary>
        public static readonly UIGroupDefine BlackBoard = new UIGroupDefine(-22, "BlackBoard");

        /// <summary>
        /// 对话
        /// </summary>
        public static readonly UIGroupDefine Dialogue = new UIGroupDefine(-23, "Dialogue");

        /// <summary>
        /// Loading 
        /// </summary>
        public static readonly UIGroupDefine Loading = new UIGroupDefine(-25, "Loading");

        /// <summary>
        /// 通知
        /// </summary>
        public static readonly UIGroupDefine Notify = new UIGroupDefine(-30, "Notify");

        /// <summary>
        /// 系统顶级
        /// </summary>
        public static readonly UIGroupDefine System = new UIGroupDefine(-35, "System");
    }
}