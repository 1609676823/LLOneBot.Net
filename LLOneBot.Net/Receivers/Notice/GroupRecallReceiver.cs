using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    /// 群消息撤回事件接收器基类
    /// </summary>
    public class GroupRecallReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 群消息撤回事件接收器基类
        /// </summary>
        public GroupRecallReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.GroupRecall;

        /// <summary>
        /// 事件发生的时间戳
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 收到事件的机器人 QQ 号
        /// </summary>
        public long self_id { get; set; }
        /// <summary>
        /// 上报类型
        /// </summary>
        public string? post_type { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        public string? notice_type { get; set; }

        /// <summary>
        /// 	群号
        /// </summary>
        public long group_id { get; set; }

        /// <summary>
        /// 消息发送者 QQ 号
        /// </summary>
        public long user_id { get; set; }


        /// <summary>
        /// 操作者 QQ 号
        /// </summary>
        public long operator_id { get; set; }



        /// <summary>
        /// 被撤回的消息 ID
        /// </summary>
        public long message_id { get; set; }
    }





}
