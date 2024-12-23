using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// Json信息
    /// </summary>
    public class JsonMessage: MessageBase
    {
        /// <summary>
        /// Json信息
        /// </summary>

        public JsonMessage() { this.data = new JsonMessageData(); }
        /// <summary>
        /// Json信息
        /// </summary>
        /// <param name="data">json内容</param>
        public JsonMessage(string data) 
        {
            this.data=new JsonMessageData();
           // this.data.text=messagetext;
         (this.data as JsonMessageData)!.data = data; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Json;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "json";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new JsonMessageData? data { get { return base.data as JsonMessageData; } set { base.data = (value); } } //= new JsonMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// JsonMessageData
    /// </summary>
    public class JsonMessageData
    {
        /// <summary>
        ///纯文本内容
        /// </summary>
        public string? data { get; set; }
    }
}
