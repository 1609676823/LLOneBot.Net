using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace LLOneBot.Net.Receivers.Notice
{
    /// <summary>
    ///群成员荣誉变更
    /// </summary>
    public class GroupHonorReceiver : NoticeReceiverBase
    {
        /// <summary>
        /// 群红包运气王
        /// </summary>
        public GroupHonorReceiver() { }

        /// <summary>
        /// Event通知事件类型
        /// </summary>
        public override Data.EventNoticeType EventNoticeType { get; set; } = Data.EventNoticeType.GroupHonor;

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
        /// 提示类型
        /// </summary>
        public string? sub_type { get; set; }

        /// <summary>
        /// 	群号
        /// </summary>
        public long group_id { get; set; }

        /// <summary>
        /// alkative、performer、emotion	荣誉类型，分别表示龙王、群聊之火、快乐源泉
        /// </summary>
        [JsonPropertyName("honor_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GroupHonorhonorType honor_type { get; set; }

        /// <summary>
        ///	成员 QQ 号
        /// </summary>
        public long user_id { get; set; }


    }

    /// <summary>
    /// alkative、performer、emotion	荣誉类型，分别表示龙王、群聊之火、快乐源泉
    /// </summary>
    public enum GroupHonorhonorType
    {
        /// <summary>
        /// 龙王
        /// </summary>
        [EnumMember(Value = "alkative")]
        [Description("alkative")]
        alkative,

        /// <summary>
        /// 群聊之火
        /// </summary>
        [EnumMember(Value = "performer")]
        [Description("performer")]
        performer,

        /// <summary>
        /// 快乐源泉
        /// </summary>
        [EnumMember(Value = "emotion")]
        [Description("emotion")]
        emotion,

    }





}
