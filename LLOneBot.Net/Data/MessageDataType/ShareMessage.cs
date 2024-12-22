using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 链接分享
    /// </summary>
    public class ShareMessage : MessageBase
    {
        /// <summary>
        /// 
        /// </summary>

        public ShareMessage() { this.data = new ShareMessageData(); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        public ShareMessage(string url, string title = "")
        {
            this.data = new ShareMessageData();
            // this.data.text=messagetext;
            (this.data as ShareMessageData)!.url = url;
            (this.data as ShareMessageData)!.title = title;
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Share;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "share";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new ShareMessageData? data { get { return base.data as ShareMessageData; } set { base.data = (value); } } //= new ShareMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// ShareMessageData
    /// </summary>
    public class ShareMessageData
    {
        /// <summary>
        ///地址
        /// </summary>
        public string? url { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? title { get; set; }
    }
}
