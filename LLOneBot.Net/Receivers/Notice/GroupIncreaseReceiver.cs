using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    /// 群成员增加事件接收器基类
    /// </summary>
    public class GroupIncreaseReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 群成员增加事件接收器基类
        /// </summary>
        public GroupIncreaseReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.GroupDecrease;

        /// <summary>
        /// 事件发生的时间戳
        /// </summary>
        public int time { get; set; }
        /// <summary>
        /// 收到事件的机器人 QQ 号
        /// </summary>
        public int self_id { get; set; }
        /// <summary>
        /// 上报类型
        /// </summary>
        public string? post_type { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        public string? notice_type { get; set; }

        /// <summary>
        /// leave、kick、kick_me 事件子类型，分别表示主动退群、成员被踢、登录号被踢
        /// </summary>
        [JsonPropertyName("sub_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GroupIncreaseSubtype sub_type { get; set; }
        /// <summary>
        /// 	群号
        /// </summary>
        public long group_id { get; set; }

        /// <summary>
        /// 操作者 QQ 号（如果是主动退群，则和 user_id 相同）
        /// </summary>
        public long operator_id { get; set; }

        /// <summary>
        /// 离开者 QQ 号
        /// </summary>
        public long user_id { get; set; }
    }

    /// <summary>
    /// approve、invite 事件子类型，分别表示管理员已同意入群、管理员邀请入群
    /// </summary>
    public enum GroupIncreaseSubtype
    {
        /// <summary>
        /// 管理员已同意入群
        /// </summary>
        [EnumMember(Value = "approve")]
        [Description("approve")]
        approve,

        /// <summary>
        /// 管理员邀请入群
        /// </summary>
        [EnumMember(Value = "invite")]
        [Description("invite")]
        invite,

      

    }


}
