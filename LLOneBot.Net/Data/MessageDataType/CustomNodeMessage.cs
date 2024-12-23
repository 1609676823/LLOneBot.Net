using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 合并转发自定义节点
    /// </summary>
    public class CustomNodeMessage : MessageBase
    {
        /// <summary>
        /// 合并转发自定义节点
        /// </summary>

        public CustomNodeMessage() { this.data = new CustomNodeMessageData(); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user_id">发送者 QQ 号</param>
        /// <param name="nickname">发送者昵称</param>
        /// <param name="content">消息内容，支持发送消息时的 message 数据类型，见 API 的参数</param>
        public CustomNodeMessage(string user_id, string nickname, string content)
        {
            this.data = new CustomNodeMessageData();
            // this.data.text=messagetext;
            (this.data as CustomNodeMessageData)!.user_id = user_id;
            (this.data as CustomNodeMessageData)!.nickname = nickname;
            (this.data as CustomNodeMessageData)!.content = content;
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.CustomNode;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "node";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new CustomNodeMessageData? data { get { return base.data as CustomNodeMessageData; } set { base.data = (value); } } //= new CustomNodeMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// CustomNodeMessageData
    /// </summary>
    public class CustomNodeMessageData
    {
        /// <summary>
        ///发送者 QQ 号
        /// </summary>
        public string? user_id { get; set; }

        /// <summary>
        /// 发送者昵称
        /// </summary>
        public string? nickname { get; set; }

        /// <summary>
        /// 消息内容，支持发送消息时的 message 数据类型，见 API 的参数
        /// </summary>
        public string? content { get; set; }
    }
}
