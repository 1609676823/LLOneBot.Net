using LLOneBot.Net.Data.MessageDataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Websocket.Client;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace LLOneBot.Net.Data
{


    /// <summary>
    /// 消息链构建方法
    /// </summary>
    public static class MessageBuilder
    {

        /// <summary>
        /// 允许常规字符串
        /// </summary>
        public static JsonSerializerOptions jsonSerializerOptions { get; set; } = new JsonSerializerOptions()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        /// <summary>
        /// MessageChain构建
        /// </summary>
        /// <param name="MessageChainJson"></param>
        /// <returns></returns>
        public static MessageChain BulderMessageChain(object MessageChainJson)
        {
            
            MessageChain messageChain = new MessageChain();
            if (MessageChainJson != null)
            {
                string json = MessageChainJson.ToString()!;
                JsonArray MessageArray = JsonArray.Parse(json)!.AsArray();

                foreach (JsonNode? item in MessageArray)
                {

                    MessageBase messageBase = new MessageBase();
                    messageBase = BulderMessageBase(item!);

                    messageChain.Add(messageBase);

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
            string type = Convert.ToString(MessageDataJson!["type"])!;
           


            if ("at".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
               // JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

                 messageBase = JsonSerializer.Deserialize<AtMessage>(MessageDataJson, jsonSerializerOptions)!;
                 messageBase.Originaljson= MessageDataJson.ToString();
                return messageBase;
            }

            if ("text".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<TextMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }

            if ("file".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<FileMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }

            if ("image".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<ImageMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }

             if ("face".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<FaceMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }

             if ("record".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<RecordMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }
             if ("video".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<VideoMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }

            if ("rps".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<RpsMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }
            if ("dice".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<DiceMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;

            }
            if ("shake".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<ShakeMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }
            if ("poke".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                messageBase = JsonSerializer.Deserialize<PokeMessage>(MessageDataJson, jsonSerializerOptions)!;
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }

            if ("anonymous".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    messageBase = JsonSerializer.Deserialize<AnonymousMessage>(MessageDataJson, jsonSerializerOptions)!;

                }
                catch (Exception)
                {

                   // throw;
                }
                messageBase.MessageType= MessageType.Anonymous;
                messageBase.type = "anonymous";
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }

            if ("share".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    messageBase = JsonSerializer.Deserialize<ShareMessage>(MessageDataJson, jsonSerializerOptions)!;

                }
                catch (Exception)
                {

                    // throw;
                }
                messageBase.MessageType = MessageType.Share;
                messageBase.type = "share";
                messageBase.Originaljson = MessageDataJson.ToString();
                return messageBase;
            }

            messageBase.Originaljson = MessageDataJson.ToString();
            return messageBase;
        }
    }
}
