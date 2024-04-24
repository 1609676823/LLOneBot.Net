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
    public class GroupDecreaseReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 可能是群成员名片变更事件接收器基类
        /// </summary>
        public GroupDecreaseReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.GroupDecrease;

    }




}
