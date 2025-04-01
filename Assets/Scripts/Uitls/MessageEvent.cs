using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uitls
{
    /**********************************************
    * Copyright (C) 2019 讯飞幻境（北京）科技有限公司
    * 模块名: MessageEvent.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：
    ***********************************************/
    public class MessageEvent
    {

        private static Hashtable listeners = new Hashtable();

        public static void Register(string message, Action action)
        {
            var actions = listeners[message] as Action;
            if (actions != null)
            {
                listeners[message] = actions + action;
            }
            else
            {
                listeners[message] = action;
            }
        }

        public static void Register<T>(string message, Action<T> action)
        {
            var actions = listeners[message] as Action<T>;
            if (actions != null)
            {
                listeners[message] = actions + action;
            }
            else
            {
                listeners[message] = action;
            }
        }

        public static void Register<T, U>(string message, Func<T, U> action)
        {
            var actions = listeners[message] as Func<T, U>;
            if (actions != null)
            {
                listeners[message] = actions + action;
            }
            else
            {
                listeners[message] = action;
            }
        }


        public static void Send(string message)
        {
            var actions = listeners[message] as Action;
            if (actions != null)
                actions();
        }

        public static void Send<T>(string message, T param)
        {
            var actions = listeners[message] as Action<T>;
            if (actions != null)
                actions(param);
        }

        public static U Send<T, U>(string message, T param) where U : class
        {
            var actions = listeners[message] as Func<T, U>;
            if (actions != null)
                return actions(param);
            return null;
        }


        public static void Unregister(string message, Action action)
        {
            var actions = listeners[message] as Action;
            if (actions != null)
            {
                listeners[message] = actions - action;
            }
        }

        public static void Unregister<T>(string message, Action<T> action)
        {
            var actions = listeners[message] as Action<T>;
            if (actions != null)
            {
                listeners[message] = actions - action;
            }
        }

        public static void Unregister<T, U>(string message, Func<T, U> action)
        {
            var actions = listeners[message] as Func<T, U>;
            if (actions != null)
            {
                listeners[message] = actions - action;
            }
        }
    }
}
