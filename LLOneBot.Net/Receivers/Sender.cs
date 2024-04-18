using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLOneBot.Net.Receivers
{
    /// <summary>
    /// 发送人信息
    /// </summary>
    public class Sender
    {
        /// <summary>
        /// 发送者 QQ 号
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string? nickname { get; set; }
        /// <summary>
        /// 群名片/备注
        /// </summary>
        public string? card { get; set; }
        /// <summary>
        /// 性别，male 或 female 或 unknown
        /// </summary>
        public string? sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string? age { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string? area { get; set; }
        /// <summary>
        /// 成员等级
        /// </summary>
        public string? level { get; set; }
        /// <summary>
        /// 角色，owner 或 admin 或 member
        /// </summary>
        public string? role { get; set; }
        /// <summary>
        /// 专属头衔
        /// </summary>
        public string? title { get; set; }

    }
}
