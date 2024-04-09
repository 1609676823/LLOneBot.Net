using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{

    /// <summary>
    /// @某人
    /// </summary>
    public class AtMessage : MessageBase
    {
        /// <summary>
        /// AtMessage
        /// </summary>
        public AtMessage() { }



        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]     
        public Data Data { get; set; }


       

    }

    public class Data
    {
        /// <summary>
        /// @的 QQ 号，all 表示全体成员
        /// </summary>
        public string qq { get; set; }
    }

}
