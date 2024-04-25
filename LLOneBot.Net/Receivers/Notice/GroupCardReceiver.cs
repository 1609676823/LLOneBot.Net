using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    /// 可能是群成员名片变更事件接收器基类
    /// </summary>
    public class GroupCardReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 可能是群成员名片变更事件接收器基类
        /// </summary>
        public GroupCardReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.GroupCard;

        /// <summary>
        /// 
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long self_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? post_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long group_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? notice_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? card_new { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? card_old { get; set; }
    }




}
