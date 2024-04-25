using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 猜拳魔法表情
    /// </summary>
    public class RpsMessage : MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
    
        public RpsMessage() { this.data = new RpsMessageData();  }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="messagetext"></param>
        //public RpsMessage(string result) 
        //{
        //    this.data=new RpsMessageData();
        //   // this.data.text=messagetext;
        // (this.data as RpsMessageData)!.result = result; 
        //}


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Rps;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "rps";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new RpsMessageData? data { get { return base.data as RpsMessageData; } set { base.data = (value); } } //= new TextMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// TextMessageData
    /// </summary>
    public class RpsMessageData
    {
        /// <summary>
        ///纯文本内容
        /// </summary>
        public string? result { get; set; }
    }
}
