namespace ConsoleTest
{
    using LLOneBot.Net.Data;
    using LLOneBot.Net.Sessions;
    using System.Net.WebSockets;
    using System.Reactive.Linq;
    using Websocket.Client;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    internal class Program
    {
        static void Main(string[] args)
        {

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

            LiteLoaderQQNTBot liteLoaderQQNTBot = new LiteLoaderQQNTBot() { Ip = "127.0.0.1", HttpPort = 3000, WebsocKetPort = 3001, AccessTocken = "1" };

            Task<LoginInfo> taskStartBot = liteLoaderQQNTBot.StartBot();
            taskStartBot.Wait();
            if ("ok".Equals(taskStartBot.Result.status, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("BOT连接成功");
                Console.WriteLine("账号:"+ taskStartBot.Result.user_id);
                Console.WriteLine("名称:" + taskStartBot.Result.nickname);

            }

            else
            { Console.WriteLine("BOT连接失败"); }


            /*接收meta_event元事件*/
            liteLoaderQQNTBot.Meta_eventReceived.Subscribe(e =>
            {
                Console.WriteLine(e);
            });
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
