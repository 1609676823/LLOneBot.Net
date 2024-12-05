# LLOneBot插件的 接入框架（LLOneBot.Net)
# 使用本框架首先需要自行了解LLOneBot内容，自行部署环境

## 接口协议标准（基于标准的onebot-11)地址如下:
https://github.com/botuniverse/onebot-11/blob/master/README.md

# 接入教程
## 第一步:使用nuget管理器安装LLOneBot.Net
对应的nuget地址为:https://www.nuget.org/packages/LLOneBot.Net

## 第二步:需要添加的引用
```
using LLOneBot.Net.Data;
using LLOneBot.Net.Data.MessageDataType;
using LLOneBot.Net.Receivers.Message.Group;
using LLOneBot.Net.Receivers.Message.Private;
using LLOneBot.Net.Receivers.Notice;
using LLOneBot.Net.Receivers.Request;
using LLOneBot.Net.Sessions;
```
## 第三部:获取message的代码示例
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