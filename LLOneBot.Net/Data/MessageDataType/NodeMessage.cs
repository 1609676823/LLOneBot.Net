using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 合并转发节点
    /// </summary>
    public class NodeMessage: MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
    
        public NodeMessage() { this.data = new NodeMessageData(); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">合并转发 ID，需通过 get_forward_msg API 获取具体内容</param>
        public NodeMessage(string id) 
        {
            this.data=new NodeMessageData();
           // this.data.text=messagetext;
         (this.data as NodeMessageData)!.id = id; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Node;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "node";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new NodeMessageData? data { get { return base.data as NodeMessageData; } set { base.data = (value); } } //= new NodeMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// NodeMessageData
    /// </summary>
    public class NodeMessageData
    {
        /// <summary>
        ///合并转发 ID，需通过 get_forward_msg API 获取具体内容
        /// </summary>
        public string? id { get; set; }
    }
}
