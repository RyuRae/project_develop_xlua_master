项目开发工具包（集成工具包）
集成功能：
1.热更新（xlua）
2.目录结构
3.部分插件
4.通用脚本功能

1.热更新使用
1）发布前生成代码Xlua-->Generate Code
2）代码注入Xlua-->Hotfix Inject in Editor
3）注入完成后即可发布
4）StreamingAssets里存储lua代码框架

2.目录结构说明
1）3rd目录
存放第三方插件相关资源

2）Editor目录
存储编辑器及相关拓展代码文件

3）Scripts目录
①第三方代码，使用的一些C#的库，如protobuf、json等
②游戏框架的代码
③游戏业务逻辑代码
④工具集

4)AssetsPackage目录
用来存放我们的资源包
①特效及粒子相关资源存放在Effects
②UI相关资源存放在Textures
③场景及地图相关资源存放在Maps
④声音相关资源存放在Sounds
⑤配置文件相关资源存放在configs
⑥视频相关资源放在Audios
⑦预制体资源放在Prefabs

5）StreamingAssets目录
①存放额外的资源，以及实现资源管理，如第一个版本ad包
②存放web资源，实现web资源加载

6）Scenes目录
①运行时候的场景main
②功能编辑的场景
③UI编辑的场景

7）Plugins目录
部分dll，及相关内容

8）XLua目录
存储热更新生成代码

3.插件说明
1）Newtonsoft：json序列化相关插件
2）DoTween：动画插件

4.通用脚本功能说明
1）UIScene:UI模块
2）ResMgr：内部资源加载测试脚本
3）DevelopEngine：txt文本读写模块
4）GameObjectPool：对象池脚本
5）Global：全局文件查找助手脚本
6）Loom：线程处理器，可在线程里执行主线程代码
7）ManagerEvent：消息处理中心（订阅模式）
8）MonoSingleton/Singleton:单例脚本（单例模式）
9）TimerMgr：定时器
10）LuaHelper：lua脚本模块
