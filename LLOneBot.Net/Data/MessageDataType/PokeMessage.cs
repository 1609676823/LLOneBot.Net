using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LLOneBot.Net.Data.MessageDataType
{
    /// <summary>
    /// 窗口抖动（戳一戳）
    /// </summary>
    public class PokeMessage : MessageBase
    {
        /// <summary>
        /// 窗口抖动（戳一戳）
        /// </summary>

        public PokeMessage() { 
           
            this.data = new PokeMessageData(); 
        }
        /// <summary>
        /// 窗口抖动（戳一戳）
        /// </summary>
        /// <param name="pokeMessageData"></param>
        public PokeMessage(PokeMessageData pokeMessageData) 
        {
            this.data = pokeMessageData;
        }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public override MessageType MessageType { get; set; } = MessageType.Poke;
        /// <summary>
        /// 类型json
        /// </summary>
        public override string type { get; set; } = "poke";

        /// <summary>
        /// 消息数据
        /// </summary>
        [JsonPropertyName("data")]
        //[JsonIgnore]
        public new PokeMessageData? data { get { return base.data as PokeMessageData; } set { base.data = (value); } } //= new TextMessageData();
        //{ get { return this.data; } set { this.data = (value); } } 
    }


    /// <summary>
    /// PokeMessageData
    /// </summary>
    public class PokeMessageData
    {
        /// <summary>
        /// 
        /// </summary>
        public PokeMessageData () { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        public PokeMessageData(string Name,long Type, long Id ) 
        { 

            this.type = Type.ToString(); 
            this.id = Id.ToString();
            this.name = Name;
        }
   
       /// <summary>
       /// 
       /// </summary>
       /// <param name="Type"></param>
       /// <param name="Id"></param>
       /// <param name="Name"></param>
        public PokeMessageData(string Type, string Id, string Name="")
        {

            this.type = Type;
            this.id = Id;
           this.name = Name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pokeMessageData"></param>
        public PokeMessageData(PokeMessageData pokeMessageData)
        {
            this.type = pokeMessageData.type;
            this.id = pokeMessageData.id;
            this.name = pokeMessageData.name;
        }
        /// <summary>
        /// 见 Mirai 的 PokeMessage 类	类型
        ///https://github.com/mamoe/mirai/blob/f5eefae7ecee84d18a66afce3f89b89fe1584b78/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/HummerMessage.kt#L49
        /// </summary>
        public string? type { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string? id { get; set; }

        /// <summary>
        /// 表情名
        /// </summary>
        public string? name { get; set; }
    }

    /// <summary>
    /// 戳一戳类型. 可以发送给好友或群.
    /// </summary>
    public static class EnumPokeMessageDataType
    {
        /** 戳一戳 */

        /// <summary>
        /// 戳一戳
        /// </summary>
        public static PokeMessageData Poke { get; set; } = new PokeMessageData("戳一戳", 1, -1);

        /** 比心 */
        /// <summary>
        /// 比心
        /// </summary>
        public static PokeMessageData ShowLove { get; set; } = new PokeMessageData("比心", 2, -1);

        /** 点赞  */
        /// <summary>
        /// 点赞
        /// </summary>
        public static PokeMessageData Like { get; set; } = new PokeMessageData("点赞", 3, -1);

        /** 心碎 */
        /// <summary>
        /// 
        /// </summary>
        public static PokeMessageData Heartbroken { get; set; } = new PokeMessageData("心碎", 4, -1);

        /** 666 */
        /// <summary>
        /// 666
        /// </summary>
        public static PokeMessageData SixSixSix { get; set; } = new PokeMessageData("666", 5, -1);

        /** 放大招 */
        /// <summary>
        /// 放大招
        /// </summary>
        public static PokeMessageData FangDaZhao { get; set; } = new PokeMessageData("放大招", 6, -1);

        /** 宝贝球 (SVIP); */
        /// <summary>
        /// 宝贝球 (SVIP);
        /// </summary>
        public static PokeMessageData BaoBeiQiu { get; set; } = new PokeMessageData("宝贝球", 126, 2011);

        /** 玫瑰花 (SVIP); */
        /// <summary>
        ///  玫瑰花 (SVIP)
        /// </summary>
        public static PokeMessageData Rose { get; set; } = new PokeMessageData("玫瑰花", 126, 2007);

        /** 召唤术 (SVIP); */
        /// <summary>
        /// 召唤术 (SVIP);
        /// </summary>
        public static PokeMessageData ZhaoHuanShu { get; set; } = new PokeMessageData("召唤术", 126, 2006);

        /** 让你皮 (SVIP); */
        /// <summary>
        /// 让你皮 (SVIP);
        /// </summary>
        public static PokeMessageData RangNiPi { get; set; } = new PokeMessageData("让你皮", 126, 2009);

        /** 结印 (SVIP); */
        /// <summary>
        ///  结印 (SVIP);
        /// </summary>
        public static PokeMessageData JieYin { get; set; } = new PokeMessageData("结印", 126, 2005);

        /** 手雷 (SVIP); */
        /// <summary>
        /// 手雷 (SVIP);
        /// </summary>
        public static PokeMessageData ShouLei { get; set; } = new PokeMessageData("手雷", 126, 2004);

        /** 勾引 */
        /// <summary>
        /// 勾引
        /// </summary>
        public static PokeMessageData GouYin { get; set; } = new PokeMessageData("勾引", 126, 2003);

        /** 抓一下 (SVIP); */
        /// <summary>
        ///  抓一下 (SVIP);
        /// </summary>
        public static PokeMessageData ZhuaYiXia { get; set; } = new PokeMessageData("抓一下", 126, 2001);

        /** 碎屏 (SVIP); */
        /// <summary>
        ///碎屏 (SVIP);
        /// </summary>
        public static PokeMessageData SuiPing { get; set; } = new PokeMessageData("碎屏", 126, 2002);

        /** 敲门 (SVIP); */
        /// <summary>
        ///  敲门 (SVIP); 
        /// </summary>
        public static PokeMessageData QiaoMen { get; set; } = new PokeMessageData("敲门", 126, 2002);

    }



}
