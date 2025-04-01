--[[
    @Copyright (C) 2020 讯飞幻境（北京）科技有限公司
    @module:luaTemplate   
    @author:RyuRae
    @time:2020-04-25 09:53:45
    @Desc:
]]

require("SysDine")

luaTemplate = {}
local this = luaTemplate


--[[
    @desc: 构造函数
    author:RyuRae
    time:2020-04-30 10:56:19
    {paramdesc}
    return
]]
function this:ctor()
    local o = {}
    setmetatable(o,self)
    self.__index = self
    return o
end

--[[
    @desc: 映射Unity Mono 的Awake函数
    author:RyuRae
    time:2020-04-30 10:56:19
    {paramdesc}
    return
]]
function luaTemplate.Awake(go)
    
end

--[[
   @desc: 映射Unity Mono 的OnEnable函数
    author:RyuRae
    time:2020-04-30 10:56:19
    {paramdesc}
    return
]]
function luaTemplate.OnEnable(go)
    
end

--[[
    @desc: 映射Unity Mono 的Start函数
    author:RyuRae
    time:2020-04-30 10:56:19
    {paramdesc}
    return
]]
function luaTemplate.Start(go)
    
end

--[[
    @desc: 映射Unity Mono 的Update函数
    author:RyuRae
    time:2020-04-30 10:56:19
    {paramdesc}
    return
]]
function luaTemplate.Update(go)
    
end

--[[
    @desc: 映射Unity Mono 的OnDisable函数
    author:RyuRae
    time:2020-04-30 10:56:19
    {paramdesc}
    return
]]
function luaTemplate.OnDisable(go)
    
end


return luaTemplate