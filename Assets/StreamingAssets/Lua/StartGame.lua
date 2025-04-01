--[[
    @Copyright (C) 2020 讯飞幻境（北京）科技有限公司
    @module:StartGame
    @author:RyuRae
    @time:
    @Desc:所有lua脚本的入口
]]


--  引入项目常量和枚举
require("SysDine")
--  引入项目初始化核心脚本
require("ProjectInit")
--  引入项目热补丁模块的注册
require("ProjectHotFix")


--  项目开始
ProjectInit.Init()
--  项目中所有的hotfix 进行注册
ProjectHotFix.HotfixUpdate()