using LLOneBot.Net.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Request
{
    /// <summary>
    /// 请求接收器基类
    /// </summary>
    public class RequestReceiverBase
    {
        /// <summary>
        /// 请求接收器基类
        /// </summary>
        public RequestReceiverBase() { }
        /// <summary>
        /// 请求接收器类型
        /// </summary>
        [JsonIgnore]
        public virtual EventRequestType ReceiveRequestType { get; set; } = EventRequestType.Unknown;
    }
}
