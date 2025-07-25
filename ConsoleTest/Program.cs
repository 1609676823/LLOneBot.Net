﻿namespace ConsoleTest
{
    using LLOneBot.Net.Data;
    using LLOneBot.Net.Data.MessageDataType;
    using LLOneBot.Net.Receivers.Message.Group;
    using LLOneBot.Net.Receivers.Message.Private;
    using LLOneBot.Net.Receivers.Notice;
    using LLOneBot.Net.Receivers.Request;
    using LLOneBot.Net.Sessions;
    using System.Reactive.Linq;
    using System.Net.WebSockets;

    using System.Text.Json;
    using Websocket.Client;
    using System;

    internal class Program
    {
        #pragma warning disable CS8602 // 解引用可能出现空引用。
        static async Task Main(string[] args)
        {

            //   string testa = @"测试123456.";
            //    Console.WriteLine(testa.Length);
            // AtMessage atMessage = new AtMessage();
            //var json = JsonSerializer.Serialize(atMessage)!;

            #region MyRegion
            //liteLoaderQQNTBot.MessageReceived.Subscribe(e =>
            //{
            //    Console.WriteLine( e);
            //});
            //liteLoaderQQNTBot.MessageReceived.OfType<ResponseMessage>().Subscribe(async msg =>
            //{
            //    await Console.Out.WriteLineAsync(msg.Text);

            //    }
            //);
            //liteLoaderQQNTBot.MessageReceived.OfType<ResponseMessage>().Subscribe(msg =>
            //{
            //    Console.WriteLine(msg.Text);
            //});
            //liteLoaderQQNTBot.ReceivedMeta_event.Subscribe(e =>
            //{
            //    Console.WriteLine(e);
            //}
            //);
            #endregion
            // 禁用鼠标点击等待
            Console.TreatControlCAsInput = true;
            var exit = new ManualResetEvent(false);
            #region 不使用await


            //LiteLoaderQQNTBot liteLoaderQQNTBot = new LiteLoaderQQNTBot() { Ip = "127.0.0.1", HttpPort = 3000, WebsocKetPort = 3001, AccessTocken = "1" };
            //Task<LoginInfo> taskStartBot = liteLoaderQQNTBot.StartBot();
            //taskStartBot.Wait();
            //if ("ok".Equals(taskStartBot.Result.status, StringComparison.OrdinalIgnoreCase))
            //{
            //    Console.WriteLine("BOT连接成功");
            //    Console.WriteLine("账号:"+ taskStartBot.Result.user_id);
            //    Console.WriteLine("名称:" + taskStartBot.Result.nickname);

            //}

            //else
            //{ Console.WriteLine("BOT连接失败"); }

            #endregion

            LiteLoaderQQNTBot liteLoaderQQNTBot = new LiteLoaderQQNTBot()
            {
                Ip = "127.0.0.1",
                HttpPort = 3000,
                WebsocKetPort = 3001,
               // AccessTocken = "1",

            };
            await liteLoaderQQNTBot.StartBot();


            Console.WriteLine(liteLoaderQQNTBot.LoginInfo.Logininfojson);


            //MessageChain messages = new MessageChain() { new MusicMessage("163", "28949129") };
            //string res = MessageManager.SendGroupMessage("782351597", messages);

            //Console.WriteLine(res);



            //MessageChain messages = new MessageChain() { new TextMessage("消息内容"),new AtMessage("被@用户的号码") };
            //MessageManager.SendGroupMessage("群号", messages);
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
                // Console.WriteLine(msg.raw_message);

                if (msg.group_id == 782351597) 
                {
                    Console.Write(msg.message_id);
                }

                MessageChain messageChain = msg.MessageChain;

                foreach (var item in messageChain)
                {
                    if (item.MessageType == MessageType.Text)
                    {

                        var message = item as TextMessage;

                    }

                    if (item.MessageType == MessageType.Image)
                    {
                        ImageMessage? imageMessage = item as ImageMessage;
                    }
                    if (item.MessageType == MessageType.At)
                    {
                        AtMessage? atMessage = item as AtMessage;

                    }
                }



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
    }




}
