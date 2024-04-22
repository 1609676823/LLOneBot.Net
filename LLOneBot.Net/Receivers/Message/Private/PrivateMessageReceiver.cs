using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LLOneBot.Net.Data;


namespace LLOneBot.Net.Receivers.Message.Private
{
    /// <summary>
    /// 私信消息接收器
    /// </summary>
    public class PrivateMessageReceiver : MessageReceiverBase
    {
        /// <summary>
        /// 接收的信息类型Group/Private
        /// </summary>
        public override EventMessageType ReceiveMessageType { get; set; } = EventMessageType.Private;
        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonPropertyName("message_type")]
        public string? message_type { get; set; }
        /// <summary>
        /// 收到事件的机器人 QQ 号
        /// </summary>
        public long? self_id { get; set; }
        /// <summary>
        /// 发送者 QQ 号
        /// </summary>
        public long? user_id { get; set; }
        /// <summary>
        /// 事件发生的时间戳
        /// </summary>
        public long? time { get; set; }
        /// <summary>
        /// 消息 ID
        /// </summary>
        public long? message_id { get; set; }
        /// <summary>
        /// real_id
        /// </summary>
        public long? real_id { get; set; }
        /// <summary>
        /// 发送人信息
        /// </summary>
        public Sender? sender { get; set; } = new Sender();
        /// <summary>
        /// 原始消息内容
        /// </summary>
        public string? raw_message { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public long? font { get; set; }
        /// <summary>
        /// friend、group、other	消息子类型，如果是好友则是 friend，如果是群临时会话则是 group
        /// </summary>
        public string? sub_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private object? _messagejson;
        // /// <summary>
        // /// 消息内容
        // /// </summary>
        // public object? message { get; set; }

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
                catch (Exception)
                {

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
        /// 上报类型(message)
        /// </summary>
        public string? post_type { get; set; }
    }
    ///// <summary>
    ///// 发送人信息
    ///// </summary>
    //public class Sender
    //{
    //    /// <summary>
    //    /// 发送者 QQ 号
    //    /// </summary>
    //    public long? user_id { get; set; }
    //    /// <summary>
    //    /// 昵称
    //    /// </summary>
    //    public string? nickname { get; set; }
    //    /// <summary>
    //    /// 备注
    //    /// </summary>
    //    public string? card { get; set; }
    //    /// <summary>
    //    /// 性别，male 或 female 或 unknown
    //    /// </summary>
    //    public string? sex { get; set; }
    //    /// <summary>
    //    /// 年龄
    //    /// </summary>
    //    public string? age { get; set; }
    //}

}
