using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uitls
{
    /**********************************************
    * Copyright (C) 刘磊
    * 模块名: GameObjectPool.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：资源缓存池类
    ***********************************************/
    public class GameObjectPool : MonoBehaviour
    {

        private static GameObjectPool _instance;
        private Dictionary<string, object> cache = new Dictionary<string, object>();
        private static object _lock = new object();

        private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

        public static GameObjectPool Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        GameObject tools = GameObject.Find("_Tools");
                        if (tools == null)
                            tools = new GameObject("_Tools");
                        _instance = tools.AddComponent<GameObjectPool>();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 从缓存池获取资源
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <param name="asset">资源预制件</param>
        /// <param name="vector">初始位置</param>
        /// <param name="quater">旋转</param>
        /// <returns></returns>
        public GameObject GetAsset(string name, GameObject asset)
        {
            GameObject go = null;
            if (!pools.ContainsKey(name))
                CreateAsset(name, asset);
            var list = pools[name];
            var currList = list.FindAll(p => !p.activeSelf);
            if (currList.Count == 0)
            {
                CreateAsset(name, asset);
                go = pools[name].Find(p => !p.activeSelf);
            }
            else
                go = currList[0];
            go.SetActive(true);
            return go;
        }

        /// <summary>
        /// 从缓存池获取资源
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <param name="asset">资源预制件</param>
        /// <param name="vector">初始位置</param>
        /// <param name="quater">旋转</param>
        /// <returns></returns>
        public GameObject GetAsset(string name, GameObject asset, Transform parent)
        {
            GameObject go = null;
            if (!pools.ContainsKey(name))
                CreateAsset(name, asset, parent);
            var list = pools[name];
            var currList = list.FindAll(p => !p.activeSelf);
            if (currList.Count == 0)
            {
                CreateAsset(name, asset, parent);
                go = pools[name].Find(p => !p.activeSelf);
            }
            else
            {
                go = currList[0];
                go.transform.SetParent(parent);
            }
            go.SetActive(true);
            return go;
        }

        /// <summary>
        /// 从缓存池获取资源
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <param name="callback">完成回调</param>
        /// <returns></returns>
        public void GetAsset(string name, Transform parent, Action<GameObject> callback)
        {
            GameObject go = null;
            if (!pools.ContainsKey(name))
                CreateAsset(name, parent, callback);
            else
            {
                var list = pools[name];
                var currList = list.FindAll(p => !p.activeSelf);
                if (currList.Count == 0)
                    CreateAsset(name, parent, callback);
                else
                {
                    go = currList[0];
                    go.transform.SetParent(parent);
                    go.SetActive(true);
                    callback?.Invoke(go);
                }
            }
        }

        /// <summary>
        /// 从缓存池获取资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="name">资源名称</param>
        /// <returns></returns>
        public T GetAsset<T>(string name, T asset, Vector3 vector, Quaternion quater) where T : class
        {
            if (cache.ContainsKey(name))
            {
                //List<T> avliable = new List<T>();
                //查找可用容器
                return cache[name] as T; 
            }
            else
                CreateAsset(name, asset, vector, quater);
            return null;
        }

        /// <summary>
        /// 获取缓存池中对应物体的组件
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <param name="type">组件类型</param>
        /// <returns></returns>
        public Component GetAsset(Type type, string name)
        {
            if (cache.ContainsKey(name))
            {
                GameObject go = cache[name] as GameObject;
                return go.GetComponent(type);
            }
            return null;
        }

        /// <summary>
        /// 获取缓存池中对应物体的组件
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <param name="type">组件全称（包含命名空间）</param>
        /// <returns></returns>
        public Component GetAsset(string type, string name)
        {
            if (cache.ContainsKey(name))
            {
                GameObject go = cache[name] as GameObject;
                return go.GetComponent(type);
            }
            return null;
        }

        /// <summary>
        /// 通过名称获取object类型的资源（根据需要转换成对应的资源）
        /// </summary>
        /// <param name="name">资源保存的名称</param>
        /// <returns></returns>
        public object GetAsset(string name)
        {
            if (cache.ContainsKey(name))
            {
                return cache[name];
            }
            return null;
        }


        private void CreateAsset(string name, GameObject asset, Transform parent = null)
        {
            var clone = Instantiate(asset, parent);
            clone.name = name;
            clone.SetActive(false);
            if (pools.ContainsKey(name))
                pools[name].Add(clone);
            else
            {
                List<GameObject> list = new List<GameObject>();
                list.Add(clone);
                pools.Add(name, list);
            }
            
        }


        private void CreateAsset(string name, Transform parent = null, Action<GameObject> callback = null)
        {
            string path = null;

            if (AssetsManager.Instance.IsInitialized)
            {
                //从持久化路径加载资源
                AssetsManager.Instance.Load(FileTools.RuntimeAssetType.BUNDLE_PREFAB, name, false, (asset) =>
                {
                    var clone = Instantiate(asset as GameObject, parent);
                    clone.name = name;
                    clone.SetActive(false);
                    if (pools.ContainsKey(name))
                        pools[name].Add(clone);
                    else
                    {
                        List<GameObject> list = new List<GameObject>();
                        list.Add(clone);
                        pools.Add(name, list);
                    }
                    clone.SetActive(true);//取出时设置物体显示
                    callback?.Invoke(clone);
                });
            }
            else
            {
                path = "Prefabs/" + name;
                //加载资源并clone，从Resources加载
                ResMgr.Instance.LoadAssetLocalAsync<GameObject>(path, (asset) =>
                {
                    var clone = Instantiate(asset, parent);
                    clone.name = name;
                    //clone.SetActive(false);
                    if (pools.ContainsKey(name))
                        pools[name].Add(clone);
                    else
                    {
                        List<GameObject> list = new List<GameObject>();
                        list.Add(clone);
                        pools.Add(name, list);
                    }
                    //clone.SetActive(true);//取出时设置物体显示
                    callback?.Invoke(clone);
                });
            }
//#else//其他资源加载方式
           

//#endif
        }

        /// <summary>创建资源，加入缓存</summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="name">资源名称</param>
        /// <param name="asset">资源本身</param>
        private void CreateAsset<T>(string name, T asset, Vector3 vector, Quaternion quater) where T : class
        {
            if (asset == null)
            {
                Debug.LogError("初次加载资源，资源物体不能为空！！！");
                return;
            }
            UnityEngine.Object obj = asset as UnityEngine.Object;
            if (!cache.ContainsKey(name))
            {
                var clone = GameObject.Instantiate(obj, vector, quater);
                clone.name = name;
                asset = clone as T;
                cache.Add(name, asset);
            }
        }

        /// <summary>
        /// 创建资源，加入缓存
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <param name="asset">资源本身</param>
        public void CreateAsset(string name, object asset)
        {
            if (!cache.ContainsKey(name))
            {
                var clone = GameObject.Instantiate(asset as UnityEngine.Object);
                clone.name = name;
                asset = clone;
                cache.Add(name, asset);
            }
        }

        /// <summary>
        /// 销毁资源(资源隐藏处理)
        /// </summary>
        /// <param name="name">资源名称</param>
        public void DoDestroy(string name)
        {
            if (cache.ContainsKey(name) && cache[name] is GameObject)
            {
                var go = cache[name] as GameObject;
                if (go.activeSelf)
                    go.SetActive(false);
            }
        }

        /// <summary>
        /// 销毁资源
        /// </summary>
        /// <param name="go">销毁的资源物体</param>
        public void DoDestroy(GameObject go)
        {
            if (go != null)
                go.SetActive(false);
        }

        /// <summary>
        /// 销毁动作按钮
        /// </summary>
        /// <param name="name"></param>
        public void DoDestroyActions(string name)
        {
            if (cache.ContainsKey(name) && cache[name] is List<GameObject>)
            {
                var list = (List<GameObject>)cache[name];
                list.ForEach(p => p.SetActive(false));
            }
        }

        void OnDestroy()
        {
            cache.Clear();
        }

    }
}
