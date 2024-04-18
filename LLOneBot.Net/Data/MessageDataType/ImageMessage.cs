using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 图片信息
    /// </summary>
    public class ImageMessage: MessageBase
    {
        /// <summary>
        /// 图片信息
        /// </summary>
        public ImageMessage () { this.data = new ImageMessageData(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">
        /// 绝对路径，例如 file:///C:\\Users\Richard\Pictures\1.png，格式使用 file URI
        ///网络 URL，例如 http://i1.piimg.com/567571/fdd6e7b6d93f1ef0.jpg
        ///Base64 编码，例如 base64://iVBORw0KGgoAAAANSUhEUgAAABQAAAAVCAIAAADJt1n/AAAAKElEQVQ4EWPk5+RmIBcwkasRpG9UM4mhNxpgowFGMARGEwnBIEJVAAAdBgBNAZf+QAAAAABJRU5ErkJggg==
        /// </param>
        /// <param name="summary">LLOneBot的扩展字段：图片预览文字</param>
        public ImageMessage(string file, string summary = "")
        {
            SetImage(file, summary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">
        /// 绝对路径，例如 file:///C:\\Users\Richard\Pictures\1.png，格式使用 file URI
        ///网络 URL，例如 http://i1.piimg.com/567571/fdd6e7b6d93f1ef0.jpg
        ///Base64 编码，例如 base64://iVBORw0KGgoAAAANSUhEUgAAABQAAAAVCAIAAADJt1n/AAAAKElEQVQ4EWPk5+RmIBcwkasRpG9UM4mhNxpgowFGMARGEwnBIEJVAAAdBgBNAZf+QAAAAABJRU5ErkJggg==
        /// </param>
        /// <param name="summary">LLOneBot的扩展字段：图片预览文字</param>
        private void  SetImage(string file, string summary="")
        {
            this.data=new ImageMessageData();
            this.data.file = file;
            this.data.summary = summary;
        }
        private string? _path;
        /// <summary>
        /// 图片路径
        /// </summary>
        [JsonIgnore]
        public string Path 
        {
            get { return _path!; }
            set
            {
                _path = value!;
                string fileurl = ConvertToUri(value);
                this.data!.file = fileurl;
            }
        
        }
        private  string ConvertToUri(string filePath)
        {
            string fileurl=string.Empty;
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                Uri fileUri = new Uri(filePath);
                fileurl= fileUri.ToString();
            }
            return fileurl;
           
        }

        private string? _base64;
        /// <summary>
        /// 图片的Base64
        /// </summary>
        [JsonIgnore]
        public string Base64 
        {
            get
            {
                return _base64!;
            }
            set
            {
                _base64 = value!;
                string urlbase64 = ConvertToBase64Url(value);
                this.data!.file = urlbase64;
            }
        }

        private string ConvertToBase64Url(string base64str)
        { 
        string urlbase64=string.Empty;
            if (!string.IsNullOrWhiteSpace(base64str)) 
            {
                urlbase64 = "base64://" + base64str;
            }
        return urlbase64;
        }

        private string? _summary;
        /// <summary>
        /// LLOneBot的扩展字段：图片预览文字
        /// </summary>
        [JsonIgnore]
        public string Summary { 
            get 
            {
                return _summary!; 
            } 
            set
            {
                _summary=value!;
                this.data!.summary=value;
            } 
        }




        /// <summary>
        /// 类型
        /// </summary>
        // [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Image;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "image";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        public new ImageMessageData? data { get { return base.data as ImageMessageData; } set { base.data = (value); } } //= new TextMessageData();
    }
    /// <summary>
    /// 
    /// </summary>
    public class ImageMessageData
    {
        /// <summary>
        ///图片文件名
        /// </summary>
        public string? file { get; set; }
        /// <summary>
        /// LLOneBot的扩展字段：图片预览文字
        /// </summary>
        public string? summary { get; set; }

        /// <summary>
        ///图片类型，flash 表示闪照，无此参数表示普通图片
        /// </summary>
        public string? type { get; set; }

        /// <summary>
        ///图片 URL
        /// </summary>
       // [JsonIgnore]
        public string? url { get; set; }
        /// <summary>
        ///只在通过网络 URL 发送时有效，表示是否使用已缓存的文件，默认 1
        /// </summary>
        public string? cache { get; set; } = "1";
        /// <summary>
        ///只在通过网络 URL 发送时有效，表示是否通过代理下载文件（需通过环境变量或配置文件配置代理），默认 1
        /// </summary>
        public string? proxy { get; set; }
        /// <summary>
        ///只在通过网络 URL 发送时有效，单位秒，表示下载网络文件的超时时间，默认不超时
        /// </summary>
        public string? timeout { get; set; }
    }
}
