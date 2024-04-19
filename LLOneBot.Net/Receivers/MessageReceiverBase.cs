using LLOneBot.Net.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Receivers
{
    /// <summary>
    /// 消息接收器基类
    /// </summary>
    public class MessageReceiverBase
    {
        /// <summary>
        /// 消息接收器类型
        /// </summary>
      //  [JsonPropertyName("message_type")]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        public virtual EventMessageType ReceiveMessageType { get; set; }

        //private string _message_type;
        //public string message_type 

        //{
        //    get { return _message_type; }
        //    set
        //    {
        //        _message_type=value;
        //        if ("group".Equals(value,StringComparison.OrdinalIgnoreCase)) { Messagetype = ReceiveMessageType.Group; }
        //        if ("private".Equals(value, StringComparison.OrdinalIgnoreCase)) { Messagetype = ReceiveMessageType.Private; }
        //    }
        //}
        ///// <summary>
        ///// 接收到的消息类型
        ///// </summary>
        //[JsonIgnore]
        //public ReceiveMessageType Messagetype {get; set;} = ReceiveMessageType.Unknown;
    }
}
