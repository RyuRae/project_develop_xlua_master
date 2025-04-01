--[[
    @Copyright (C) 2020 讯飞幻境（北京）科技有限公司
    @module:cs_coroutine
    @author:RyuRae
    @time:
    @Desc:实现Unity协程的开启与停止
]]


local util = require 'xlua.util'

local gameobject = CS.UnityEngine.GameObject('Coroutine_Runner')
CS.UnityEngine.Object.DontDestroyOnLoad(gameobject)
local cs_coroutine_runner = gameobject:AddComponent(typeof(CS.Coroutine_Runner))

return {
    start = function(...)
	    local holder = CS.IEnumeratorHolder(util.cs_generator(...))
	    cs_coroutine_runner:StartCoroutine(holder)
		return holder
	end;
	stop = function(holder)
	    cs_coroutine_runner:StopCoroutine(holder)
	end
}