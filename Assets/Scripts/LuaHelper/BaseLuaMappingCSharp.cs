using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace HotUpdateModule
{
    /**********************************************
    * Copyright (C) 2019 讯飞幻境（北京）科技有限公司
    * 模块名: BaseLuaMappingCSharp.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：
    ***********************************************/
    public class BaseLuaMappingCSharp : MonoBehaviour
    {

        [CSharpCallLua]
        public delegate void MappingCSharpAction(LuaTable luaTable, GameObject go);

        private LuaEnv luaEnv;
        private LuaTable luaTable;
        private MappingCSharpAction luaAwake;
        private MappingCSharpAction luaStart;
        private MappingCSharpAction luaUpdate;
        private MappingCSharpAction luaDestroy;
        private MappingCSharpAction luaEnable;     
        private MappingCSharpAction luaDisable;
        private MappingCSharpAction luaFixedUpdate;

        private LuaFunction luaTriggerEnter;
        private LuaFunction luaTriggerStay;
        private LuaFunction luaTriggerExit;
        private LuaFunction luaCollisionEnter;
        private LuaFunction luaCollisionStay;
        private LuaFunction luaCollisionExit;



        [Header("映射的lua脚本名称，为空时将映射gameObject名称")]
        public string mappingLuaName = string.Empty;

        void Awake()
        {
            luaEnv = LuaHelper.Instance.GetLuaEnv();
            luaTable = luaEnv.NewTable();
            LuaTable tmpTab = luaEnv.NewTable();
            tmpTab.Set("__index", luaEnv.Global);
            luaTable.SetMetaTable(tmpTab);
            tmpTab.Dispose();
            //string prefabName = this.name;

            if (string.IsNullOrEmpty(mappingLuaName))
                mappingLuaName = this.name;
            if (mappingLuaName.Contains("(Clone)"))
                mappingLuaName = mappingLuaName.Replace("(Clone)", "");

            /* 查找指定路径下lua文件中的方法，映射为委托*/
            luaAwake = luaTable.GetInPath<MappingCSharpAction>(mappingLuaName + ".Awake");
            luaStart = luaTable.GetInPath<MappingCSharpAction>(mappingLuaName + ".Start");
            luaUpdate = luaTable.GetInPath<MappingCSharpAction>(mappingLuaName + ".Update");
            luaDestroy = luaTable.GetInPath<MappingCSharpAction>(mappingLuaName + ".OnDestroy");
            luaEnable = luaTable.GetInPath<MappingCSharpAction>(mappingLuaName + ".OnEnable");
            luaDisable = luaTable.GetInPath<MappingCSharpAction>(mappingLuaName + ".OnDisable");
            luaFixedUpdate = luaTable.GetInPath<MappingCSharpAction>(mappingLuaName + ".FixedUpdate");
            luaTriggerEnter = luaTable.GetInPath<LuaFunction>(mappingLuaName + ".OnTriggerEnter");
            luaTriggerStay = luaTable.GetInPath<LuaFunction>(mappingLuaName + ".OnTriggerStay");
            luaTriggerExit = luaTable.GetInPath<LuaFunction>(mappingLuaName + ".OnTriggerExit");
            luaCollisionEnter = luaTable.GetInPath<LuaFunction>(mappingLuaName + ".OnCollisionEnter");
            luaCollisionStay = luaTable.GetInPath<LuaFunction>(mappingLuaName + ".OnCollisionStay");
            luaCollisionExit = luaTable.GetInPath<LuaFunction>(mappingLuaName + ".OnCollisionExit");
            if (luaAwake != null)
                luaAwake.Invoke(luaTable, gameObject);
        }     

        void OnEnable()
        {
            if (luaEnable != null)
                luaEnable.Invoke(luaTable, gameObject);
        }

        void Start()
        {
            if (luaStart != null)
                luaStart.Invoke(luaTable, gameObject);
        }

      
        void Update()
        {
            if (luaUpdate != null)
                luaUpdate.Invoke(luaTable, gameObject);
        }

        void FixedUpdate()
        {
            if (luaFixedUpdate != null)
                luaFixedUpdate.Invoke(luaTable, gameObject);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (luaTriggerEnter != null)
                luaTriggerEnter.Call(luaTable, other, gameObject);
        }


        private void OnTriggerStay(Collider other)
        {
            if (luaTriggerStay != null)
                luaTriggerStay.Call(luaTable, other, gameObject);
        }


        private void OnTriggerExit(Collider other)
        {
            if (luaTriggerExit != null)
                luaTriggerExit.Call(luaTable, other, gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (luaCollisionEnter != null)
                luaCollisionEnter.Call(luaTable, collision, gameObject);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (luaCollisionStay != null)
                luaCollisionStay.Call(luaTable, collision, gameObject);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (luaCollisionExit != null)
                luaCollisionExit.Call(luaTable, collision, gameObject);
        }


        void OnDisable()
        {
            if (luaDisable != null)
                luaDisable.Invoke(luaTable, gameObject);
        }

        void OnDestroy()
        {
            if (luaDestroy != null)
                luaDestroy(luaTable, gameObject);
            luaAwake = null;
            luaStart = null;
            luaUpdate = null;
            luaEnable = null;
            luaDisable = null;
            luaFixedUpdate = null;
            luaTriggerEnter = null;
            luaTriggerStay = null;
            luaTriggerExit = null;
            luaCollisionEnter = null;
            luaCollisionStay = null;
            luaCollisionExit = null;
            luaDestroy = null;
        }
    }
}