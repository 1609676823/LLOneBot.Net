using LLOneBot.Net.Receivers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
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
        /// send_private_msg 发送私聊消息
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
        /// send_private_msg 发送私聊消息异步
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
        /// delete_msg 撤回消息
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
        /// delete_msg 撤回消息异步
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

        /// <summary>
        /// get_msg 获取消息
        /// </summary>
        /// <param name="message_id"></param>
        /// <returns></returns>
        public static string GetMessage(long message_id)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_msg");
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
        /// get_forward_msg 获取合并转发消息
        /// </summary>
        /// <param name="id">合并转发 ID</param>
        /// <returns></returns>
        public static string GetForwardMessage(string id)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_forward_msg");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("id", id);
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
        /// send_like 发送好友赞
        /// </summary>
        /// <param name="user_id">对方 QQ 号</param>
        /// <param name="times">赞的次数，每个好友每天最多 10 次</param>
        /// <returns></returns>
        public static string SendLike(string user_id,int times=1)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "send_like");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("times", times);
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
        /// send_like 发送好友赞异步
        /// </summary>
        /// <param name="user_id">对方 QQ 号</param>
        /// <param name="times">赞的次数，每个好友每天最多 10 次</param>
        /// <returns></returns>
        public static async Task<string> SendLikeAsync(string user_id, int times = 1)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SendLike(user_id, times);
                return resjson;
            });


            return objres;


        }

        /// <summary>
        /// set_group_kick 群组踢人
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要踢的 QQ 号</param>
        /// <param name="reject_add_request">拒绝此人的加群请求</param>
        /// <returns></returns>
        public static string SetGroupKick(string group_id, string user_id, bool reject_add_request = false)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_kick");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("reject_add_request", reject_add_request);
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
        ///  set_group_kick 群组踢人异步
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要踢的 QQ 号</param>
        /// <param name="reject_add_request">拒绝此人的加群请求</param>
        /// <returns></returns>

        public static async Task<string> SetGroupKickAsync(string group_id, string user_id, bool reject_add_request = false)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupKick(group_id, user_id, reject_add_request);
                return resjson;
            });
            return objres;


        }

        /// <summary>
        /// set_group_ban 群组单人禁言/解除禁言
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要禁言的 QQ 号</param>
        /// <param name="duration">禁言时长，单位秒，0 表示取消禁言</param>
        /// <returns></returns>
        public static string SetGroupBan(string group_id, string user_id, long duration )
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_ban");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("duration", duration);
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
        /// set_group_ban 群组单人禁言/解除禁言 异步
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要禁言的 QQ 号</param>
        /// <param name="duration">禁言时长，单位秒，0 表示取消禁言</param>
        /// <returns></returns>
        public static async Task<string> SetGroupBanAsync(string group_id, string user_id, long duration )
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupBan(group_id, user_id, duration);
                return resjson;
            });


            return objres;
        }

        /// <summary>
        /// set_group_anonymous_ban 群组匿名用户禁言
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="anonymous">要禁言的匿名用户对象（群消息上报的 anonymous 字段）</param>
        /// <param name="duration">禁言时长，单位秒，无法取消匿名用户禁言</param>
        /// <returns></returns>
        public static string SetGroupAnonymousBan(string group_id, Anonymous anonymous, long duration)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_anonymous_ban");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("anonymous", JsonSerializer.SerializeToNode(anonymous, jsonSerializerOptions));
                jsonNodepost.Add("duration", duration);
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
        /// set_group_anonymous_ban 群组匿名用户禁言
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="anonymous_flag">要禁言的匿名用户的 flag（需从群消息上报的数据中获得）</param>
        /// <param name="duration">禁言时长，单位秒，无法取消匿名用户禁言</param>
        /// <returns></returns>
        public static string SetGroupAnonymousBan(string group_id, string anonymous_flag, long duration)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_anonymous_ban");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("anonymous_flag", anonymous_flag);
                jsonNodepost.Add("flag", anonymous_flag);
                jsonNodepost.Add("duration", duration);
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
        /// set_group_anonymous_ban 群组匿名用户禁言异步
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="anonymous">要禁言的匿名用户对象（群消息上报的 anonymous 字段）</param>
        /// <param name="duration">禁言时长，单位秒，无法取消匿名用户禁言</param>
        /// <returns></returns>
        public static async Task<string> SetGroupAnonymousBanAsync(string group_id, Anonymous anonymous, long duration)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupAnonymousBan(group_id, anonymous, duration);
                return resjson;
            });


            return objres;
        }
        /// <summary>
        /// set_group_anonymous_ban 群组匿名用户禁言异步
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="anonymous_flag">要禁言的匿名用户的 flag（需从群消息上报的数据中获得）</param>
        /// <param name="duration">禁言时长，单位秒，无法取消匿名用户禁言</param>
        /// <returns></returns>
        public static async Task<string> SetGroupAnonymousBanAsync(string group_id, string anonymous_flag, long duration)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupAnonymousBan(group_id, anonymous_flag, duration);
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
