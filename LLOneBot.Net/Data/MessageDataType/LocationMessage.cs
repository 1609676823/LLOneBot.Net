using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 位置信息
    /// </summary>
    public class LocationMessage: MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
    
        public LocationMessage() { this.data = new LocationMessageData(); }
        /// <summary>
        /// 位置信息
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        public LocationMessage(string lon, string lat) 
        {
            this.data=new LocationMessageData();
            // this.data.text=messagetext;
            (this.data as LocationMessageData)!.lon = lon;
            (this.data as LocationMessageData)!.lat = lat; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Location;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "location";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new LocationMessageData? data { get { return base.data as LocationMessageData; } set { base.data = (value); } } //= new LocationMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// LocationMessageData
    /// </summary>
    public class LocationMessageData
    {
        /// <summary>
        ///纬度
        /// </summary>
        public string? lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string? lon { get; set; }
    }
}
