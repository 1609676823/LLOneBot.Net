using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    /// 好友添加事件接收器基类
    /// </summary>
    public class FriendAddReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 好友添加事件接收器基类
        /// </summary>
        public FriendAddReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.FriendAdd;

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
        /// 新添加好友 QQ 号
        /// </summary>
        public long user_id { get; set; }

   
    }



}
