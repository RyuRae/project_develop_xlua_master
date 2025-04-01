--[[
    @Copyright (C) 2020 讯飞幻境（北京）科技有限公司
    @module:ProjectInit
    @author:RyuRae
    @time:
    @Desc:
    “lua框架”项目的初始化
    功能：
        1：引入项目中所有脚本
        2：通过CtrlMgr.lua(控制层)脚本，来缓存系统中所有其他控制层脚本
        3：提供访问其他控制层脚本的入口函数
]]


--  引入控制层管理器脚本
require("CtrlMgr")
require("SysDine")

ProjectInit = {}
local this = ProjectInit

function ProjectInit.Init()
    --print("ProjectInit.Init")
    --引入项目中所有的脚本
    this.ImportAllScripts()
    --lua控制器初始化
    CtrlMgr.Init()   
    --场景初始化
    CtrlMgr.StartProcess(CtrlName.SceneRootMgr)
end

-- 引入项目中所有脚本
function ProjectInit.ImportAllScripts()
    for i=1,#ScriptNames do
        require(tostring(ScriptNames[i]))
    end
end