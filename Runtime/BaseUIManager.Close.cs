// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Collections.Generic;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// 界面管理器。
    /// </summary>
    public partial class BaseUIManager
    {
        protected EventHandler<CloseUIFormCompleteEventArgs> m_CloseUIFormCompleteEventHandler;

        /// <summary>
        /// 关闭界面完成事件。
        /// </summary>
        public event EventHandler<CloseUIFormCompleteEventArgs> CloseUIFormComplete
        {
            add { m_CloseUIFormCompleteEventHandler += value; }
            remove { m_CloseUIFormCompleteEventHandler -= value; }
        }

        /// <summary>
        /// 回收界面实例对象。
        /// </summary>
        /// <param name="uiForm"></param>
        /// <param name="isDispose">是否销毁释放</param>
        protected abstract void RecycleUIForm(IUIForm uiForm, bool isDispose = false);

        /// <summary>
        /// 回收界面实例对象到实例池。
        /// </summary>
        /// <param name="uiForm">要回收的界面实例对象。</param>
        protected abstract void RecycleToPoolUIForm(IUIForm uiForm);

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        public void CloseUIForm(int serialId, bool isNowRecycle = false)
        {
            CloseUIForm(serialId, null, isNowRecycle);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        public void CloseUIForm(int serialId, object userData, bool isNowRecycle = false)
        {
            IUIForm uiForm = GetUIForm(serialId);
            if (uiForm == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not find UI form '{0}'.", serialId));
            }

            CloseUIForm(uiForm, userData, isNowRecycle);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        public void CloseUIForm(IUIForm uiForm, bool isNowRecycle = false)
        {
            CloseUIForm(uiForm, null, isNowRecycle);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        /// <typeparam name="T"></typeparam>
        public void CloseUIForm<T>(object userData, bool isNowRecycle = false) where T : IUIForm
        {
            var fullName = typeof(T).FullName;
            IUIForm[] uiForms = GetAllLoadedUIForms();
            foreach (IUIForm uiForm in uiForms)
            {
                if (uiForm.FullName != fullName)
                {
                    continue;
                }

                if (!HasUIFormFullName(uiForm.FullName))
                {
                    continue;
                }

                CloseUIForm(uiForm, userData, isNowRecycle);
                break;
            }
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isNowRecycle">是否立即回收界面,默认是否</param>
        public void CloseUIForm(IUIForm uiForm, object userData, bool isNowRecycle = false)
        {
            GameFrameworkGuard.NotNull(uiForm, nameof(uiForm));

            if (uiForm.IsDisableClosing)
            {
                return;
            }

            GameFrameworkGuard.NotNull(uiForm.UIGroup, nameof(uiForm.UIGroup));
            UIGroup uiGroup = (UIGroup)uiForm.UIGroup;
            var serialId = uiForm.SerialId;
            uiGroup.RemoveUIForm(uiForm);
            uiForm.OnClose(m_IsShutdown, userData);
            uiGroup.Refresh();
            if (IsLoadingUIForm(serialId))
            {
                m_UIFormsToReleaseOnLoad[serialId] = uiForm;
                m_UIFormsBeingLoaded.Remove(serialId);
            }

            bool isRecycled = false;
            if (isNowRecycle && !uiForm.IsDisableRecycling)
            {
                isRecycled = true;
                RecycleUIForm(uiForm, true);
            }

            if (uiForm.IsDisableRecycling == false && isRecycled == false)
            {
                m_UIFormsToReleaseOnLoad[serialId] = uiForm;
                RecycleToPoolUIForm(uiForm);
            }

            if (m_CloseUIFormCompleteEventHandler != null)
            {
                CloseUIFormCompleteEventArgs closeUIFormCompleteEventArgs = CloseUIFormCompleteEventArgs.Create(uiForm.SerialId, uiForm.UIFormAssetName, uiGroup, userData);
                m_CloseUIFormCompleteEventHandler(this, closeUIFormCompleteEventArgs);
            }
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        public void CloseAllLoadedUIForms()
        {
            CloseAllLoadedUIForms(null);
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public void CloseAllLoadedUIForms(object userData)
        {
            IUIForm[] uiForms = GetAllLoadedUIForms();
            foreach (IUIForm uiForm in uiForms)
            {
                if (!HasUIForm(uiForm.SerialId))
                {
                    continue;
                }

                CloseUIForm(uiForm, userData);
            }
        }

        /// <summary>
        /// 关闭所有正在加载的界面。
        /// </summary>
        public void CloseAllLoadingUIForms()
        {
            foreach (KeyValuePair<int, string> uiFormBeingLoaded in m_UIFormsBeingLoaded)
            {
                m_UIFormsToReleaseOnLoad[uiFormBeingLoaded.Key] = GetUIForm(uiFormBeingLoaded.Key);
            }

            m_UIFormsBeingLoaded.Clear();
        }

        /// <summary>
        /// 释放所有已加载的界面。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public void ReleaseAllLoadedUIForms(object userData)
        {
            foreach (var keyValuePair in m_UIFormsToReleaseOnLoad)
            {
                var uiForm = keyValuePair.Value;
                if (uiForm != null)
                {
                    RecycleUIForm(uiForm, !m_IsRecycleToPool);
                }
            }

            m_UIFormsToReleaseOnLoad.Clear();
        }
    }
}