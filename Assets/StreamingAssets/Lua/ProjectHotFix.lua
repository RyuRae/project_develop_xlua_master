--[[
    @Copyright (C) 2020 讯飞幻境（北京）科技有限公司
    @module:ProjectHotFix
    @author:RyuRae
    @time:
    @Desc:
        项目热补丁文件注册与注销文件
        功能：本项目中所有的“热补丁”都在本文件注册，以及注销
]]


ProjectHotFix = {}
local this = ProjectHotFix

-- 定义局部变量--


--  热补丁注册
function this:HotfixUpdate()
    print("热补丁注册 HotfixUpdate()")

    --注册补丁1：
    -- xlua.private_accessible(CS.Global)                      --  可以访问CS中的私有字段 
    -- xlua.hotfix(CS.Global, 'Test',
    --     function (self)
    --         print("热补丁格式测试!")
    --     end
    -- )

    --注册补丁2：

    --注册补丁3：

end


function this:UnRegister_HotFix()
    print("热补丁注销 UnRegister_HotFix()")
    --注销1：

    --注销2：

    --注销3：
    
end