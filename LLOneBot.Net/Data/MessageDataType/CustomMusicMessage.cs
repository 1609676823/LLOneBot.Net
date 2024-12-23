using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 自定义音乐分享信息
    /// </summary>
    public class CustomMusicMessage: MessageBase
    {
        /// <summary>
        /// 自定义音乐分享信息
        /// </summary>

        public CustomMusicMessage() { this.data = new CustomMusicMessageData(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">custom	表示音乐自定义分享</param>
        /// <param name="url">点击后跳转目标 URL</param>
        /// <param name="audio">音乐 URL</param>
        /// <param name="title">标题</param>
        /// <param name="content">发送时可选，内容描述</param>
        /// <param name="image">发送时可选，图片 URL</param>
        public CustomMusicMessage(string url,string audio,string title,string content="",string image="", string type= "custom") 
        {
            this.data=new CustomMusicMessageData();
            // this.data.text=messagetext;
            (this.data as CustomMusicMessageData)!.type = type;
            (this.data as CustomMusicMessageData)!.url = url;
            (this.data as CustomMusicMessageData)!.audio = audio;
            (this.data as CustomMusicMessageData)!.title = title;
            (this.data as CustomMusicMessageData)!.content = content;
            (this.data as CustomMusicMessageData)!.image = image;
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.CustomMusic;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "music";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new CustomMusicMessageData? data { get { return base.data as CustomMusicMessageData; } set { base.data = (value); } } //= new CustomMusicMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// CustomMusicMessageData
    /// </summary>
    public class CustomMusicMessageData
    {
        /// <summary>
        ///custom	表示音乐自定义分享
        /// </summary>
        public string? type { get; set; } = "custom";
        /// <summary>
        /// 点击后跳转目标 URL
        /// </summary>
        public string? url { get; set; }
        /// <summary>
        /// 音乐 URL
        /// </summary>
        public string? audio { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string? title { get; set; }
        /// <summary>
        /// 发送时可选，内容描述
        /// </summary>
        public string? content { get; set; }
        /// <summary>
        /// 发送时可选，图片 URL
        /// </summary>
        public string? image { get; set; }
    }
}
