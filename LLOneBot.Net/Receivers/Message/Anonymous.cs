using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLOneBot.Net.Receivers.Message
{
    /// <summary>
    /// 匿名用户信息
    /// </summary>
    public class Anonymous
    {
        /// <summary>
        ///匿名用户 ID
        /// </summary>
        public long id { get; set; }

        /// <summary>
        ///匿名用户名称
        /// </summary>
        public long name { get; set; }
        /// <summary>
        ///匿名用户 flag，在调用禁言 API 时需要传入
        /// </summary>
        public long flag { get; set; }

    }
}
