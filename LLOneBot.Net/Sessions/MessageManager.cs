using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
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
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postjson"></param>
        /// <returns></returns>
        private static string ApiPublicPost(string url,string postjson) 
        {
            string resjson = string.Empty;
            #region post

            ComWebHelper.WebHelper webHelper = new ComWebHelper.WebHelper();
            string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
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
            resjson = task.Result;
            #endregion
            return resjson;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string ApiPublicGet(string url)
        {
            string resjson = string.Empty;
            #region post

            ComWebHelper.WebHelper webHelper = new ComWebHelper.WebHelper();
            string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
            if (!string.IsNullOrWhiteSpace(accesstocken))
            {

                //  Dictionary<string,string> Headersdic = new Dictionary<string,string>();
                // webHelper.RequestHeaders.Add("Content-Type", "application/json");
                webHelper.RequestHeaders.Add("Authorization", accesstocken);

            }
            webHelper.HttpMethod = HttpMethod.Get;
            webHelper.bodyType = ComWebHelper.BodyType.none;
            //webHelper.Body_Raw = postjson;
            Task<string> task = webHelper.SendHttpRequestAsync(url);
            task.Wait();
            resjson = task.Result;
            #endregion
            return resjson;
        }

        /// <summary>
        /// 允许常规字符串
        /// </summary>
        public static JsonSerializerOptions jsonSerializerOptions { get; set; } = new JsonSerializerOptions()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        /// <summary>
        ///  发送群消息
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="chain">消息链</param>
        /// <param name="auto_escape">消息内容是否作为纯文本发送（即不解析 CQ 码），只在 message 字段是字符串时有效</param>
        /// <returns></returns>
        public static string SendGroupMessage(string groupId, Data.MessageChain chain, bool auto_escape = false)
        {
            string resjson = string.Empty;
            try
            {


                //string chainjson = JsonSerializer.Serialize(chain, new JsonSerializerOptions() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "send_group_msg");

             

                /*允许常规字符串*/
                // JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", groupId);
                jsonNodepost.Add("message", JsonSerializer.SerializeToNode(chain, jsonSerializerOptions));

                if (auto_escape) { jsonNodepost.Add("auto_escape", auto_escape); }

                string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson= ApiPublicPost(url, postjson);
              
            }
            catch (Exception)
            {

                // throw;
            }
            return resjson;


        }
        /// <summary>
        /// 发送群消息异步
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="chain">消息链</param>
        /// <param name="auto_escape">消息内容是否作为纯文本发送（即不解析 CQ 码），只在 message 字段是字符串时有效</param>
        /// <returns></returns>
        public static async Task<string> SendGroupMessageAsync(string groupId, Data.MessageChain chain, bool auto_escape = false)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SendGroupMessage(groupId, chain, auto_escape);
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
        /// 发送好友消息
        /// </summary>
        /// <param name="user_id">对方 QQ 号</param>
        /// <param name="chain">要发送的消息链</param>
        /// <param name="auto_escape">消息内容是否作为纯文本发送（即不解析 CQ 码），只在 message 字段是字符串时有效</param>
        public static string SendFriendMessage(string user_id, Data.MessageChain chain, bool auto_escape = false)
        {
            return SendPrivateMessage(user_id, chain, auto_escape);
        }
        /// <summary>
        /// 发送好友消息异步
        /// </summary>
        /// <param name="user_id">对方 QQ 号</param>
        /// <param name="chain">要发送的消息链</param>
        /// <param name="auto_escape">消息内容是否作为纯文本发送（即不解析 CQ 码），只在 message 字段是字符串时有效</param>
        /// <returns></returns>
        public static async Task<string> SendFriendMessageAsync(string user_id, Data.MessageChain chain, bool auto_escape = false)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SendFriendMessage(user_id, chain, auto_escape);
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
        /// 发送私聊消息
        /// </summary>
        /// <param name="user_id">对方 QQ 号</param>
        /// <param name="chain">要发送的消息链</param>
        /// <param name="auto_escape">消息内容是否作为纯文本发送（即不解析 CQ 码），只在 message 字段是字符串时有效</param>
        public static string SendPrivateMessage(string user_id, Data.MessageChain chain, bool auto_escape = false)
        {
            string resjson = string.Empty;
            try
            {

              //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "send_private_msg");

                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("message", JsonSerializer.SerializeToNode(chain, jsonSerializerOptions));
                if (auto_escape) { jsonNodepost.Add("auto_escape", auto_escape); }
                string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);

                resjson = ApiPublicPost(url, postjson);



            }
            catch (Exception)
            {

                //  throw;
            }



            return resjson;
        }

        /// <summary>
        /// 发送私聊消息异步
        /// </summary>
        /// <param name="user_id">对方 QQ 号</param>
        /// <param name="chain">要发送的消息链</param>
        /// <param name="auto_escape">消息内容是否作为纯文本发送（即不解析 CQ 码），只在 message 字段是字符串时有效</param>
        /// <returns></returns>
        public static async Task<string> SendPrivateMessageAsync(string user_id, Data.MessageChain chain, bool auto_escape = false)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SendPrivateMessage(user_id, chain, auto_escape);
                return resjson;
            });


            return objres;

           
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message_type">消息类型，支持 private、group，分别对应私聊、群组，如不传入，则根据传入的 *_id 参数判断</param>
        /// <param name="user_id">对方 QQ 号（消息类型为 private 时需要）</param>
        /// <param name="group_id">群号（消息类型为 group 时需要）</param>
        /// <param name="chain">要发送的内容(消息链)</param>
        /// <param name="auto_escape">消息内容是否作为纯文本发送（即不解析 CQ 码），只在 message 字段是字符串时有效</param>
        /// <returns></returns>
        public static string SendMessage(string user_id,string group_id, Data.MessageChain chain, Data.EventMessageType message_type= Data.EventMessageType.None, bool auto_escape = false)
        {
            string resjson = string.Empty;
            try
            {
                
               

                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "send_msg");

                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();

                if (!message_type.Equals(Data.EventMessageType.None)) 
                {
                    jsonNodepost.Add("message_type", message_type.ToString().ToLower());
                }
                
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("message", JsonSerializer.SerializeToNode(chain, jsonSerializerOptions));
                if (auto_escape) { jsonNodepost.Add("auto_escape", auto_escape); }
                string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);

                resjson = ApiPublicPost(url, postjson);



            }
            catch (Exception)
            {

                //  throw;
            }



            return resjson;
        }

        /// <summary>
        /// 发送消息异步
        /// </summary>
        /// <param name="user_id">对方 QQ 号（消息类型为 private 时需要）</param>
        /// <param name="group_id">群号（消息类型为 group 时需要）</param>
        /// <param name="chain">要发送的内容(消息链)</param>
        /// <param name="message_type">消息类型，支持 private、group，分别对应私聊、群组，如不传入，则根据传入的 *_id 参数判断</param>
        /// <param name="auto_escape">消息内容是否作为纯文本发送（即不解析 CQ 码），只在 message 字段是字符串时有效</param>
        /// <returns></returns>
        public static async Task<string> SendMessageAsync(string user_id, string group_id, Data.MessageChain chain, Data.EventMessageType message_type = Data.EventMessageType.None, bool auto_escape = false)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SendMessage(user_id, group_id, chain, message_type,auto_escape);
                return resjson;
            });


            return objres;


        }
        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns></returns>
        public static string DeleteMessage(long message_id)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "delete_msg");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("message_id", message_id);
                string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicPost(url, postjson);

            }
            catch (Exception)
            {

                //  throw;
            }



            return resjson;

        }
        /// <summary>
        /// 撤回消息异步
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns></returns>
        public static async Task<string> DeleteMessageAsync(long message_id)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = DeleteMessage(message_id);
                return resjson;
            });


            return objres;


        }

        /********************************************************************************************************************************************/
        /*****************************************自定义公共方法***************************************************************************************************/
        /********************************************************************************************************************************************/
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

        /// <summary>
        /// 获取API响应类
        /// </summary>
        /// <param name="json">API响应的json</param>
        /// <returns></returns>
        public static Data.OneBotApiResponse GetOneBotApiResponse(string json) 
        {
            Data.OneBotApiResponse oneBotApiResponse = new Data.OneBotApiResponse();
            try
            {
                oneBotApiResponse = JsonSerializer.Deserialize<Data.OneBotApiResponse>(json)!;
            }
            catch (Exception)
            {

               // throw;
            }
          

            return oneBotApiResponse;
        }
    }


}
