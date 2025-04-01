--[[
    @Copyright (C) 2020 讯飞幻境（北京）科技有限公司
    @module:SceneRootMgr
    @author:RyuRae
    @time:
    @Desc:
        场景初始化加载脚本
        功能：
            1.加载所有场景中的预制物体
            2.添加对应的物体脚本
            3.根据记录文档初始化场景文件顺序
]]


require("SysDine")
--require("MouseTrackable")


SceneRootMgr = {}
local this = SceneRootMgr

--local xmlDoc = XmlDocument()
--local scene = GameObject("mouseclean")
--local sceneRoot = nil
--local _end = "_mouseclean"
--local List_String = CS.System.Collections.Generic.List(CS.System.String)
--得到实例
function  SceneRootMgr.GetInstance()
    --print("------SceneRootMgr------")
    return this
end

--[[
    @desc: 开始处理核心逻辑
    author:RyuRae
    time:2020-04-26 08:17:52
    @return:
]]
function  SceneRootMgr.StartProcess()
    --获取场景物体
    --print("------SceneRootMgr 启动成功，开始场景初始化！！！------")
    --sceneRoot = GameObject.Find(Tips.SceneRoot):GetComponent(typeof(Transform));
    --print(sceneRoot)
    --AutoCameraDevice.OpenCameraDevice()
    --AssetsManager.Instance:Load(RuntimeAssetType.TEXT, "mouseclean.xml", false, function (record)
        --print(record)
    --    xmlDoc:LoadXml(tostring(record))
    --    scene.transform:SetParent(sceneRoot)
        -- local list = AssetsManager.Instance.RecordsInfo:GetRecordsList(RuntimeAssetType.BUNDLE_PREFAB, 
        -- nil)
    --    local list = AssetsManager.Instance:GetRecords(RuntimeAssetType.BUNDLE_PREFAB, _end)
    --   AssetsManager.Instance:LoadMulti(RuntimeAssetType.BUNDLE_PREFAB, list, false, LoadMethod.BUNDLE_FILE, 
    --    function (obj)
            --print(obj)
    --        cast(obj, typeof(GameObject))
    --        clone = GameObject.Instantiate(obj,scene.transform)
    --        clone.name = string.sub( obj.name,1, string.find(obj.name, "_",1) - 1)
    --        if clone:GetComponent(typeof(ImageTargetBehaviour)) ~= nil then
                --添加对应的c#映射脚本
    --            clone:AddComponent(typeof(BaseLuaMappingCSharp))
                --添加识别脚本的公共脚本
                --LuaTrackerComponent.Add(clone, MouseTrackable)  
              
    --        end
                 
    --    end, 
    --    this.OnComplete)
        
    --end)
    
end


--[[
    @desc: 处理核心逻辑完毕的回调函数
    author:RyuRae
    time:2020-04-26 08:17:31
    @return:
]]
function SceneRootMgr.OnComplete()
    -- todo
    --print("------SceneRootMgr.OnComplete------")
    --local arCamera = GameObject.Find("ARCamera")
    --arCamera:AddComponent(typeof(BaseLuaMappingCSharp))
    --根据场景信息重新设置场景内容
    --local nodeList = xmlDoc:SelectSingleNode(Tips.SceneRoot).ChildNodes[0].ChildNodes
    -- AssetsManager.Instance:Load(RuntimeAssetType.BUNDLE_PREFAB, "小鼠波波-扫描页", false, function(obj)
    --     cast(obj, typeof(GameObject))
    --     GameObject.Instantiate(obj, arCamera.transform)
    -- end)
    --AssetsManager.Instance:ProcessCreate(sceneRoot, nodeList)
    --local path = FileHelper.GetLocalDirPath(RuntimeAssetType.TEXT, AssetsManager.Instance.RecordsInfo)
    --print(path)
    --VuforiaControl.Instance:LoadDataSet(path, "MouseBoBo", false)
    --UIManager.Instance:SetVisible(UIName.mUIScene_Scan, true)
    --SceneLoadMgr.Instance:UnLoad(Tips.loading)

end