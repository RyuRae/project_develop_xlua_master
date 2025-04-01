using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace HotUpdateModule
{
    /**********************************************
    * Copyright (C) 2019 讯飞幻境（北京）科技有限公司
    * 模块名: BaseLuaMappingUIScene.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：
    ***********************************************/
    public class BaseLuaMappingUIScene : UISceneModule.UIScene
    {
        //[CSharpCallLua]
        //public delegate void MappingCSharpAction(GameObject go);

        private LuaEnv luaEnv;
        private LuaTable luaTable;
        private LuaFunction luaAwake;
        private LuaFunction luaStart;
        private LuaFunction luaUpdate;
        private LuaFunction luaDestroy;
        private LuaFunction luaEnable;
        private LuaFunction luaDisable;
        private LuaFunction luaFixedUpdate;


        void Awake()
        {
            luaEnv = LuaHelper.Instance.GetLuaEnv();
            luaTable = luaEnv.NewTable();
            LuaTable tmpTab = luaEnv.NewTable();
            tmpTab.Set("__index", luaEnv.Global);
            luaTable.SetMetaTable(tmpTab);
            tmpTab.Dispose();
            string prefabName = this.name;

            if (prefabName.Contains("(Clone)"))
                prefabName = prefabName.Replace("(Clone)", "");

            /* 查找指定路径下lua文件中的方法，映射为委托*/
            luaAwake = luaTable.GetInPath<LuaFunction>(prefabName + ".Awake");
            luaStart = luaTable.GetInPath<LuaFunction>(prefabName + ".Start");
            luaUpdate = luaTable.GetInPath<LuaFunction>(prefabName + ".Update");
            luaEnable = luaTable.GetInPath<LuaFunction>(prefabName + ".OnEnable");
            luaDisable = luaTable.GetInPath<LuaFunction>(prefabName + ".OnDisable");
            luaFixedUpdate = luaTable.GetInPath<LuaFunction>(prefabName + ".FixedUpdate");

            if (luaAwake != null)
                luaAwake.Call(luaTable, gameObject);

        }

        void OnEnable()
        {
            if (luaEnable != null)
                luaEnable.Call(luaTable, gameObject);
        }

        protected override void Start()
        {
            base.Start();
            if (luaStart != null)
                luaStart.Call(luaTable, gameObject);
        }

        void Update()
        {
            if (luaUpdate != null)
                luaUpdate.Call(luaTable, gameObject);
        }

        void FixedUpdate()
        {
            if (luaFixedUpdate != null)
                luaFixedUpdate.Call(luaTable, gameObject);
        }


        void OnDisable()
        {
            if (luaDisable != null)
                luaDisable.Call(luaTable, gameObject);
        }

        void OnDestroy()
        {
            if (luaDestroy != null)
                luaDestroy.Call(luaTable, gameObject);
            luaAwake = null;
            luaStart = null;
            luaUpdate = null;
            luaDestroy = null;
            luaEnable = null;
            luaDisable = null;
            luaFixedUpdate = null;
        }
    }
}
