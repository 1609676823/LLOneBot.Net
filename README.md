# LLOneBot以及NapCatQQ 的 接入框架（LLOneBot.Net),同步兼容 NapCatQQ 框架

理论上符合onebot-11的协议标准的框架，均可以支持。onebot官网如下:
https://docs.onebots.org/

# 使用本框架首先需要自行了解LLOneBot以及NapCatQQ内容，自行部署环境
## LLOneBot官网地址如下:
https://llonebot.github.io/zh-CN/

https://github.com/LLOneBot/LLOneBot

## NapCatQQ官网地址如下:
https://napneko.icu/

https://github.com/NapNeko/NapCatQQ


## 接口协议标准（基于标准的onebot-11)地址如下:
https://github.com/botuniverse/onebot-11/blob/master/README.md

# 接入教程
## 1.第一步:使用nuget管理器安装(LLOneBot.Net)包，或者自己手动添加对应的dll，自行学习如何使用nuget管理器
对应的nuget地址为:https://www.nuget.org/packages/LLOneBot.Net

## 2.第二步:需要添加的引用
```
using LLOneBot.Net.Data;
using LLOneBot.Net.Data.MessageDataType;
using LLOneBot.Net.Receivers.Message.Group;
using LLOneBot.Net.Receivers.Message.Private;
using LLOneBot.Net.Receivers.Notice;
using LLOneBot.Net.Receivers.Request;
using LLOneBot.Net.Sessions;
```
## 3.第三步:(示例代码)获取事件的demo
```
static async Task Main(string[] args)
{
    // 禁用鼠标点击等待
    Console.TreatControlCAsInput = true;
    var exit = new ManualResetEvent(false);

    LiteLoaderQQNTBot liteLoaderQQNTBot = new LiteLoaderQQNTBot()
    {
        Ip = "127.0.0.1",
        HttpPort = 3000,
        WebsocKetPort = 3001,
        AccessTocken = "1"
    };

    await liteLoaderQQNTBot.StartBot();
    Console.WriteLine("启动成功");

    /* 接收message 消息事件*/
    /* 接收到私信消息*/
    liteLoaderQQNTBot.MessageReceived.OfType<PrivateMessageReceiver>().Subscribe(msg =>
    {
        Console.WriteLine("接收到用户: " + msg.user_id + " 的私信消息: " + msg.raw_message);
    });

    /* 接收到群消息*/
    liteLoaderQQNTBot.MessageReceived.OfType<GroupMessageReceiver>().Subscribe(msg =>
    {
        Console.WriteLine("接收到群: " + msg.group_id + " 的消息:" + msg.raw_message);
    });

    /* request 群 请求事件*/
    liteLoaderQQNTBot.RequestReceived.OfType<GroupRequestReceiver>().Subscribe(msg =>
    {
        Console.WriteLine("接收到群 request 请求事件");
        Console.WriteLine(msg.comment);
    });

    /* request 好友 请求事件*/
    liteLoaderQQNTBot.RequestReceived.OfType<FriendRequestReceiver>().Subscribe(msg =>
    {
        Console.WriteLine("接收到好友 request 请求事件");
        Console.WriteLine(msg.comment);
    });

    /* notice 通知事件*/
    liteLoaderQQNTBot.NoticeReceived.OfType<NoticeReceiverBase>().Subscribe(msg =>
    {
        Console.WriteLine("接收到notice 通知事件");
        Console.WriteLine(msg.Originaljson);
    });

    /*接收meta_event元事件*/
    liteLoaderQQNTBot.Meta_eventReceived.Subscribe(e =>
    {
        Console.WriteLine("接收到meta_event元事件");
        Console.WriteLine(e);
    });

    /*断开连接事件*/
    liteLoaderQQNTBot.DisconnectionHappened.Subscribe(e =>
    {
        Console.WriteLine("websocket断开连接：" + e);
        // liteLoaderQQNTBot.StartBot();
    });

    exit.WaitOne();
    //  while (true) {  }
    //Console.WriteLine("Hello, World!");
}

```

## 4.第四步:使用消息管理器和消息链进行信息上报
### 4.1纯文本消息链发送群消息为代码示例:
```
LiteLoaderQQNTBot liteLoaderQQNTBot = new LiteLoaderQQNTBot()
{
    Ip = "127.0.0.1",
    HttpPort = 3000,
    WebsocKetPort = 3001,
    AccessTocken = "1"
};
await liteLoaderQQNTBot.StartBot();
MessageChain messages = new MessageChain() { new TextMessage("消息内容") };
MessageManager.SendGroupMessage("群号", messages);
```
### 4.2发送@消息代码示例:
```
LiteLoaderQQNTBot liteLoaderQQNTBot = new LiteLoaderQQNTBot()
{
    Ip = "127.0.0.1",
    HttpPort = 3000,
    WebsocKetPort = 3001,
    AccessTocken = "1"
};
await liteLoaderQQNTBot.StartBot();
MessageChain messages = new MessageChain() { new TextMessage("消息内容"),new AtMessage("被@用户的号码") };
MessageManager.SendGroupMessage("群号", messages);
```
# 熟悉消息链的概念，以及使用消息链进行信息发送上报
消息链由一系列的消息元素组成，每个消息元素表示消息中的一个部分。例如，一条消息可以包含文本、图片、语音、表情等不同类型的元素。每种类型的元素都对应着 MessageChain 中的一个类，比如 Plain 表示纯文本消息，Image 表示图片消息等。
## 消息类型如下：

### AtMessage（@某人）

用于在消息中 @某人。

### DiceMessage（骰子魔法表情）

用于发送骰子魔法表情。

### FaceMessage（QQ 表情）

用于发送 QQ 表情。

### FileMessage（图片信息）

用于发送图片文件。

### ImageMessage（图片信息）

用于发送图片信息。

### PokeMessage（窗口抖动）

用于发送窗口抖动（戳一戳）消息。

### RecordMessage（语音信息）

用于发送语音文件。

### ReplyMessage（回复）

用于发送回复消息。

### RpsMessage（猜拳魔法表情）

用于发送猜拳魔法表情。

### ShakeMessage（窗口抖动，已弃用）

用于发送窗口抖动消息（已弃用）。

### TextMessage（文本信息）

用于发送纯文本消息。

### VideoMessage（语音信息）

用于发送视频文件。

# 消息管理器功能介绍
# 以下是该库中 MessageManager 类的主要功能介绍

## 消息发送与管理

### 发送群消息
- SendGroupMessage
- SendGroupMessageAsync

### 发送好友消息
- SendFriendMessage
- SendFriendMessageAsync

### 发送私聊消息
- SendPrivateMessage
- SendPrivateMessageAsync

### 发送消息
- SendMessage
- SendMessageAsync

### 撤回消息
- DeleteMessage
- DeleteMessageAsync

### 获取消息
- GetMessage

### 获取合并转发消息
- GetForwardMessage

## 群组管理

### 群组踢人
- SetGroupKick
- SetGroupKickAsync

### 群组禁言/解除禁言
- SetGroupBan
- SetGroupBanAsync

### 群组全员禁言
- SetGroupWholeBan
- SetGroupWholeBanAsync

### 群组设置管理员
- SetGroupAdmin
- SetGroupAdminAsync

### 群组匿名聊天
- SetGroupAnonymous
- SetGroupAnonymousAsync

### 设置群名片
- SetGroupCard
- SetGroupCardAsync

### 设置群名
- SetGroupName
- SetGroupNamedAsync

### 退出群组
- SetGroupLeave
- SetGroupLeaveAsync

### 设置群组专属头衔
- SetGroupSpecialTitle
- SetGroupSpecialTitleAsync

## 好友管理

### 处理加好友请求
- SetFriendAddRequest
- SetFriendAddRequestAsync

### 处理加群请求/邀请
- SetGroupAddRequest
- SetGroupAddRequestAsync

## 其他功能

### 获取登录号信息
- GetLoginInfo

### 获取陌生人信息
- GetStrangerInfo

### 获取好友列表
- GetFriendList

### 获取群信息
- GetGroupInfo

### 获取群列表
- GetGroupList

### 获取群成员信息
- GetGroupMemberInfo

### 获取群成员列表
- GetGroupMemberList

### 获取群荣誉信息
- GetGroupHonorInfo

### 获取 Cookies
- GetCookies

### 获取 CSRF Token
- GetCsrfToken

### 获取 QQ 相关接口凭证
- GetCredentials

### 获取语音文件
- GetRecord

### 获取图片文件
- GetImage

### 检查是否可以发送图片
- CanSendImage

### 检查是否可以发送语音
- CanSendRecord

### 获取运行状态
- GetStatus

### 获取版本信息
- GetVersionInfo

### 重启 OneBot 实现
- SetRestart
- SetRestartAsync

### 清理缓存
- CleanCache
- CleanCacheAsync
