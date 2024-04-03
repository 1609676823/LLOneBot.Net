using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LLOneBot.Net.Data
{
 /// <summary>
/// 登录用户的信息
/// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 构造登录信息类
        /// </summary>
        /// <param name="logininfojson">登录信息json</param>
        public LoginInfo(string logininfojson) 
        {
           Logininfojson = logininfojson;


            JsonDocument jsonDocument ;
            JsonElement root=new JsonElement() ;
          
            try
            {
                jsonDocument = JsonDocument.Parse(logininfojson);
                root = jsonDocument.RootElement;
                status = Convert.ToString(root.GetProperty("status"))!;
            }
            catch (Exception)
            {

                //throw;
            }

            try
            {
                JsonElement data = root.GetProperty("data");
                user_id = Convert.ToString(data.GetProperty("user_id"))!;
                nickname = Convert.ToString(data.GetProperty("nickname"))!;


            }
            catch (Exception)
            {

              // throw;
            }
            //Console.WriteLine($"Name: {root.GetProperty("Name").GetString()}");
            //Console.WriteLine($"Age: {root.GetProperty("Age").GetInt32()}");
            //Console.WriteLine($"City: {root.GetProperty("City").GetString()}");

        }
        /// <summary>
        /// 状态
        /// </summary>
       [JsonPropertyName("status")] public string? status {  get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string? user_id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string? nickname { get; set; }
        /// <summary>
        /// 登录信息json
        /// </summary>
        public string? Logininfojson { get; set; }



    }
}
