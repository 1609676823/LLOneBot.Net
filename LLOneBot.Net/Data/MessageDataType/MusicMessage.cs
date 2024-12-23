using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 音乐分享信息
    /// </summary>
    public class MusicMessage: MessageBase
    {
        /// <summary>
        /// 
        /// </summary>
    
        public MusicMessage() { this.data = new MusicMessageData(); }
        /// <summary>
        /// 音乐分享
        /// </summary>
        /// <param name="type">qq 163 xm	分别表示使用 QQ 音乐、网易云音乐、虾米音乐</param>
        /// <param name="id">歌曲 ID</param>
        public MusicMessage(string type, string id) 
        {
            this.data=new MusicMessageData();
            // this.data.text=messagetext;
            (this.data as MusicMessageData)!.type = type;
            (this.data as MusicMessageData)!.id = id; 
        }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Music;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "music";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new MusicMessageData? data { get { return base.data as MusicMessageData; } set { base.data = (value); } } //= new MusicMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// MusicMessageData
    /// </summary>
    public class MusicMessageData
    {
        /// <summary>
        ///qq 163 xm	分别表示使用 QQ 音乐、网易云音乐、虾米音乐
        /// </summary>
        public string? type { get; set; }
        /// <summary>
        /// 歌曲 ID
        /// </summary>
        public string? id { get; set; }
    }
}
