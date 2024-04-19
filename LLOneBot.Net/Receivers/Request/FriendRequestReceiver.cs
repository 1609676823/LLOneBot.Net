using LLOneBot.Net.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Request
{
    /// <summary>
    /// Friend请求接收器类型
    /// </summary>
    public class FriendRequestReceiver : RequestReceiverBase
    {

        /// <summary>
        /// Friend请求接收器类型
        /// </summary>
        public FriendRequestReceiver() { }
        /// <summary>
        /// Friend请求接收器类型
        /// </summary>
        [JsonIgnore]
        public override EventRequestType ReceiveRequestType { get; set; } = EventRequestType.Friend;

        /// <summary>
        /// 事件发生的时间戳
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 收到事件的机器人 QQ 号
        /// </summary>
        public long self_id { get; set; }
        /// <summary>
        /// 上报类型 request
        /// </summary>
        public string post_type { get; set; } = string.Empty;
        /// <summary>
        /// 请求类型 friend
        /// </summary>
        public string request_type { get; set; } = string.Empty;
        /// <summary>
        /// 发送请求的 QQ 号
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 验证信息
        /// </summary>
        public string comment { get; set; } = string.Empty;
        /// <summary>
        /// 请求 flag，在调用处理请求的 API 时需要传入
        /// </summary>
        public string flag { get; set; }=string.Empty;
    }



}
