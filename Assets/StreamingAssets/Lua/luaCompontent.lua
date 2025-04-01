--[[
    luaide  模板位置位于 Template/FunTemplate/NewFileTemplate.lua 其中 Template 为配置路径 与luaide.luaTemplatesDir
    luaide.luaTemplatesDir 配置 https://www.showdoc.cc/web/#/luaide?page_id=713062580213505
    author:{author}
    time:2020-04-20 11:16:32
]]


luaCompontent = {}
local this = luaCompontent

function luaCompontent:ctor(obj)
    local o = {}
    setmetatable(o,self)
    self._index = self
    return o
end

function luaCompontent.Awake()
    print("---LuaCompontent Awake---")
end


function luaCompontent.Start()
    print("---LuaCompontent Start---")
end


function luaCompontent.Update()
    print("---LuaCompontent Update---")
end


function luaCompontent.OnDestroy()
    print("---LuaCompontent OnDestroy---")
end