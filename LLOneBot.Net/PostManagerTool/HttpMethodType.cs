using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManagerTool.Net
{    /// <summary>
     ///HttpMethodType类用于存储常见的HTTP请求方法相关的信息。
     /// 它将各种HTTP请求方法表示为字符串属性，方便在需要使用HTTP请求相关操作的代码中使用。
     /// </summary>
    public  class HttpMethodType
    {

        /// <summary>
        /// GET请求方法字符串，用于从服务器获取资源。
        /// </summary>
        public static string Get { get; } = "GET";

        /// <summary>
        /// POST请求方法字符串，用于向服务器提交数据，通常用于创建新资源。
        /// </summary>
        public static string Post { get; } = "POST";

        /// <summary>
        /// PUT请求方法字符串，用于更新服务器上的整个资源。
        /// </summary>
        public static string Put { get; } = "PUT";

        /// <summary>
        /// DELETE请求方法字符串，用于从服务器删除指定资源。
        /// </summary>
        public static string Delete { get; } = "DELETE";

        /// <summary>
        /// PATCH请求方法字符串，用于对服务器上的资源进行部分更新。
        /// </summary>
        public static string Patch { get; } = "PATCH";

        /// <summary>
        /// HEAD请求方法字符串，类似于GET请求，但只返回头部信息。
        /// </summary>
        public static string Head { get; } = "HEAD";

        /// <summary>
        /// OPTIONS请求方法字符串，用于获取服务器针对特定资源所支持的HTTP方法。
        /// </summary>
        public static string Options { get; } = "OPTIONS";
    }
}
