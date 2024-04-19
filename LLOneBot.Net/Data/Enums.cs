using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Diagnostics.CodeAnalysis;

using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data
{
    /// <summary>
    /// Event消息事件类型
    /// </summary>
    public enum EventMessageType
    {
        /// <summary>
        /// Private
        /// </summary>
        [EnumMember(Value = "private")]
        [Description("private")]
      

        Private,

        /// <summary>
        /// Group
        /// </summary>

        [EnumMember(Value = "group")]
        [Description("group")]
        Group,
        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember(Value = "unknown")]
        [Description("unknown")]
        Unknown,

        /// <summary>
        /// None
        /// </summary>
        [EnumMember(Value = "none")]
        [Description("none")]
        None


    }

    /// <summary>
    /// Event请求事件类型
    /// </summary>
    public enum EventRequestType
    {
        /// <summary>
        /// Private
        /// </summary>
        [EnumMember(Value = "friend")]
        [Description("friend")]


        Friend,

        /// <summary>
        /// Group
        /// </summary>

        [EnumMember(Value = "group")]
        [Description("group")]
        Group,

        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember(Value = "unknown")]
        [Description("unknown")]
        Unknown,


    }

    /// <summary>
    /// 发送者的性别
    /// </summary>
    public enum Genders
    {
        /// <summary>
        /// 男性
        /// </summary>
        [EnumMember(Value = "MALE")]
        [Description("MALE")]
        Male,

        /// <summary>
        /// 女性
        /// </summary>
        [EnumMember(Value = "FEMALE")]
        [Description("FEMALE")]
        Female,

        /// <summary>
        /// 未知
        /// </summary>
        [EnumMember(Value = "UNKNOWN")]
        [Description("UNKNOWN")]
        Unknown
    }

    /// <summary>
    /// 群内权限
    /// </summary>
    public enum Permissions
    {
        /// <summary>
        /// 群主
        /// </summary>
        [EnumMember(Value = "OWNER")]
        [Description("OWNER")]
        Owner,

        /// <summary>
        /// 管理员
        /// </summary>
        [EnumMember(Value = "ADMINISTRATOR")]
        [Description("ADMINISTRATOR")]
        Administrator,

        /// <summary>
        /// 群员
        /// </summary>
        [EnumMember(Value = "MEMBER")]
        [Description("MEMBER")]
        Member
    }
    /// <summary>
    /// 发送消息类型
    /// </summary>
    public enum SendMessageType
    {
        /// <summary>
        /// at
        /// </summary>
        At,

        /// <summary>
        /// 文本
        /// </summary>
        Text,

        /// <summary>
        /// 表情
        /// </summary>
        Face,

        /// <summary>
        /// 图片
        /// </summary>
        Image,

        /// <summary>
        /// json数据
        /// </summary>
        Json,

        /// <summary>
        /// 回复
        /// </summary>
        Reply,

        /// <summary>
        /// 语言
        /// </summary>
        Record,

        /// <summary>
        /// 视频
        /// </summary>
        Video,

        /// <summary>
        /// 篮球表情
        /// </summary>
        Basketball,

        /// <summary>
        /// 新猜拳
        /// </summary>
        New_rps,

        /// <summary>
        /// 新骰子
        /// </summary>
        New_dice,

        /// <summary>
        /// 戳一戳
        /// </summary>
        Poke,

        /// <summary>
        /// 戳一戳（双击头像）
        /// </summary>
        Touch,

        /// <summary>
        /// 音乐
        /// </summary>
        Music,

        /// <summary>
        /// 天气
        /// </summary>
        Weather,

        /// <summary>
        /// 位置
        /// </summary>
        Location,

        /// <summary>
        /// 连接分享
        /// </summary>
        Share,

        /// <summary>
        /// 礼物
        /// </summary>
        Gift,

        /// <summary>
        /// 事件
        /// </summary>
        Button,

        /// <summary>
        /// Markdown文本
        /// </summary>
        Markdown,
    }


   

}
