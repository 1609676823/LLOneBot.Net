using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Receivers
{
    public class MessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonPropertyName("type")]
        [System.Text.Json.Serialization.JsonConverter(typeof(MessageType))]
        public virtual string? Type { get; set; }
    }


    /// <summary>
    /// 发送消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 纯文本内容text
        /// </summary>
        Text,

        /// <summary>
        /// 表情
        /// </summary>
        Face,

        /// <summary>
        /// 图片
        /// </summary>
        Image,
        /// <summary>
        /// 语音
        /// </summary>
        Record,
    }
}
