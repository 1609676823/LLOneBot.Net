using LLOneBot.Net.Receivers.Message;
using PostManagerTool.Net;
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

            PostManagerToolClient webHelper = new PostManagerToolClient();
            string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
            if (!string.IsNullOrWhiteSpace(accesstocken))
            {

                //  Dictionary<string,string> Headersdic = new Dictionary<string,string>();
                // webHelper.RequestHeaders.Add("Content-Type", "application/json");
                webHelper.RequestHeaders.Add("Authorization", accesstocken);

            }
            webHelper.HttpWebRequestMethod = HttpMethodType.Post;
            webHelper.bodyType = BodyType.raw;
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

            PostManagerToolClient webHelper = new PostManagerToolClient();
            string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
            if (!string.IsNullOrWhiteSpace(accesstocken))
            {

                //  Dictionary<string,string> Headersdic = new Dictionary<string,string>();
                // webHelper.RequestHeaders.Add("Content-Type", "application/json");
                webHelper.RequestHeaders.Add("Authorization", accesstocken);

            }
            webHelper.HttpWebRequestMethod = HttpMethodType.Get;
            webHelper.bodyType = BodyType.none;
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
        public static string SendLike(string user_id,long times=1)
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
        public static async Task<string> SendLikeAsync(string user_id, long times = 1)
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

        /// <summary>
        /// set_group_whole_ban 群组全员禁言
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="enable">是否禁言</param>
        /// <returns></returns>
        public static string SetGroupWholeBan(string group_id, bool enable)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_whole_ban");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);      
                jsonNodepost.Add("enable", enable);
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
        /// set_group_whole_ban 群组全员禁言异步
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="enable">是否禁言</param>
        /// <returns></returns>
        public static async Task<string> SetGroupWholeBanAsync(string group_id, bool enable)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupWholeBan(group_id, enable);
                return resjson;
            });


            return objres;
        }

        /// <summary>
        /// set_group_admin 群组设置管理员
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要设置管理员的 QQ 号</param>
        /// <param name="enable">true 为设置，false 为取消</param>
        /// <returns></returns>
        public static string SetGroupAdmin(string group_id,string user_id, bool enable)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_admin");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("enable", enable);
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
        /// set_group_admin 群组设置管理员异步
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要设置管理员的 QQ 号</param>
        /// <param name="enable">true 为设置，false 为取消</param>
        /// <returns></returns>
        public static async Task<string> SetGroupAdminAsync(string group_id,string user_id,bool enable)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupAdmin(group_id, user_id, enable);
                return resjson;
            });


            return objres;
        }

        /// <summary>
        /// set_group_anonymous 群组匿名(可能不生效)
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="enable">是否允许匿名聊天</param>
        /// <returns></returns>
        public static string SetGroupAnonymous(string group_id, bool enable)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_anonymous");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);     
                jsonNodepost.Add("enable", enable);
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
        /// set_group_anonymous 群组匿名异步(可能不生效)
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="enable">是否允许匿名聊天</param>
        /// <returns></returns>
        public static async Task<string> SetGroupAnonymousAsync(string group_id, bool enable)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupAnonymous(group_id,enable);
                return resjson;
            });


            return objres;
        }

        /// <summary>
        /// set_group_card 设置群名片（群备注）
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要设置的 QQ 号</param>
        /// <param name="card">群名片内容，不填或空字符串表示删除群名片</param>
        /// <returns></returns>
        public static string SetGroupCard(string group_id, string user_id, string card)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url,"set_group_card");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("card", card);
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
        /// set_group_card 设置群名片（群备注）异步
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要设置的 QQ 号</param>
        /// <param name="card">群名片内容，不填或空字符串表示删除群名片</param>
        /// <returns></returns>
        public static async Task<string> SetGroupCardAsync(string group_id, string user_id, string card)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupCard(group_id, user_id, card);
                return resjson;
            });


            return objres;
        }
        /// <summary>
        /// set_group_name 设置群名
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="group_name">新群名</param>
        /// <returns></returns>
        public static string SetGroupName(string group_id, string group_name)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_name");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("group_name", group_name);
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
        /// set_group_name 设置群名异步
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="group_name">新群名</param>
        /// <returns></returns>
        public static async Task<string> SetGroupNamedAsync(string group_id, string group_name)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupName(group_id, group_name);
                return resjson;
            });


            return objres;
        }

        /// <summary>
        /// set_group_leave 退出群组(谨慎使用)
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="is_dismiss">(谨慎使用)是否解散，如果登录号是群主，则仅在此项为 true 时能够解散</param>
        /// <returns></returns>
        public static string SetGroupLeave(string group_id, bool is_dismiss=false)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_leave");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("is_dismiss", is_dismiss);
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
        /// set_group_leave 退出群组异步(谨慎使用)
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="is_dismiss">(谨慎使用)是否解散，如果登录号是群主，则仅在此项为 true 时能够解散</param>
        /// <returns></returns>
        public static async Task<string> SetGroupLeaveAsync(string group_id, bool is_dismiss = false)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupLeave(group_id, false);
                return resjson;
            });


            return objres;
        }
        /// <summary>
        /// set_group_special_title 设置群组专属头衔(预留功能可能未支持)
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要设置的 QQ 号</param>
        /// <param name="special_title">专属头衔，不填或空字符串表示删除专属头衔</param>
        /// <param name="duration">专属头衔有效期，单位秒，-1 表示永久，不过此项似乎没有效果，可能是只有某些特殊的时间长度有效，有待测试</param>
        /// <returns></returns>
        public static string SetGroupSpecialTitle(string group_id, string user_id ,string special_title,long duration=-1)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_special_title");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("special_title", special_title);
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
        /// set_group_special_title 设置群组专属头衔异步(预留功能可能未支持)
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">要设置的 QQ 号</param>
        /// <param name="special_title">专属头衔，不填或空字符串表示删除专属头衔</param>
        /// <param name="duration">专属头衔有效期，单位秒，-1 表示永久，不过此项似乎没有效果，可能是只有某些特殊的时间长度有效，有待测试</param>
        /// <returns></returns>
        public static async Task<string> SetGroupSpecialTitleAsync(string group_id, string user_id, string special_title, long duration = -1)
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupSpecialTitle(group_id, user_id,special_title, duration);
                return resjson;
            });


            return objres;
        }

        /// <summary>
        /// set_friend_add_request 处理加好友请求
        /// </summary>
        /// <param name="flag">加好友请求的 flag（需从上报的数据中获得）</param>
        /// <param name="approve">是否同意请求</param>
        /// <param name="remark">添加后的好友备注（仅在同意时有效）</param>
        /// <returns></returns>
        public static string SetFriendAddRequest(string flag, bool approve, string remark="")
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url,"set_friend_add_request");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("flag", flag);
                jsonNodepost.Add("approve", approve);
                jsonNodepost.Add("remark", remark);
               

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
        /// set_friend_add_request 处理加好友请求异步
        /// </summary>
        /// <param name="flag">加好友请求的 flag（需从上报的数据中获得）</param>
        /// <param name="approve">是否同意请求</param>
        /// <param name="remark">添加后的好友备注（仅在同意时有效）</param>
        /// <returns></returns>
        public static async Task<string> SetFriendAddRequestAsync(string flag, bool approve, string remark = "")
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetFriendAddRequest(flag, approve,remark);
                return resjson;
            });


            return objres;
        }

        /// <summary>
        /// set_group_add_request 处理加群请求／邀请
        /// </summary>
        /// <param name="flag">加群请求的 flag（需从上报的数据中获得）</param>
        /// <param name="sub_type">add 或 invite，请求类型（需要和上报消息中的 sub_type 字段相符）</param>
        /// <param name="approve">是否同意请求／邀请</param>
        /// <param name="reason">拒绝理由（仅在拒绝时有效）</param>
        /// <returns></returns>
        public static string SetGroupAddRequest(string flag, string sub_type, bool approve, string reason = "")
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_group_add_request");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("flag", flag);
                jsonNodepost.Add("sub_type", sub_type);
                jsonNodepost.Add("type",sub_type);
                jsonNodepost.Add("approve", approve);
                jsonNodepost.Add("reason", reason);


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
        /// set_group_add_request 处理加群请求／邀请异步
        /// </summary>
        /// <param name="flag">加群请求的 flag（需从上报的数据中获得）</param>
        /// <param name="sub_type">add 或 invite，请求类型（需要和上报消息中的 sub_type 字段相符）</param>
        /// <param name="approve">是否同意请求／邀请</param>
        /// <param name="reason">拒绝理由（仅在拒绝时有效）</param>
        /// <returns></returns>
        public static async Task<string> SetGroupAddRequestAsync(string flag, string sub_type, bool approve, string reason = "")
        {
            var objres = await Task.Run(() =>
            {
                string resjson = SetGroupAddRequest(flag, sub_type, approve, reason);
                return resjson;
            });


            return objres;
        }

        /// <summary>
        /// get_login_info 获取登录号信息
        /// </summary>
        /// <returns></returns>
        public static string GetLoginInfo()
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_login_info");
                //System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                //jsonNodepost.Add("flag", flag);
  


                //string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicGet(url);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }

        /// <summary>
        /// get_stranger_info 获取陌生人信息
        /// </summary>
        /// <param name="user_id">QQ 号</param>
        /// <param name="no_cache">是否不使用缓存（使用缓存可能更新不及时，但响应更快）</param>
        /// <returns></returns>
        public static string GetStrangerInfo(string user_id, bool no_cache=false)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_stranger_info");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("no_cache", no_cache);
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
        /// get_friend_list 获取好友列表
        /// </summary>
        /// <returns></returns>
        public static string GetFriendList()
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_friend_list");
                //System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                //jsonNodepost.Add("flag", flag);



                //string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicGet(url);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }

        /// <summary>
        /// get_group_info 获取群信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="no_cache">是否不使用缓存（使用缓存可能更新不及时，但响应更快）</param>
        /// <returns></returns>
        public static string GetGroupInfo(string group_id, bool no_cache = false)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_group_info");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("no_cache", no_cache);
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
        /// get_group_list 获取群列表
        /// </summary>
        /// <returns></returns>
        public static string GetGroupList()
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_group_list");
                //System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                //jsonNodepost.Add("flag", flag);



                //string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicGet(url);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }

        /// <summary>
        /// get_group_member_info 获取群成员信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">QQ 号</param>
        /// <param name="no_cache">是否不使用缓存（使用缓存可能更新不及时，但响应更快）</param>
        /// <returns></returns>
        public static string GetGroupMemberInfo(string group_id,string user_id, bool no_cache = false)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_group_member_info");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("user_id", user_id);
                jsonNodepost.Add("no_cache", no_cache);
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
        /// get_group_member_list 获取群成员列表
        /// </summary>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public static string GetGroupMemberList(string group_id)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_group_member_list");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
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
        /// get_group_honor_info 获取群荣誉信息(预留功能可能未生效)(https://github.com/botuniverse/onebot-11/blob/master/api/public.md#get_group_honor_info-%E8%8E%B7%E5%8F%96%E7%BE%A4%E8%8D%A3%E8%AA%89%E4%BF%A1%E6%81%AF)
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="type">要获取的群荣誉类型，可传入 talkative performer legend strong_newbie emotion 以分别获取单个类型的群荣誉数据，或传入 all 获取所有数据 </param>
        /// <returns></returns>
        public static string GetGroupHonorInfo(string group_id,string type)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_group_honor_info");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("group_id", group_id);
                jsonNodepost.Add("type", type);
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
        /// get_cookies 获取 Cookies(预留功能可能未生效)
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string GetCookies(string domain)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_cookies");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("domain", domain);
                
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
        /// get_csrf_token 获取 CSRF Token(预留功能可能未生效)
        /// </summary>
        /// <returns></returns>
        public static string GetCsrfToken()
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_csrf_token");
                //System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                //jsonNodepost.Add("flag", flag);



                //string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicGet(url);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }

        /// <summary>
        /// get_credentials 获取 QQ 相关接口凭证
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string GetCredentials(string domain)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_credentials");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("domain", domain);

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
        /// get_record 获取语音
        /// </summary>
        /// <param name="file">收到的语音文件名（消息段的 file 参数），如 0B38145AA44505000B38145AA4450500.silk</param>
        /// <param name="out_format">要转换到的格式，目前支持 mp3、amr、wma、m4a、spx、ogg、wav、flac</param>
        /// <returns></returns>
        public static string GetRecord(string file,string out_format)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_record");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("file", file);
                jsonNodepost.Add("out_format", out_format);
                string postjson = JsonSerializer.Serialize(jsonNodepost,jsonSerializerOptions);
                resjson = ApiPublicPost(url, postjson);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }


        /// <summary>
        /// get_image 获取图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetImage(string file)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_image");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("file", file);
               
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
        /// can_send_image 检查是否可以发送图片
        /// </summary>
        /// <returns></returns>
        public static string CanSendImage()
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "can_send_image");
                //System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                //jsonNodepost.Add("flag", flag);



                //string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicGet(url);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }


        /// <summary>
        /// can_send_record 检查是否可以发送语音
        /// </summary>
        /// <returns></returns>
        public static string CanSendRecord()
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "can_send_record");
                //System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                //jsonNodepost.Add("flag", flag);



                //string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicGet(url);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }

        /// <summary>
        /// get_status 获取运行状态
        /// </summary>
        /// <returns></returns>
        public static string GetStatus()
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_status");
                //System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                //jsonNodepost.Add("flag", flag);



                //string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicGet(url);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }


        /// <summary>
        /// get_version_info 获取版本信息
        /// </summary>
        /// <returns></returns>
        public static string GetVersionInfo()
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "get_version_info");
                //System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                //jsonNodepost.Add("flag", flag);

                //string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicGet(url);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }

        /// <summary>
        /// set_restart 重启 OneBot 实现(预留功能未生效)
        /// 由于重启 OneBot 实现同时需要重启 API 服务，这意味着当前的 API 请求会被中断，因此需要异步地重启，接口返回的 status 是 async。
        /// </summary>
        /// <param name="delay">要延迟的毫秒数，如果默认情况下无法重启，可以尝试设置延迟为 2000 左右</param>
        /// <returns></returns>
        public static string SetRestart(long delay=0)
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url, "set_restart");
                System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                jsonNodepost.Add("delay", delay);

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
        /// clean_cache 清理缓存
        /// 用于清理积攒了太多的缓存文件。
        /// </summary>
        /// <returns></returns>
        public static string CleanCache()
        {
            string resjson = string.Empty;
            try
            {
                //  string accesstocken = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.AccessTocken! : string.Empty;
                string url = LiteLoaderQQNTBot.Instance != null ? LiteLoaderQQNTBot.Instance.HttpIpaddress! : string.Empty;
                url = AppendRoutingToUrl(url,"clean_cache");
                //System.Text.Json.Nodes.JsonObject jsonNodepost = new System.Text.Json.Nodes.JsonObject();
                //jsonNodepost.Add("flag", flag);

                //string postjson = JsonSerializer.Serialize(jsonNodepost, jsonSerializerOptions);
                resjson = ApiPublicGet(url);

            }
            catch (Exception)
            {
                //  throw;
            }
            return resjson;

        }
        /// <summary>
        /// clean_cache 清理缓存异步
        /// 用于清理积攒了太多的缓存文件。
        /// </summary>
        /// <returns></returns>
        public static async Task<string> CleanCacheAsync()
        {
            var objres = await Task.Run(() =>
            {
                string resjson = CleanCache();
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
            if (!url.EndsWith('/'))
            {
                url += "/";
            }

            return url + routing;
        }

        /// <summary>
        /// 获取发送消息的API响应类
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
