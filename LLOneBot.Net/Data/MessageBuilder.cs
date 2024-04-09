using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data
{
    /// <summary>
    /// 消息链构建方法
    /// </summary>
    public static class MessageBuilder
    {
        /// <summary>
        /// MessageChain构建
        /// </summary>
        /// <param name="MessageChainJson"></param>
        /// <returns></returns>
        public static MessageChain BulderMessageChain(object MessageChainJson)
        {
            string json=MessageChainJson.ToString()!;    
            MessageChain messageChain = new MessageChain();
          
            JsonArray MessageArray = JsonArray.Parse(json)!.AsArray();

            foreach (JsonNode item in MessageArray) 
            {
                string type = Convert.ToString(item!["type"])!;

                if ("at".Equals(type, StringComparison.OrdinalIgnoreCase))
                { 
                
                }


            }


            return messageChain;
        }

        /// <summary>
        /// MessageBase构建
        /// </summary>
        /// <param name="MessageDataJson"></param>
        /// <returns></returns>
        public static MessageBase BulderMessageBase(JsonNode MessageDataJson) 
        {
            MessageBase messageBase = new MessageBase();

            return messageBase;
        }
    }
}
