using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 推荐好友/群
    /// </summary>
    public class ContactMessage: MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
    
        public ContactMessage() { this.data = new ContactMessageData(); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">推荐的QQ号码/群号码</param>
        /// <param name="type">推荐类型 group/qq</param>
        public ContactMessage(string id,string type) 
        {
            this.data=new ContactMessageData();
            // this.data.text=messagetext;
            (this.data as ContactMessageData)!.type = type;
            (this.data as ContactMessageData)!.id = id; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Contact;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "contact";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new ContactMessageData? data { get { return base.data as ContactMessageData; } set { base.data = (value); } } //= new ContactMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// ContactMessageData
    /// </summary>
    public class ContactMessageData
    {
        /// <summary>
        ///推荐类型 group/qq
        /// </summary>
        public string? type { get; set; }
        /// <summary>
        /// 推荐好友/群
        /// </summary>
        public string? id { get; set; }
    }
}
