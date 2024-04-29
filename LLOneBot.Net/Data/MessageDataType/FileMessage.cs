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
    public class FileMessage : MessageBase
    {
        /// <summary>
        /// 图片信息
        /// </summary>
        public FileMessage() { this.data = new FileMessageData(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">
        /// 绝对路径，例如 file:///C:\\Users\Richard\Pictures\1.png，格式使用 file URI
        ///网络 URL，例如 http://i1.piimg.com/567571/fdd6e7b6d93f1ef0.jpg
        ///Base64 编码，例如 base64://iVBORw0KGgoAAAANSUhEUgAAABQAAAAVCAIAAADJt1n/AAAAKElEQVQ4EWPk5+RmIBcwkasRpG9UM4mhNxpgowFGMARGEwnBIEJVAAAdBgBNAZf+QAAAAABJRU5ErkJggg==
        /// </param>
        /// <param name="name">文件名</param>
        public FileMessage(string file, string name = "")
        {
          string uri=  ConvertToUri(file);
            SetFile(uri, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">
        /// 绝对路径，例如 file:///C:\\Users\Richard\Pictures\1.png，格式使用 file URI
        ///网络 URL，例如 http://i1.piimg.com/567571/fdd6e7b6d93f1ef0.jpg
        ///Base64 编码，例如 base64://iVBORw0KGgoAAAANSUhEUgAAABQAAAAVCAIAAADJt1n/AAAAKElEQVQ4EWPk5+RmIBcwkasRpG9UM4mhNxpgowFGMARGEwnBIEJVAAAdBgBNAZf+QAAAAABJRU5ErkJggg==
        /// </param>
        /// <param name="name">LLOneBot的扩展字段：图片预览文字</param>
        private void  SetFile(string file, string name="")
        {
            this.data=new FileMessageData();
            this.data.file = file;
            this.data.name = name;
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

        private string? _name;
        /// <summary>
        /// LLOneBot的扩展字段：图片预览文字
        /// </summary>
        [JsonIgnore]
        public string Name { 
            get 
            {
                return _name!; 
            } 
            set
            {
                _name = value!;
                this.data!.name=value;
            } 
        }




        /// <summary>
        /// 类型
        /// </summary>
        // [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.File;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "file";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        public new FileMessageData? data { get { return base.data as FileMessageData; } set { base.data = (value); } } //= new TextMessageData();
    }
    /// <summary>
    /// 
    /// </summary>
    public class FileMessageData
    {
        /// <summary>
        ///路径
        /// </summary>
        public string? file { get; set; }
        /// <summary>
        /// 自定义显示的文件名
        /// </summary>
        public string? name { get; set; }

     
    }
}
