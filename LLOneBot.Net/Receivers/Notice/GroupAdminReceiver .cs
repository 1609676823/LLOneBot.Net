using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    /// 群管理员变动通知事件接收器基类
    /// </summary>
    public class GroupAdminReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 群管理员变动通知事件接收器基类
        /// </summary>
        public GroupAdminReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.GroupAdmin;
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
        /// 事件子类型，分别表示设置和取消管理员(set、unset)
        /// </summary>

        [JsonPropertyName("sub_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GroupAdminSubtype? sub_type { get; set; }

        /// <summary>
        /// 群号
        /// </summary>
        public int group_id { get; set; }

        /// <summary>
        /// 发送者 QQ 号
        /// </summary>
        public long user_id { get; set; }


    }
    /// <summary>
    /// 事件子类型，分别表示设置和取消管理员(set、unset)
    /// </summary>
    public enum GroupAdminSubtype 
    {
        /// <summary>
        /// 设置管理员
        /// </summary>
        [EnumMember(Value = "set")]
        [Description("set")]
        Set,

        /// <summary>
        /// 取消管理员
        /// </summary>
        [EnumMember(Value = "unset")]
        [Description("unset")]
        Unset,

    }



}
