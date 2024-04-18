using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data
{
    /// <summary>
    /// API响应结果
    /// </summary>
    public class OneBotApiResponse
    {
        /// <summary>
        /// API响应结果
        /// </summary>
        public OneBotApiResponse() { }
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonIgnore]
        public bool IsSucess { get; set; } = false;

        [JsonIgnore]
        private string? _status;

        /// <summary>
        /// status (ok/failed)
        /// </summary>
        public string? status {
            get { return _status; } 
            set
            {
                _status = value;
                if (string.IsNullOrWhiteSpace(value))
                {

                    this.data = new OneBotApiResponseData();
                }
                else 
                {
                    if (value.IndexOf("ok", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        this.IsSucess = true;
                    }
                }
            } 
        }
        /// <summary>
        ///retcode
        /// </summary>
        public int? retcode { get; set; }
        /// <summary>
        /// data
        /// </summary>
        [JsonPropertyName("data")]
        public OneBotApiResponseData? data { get; set; }=new OneBotApiResponseData();
        /// <summary>
        /// message
        /// </summary>
        public string? message { get; set; }
        /// <summary>
        /// wording
        /// </summary>
        public string? wording { get; set; }
        /// <summary>
        /// echo
        /// </summary>
        public object? echo { get; set; }
    }
    /// <summary>
    /// 响应的 data
    /// </summary>
    public class OneBotApiResponseData
    {
        /// <summary>
        /// 消息id
        /// </summary>
        [JsonPropertyName("message_id")]
        public long message_id { get; set; }
    }

}
