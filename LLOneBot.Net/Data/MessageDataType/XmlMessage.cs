using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// XML信息
    /// </summary>
    public class XmlMessage: MessageBase
    {
        /// <summary>
        /// XML信息
        /// </summary>

        public XmlMessage() { this.data = new XmlMessageData(); }
        /// <summary>
        /// XML信息
        /// </summary>
        /// <param name="data">xml内容</param>
        public XmlMessage(string data) 
        {
            this.data=new XmlMessageData();
           // this.data.text=messagetext;
         (this.data as XmlMessageData)!.data = data; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Xml;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "xml";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new XmlMessageData? data { get { return base.data as XmlMessageData; } set { base.data = (value); } } //= new XmlMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// XmlMessageData
    /// </summary>
    public class XmlMessageData
    {
        /// <summary>
        ///xml内容
        /// </summary>
        public string? data { get; set; }
    }
}
