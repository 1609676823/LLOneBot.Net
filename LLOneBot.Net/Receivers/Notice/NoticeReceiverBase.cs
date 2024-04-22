using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    /// 通知事件接收器基类
    /// </summary>
    public class NoticeReceiverBase
    {
        /// <summary>
        /// 通知事件接收器基类
        /// </summary>
        public NoticeReceiverBase() { }


        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public virtual Data.EventNoticeType EventNoticeType { get; set; }= Data.EventNoticeType.Unknown;

        /// <summary>
        /// 原始的json
        /// </summary>
        [JsonIgnore]
        public string Originaljson { get; set; } = string.Empty;
    }
}
