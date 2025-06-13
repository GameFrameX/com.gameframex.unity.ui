// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面组界面信息。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed class UIFormInfo : IReference
    {
        private IUIForm m_UIForm = null;
        private bool m_Paused = false;
        private bool m_Covered = false;

        /// <summary>
        /// 获取界面。
        /// </summary>
        public IUIForm UIForm
        {
            get { return m_UIForm; }
        }

        /// <summary>
        /// 获取或设置界面是否暂停。
        /// </summary>
        public bool Paused
        {
            get { return m_Paused; }
            set { m_Paused = value; }
        }

        /// <summary>
        /// 获取或设置界面是否被覆盖。
        /// </summary>
        public bool Covered
        {
            get { return m_Covered; }
            set { m_Covered = value; }
        }

        /// <summary>
        /// 创建界面组界面信息。
        /// </summary>
        /// <param name="uiForm">界面。</param>
        /// <returns>创建的界面组界面信息。</returns>
        /// <exception cref="GameFrameworkException">界面为空时抛出。</exception>
        public static UIFormInfo Create(IUIForm uiForm)
        {
            if (uiForm == null)
            {
                throw new GameFrameworkException("UI form is invalid.");
            }

            UIFormInfo uiFormInfo = ReferencePool.Acquire<UIFormInfo>();
            uiFormInfo.m_UIForm = uiForm;
            uiFormInfo.m_Paused = true;
            uiFormInfo.m_Covered = true;
            return uiFormInfo;
        }

        /// <summary>
        /// 清理界面组界面信息。
        /// </summary>
        public void Clear()
        {
            m_UIForm = null;
            m_Paused = false;
            m_Covered = false;
        }
    }
}