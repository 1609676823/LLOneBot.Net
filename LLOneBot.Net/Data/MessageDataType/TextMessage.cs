using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 文本信息
    /// </summary>
    public class TextMessage: MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
    
        public TextMessage() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messagetext"></param>
        public TextMessage(string messagetext) 
        {
        (this.data as TextMessageData).text = messagetext; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Text;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "text";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        public override object data { get; set; } = new TextMessageData();
    }


    /// <summary>
    /// TextMessageData
    /// </summary>
    public class TextMessageData
    {
        /// <summary>
        ///纯文本内容
        /// </summary>
        public string text { get; set; }
    }
}
