using System;
using System.Collections;
using System.Collections.Generic;
using Uitls;
using UnityEngine;

/// <summary>
/// 资源加载
/// </summary>
public class ResMgr : MonoSingleton<ResMgr>
{

    private void Awake()
    {

    }

    /// <summary>
    /// 从资源池获取数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetAssetCache<T>(string name) where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        string path = "Assets/AssetsPackage/" + name;

        UnityEngine.Object target = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
        return target as T;
#elif UNITY_STANDALONE
        
        return null;
#endif

    }


    public void LoadAssetLocalAsync<T>(string path, Action<T> action) where T : UnityEngine.Object
    {
        T target = null;
#if UNITY_EDITOR || UNITY_STANDALONE
        //var request = Resources.LoadAsync<GameObject>("Prefabs/teacher.prefab");
        //request.completed
        StartCoroutine(LoadAssetAsync<T>(path, (obj) =>
        {
            target = obj as T;
            action(target);
        }));
#endif
    }


    IEnumerator LoadAssetAsync<T>(string path, Action<T> callback) where T : UnityEngine.Object
    {
        {
            var request = Resources.LoadAsync<T>(path);
            yield return request;
            if (request.isDone)
            {
                callback(request.asset as T);
            }
        }

    }


    public T LoadAssetLocal<T>(string path) where T : UnityEngine.Object
    {
        T target = null;
        target = Resources.Load<T>(path);
        return target;
    }
}