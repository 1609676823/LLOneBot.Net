using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 回复信息
    /// </summary>
    public class ReplyMessage : MessageBase
    {
        /// <summary>
        /// 回复信息
        /// </summary>

        public ReplyMessage() { }

        /// <summary>
        /// 回复信息
        /// </summary>
        /// <param name="id">回复时引用的消息 ID</param>
        public ReplyMessage(string id) 
        {
            this.data=new ReplyMessageData();
           // this.data.text=messagetext;
         (this.data as ReplyMessageData)!.id = id; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Reply;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "reply";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new ReplyMessageData? data { get { return base.data as ReplyMessageData; } set { base.data = (value); } }
       
    }


    /// <summary>
    /// FaceMessageData
    /// </summary>
    public class ReplyMessageData
    {
        /// <summary>
        ///纯文本内容
        /// </summary>
        public string id { get; set; }=string.Empty;
    }
}
