using System.Collections;

namespace Uitls
{
    public class ManagerEvent
    {
        public static string MSG_DiviceInfo = "MSG_DiviceInfo";
        public static string MSG_ProgressBar = "MSG_ProgressBar";
        public static string MSG_ServerConnection = "MSG_ServerConnection";
        public static string LOADRES = "Load_Res";
        public static string LOADSCENE = "Load_Scene";

        private static Hashtable listeners = new Hashtable();

        public delegate void Handler(params object[] args);

        public delegate void Handler<T>(T args);

        public delegate object HandlerFunc(params object[] args);

        /// <summary>
        /// 消息事件注册
        /// </summary>
        /// <param name="message">事件名称</param>
        /// <param name="action">事件对应的方法</param>
        public static void Register(string message, Handler action)
        {
            var actions = listeners[message] as Handler;
            if (actions != null)
            {
                listeners[message] = actions + action;
            }
            else
            {
                listeners[message] = action;
            }
        }

        public static void RegisterFunc(string message, HandlerFunc action)
        {
            var actions = listeners[message] as HandlerFunc;
            if (actions != null)
                listeners[message] = actions + action;
            else
                listeners[message] = action;
        }

        public static void Register<T>(string message, Handler<T> action)
        {
            var actions = listeners[message] as Handler<T>;
            if (actions != null)
                listeners[message] = actions + action;
            else
                listeners[message] = action;
        }

        /// <summary>
        /// 消息事件注销
        /// </summary>
        /// <param name="message">事件名称</param>
        /// <param name="action">事件方法</param>
        public static void Unregister(string message, Handler action)
        {
            var actions = listeners[message] as Handler;
            if (actions != null)
            {
                listeners[message] = actions - action;
            }
        }

        /// <summary>
        /// 消息事件注销
        /// </summary>
        /// <param name="message">事件名称</param>
        /// <param name="action">事件方法</param>
        public static void UnregisterFunc(string message, HandlerFunc action)
        {
            var actions = listeners[message] as HandlerFunc;
            if (actions != null)
            {
                listeners[message] = actions - action;
            }
        }

        public static void Unregister<T>(string message, Handler<T> action)
        {
            var actions = listeners[message] as Handler<T>;
            if (actions != null)
                listeners[message] = actions - action;
        }

        /// <summary>
        /// 消息事件调用
        /// </summary>
        /// <param name="message">消息事件名称</param>
        /// <param name="args">事件参数</param>
        public static void Call(string message, params object[] args)
        {
            var actions = listeners[message] as Handler;
            if (actions != null)
            {
                actions(args);
            }
        }

        /// <summary>
        /// 消息事件调用
        /// </summary>
        /// <param name="message">消息事件名称</param>
        /// <param name="args">事件参数</param>
        public static object SendFunc(string message, params object[] args)
        {
            var actions = listeners[message] as HandlerFunc;
            if (actions != null)
            {
                return actions(args);
            }
            return null;
        }

        public static void Send<T>(string message, T args)
        {
            var actions = listeners[message] as Handler<T>;
            if (actions != null)
                actions(args);
        }

        /// <summary>
        /// 清空事件
        /// </summary>
        public static void Clear()
        {
            listeners.Clear();
        }
    }
}