using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace HotUpdateModule
{
    /**********************************************
    * Copyright (C) 2019 讯飞幻境（北京）科技有限公司
    * 模块名: LuaComponent.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：
    ***********************************************/
    [LuaCallCSharp]
    public class LuaComponent : MonoBehaviour
    {

        public LuaTable luaTable;

        LuaFunction luaStart;
        LuaFunction luaUpdate;
        LuaFunction luaOnDestroy;
        LuaFunction luaAwake;
        LuaFunction luaOnEnable;
        LuaFunction luaFixedUpdate;
        LuaFunction luaOnDisable;
        LuaFunction luaLateUpdate;

        private LuaFunction luaTriggerEnter;
        private LuaFunction luaTriggerStay;
        private LuaFunction luaTriggerExit;
        private LuaFunction luaCollisionEnter;
        private LuaFunction luaCollisionStay;
        private LuaFunction luaCollisionExit;

        /// <summary>
        /// 添加lua组件
        /// </summary>
        /// <param name="go"></param>
        /// <param name="tableClass"></param>
        /// <returns></returns>
        public static LuaTable Add(GameObject go, LuaTable tableClass)
        {
            LuaFunction luaCtor = tableClass.Get<LuaFunction>("ctor");
            if (null != luaCtor)
            {
                object[] rets = luaCtor.Call(tableClass);
                if (1 != rets.Length)
                {
                    return null;
                }
                LuaComponent cmp = go.AddComponent<LuaComponent>();
                cmp.luaTable = (LuaTable)rets[0];
                cmp.initLuaFunction();
                cmp.CallAwake();
                return cmp.luaTable;
            }
            else
            {
                throw new Exception("Lua function .ctor not found");
            }
        }

        /// <summary>
        /// 获取lua组件
        /// </summary>
        /// <param name="go"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static LuaTable Get(GameObject go, LuaTable table)
        {
            LuaComponent[] cmps = go.GetComponents<LuaComponent>();
            string tableStr = table.ToString();
            for (int i = 0; i < cmps.Length; i++)
            {
                string temp = cmps[i].luaTable.ToString();
                if (string.Equals(tableStr, cmps[i].luaTable.ToString()))
                {
                    return cmps[i].luaTable;
                }
            }
            return null;

        }

        /// <summary>
        /// 设置组件父节点
        /// </summary>
        /// <param name="go"></param>
        /// <param name="parent"></param>
        public static void SetParent(GameObject go, Transform parent)
        {
            go.transform.SetParent(parent);
        }

        /// <summary>
        /// 初始化Lua脚本中的方法
        /// </summary>
        void initLuaFunction()
        {
            luaOnDestroy = luaTable.Get<LuaFunction>("OnDestroy");
            luaAwake = luaTable.Get<LuaFunction>("Awake");
            luaStart = luaTable.Get<LuaFunction>("Start");
            luaUpdate = luaTable.Get<LuaFunction>("Update");
            luaOnEnable = luaTable.Get<LuaFunction>("OnEnable");
            luaFixedUpdate = luaTable.Get<LuaFunction>("FixedUpdate");
            luaOnDisable = luaTable.Get<LuaFunction>("OnDisable");
            luaLateUpdate = luaTable.Get<LuaFunction>("LateUpdate");

            luaTriggerEnter = luaTable.Get<LuaFunction>("OnTriggerEnter");
            luaTriggerStay = luaTable.Get<LuaFunction>("OnTriggerStay");
            luaTriggerExit = luaTable.Get<LuaFunction>("OnTriggerExit");
            luaCollisionEnter = luaTable.Get<LuaFunction>("OnCollisionEnter");
            luaCollisionStay = luaTable.Get<LuaFunction>("OnCollisionStay");
            luaCollisionExit = luaTable.Get<LuaFunction>("OnCollisionExit");
        }

        /// <summary>
        /// 调用lua脚本中定义的Awake方法
        /// </summary>
        void CallAwake()
        {
            if (null != luaAwake)
            {
                luaAwake.Call(luaTable, gameObject);
            }
        }


        void Start()
        {
            if (null != luaStart)
            {
                luaStart.Call(luaTable, gameObject);
            }
        }

        /// <summary>
        /// 调用lua脚本中定义的Update方法
        /// </summary>
        private void Update()
        {
            if (null != luaUpdate)
            {
                luaUpdate.Call(luaTable, gameObject);
            }
        }

        /// <summary>
        /// 调用lua脚本中定义的FixedUpdate方法
        /// </summary>
        private void FixedUpdate()
        {
            if (null != luaFixedUpdate)
            {
                luaFixedUpdate.Call(luaTable, gameObject);
            }
        }

        /// <summary>
        /// 调用lua脚本中定义的 LateUpdate
        /// </summary>
        private void LateUpdate()
        {
            if (null != luaLateUpdate)
            {
                luaLateUpdate.Call(luaTable, gameObject);
            }
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

        /// <summary>
        /// 调用lua脚本中定义的 OnEnable
        /// </summary>
        private void OnEnable()
        {
            if (null != luaOnEnable)
            {
                luaOnEnable.Call(luaTable, gameObject);
            }
        }

        /// <summary>
        /// 调用lua脚本中定义的 OnDisable
        /// </summary>
        private void OnDisable()
        {
            if (null != luaOnDisable)
            {
                luaOnDisable.Call(luaTable, gameObject);
            }
        }


        /// <summary>
        /// 调用lua脚本中定义的OnDestroy方法
        /// </summary>
        private void OnDestroy()
        {
            if (null != luaOnDestroy)
            {
                luaOnDestroy.Call(luaTable, gameObject);
            }

            luaAwake = null;
            luaStart = null;
            luaUpdate = null;
            luaOnEnable = null;
            luaOnDisable = null;
            luaFixedUpdate = null;
            luaTriggerEnter = null;
            luaTriggerStay = null;
            luaTriggerExit = null;
            luaCollisionEnter = null;
            luaCollisionStay = null;
            luaCollisionExit = null;
            luaOnDestroy = null;
        }

    }
}