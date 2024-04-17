namespace ConsoleTest
{
    using LLOneBot.Net.Data;
    using LLOneBot.Net.Data.MessageDataType;
    using LLOneBot.Net.Receivers;
    using LLOneBot.Net.Sessions;
    using System.Net.WebSockets;
    using System.Reactive.Linq;
    using System.Text.Json;
    using Websocket.Client;
   
    internal class Program
    {
        static async Task Main(string[] args)
        {
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

            LiteLoaderQQNTBot liteLoaderQQNTBot = new LiteLoaderQQNTBot() { Ip = "127.0.0.1", HttpPort = 3000, WebsocKetPort = 3001, AccessTocken = "1" };

            MessageChain messageChain = new MessageChain
                             {
                            
                               // new AtMessage(x.Sender.Id),
                                new TextMessage("测试1"),
                              };
   
            await liteLoaderQQNTBot.StartBot();

           MessageManager.SendFriendMessage("2361803582", messageChain,true);

            /* 接收message 消息事件*/

            /* 接收到群消息*/
            liteLoaderQQNTBot.MessageReceived.OfType<GroupMessageReceiver>().Subscribe(msg =>
            {
                Console.WriteLine("接收到群消息");
                Console.WriteLine(msg.raw_message);

                MessageChain messageChain = msg.MessageChain;

                foreach (var itemmessage in messageChain)
                {
                    if (itemmessage.MessageType== MessageType.Text)
                    {
                        
                        var message= itemmessage as TextMessage;
                       
                    }
                
                }
                //foreach (MessageBase item in messageChain)
                //{
                //    if (item.MessageType == MessageType.At)
                //    {
                //        AtMessage? atMessage = item as AtMessage;
                       
                //    }
                //    if (item.MessageType == MessageType.Image)
                //    {
                //        ImageMessage? imageMessage = item as ImageMessage;
                //    }
                //}


            });
            /* request 请求事件*/
            liteLoaderQQNTBot.RequestReceived.OfType<ResponseMessage>().Subscribe(msg =>
            {
                Console.WriteLine("接收到request 请求事件");
                Console.WriteLine(msg.Text);
            });
            /* notice 通知事件*/
            liteLoaderQQNTBot.NoticeReceived.OfType<ResponseMessage>().Subscribe(msg =>
            {
                Console.WriteLine("接收到notice 通知事件");
                Console.WriteLine(msg.Text);
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
