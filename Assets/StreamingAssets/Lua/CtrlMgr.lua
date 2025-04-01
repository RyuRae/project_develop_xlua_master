--[[
    @Copyright (C) 2020 讯飞幻境（北京）科技有限公司
    @module:CtrlMgr
    @author:RyuRae
    @Desc:
        "lua框架"项目中控制层管理器
        功能：
            1：缓存所有项目中控制层lua脚本
            2：提供访问项目中所有控制层lua脚本的入口函数
]]

--  引入控制层管理器脚本
require("SceneRootMgr")

CtrlMgr = {}
local this = CtrlMgr

--  定义一个控制器列表（缓存所有项目中用到的所有控制层lua脚本）
local ctrlList = {}

--  lua控制器初始化（缓存所有项目中控制层lua脚本）
function CtrlMgr.Init()
    ctrlList[CtrlName.SceneRootMgr] = SceneRootMgr.GetInstance() --得到脚本的实例
    
    --print("CtrlMgr")
end

--  获取控制层lua脚本
function CtrlMgr.GetCtrlInstance(ctrlName)
    return ctrlList[ctrlName]
end

--  获取控制器lua脚本实例，且调用StartProcess函数
function CtrlMgr.StartProcess(ctrlName)
    local ctrlObj = CtrlMgr.GetCtrlInstance(ctrlName)
    if(ctrlObj ~= nil)then
        ctrlObj.StartProcess()
    end
end