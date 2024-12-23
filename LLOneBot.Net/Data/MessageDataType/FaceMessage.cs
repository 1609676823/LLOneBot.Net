using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 表情信息
    /// </summary>
    public class FaceMessage : MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
    
        public FaceMessage() { }
        /// <summary>
        ///  QQ 表情 ID 表
        /// https://github.com/kyubotics/coolq-http-api/wiki/%E8%A1%A8%E6%83%85-CQ-%E7%A0%81-ID-%E8%A1%A8
        /// </summary>
        /// <param name="id"></param>
        public FaceMessage(string id) 
        {
            this.data=new FaceMessageData();
           // this.data.text=messagetext;
         (this.data as FaceMessageData)!.id = id; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Face;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "face";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new FaceMessageData? data { get { return base.data as FaceMessageData; } set { base.data = (value); } }
       
    }


    /// <summary>
    /// FaceMessageData
    /// </summary>
    public class FaceMessageData
    {
        /// <summary>
        ///纯文本内容
        /// </summary>
        public string? id { get; set; }
    }
}
