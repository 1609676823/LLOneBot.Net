using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data
{
    /// <summary>
    /// 所有消息的基类
    /// </summary>
    public  class MessageBase
    {

        /// <summary>
        /// MessageBase
        /// </summary>
        public MessageBase()
        {
            
        }


        // [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]

        /// <summary>
        /// 消息类型
        /// </summary>
        //
        [JsonIgnore]
        public virtual MessageType MessageType { get; set; } = MessageType.Unknown;
        /// <summary>
        /// 消息类型string
        /// </summary>
        [JsonPropertyName("type")]
        public virtual string type { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("data")]
        public virtual object? data { get; set; }



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
        Contact,
        /// <summary>
        /// 位置
        /// </summary>
        Location,
        /// <summary>
        /// 音乐分享/音乐自定义分享
        /// </summary>
        Music,
        /// <summary>
        /// 回复
        /// </summary>
        Reply,
        /// <summary>
        /// 合并转发
        /// </summary>
        Forward,
        /// <summary>
        /// 合并转发节点/合并转发自定义节点
        /// </summary>
        Node,
        /// <summary>
        /// XML 消息
        /// </summary>
        Xml,
        /// <summary>
        /// JSON 消息
        /// </summary>
        Json,
        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember(Value = "Unknown")]
        [Description("Unknown")]
        Unknown,


    }
}
