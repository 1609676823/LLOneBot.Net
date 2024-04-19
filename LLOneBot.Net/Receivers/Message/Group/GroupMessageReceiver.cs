using LLOneBot.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Receivers.Message.Group
{
    /// <summary>
    /// 群消息接收器
    /// </summary>
    public class GroupMessageReceiver : MessageReceiverBase
    {
        /// <summary>
        /// 接收的信息类型Group/Private
        /// </summary>
        public override EventMessageType ReceiveMessageType { get; set; } = EventMessageType.Group;

        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonPropertyName("message_type")]
        public string? message_type { get; set; }
        /// <summary>
        /// 收到事件的机器人 QQ 号
        /// </summary>
        [JsonPropertyName("self_id")]
        public int self_id { get; set; }
        /// <summary>
        /// 发送者 QQ 号
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 事件发生的时间戳
        /// </summary>
        public int time { get; set; }
        /// <summary>
        /// 消息 ID
        /// </summary>
        public long message_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int real_id { get; set; }
        /// <summary>
        /// 发送人信息
        /// </summary>
        public Sender? sender { get; set; }
        /// <summary>
        /// 原始消息内容
        /// </summary>
        public string? raw_message { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public int font { get; set; }
        /// <summary>
        /// 消息子类型，正常消息是 normal，匿名消息是 anonymous，系统提示（如「管理员已禁止群内匿名聊天」）是 notice
        /// </summary>
        public string? sub_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private object? _messagejson;
        /// <summary>
        /// 消息json
        /// </summary>
        [JsonPropertyName("message")]
        public object? messagejson
        {

            get { return _messagejson; }
            set
            {
                _messagejson = value;
                try
                {
                    MessageChain = MessageBuilder.BulderMessageChain(value!);
                }
                catch (Exception ex)
                {
                    string error=ex.Message;
                    //  throw;
                }

            }

        }

        /// <summary>
        /// 接收的消息链
        /// </summary>
        [JsonIgnore]
        public MessageChain MessageChain { get; set; } = new MessageChain();
        /// <summary>
        /// 
        /// </summary>
        public string? message_format { get; set; }
        /// <summary>
        /// 上报类型 message
        /// </summary>
        public string? post_type { get; set; }
        /// <summary>
        /// 群号
        /// </summary>
        public int? group_id { get; set; }

        /// <summary>
        /// 匿名信息，如果不是匿名消息则为 null
        /// </summary>
        public Anonymous? anonymous { get; set; } = new Anonymous();
    }







    //public class Message
    //{
    //    public Data data { get; set; }
    //    public string type { get; set; }
    //}

    //public class Data
    //{
    //    public string id { get; set; }
    //    public string text { get; set; }
    //}

}
