--[[
    luaide  模板位置位于 Template/FunTemplate/NewFileTemplate.lua 其中 Template 为配置路径 与luaide.luaTemplatesDir
    luaide.luaTemplatesDir 配置 https://www.showdoc.cc/web/#/luaide?page_id=713062580213505
    author:{author}
    time:2020-03-21 12:41:12
]]

---
--  UI根预设_控制脚本
--- 功能：
---     1：加载UIRootView UI面板（预设）
---     2：给UI面板（预设），动态添加“BaseLuaUIForm”的lua脚本
---     3：“BaseLuaUIForm”脚本，通过xlua的C#--lua的映射，使得
---         与“UI预设”相同名称的lua脚本，获取Unity的生命周期
---


UIRootCtrl = {}
local this = UIRootCtrl

--得到实例
function  UIRootCtrl.GetInstance()
    print("UIRootCtrl")
    return this
end

--开始处理核心逻辑
function  UIRootCtrl.StartProcess()

    print("------UIRootCtrl UI根预设_控制脚本 启动成功！！！")
    
end

--  处理核心逻辑完毕的回调函数
function UIRootCtrl.OnComplete()
    -- todo
end

