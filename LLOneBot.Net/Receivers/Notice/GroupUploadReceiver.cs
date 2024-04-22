using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    /// 群文件上传通知事件接收器基类
    /// </summary>
    public class GroupUploadReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 群文件上传通知事件接收器基类
        /// </summary>
        public GroupUploadReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.GroupUpload;
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
        /// 群号
        /// </summary>
        public long group_id { get; set; }

        /// <summary>
        /// 发送者 QQ 号
        /// </summary>
        public long user_id { get; set; }

        /// <summary>
        /// 文件信息
        /// </summary>
        public FileGroupUpload file { get; set; }=new FileGroupUpload();
    }

    public class FileGroupUpload
    {
        /// <summary>
        /// 文件 ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 文件大小（字节数）
        /// </summary>
        public long size { get; set; }

        /// <summary>
        /// busid（目前不清楚有什么作用）
        /// </summary>
        public long busid { get; set; }
    }

}
