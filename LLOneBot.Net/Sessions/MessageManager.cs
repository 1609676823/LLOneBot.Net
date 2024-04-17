using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace LLOneBot.Net.Sessions
{
    /// <summary>
    /// 消息管理器
    /// </summary>
    public static class MessageManager
    {
        /// <summary>
        /// 允许常规字符串
        /// </summary>
        public static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        /// <summary>
        /// 发送群消息
        /// </summary>
        public static string SendGroupMessage(string groupId, Data.MessageChain chain)
        {

            //string chainjson = JsonSerializer.Serialize(chain, new JsonSerializerOptions() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
            url = AppendRoutingToUrl(url, "send_group_msg");

            string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;

            /*允许常规字符串*/
            // JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

            System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
            jsonNodepost.Add("group_id", groupId);
            jsonNodepost.Add("message", JsonSerializer.SerializeToNode(chain, jsonSerializerOptions));

            string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);

            ComWebHelper.WebHelper webHelper = new ComWebHelper.WebHelper();

            if (!string.IsNullOrWhiteSpace(accesstocken))
            {

                //  Dictionary<string,string> Headersdic = new Dictionary<string,string>();
                // webHelper.RequestHeaders.Add("Content-Type", "application/json");
                webHelper.RequestHeaders.Add("Authorization", accesstocken);

            }
            webHelper.HttpMethod = HttpMethod.Post;
            webHelper.bodyType = ComWebHelper.BodyType.raw;
            webHelper.Body_Raw = postjson;
            Task<string> task = webHelper.SendHttpRequestAsync(url);
            task.Wait();
            string res = task.Result;
            return res;
        }


        /// <summary>
        /// 发送群消息异步
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="chain"></param>
        /// <returns></returns>
        public static async Task<string> SendGroupMessageAsync(string groupId, Data.MessageChain chain)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SendGroupMessage(groupId, chain);
                return resjson;
            });


            return objres;

            //Task<string> myTask = Task.Factory.StartNew(() => {
            //    // 在这里执行异步操作，并返回字符串结果
            //    return "Hello, World!";
            //});
            //return myTask.Result;
        }

        /// <summary>
        /// 拼接地址路由信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="routing"></param>
        /// <returns></returns>
        public static string AppendRoutingToUrl(string url, string routing)
        {
            if (!url.EndsWith("/"))
            {
                url += "/";
            }

            return url + routing;
        }
    }


}
