using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 合并转发
    /// </summary>
    public class ForwardMessage: MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
    
        public ForwardMessage() { this.data = new ForwardMessageData(); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">合并转发 ID，需通过 get_forward_msg API 获取具体内容</param>
        public ForwardMessage(string id) 
        {
            this.data=new ForwardMessageData();
           // this.data.text=messagetext;
         (this.data as ForwardMessageData)!.id = id; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Forward;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "forward";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new ForwardMessageData? data { get { return base.data as ForwardMessageData; } set { base.data = (value); } } //= new ForwardMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// ForwardMessageData
    /// </summary>
    public class ForwardMessageData
    {
        /// <summary>
        ///合并转发 ID，需通过 get_forward_msg API 获取具体内容
        /// </summary>
        public string? id { get; set; }
    }
}
