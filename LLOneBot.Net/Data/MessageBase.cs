using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data
{
    public class MessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonPropertyName("type")]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        public virtual MessageType Type { get; set; }
        
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
        /// <summary>
        /// 短视频
        /// </summary>
        Video,
        /// <summary>
        /// @某人
        /// </summary>
        At,
        /// <summary>
        /// 猜拳魔法表情
        /// </summary>
        Rps,
        /// <summary>
        /// 窗口抖动（戳一戳）
        /// </summary>
        Shake,
        /// <summary>
        /// 匿名发消息
        /// </summary>
        Anonymous,
        /// <summary>
        /// 链接分享
        /// </summary>
        Share,
        /// <summary>
        /// 推荐好友/群
        /// </summary>
        Contact

    }
}
