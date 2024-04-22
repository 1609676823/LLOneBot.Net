using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    /// 群文件上传通知事件接收器基类
    /// </summary>
    public class GroupUploadReceiver: NoticeReceiverBase
    {
        /// <summary>
        /// 群文件上传通知事件接收器基类
        /// </summary>
        public GroupUploadReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; }= Data.EventNoticeType.GroupUpload;

    }
}
