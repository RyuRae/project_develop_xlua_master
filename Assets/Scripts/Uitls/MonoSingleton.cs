using UnityEngine;
using System.Collections;
using DevelopEngine;

namespace Uitls
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance = null;
        private static bool _applicationIsQuitting = false;

        public static T Instance
        {
            get
            {
                if (instance == null && !_applicationIsQuitting)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                    if (instance == null && !_applicationIsQuitting)
                    {
                        instance = new GameObject("_" + typeof(T).Name).AddComponent<T>();
                        DontDestroyOnLoad(instance);
                    }
                    if (instance == null)
                        Debugger.LogError("Failed to create instance of " + typeof(T).FullName + ".");
                }
                return instance;
            }
        }

        private static T Create()
        {

            var go = new GameObject(typeof(T).Name, typeof(T));
            DontDestroyOnLoad(go);
            return go.GetComponent<T>();
        }

        void OnApplicationQuit()
        {
            if (instance == null) return;
            Destroy(instance.gameObject);
            instance = null;
        }

        public static T CreateInstance()
        {
            if (Instance != null) Instance.OnCreate();
            return Instance;
        }

        protected virtual void OnCreate()
        {

        }

        public virtual void DoDestory()
        {
            if (instance == null) return;
            Destroy(instance.gameObject);
            instance = null;
        }

        protected virtual void OnDestory()
        {
            _applicationIsQuitting = true;
        }
    }
}