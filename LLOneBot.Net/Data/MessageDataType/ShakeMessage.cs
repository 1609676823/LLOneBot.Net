using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 骰子魔法表情
    /// </summary>
    public class ShakeMessage : MessageBase
    {
        /// <summary>
        /// 骰子魔法表情
        /// </summary>

        public ShakeMessage() { 
            //this.data = new DiceMessageData(); 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Dice;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "dice";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new object? data { get { return base.data; } set { base.data = (value); } } //= new TextMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// TextMessageData
    /// </summary>
    public class ShakeMessageData
    {
        /// <summary>
        ///纯文本内容
        /// </summary>
        public string? result { get; set; }
    }
}
