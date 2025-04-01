using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uitls
{
    public class DebugHelper : MonoBehaviour
    {

        /// <summary>
        /// log日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(object msg)
        {
#if UNITY_EDITOR
            Debug.Log(msg);
#endif
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"></param>
        public static void LogWarning(object msg)
        {
#if UNITY_EDITOR
            Debug.LogWarning(msg);
#endif
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void LogError(object msg)
        {
#if UNITY_EDITOR
            Debug.LogError(msg);
#endif
        }

        /// <summary>
        /// 格式化日志
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void LogFormat(string format, params object[] args)
        {
#if UNITY_EDITOR
            Debug.LogFormat(format, args);
#endif
        }


    }
}