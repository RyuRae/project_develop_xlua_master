
using System.Collections.Generic;
using Uitls;
using UnityEngine;

namespace DevelopEngine
{
    public class Configuration : MonoSingleton<Configuration>
    {
        private static WWW www;

        public static string path;
        public static string mFilePath;
        private static bool isDone = false;
        public static bool IsDone
        {
            get
            {
                if (www != null && www.isDone)
                {
                    Load(www.bytes);
                    isDone = true;
                    return isDone;
                }
                return isDone;
            }
        }

        private static bool isComplete = false;
        public static bool IsComplete
        {
            get
            {
                if (www != null && www.isDone)
                {
                    LoadCourse(www.bytes);
                    isComplete = true;
                    return isComplete;
                }

                return isComplete;
            }
        }

        protected static Dictionary<string, Dictionary<string, string>> mDictionary = new Dictionary<string, Dictionary<string, string>>();

        protected static Dictionary<string, Dictionary<string, string>> mConfig = new Dictionary<string, Dictionary<string, string>>();

        public static void LoadConfig(string configPath)
        {
            path = Application.dataPath + "/StreamingAssets" + configPath;

            if (isDone)
                return;

            if (www == null)
            {
#if UNITY_EDITOR
				www = new WWW("file://" + Application.dataPath + "/StreamingAssets" + configPath);
#elif UNITY_STANDALONE_WIN
				www = new WWW("file://" + Application.dataPath + "/StreamingAssets" + configPath);
#elif UNITY_IPHONE
				www = new WWW("file://" + Application.dataPath + "/Raw" + configPath);	
#elif UNITY_ANDROID
				www = new WWW("jar:file://" + Application.dataPath + "!/assets" + configPath);
#endif
            }
        }


        public static void LoadCourseConfig(string filePath)
        {
            mFilePath = filePath;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            www = new WWW("file://" + filePath);
#elif UNITY_IPHONE
				www = new WWW("file://" + filePath);	
#elif UNITY_ANDROID
				www = new WWW("jar:file://" + filePath);
#endif
        }

        static void Load(byte[] bytes)
        {
            if (isDone)
                return;

            ConfigReader reader = new ConfigReader(bytes);

            mDictionary = reader.ReadDictionary();

            www = null;
        }

        static void LoadCourse(byte[] bytes)
        {
            if (isComplete)
                return;

            ConfigReader reader = new ConfigReader(bytes);

            mConfig = reader.ReadDictionary();

            www = null;
        }


        public static void Load(TextAsset asset)
        {
            ConfigReader reader = new ConfigReader(asset);
            mDictionary = reader.ReadDictionary();
            //NGUITools.Broadcast("OnConfig", this);
            isDone = true;
        }

        //public Dictionary<string, Dictionary<string, string>> ConfigDictionary {get {return mDictionary;}}

        public static string Get(string mainKey, string subKey)
        {
            if (mDictionary.ContainsKey(mainKey) && mDictionary[mainKey].ContainsKey(subKey))
                return mDictionary[mainKey][subKey];

            return mainKey + "." + subKey;
        }

        public static Dictionary<string, string> Get(string mainKey)
        {
            if (mDictionary.ContainsKey(mainKey))
                return mDictionary[mainKey];

            return null;
        }

        public static int GetInt(string mainKey, string subKey)
        {
            int ret;
            int.TryParse(Get(mainKey, subKey), out ret);
            return ret;
        }

        public static float GetFloat(string mainKey, string subKey)
        {
            float ret;
            float.TryParse(Get(mainKey, subKey), out ret);
            return ret;
        }

        public static int GetChildCount(string mainKey)
        {
            if (mDictionary.ContainsKey(mainKey))
            {
                int ChildCount = mDictionary[mainKey].Count;
                return ChildCount;
            }
            return 0;
        }

        public static string GetContent(string mainKey, string subKey)
        {
            string ret = Get(mainKey, subKey);
            if (ret.StartsWith("\"")) ret = ret.Substring(1, ret.Length - 1);
            if (ret.EndsWith(";")) ret = ret.Substring(0, ret.Length - 2);
            if (ret.EndsWith("\"")) ret = ret.Substring(0, ret.Length - 2);
            return ret;
        }

        public static void SetContent(string mainKey, string subKey, string text)
        {
            ConfigWriter.WriteDictionary(path, mainKey, subKey, text);
        }

        public static void SetNewSubkey(string mainKey, string subKey, string text)
        {
            ConfigWriter.WriteDictionary(mFilePath, mainKey, subKey, text);
        }
    }
}
