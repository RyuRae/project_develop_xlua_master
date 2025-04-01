--[[
    @Copyright (C) 2020 讯飞幻境（北京）科技有限公司
    @module:SysDine
    @author:RyuRae
    @time:
    @Desc:
        “lua框架”定义所有的常量与“枚举”（表）
]]

-- 项目中用到的控制层脚本名称
CtrlName ={
    SceneRootMgr = "SceneRootMgr"
}

-- 项目中用到的视图层脚本名称
 ScriptNames = {
    --"GeneralTrackable"
    --"PigsTrackable"
    "ARCamera",
    "TimelinePause",
    "click2",
    "mouse0",
    "mouse2",
    "mouse4",
    "mouse5",
    "mouse6",
    "mouse8",
    "mouse9",
    "mouse10",
    "MouseTrackable",
    "SelfCTL",
    "Suofang",
    "yaohuang",
    "Yidong",
    "Disable",
    "click1bobo",
    "dangaosuofang",
    "UISceneReturn"
}

--[[
    @desc: 
    author:RyuRae
    time:2020-04-29 12:18:44
    --@min:随机数开始的最小值
    --@max:随机数开始的最大值
    @return:随机到的数
]]
function Random(min, max)
    local strTime = tostring(os.time())  
    --os.time()
    local strRev = string.reverse(strTime)
    local strRandomTime = string.sub(strRev, 1, 6)
    math.randomseed(strRandomTime)
    return math.random(min, max)
end

-- 这里建议封装到一个通用的lua文件中，在lua虚拟机启动时require 拿到全局的class函数
function class(className, super)
    local cls
    cls.__cname = className

    if super then
        cls = {}
        setmetatable(cls, {__index = super})
        cls.super = super
    else
        cls = {}
    end

    function cls.ctor()
        local o = {}
        setmetatable(o, cls)
        return o
    end
    return cls
end

--  引入Unity内置的常用类型
GameObject = CS.UnityEngine.GameObject
Transform = CS.UnityEngine.Transform
Global = CS.Global
LuaHelper = CS.HotUpdateModule.LuaHelper
Tips = CS.Common.Tips
AssetsManager = CS.AssetsManager
BaseLuaMappingCSharp = CS.HotUpdateModule.BaseLuaMappingCSharp
LuaComponent = CS.HotUpdateModule.LuaComponent
BaseLuaMappingTracker = CS.HotUpdateModule.BaseLuaMappingTracker
LuaTrackerComponent = CS.HotUpdateModule.LuaTrackerComponent
SceneLoadMgr = CS.SceneLoad.SceneLoadMgr
RuntimeAssetType = CS.FileTools.RuntimeAssetType
XmlDocument = CS.System.Xml.XmlDocument
List_String = CS.System.Collections.Generic.List(CS.System.String)
LoadMethod = CS.FileTools.LoadMethod
GeneralTrackable = CS.HotUpdateModule.GeneralTrackable
AutoCameraDevice = CS.AutoCameraDevice
VuforiaControl = CS.VuforiaControl
FileHelper = CS.FileTools.FileHelper
Input = CS.UnityEngine.Input
Ray = CS.UnityEngine.Ray
RaycastHit = CS.UnityEngine.RaycastHit
Camera = CS.UnityEngine.Camera
UnityEngine = CS.UnityEngine
PlayableDirector = CS.UnityEngine.Playables.PlayableDirector
Vector3 = CS.UnityEngine.Vector3
BoxCollider = CS.UnityEngine.BoxCollider
SpriteRenderer = CS.UnityEngine.SpriteRenderer
AudioManager = CS.Common.Audio.AudioManager
Anim = CS.Common.Anim
MonoBehaviour = CS.UnityEngine.MonoBehaviour
List_Anim = CS.System.Collections.Generic.List(CS.Common.Anim.AnimUnit)
WaitForSeconds = CS.UnityEngine.WaitForSeconds
List_GameObject = CS.System.Collections.Generic.List(CS.UnityEngine.GameObject)
SpriteRenderer = CS.UnityEngine.SpriteRenderer
Color = CS.UnityEngine.Color
AudioSource = CS.UnityEngine.AudioSource
Animator = CS.UnityEngine.Animator
ImageTargetBehaviour = CS.Vuforia.ImageTargetBehaviour
Button = CS.UnityEngine.UI.Button
NativeCall = CS.Common.NativeCall
UIManager = CS.UISceneModule.UIManager
UIName = CS.UISceneModule.UIName