using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using XLua;
using System;

namespace Uitls
{
    //[LuaCallCSharp]
    public abstract class Global
    {
        public static string LoadUIName = "";
        public static string LoadSceneName = "";
        public static bool isBattle = false;

        public static bool Contain3DScene = false;

        public static string PlayerName = "";
        private static Dictionary<string, GameObject> clones = new Dictionary<string, GameObject>();
        private static Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
        private static Dictionary<string, GameObject> models = new Dictionary<string, GameObject>();
        //private static Dictionary<Transform, List<T>> cache = new Dictionary<Transform, List<T>>();


        /// <summary>
        /// 查找gameObject
        /// </summary>
        /// <param name="trans">开始查找的父级</param>
        /// <param name="childName">查找的物体名称</param>
        /// <returns></returns>
        public static GameObject FindChild(Transform trans, string childName)
        {
            Transform child = trans.Find(childName);
            if (child != null)
            {
                return child.gameObject;
            }
            int count = trans.childCount;
            GameObject go = null;
            for (int i = 0; i < count; ++i)
            {
                child = trans.GetChild(i);
                go = FindChild(child, childName);
                if (go != null)
                    return go;
            }
            return null;
        }

        /// <summary>
        /// 查找满足条件的物体
        /// </summary>
        /// <typeparam name="T">物体类型</typeparam>
        /// <param name="trans">开始查找的父物体</param>
        /// <param name="childName">物体名称</param>
        /// <returns></returns>
        public static T FindChild<T>(Transform trans, string childName) where T : Component
        {
            GameObject go = FindChild(trans, childName);
            if (go == null)
                return null;
            return go.GetComponent<T>();
        }

        /// <summary>
        /// 查找指定物体上的单个组件
        /// </summary>
        /// <param name="type">组件类型</param>
        /// <param name="trans">开始查找的父物体</param>
        /// <param name="childName">要查找的物体名称</param>
        /// <returns></returns>
        public static Component FindChild(Type type, Transform trans, string childName)
        {
            GameObject go = FindChild(trans, childName);
            if (go != null)
            {
                return go.GetComponent(type);
            }
            return null;
        }

        /// <summary>
        /// 查找指定物体上多个相同类型的组件
        /// </summary>
        /// <param name="type">组件类型</param>
        /// <param name="trans">开始查找的父物体</param>
        /// <param name="childName">要查找的物体名称</param>
        /// <returns></returns>
        public static Component[] FindChildComponents(Type type, Transform trans, string childName)
        {
            GameObject go = FindChild(trans, childName);
            if (go != null)
            {
                return go.GetComponents(type);
            }
            return null;
        }

        /// <summary>
        /// 查找指定物体上多个相同类型的组件
        /// </summary>
        /// <param name="trans">开始查找的父物体</param>
        /// <param name="childName">要查找的物体名称</param>
        /// <returns></returns>
        public static T[] FindChildComponents<T>(Transform trans, string childName)
        {
            GameObject go = FindChild(trans, childName);
            if (go != null)
            {
                return go.GetComponents<T>();
            }
            return null;
        }

        /// <summary>
        /// 查找子物体中所有满足要求的组件
        /// </summary>
        /// <param name="type">组件类型</param>
        /// <param name="trans">需要查找的物体</param>
        /// <returns></returns>
        public static Component[] FindComponents(Type type, Transform trans)
        {
            List<Component> compontens = new List<Component>();
            PrecessFind(type, trans, compontens);
            return compontens.ToArray();
        }

        //递归查找组件
        private static void PrecessFind(Type type, Transform trans, List<Component> compontens)
        {
            Component com = trans.GetComponent(type);
            if (com != null)
            {
                compontens.Add(com);
            }
            if (trans.childCount > 0)
            {
                for (int i = 0; i < trans.childCount; i++)
                {
                    PrecessFind(type, trans.GetChild(i), compontens);
                }
            }
        }

        /// <summary>
        /// 查找满足条件的所有物体
        /// </summary>
        /// <typeparam name="T">要查找的组件</typeparam>
        /// <param name="trans">查找开始的层级</param>
        public static T[] FindComponents<T>(Transform trans)
        {
            List<T> compontens = new List<T>();
            ProcessFind(trans, compontens);
            return compontens.ToArray();
        }

        //递归查找
        private static void ProcessFind<T>(Transform trans, List<T> compontens)
        {
            T com = trans.GetComponent<T>();
            if (com != null)
            {
                compontens.Add(com);
            }
            if (trans.childCount > 0)
            {
                for (int i = 0; i < trans.childCount; i++)
                {
                    ProcessFind<T>(trans.GetChild(i), compontens);
                }
            }
        }


        /// <summary>
        /// 获取时间格式字符串，显示mm:ss
        /// </summary>
        /// <returns>The minute time.</returns>
        /// <param name="time">Time.</param>
        public static string GetMinuteTime(float time)
        {
            int mm, ss;
            string stime = "0:00";
            if (time <= 0) return stime;
            mm = (int)time / 60;
            ss = (int)time % 60;
            if (mm > 60)
                stime = "59:59";
            else if (mm < 10 && ss >= 10)
            {
                stime = "0" + mm + ":" + ss;
            }
            else if (mm < 10 && ss < 10)
            {
                stime = "0" + mm + ":0" + ss;
            }
            else if (mm >= 10 && ss < 10)
            {
                stime = mm + ":0" + ss;
            }
            else
            {
                stime = mm + ":" + ss;
            }
            return stime;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="orgin"></param>
        /// <returns></returns>
        public static string UpperCaseFirstChar(string orgin)
        {
            if (string.IsNullOrEmpty(orgin))
            {
                return string.Empty;
            }
            char[] temp = orgin.ToCharArray();
            temp[0] = char.ToUpper(temp[0]);
            string result = new string(temp);
            return result;
        }
    }
}