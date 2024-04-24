using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    ///群红包运气王
    /// </summary>
    public class GroupLuckykingReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 群红包运气王
        /// </summary>
        public GroupLuckykingReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.GroupLuckyKing;

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
        /// 提示类型
        /// </summary>
        public string? sub_type { get; set; }

        /// <summary>
        /// 	群号
        /// </summary>
        public long group_id { get; set; }

        /// <summary>
        ///红包发送者 QQ 号
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 运气王 QQ 号
        /// </summary>
        public long target_id { get; set; }
    }





}
