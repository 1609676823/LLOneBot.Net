using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    public class AnonymousMessage: MessageBase
    {

        /// <summary>
        /// 匿名消息(仅封装，功能可能未支持)
        /// </summary>

        public AnonymousMessage() { this.data = new AnonymousMessageData(); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messagetext">信息内容</param>
        /// <param name="ignore"> 可选，表示无法匿名时是否继续发送 0 ,1</param>
        public AnonymousMessage(string messagetext, string ignore="1")
        {
            this.data = new AnonymousMessageData();
            // this.data.text=messagetext;
            (this.data as AnonymousMessageData)!.text = messagetext;
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Anonymous;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "anonymous";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new AnonymousMessageData? data { get { return base.data as AnonymousMessageData; } set { base.data = (value); } } //= new AnonymousMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// AnonymousMessageData
    /// </summary>
    public class AnonymousMessageData
    {
        /// <summary>
        ///纯文本内容
        /// </summary>
        public string? text { get; set; }
        /// <summary>
        /// 可选，表示无法匿名时是否继续发送 0 ,1
        /// </summary>
        public string ignore { get; set; } = "1";
    }
}
