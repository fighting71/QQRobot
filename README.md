# QQRobot
 qq聊天机器人

- 感谢[newbe36524](https://github.com/newbe36524/)的[Newbe.Mahua.Framework](https://github.com/newbe36524/Newbe.Mahua.Framework)框架的支持。 虽然有坑...

- 全程使用mpq测试开发....

## Project struct ##

- Newbe.Mahua.Plugins.Pikachu 核心
- Domain 通用项目
- Data.Module 数据库上下文
- Services 数据逻辑操作层
- GenerateMsg 消息处理相关 (主要是群聊和私聊)
- PikachuRobot.Job.Hangfire hangfire相关

### Newbe.Mahua.Plugins.Pikachu intro ###

- MahuaModule 容器注册
- MahuaEvent 事件处理
- MahuaApis api处理

## 简单说明 ##

可使用基本功能(私聊、群聊等)

任务调度(参考testjob)

事件补充(参考CusEventFun10000的使用)

api修正(参考GetGroupMemebersWithModelApiMahuaCommandHandler)

使用mysql + codefirst 可直接生成相关表

使用redis(简单应用)


----------

踩坑记录

IInitializationMahuaEvent 说明是插件初始化事件，查看源码后，发现是跟随着热更新触发的，然后再去Newbe.Mahua.MPQ 查看10000(初始化对应的EventId)的实现基本上是空的。。。 

mpq添加qq无效...

>  12,MPQ,未知,16:11:59,Api_AddQQ已废止使用.给您造成的不便还请谅解.m( _

----------

since:9/20/2019 10:02:10 AM 

10/14/2019 3:11:47 PM 
