// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Collections.Generic;
using GameFrameX.Event.Runtime;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// UI事件订阅器
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed class UIEventSubscriber : IReference
    {
        private readonly GameFrameworkMultiDictionary<string, EventHandler<GameEventArgs>> m_DicEventHandler;

        /// <summary>
        /// 持有者
        /// </summary>
        public object Owner { get; private set; }

        private readonly List<string> m_removeList;

        public UIEventSubscriber()
        {
            m_removeList = new List<string>();
            m_DicEventHandler = new GameFrameworkMultiDictionary<string, EventHandler<GameEventArgs>>();
            Owner = null;
        }

        /// <summary>
        /// 检查订阅
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="handler">处理对象</param>
        /// <exception cref="Exception"></exception>
        public void CheckSubscribe(string id, EventHandler<GameEventArgs> handler)
        {
            if (handler == null)
            {
                throw new Exception("Event handler is invalid.");
            }

            m_DicEventHandler.Add(id, handler);
            GameEntry.GetComponent<EventComponent>().CheckSubscribe(id, handler);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="handler">处理对象</param>
        /// <exception cref="Exception"></exception>
        public void UnSubscribe(string id, EventHandler<GameEventArgs> handler)
        {
            if (!m_DicEventHandler.Remove(id, handler))
            {
                throw new Exception(Utility.Text.Format("Event '{0}' not exists specified handler.", id.ToString()));
            }

            GameEntry.GetComponent<EventComponent>().Unsubscribe(id, handler);
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="e">消息对象</param>
        public void Fire(string id, GameEventArgs e)
        {
            if (m_DicEventHandler.TryGetValue(id, out var handlers))
            {
                foreach (var eventHandler in handlers)
                {
                    try
                    {
                        eventHandler.Invoke(this, e);
                    }
                    catch (Exception exception)
                    {
                        Log.Error(exception);
                    }
                }

                GameEntry.GetComponent<EventComponent>().Fire(this, e);
            }
        }

        /// <summary>
        /// 取消所有订阅
        /// </summary>
        public void UnSubscribeAll(List<string> ignoreList = null)
        {
            if (m_DicEventHandler == null)
            {
                return;
            }

            foreach (var item in m_DicEventHandler)
            {
                if (ignoreList != null && ignoreList.Contains(item.Key))
                {
                    continue;
                }

                m_removeList.Add(item.Key);
                foreach (var eventHandler in item.Value)
                {
                    GameEntry.GetComponent<EventComponent>().Unsubscribe(item.Key, eventHandler);
                }
            }

            if (ignoreList == null)
            {
                m_DicEventHandler.Clear();
            }
            else
            {
                foreach (var key in m_removeList)
                {
                    m_DicEventHandler.RemoveAll(key);
                }
            }
        }

        /// <summary>
        /// 创建事件订阅器
        /// </summary>
        /// <param name="owner">持有者</param>
        /// <returns></returns>
        public static UIEventSubscriber Create(object owner)
        {
            var eventSubscriber = ReferencePool.Acquire<UIEventSubscriber>();
            eventSubscriber.Owner = owner;

            return eventSubscriber;
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void Clear()
        {
            m_DicEventHandler.Clear();
            m_removeList.Clear();
            Owner = null;
        }
    }
}