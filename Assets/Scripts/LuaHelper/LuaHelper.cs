using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
using FileTools;
//using FileTools;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: LuaHelper.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：lua帮助类
***********************************************/
namespace HotUpdateModule
{
    public class LuaHelper
    {

        //本类静态实例
        private static LuaHelper _Instance;
        //Lua环境
        private LuaEnv _luaEnv = new LuaEnv();
        //缓存lua文件名称与对应的lua信息
        private Dictionary<string, byte[]> _DicLuaFileArray = new Dictionary<string, byte[]>();

        private LuaHelper()
        {
            //私有构造函数
            _luaEnv.AddLoader(customLoader);
        }


        /// <summary>
        /// 得到帮助类实例
        /// </summary>
        /// <returns></returns>
        public static LuaHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LuaHelper();
                }
                return _Instance;
            }
            
        }

        /// <summary>
        /// 得到lua环境
        /// </summary>
        /// <returns></returns>
        public LuaEnv GetLuaEnv()
        {
            if (_luaEnv != null)
                return _luaEnv;
            else
            {
                Debug.LogError(GetType() + "/GetLuaEnv()/出现严重错误！  _luaEnv = null!!!");
                return null;
            }
        }

        /// <summary>
        /// 执行lua代码
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="chunkName"></param>
        /// <param name="env"></param>
        public void DoString(string chunk, string chunkName = "chunk", LuaTable env = null)
        {
            _luaEnv.DoString(chunk, chunkName, env);
        }

        /// <summary>
        /// 调用lua中的方法
        /// </summary>
        /// <param name="luaScriptName">lua脚本明层</param>
        /// <param name="luaMethodName">lua的方法</param>
        /// <param name="args">可变object类型数组</param>
        /// <returns>对象数组</returns>
        public object[] CallLuaFuntion(string luaScriptName, string luaMethodName, params object[] args)
        {
            LuaTable luaTab = _luaEnv.Global.Get<LuaTable>(luaScriptName);
            if (luaTab != null)
            {
                LuaFunction luaFun = luaTab.Get<LuaFunction>(luaMethodName);
                if (luaFun != null)
                    return luaFun.Call(args);
            }
            return null;
        }


        /// <summary>
        /// 自定义调取lua文件内容
        /// </summary>
        /// <param name="fileName">lua文件名称</param>
        /// <returns></returns>
        private byte[] customLoader(ref string fileName)
        {
            //获取lua所在目录
            string luaPath =
#if UNITY_EDITOR || UNITY_STANDALONE
                StrConst.Instance.LocalFileAddress + StrConst.PATH_LUAASSET_FOLDER;
#elif UNITY_ANDROID || UNITY_IOS
                StrConst.Instance.LocalFileAddress + StrConst.PATH_LUAASSET_FOLDER;/*Application.dataPath + "/Scripts/LuaScripts/";*/
#endif
            //文件不存在，返回空
            if (!Directory.Exists(luaPath)) return null;

            //缓存判断处理：根据lua文件路径，获取lua的内容
            if (_DicLuaFileArray.ContainsKey(fileName))
            {
                //如果在缓存中可以查找成功，则直接返回结果。
                return _DicLuaFileArray[fileName];
            }
            else
            {
                return ProcessDIR(new DirectoryInfo(luaPath), fileName);
            }
        }

        /// <summary>
        /// 根据lua文件名称，递归取的lua内容信息
        /// </summary>
        /// <param name="fileSysInfo">lua的文件信息</param>
        /// <param name="fileName">查询的lua文件名称</param>
        /// <returns></returns>
        private byte[] ProcessDIR(FileSystemInfo fileSysInfo, string fileName)
        {
           DirectoryInfo dirInfo =  fileSysInfo as DirectoryInfo;
           FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

            foreach (FileSystemInfo item in files)
            {
                FileInfo fileInfo = item as FileInfo;
                //表示一个文件夹
                if (fileInfo == null)
                {
                    //递归处理
                    ProcessDIR(item, fileName);
                }
                else//表示文件本身
                {
                    //得到文件本身，去掉后缀
                    string tmpName = item.Name.Split('.')[0];
                    if (item.Extension.Equals(".meta") || !tmpName.Equals(fileName))
                        continue;
                    //读取lua文件内容字节信息
                    byte[] bytes = File.ReadAllBytes(fileInfo.FullName);
                    //添加到缓存集合中
                    _DicLuaFileArray.Add(fileName, bytes);
                    return bytes;
                }
            }
            return null;
        }//ProcessDIR_end


        public void AddBaseLuaUIForm(GameObject go)
        {
            go.AddComponent<BaseLuaMappingCSharp>();
        }


        public bool RayFunction(Ray ray, out RaycastHit hit)
        {
            return Physics.Raycast(ray, out hit);
        }

        public bool RayFunctionLayer(Ray ray, out RaycastHit hit, float distance, int layer)
        {
            return Physics.Raycast(ray, out hit, distance, layer);
        }

    }//CLass_end
}
