// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace GameFrameX.UI.Runtime
{
    public partial class UIComponent
    {
        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        public void CloseUIForm(int serialId, bool isNowRecycle = false)
        {
            m_UIManager.CloseUIForm(serialId, isNowRecycle);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        public void CloseUIForm(int serialId, object userData, bool isNowRecycle = false)
        {
            m_UIManager.CloseUIForm(serialId, userData, isNowRecycle);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        public void CloseUIForm(IUIForm uiForm, bool isNowRecycle = false)
        {
            m_UIManager.CloseUIForm(uiForm, isNowRecycle);
        }

        /// <summary>
        /// 关闭界面。
        /// 该函数只适用于界面只有一个的情况.因为当找到一个目标对象之后就会立即终止
        /// </summary>
        /// <typeparam name="T">关闭界面的类型</typeparam>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        public void CloseUIForm<T>(object userData = null, bool isNowRecycle = false) where T : IUIForm
        {
            m_UIManager.CloseUIForm<T>(userData, isNowRecycle);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        public void CloseUIForm(IUIForm uiForm, object userData, bool isNowRecycle = false)
        {
            m_UIManager.CloseUIForm(uiForm, userData, isNowRecycle);
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        public void CloseAllLoadedUIForms()
        {
            m_UIManager.CloseAllLoadedUIForms();
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseAllLoadedUIForms(object userData)
        {
            m_UIManager.CloseAllLoadedUIForms(userData);
        }

        /// <summary>
        /// 关闭所有正在加载的界面。
        /// </summary>
        public void CloseAllLoadingUIForms()
        {
            m_UIManager.CloseAllLoadingUIForms();
        }
    }
}