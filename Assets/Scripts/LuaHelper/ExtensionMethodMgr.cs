using HotUpdateModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/**********************************************
* Copyright (C) 2019 讯飞幻境（北京）科技有限公司
* 模块名: ExtensionMethodMgr.cs
* 创建者：RyuRae
* 修改者列表：
* 创建日期：
* 功能描述：扩展方法管理类，用于管理不同类的扩展方法
*           以便实现lua对泛型的调用
***********************************************/
[CSharpCallLua]
public static class ExtensionMethodMgr {

    ////测试
    //public static void SetName(this LuaHelper help, string name)
    //{

    //}

    public static bool RayFunction(this Physics phy, Ray ray, out RaycastHit hit)
    {
        return Physics.Raycast(ray, out hit);
    }
}
