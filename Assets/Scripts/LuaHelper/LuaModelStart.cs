using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HotUpdateModule
{
    /**********************************************
    * Copyright (C) 2019 讯飞幻境（北京）科技有限公司
    * 模块名: LuaModelStart.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：
    ***********************************************/
    public class LuaModelStart : MonoBehaviour, IStartProject
    {

        public void ReceiveInfoStartRuning()
        {
            LuaHelper.Instance.DoString("require'main'");
        }

        //注销热更新列表
        void OnDisable()
        {
            LuaHelper.Instance.CallLuaFuntion("ProjectHotFix", "UnRegister_HotFix");
        }
    }
}
