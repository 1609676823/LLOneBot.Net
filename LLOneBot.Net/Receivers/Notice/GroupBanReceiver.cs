using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    /// 群禁言事件接收器基类
    /// </summary>
    public class GroupBanReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 群禁言事件接收器基类
        /// </summary>
        public GroupBanReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.GroupIncrease;

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
        public GroupBanSubtype sub_type { get; set; }
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

        /// <summary>
        /// 禁言时长，单位秒
        /// </summary>
        public long duration { get; set; }
    }

    /// <summary>
    ///ban、lift_ban  事件子类型，分别表示禁言、解除禁言
    /// </summary>
    public enum GroupBanSubtype
    {
        /// <summary>
        /// 禁言
        /// </summary>
        [EnumMember(Value = "ban")]
        [Description("ban")]
        ban,

        /// <summary>
        /// 解除禁言
        /// </summary>
        [EnumMember(Value = "lift_ban")]
        [Description("lift_ban")]
        lift_ban,

      

    }


}
