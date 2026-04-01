// ==========================================================================================
//   GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//   GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//   均受中华人民共和国及相关国际法律法规保护。
//   are protected by the laws of the People's Republic of China and relevant international regulations.
//   使用本项目须严格遵守相应法律法规及开源许可证之规定。
//   Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
//   本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//   This project is dual-licensed under the MIT License and Apache License 2.0,
//   完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//   please refer to the LICENSE file in the root directory of the source code for the full license text.
//   禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//   It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//   侵犯他人合法权益等法律法规所禁止的行为！
//   or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//   因基于本项目二次开发所产生的一切法律纠纷与责任，
//   Any legal disputes and liabilities arising from secondary development based on this project
//   本项目组织与贡献者概不承担。
//   shall be borne solely by the developer; the project organization and contributors assume no responsibility.
//   GitHub 仓库：https://github.com/GameFrameX
//   GitHub Repository: https://github.com/GameFrameX
//   Gitee  仓库：https://gitee.com/GameFrameX
//   Gitee Repository:  https://gitee.com/GameFrameX
//   CNB  仓库：https://cnb.cool/GameFrameX
//   CNB Repository:  https://cnb.cool/GameFrameX
//   官方文档：https://gameframex.doc.alianblank.com/
//   Official Documentation: https://gameframex.doc.alianblank.com/
//  ==========================================================================================

using System;
using System.Collections.Generic;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    public partial class UIComponent
    {
        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <remarks>
        /// Gets a UI form by serial ID.
        /// </remarks>
        /// <param name="serialId">界面序列编号。 / The UI form serial ID.</param>
        /// <returns>要获取的界面。 / The UI form to get.</returns>
        [UnityEngine.Scripting.Preserve]
        public IUIForm GetUIForm(int serialId)
        {
            return m_UIManager.GetUIForm(serialId);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <remarks>
        /// Gets a UI form by asset name.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。 / The UI form asset name.</param>
        /// <returns>要获取的界面。 / The UI form to get.</returns>
        [UnityEngine.Scripting.Preserve]
        public IUIForm GetUIForm(string uiFormAssetName)
        {
            return m_UIManager.GetUIForm(uiFormAssetName);
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <remarks>
        /// Gets all UI forms by asset name.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。 / The UI form asset name.</param>
        /// <returns>要获取的界面数组。 / The array of UI forms to get.</returns>
        [UnityEngine.Scripting.Preserve]
        public IUIForm[] GetUIForms(string uiFormAssetName)
        {
            var uiForms = m_UIManager.GetUIForms(uiFormAssetName);
            var uiFormImpls = new IUIForm[uiForms.Length];
            for (int i = 0; i < uiForms.Length; i++)
            {
                uiFormImpls[i] = uiForms[i];
            }

            return uiFormImpls;
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <remarks>
        /// Gets all UI forms by asset name.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。 / The UI form asset name.</param>
        /// <param name="results">要获取的界面列表。 / The list of UI forms to get.</param>
        [UnityEngine.Scripting.Preserve]
        public void GetUIForms(string uiFormAssetName, List<IUIForm> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
                return;
            }

            results.Clear();
            m_UIManager.GetUIForms(uiFormAssetName, m_InternalUIFormResults);
            foreach (var uiForm in m_InternalUIFormResults)
            {
                results.Add(uiForm);
            }
        }

        /// <summary>
        /// 根据界面逻辑类型获取界面列表，会返回所有符合条件的集合。
        /// </summary>
        /// <remarks>
        /// Gets the list of UI forms by logic type, returns all matching instances.
        /// </remarks>
        /// <typeparam name="T">界面逻辑类型。 / The UI form logic type.</typeparam>
        /// <returns>符合条件的界面列表。 / The list of matching UI forms.</returns>
        public List<T> GetLoadedList<T>() where T : class, IUIForm
        {
            var fullName = typeof(T).FullName;
            var uiForms = m_UIManager.GetAllLoadedUIForms();
            var results = new List<T>();
            foreach (var uiForm in uiForms)
            {
                if (uiForm.FullName == fullName)
                {
                    results.Add(uiForm as T);
                }
            }

            return results;
        }

        /// <summary>
        /// 获取已加载且正在显示的UI。
        /// </summary>
        /// <remarks>
        /// Gets a loaded and showing UI form by type.
        /// </remarks>
        /// <param name="type">UI的具体类型。 / The type of the UI form.</param>
        /// <returns>返回已加载且正在显示的UI实例，如果未找到则返回null。 / The loaded and showing UI form instance, or null if not found.</returns>
        [UnityEngine.Scripting.Preserve]
        public IUIForm GetLoadedAndShowing(Type type)
        {
            var fullName = type.FullName;
            var uiForms = m_UIManager.GetAllLoadedUIForms();
            foreach (var uiForm in uiForms)
            {
                if (uiForm.FullName == fullName && uiForm.Visible && uiForm.Available)
                {
                    return uiForm;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取已加载且正在显示的UI。
        /// </summary>
        /// <remarks>
        /// Gets a loaded and showing UI form by type.
        /// </remarks>
        /// <typeparam name="T">UI的具体类型。 / The type of the UI form.</typeparam>
        /// <returns>返回已加载且正在显示的UI实例，如果未找到则返回null。 / The loaded and showing UI form instance, or null if not found.</returns>
        public T GetLoadedAndShowing<T>() where T : class, IUIForm
        {
            var fullName = typeof(T).FullName;
            var uiForms = m_UIManager.GetAllLoadedUIForms();
            foreach (var uiForm in uiForms)
            {
                if (uiForm.FullName == fullName && uiForm.Visible && uiForm.Available)
                {
                    return uiForm as T;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取已加载且正在显示的UI。
        /// </summary>
        /// <remarks>
        /// Checks if a loaded and showing UI form exists by asset name.
        /// </remarks>
        /// <param name="uiFormAssetName">界面资源名称。 / The UI form asset name.</param>
        /// <returns>是否存在已加载且正在显示的UI。 / Whether a loaded and showing UI form exists.</returns>
        [UnityEngine.Scripting.Preserve]
        public bool HasLoadedAndShowing(string uiFormAssetName)
        {
            var uiForms = m_UIManager.GetAllLoadedUIForms();
            foreach (var uiForm in uiForms)
            {
                if (uiForm.UIFormAssetName == uiFormAssetName && uiForm.Visible && uiForm.Available)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 根据界面逻辑类型获取界面。只要找到任意的一个即返回。
        /// </summary>
        /// <remarks>
        /// Gets a loaded UI form by logic type. Returns the first match found.
        /// </remarks>
        /// <param name="type">逻辑界面类型。 / The UI form logic type.</param>
        /// <returns>返回已加载的UI实例，如果未找到则返回null。 / The loaded UI form instance, or null if not found.</returns>
        [UnityEngine.Scripting.Preserve]
        public IUIForm GetLoaded(Type type)
        {
            var fullName = type.FullName;
            var uiForms = m_UIManager.GetAllLoadedUIForms();
            foreach (var uiForm in uiForms)
            {
                if (uiForm.FullName == fullName)
                {
                    return uiForm;
                }
            }

            return null;
        }

        /// <summary>
        /// 根据界面逻辑类型获取界面。只要找到任意的一个即返回。
        /// </summary>
        /// <remarks>
        /// Gets a loaded UI form by logic type. Returns the first match found.
        /// </remarks>
        /// <typeparam name="T">逻辑界面类型。 / The UI form logic type.</typeparam>
        /// <returns>返回已加载的UI实例，如果未找到则返回null。 / The loaded UI form instance, or null if not found.</returns>
        public T GetLoaded<T>() where T : class, IUIForm
        {
            var fullName = typeof(T).FullName;
            var uiForms = m_UIManager.GetAllLoadedUIForms();
            foreach (var uiForm in uiForms)
            {
                if (uiForm.FullName == fullName)
                {
                    return uiForm as T;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <remarks>
        /// Gets all loaded UI forms.
        /// </remarks>
        /// <returns>所有已加载的界面。 / All loaded UI forms.</returns>
        [UnityEngine.Scripting.Preserve]
        public IUIForm[] GetAllLoadedUIForms()
        {
            var uiForms = m_UIManager.GetAllLoadedUIForms();
            var uiFormImpls = new IUIForm[uiForms.Length];
            for (int i = 0; i < uiForms.Length; i++)
            {
                uiFormImpls[i] = uiForms[i];
            }

            return uiFormImpls;
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <remarks>
        /// Gets all loaded UI forms.
        /// </remarks>
        /// <param name="results">所有已加载的界面列表。 / The list of all loaded UI forms.</param>
        [UnityEngine.Scripting.Preserve]
        public void GetAllLoadedUIForms(List<IUIForm> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
                return;
            }

            results.Clear();
            m_UIManager.GetAllLoadedUIForms(m_InternalUIFormResults);
            foreach (var uiForm in m_InternalUIFormResults)
            {
                results.Add(uiForm);
            }
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <remarks>
        /// Gets all loading UI form serial IDs.
        /// </remarks>
        /// <returns>所有正在加载界面的序列编号。 / All loading UI form serial IDs.</returns>
        [UnityEngine.Scripting.Preserve]
        public int[] GetAllLoadingUIFormSerialIds()
        {
            return m_UIManager.GetAllLoadingUIFormSerialIds();
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <remarks>
        /// Gets all loading UI form serial IDs.
        /// </remarks>
        /// <param name="results">所有正在加载界面的序列编号列表。 / The list of all loading UI form serial IDs.</param>
        [UnityEngine.Scripting.Preserve]
        public void GetAllLoadingUIFormSerialIds(List<int> results)
        {
            m_UIManager.GetAllLoadingUIFormSerialIds(results);
        }
    }
}