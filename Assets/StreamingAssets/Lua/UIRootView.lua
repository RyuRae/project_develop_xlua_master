--[[
    luaide  模板位置位于 Template/FunTemplate/NewFileTemplate.lua 其中 Template 为配置路径 与luaide.luaTemplatesDir
    luaide.luaTemplatesDir 配置 https://www.showdoc.cc/web/#/luaide?page_id=713062580213505
    author:{author}
    time:2020-03-23 09:24:41
]]


--
--  UI根预设_显示脚本
--


UIRootView = {}
local this = UIRootView


function UIRootView.Awake()
    print("---UIRootView Awake---")
end


function UIRootView.Start()
    print("---UIRootView Start---")
end


function UIRootView.Update()
    print("---UIRootView Update---")
end


function UIRootView.OnDestroy()
    print("---UIRootView OnDestroy---")
end