#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER || NET45_OR_GREATER

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

//using System.Runtime.Remoting.Messaging;

namespace PostManagerTool.Net
{


    /// <summary>
    /// 
    /// </summary>
    public class PostManagerToolClient
    {
        /// <summary>
        /// 
        /// </summary>
        public PostManagerToolClient() { /*Instance = this;*/ }
        /// <summary>
        /// 最后一个启动的 PostManagerToolClient实例
        /// </summary>
        public static PostManagerToolClient? Instance { get; set; }
        /// <summary>
        /// queryParameters,构建请求的query拼接
        /// </summary>
        public Dictionary<string, string> queryParameters { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 请求的CookieContainer容器
        /// </summary>
        public CookieContainer Requestcookies { get; set; } = new CookieContainer();

        /// <summary>
        /// 响应的CookieContainer容器
        /// </summary>
        public CookieContainer Responsecookies { get; set; } = new CookieContainer();
        /// <summary>
        /// 请求头的键值对
        /// </summary>
        public Dictionary<string, string> RequestHeaders { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 响应的头文件
        /// </summary>
        public HttpResponseHeaders? ResponseHeaders { get; set; } = null;
        /// <summary>
        /// 请求的类型 POST GET DELETE ...
        /// </summary>
       // public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;

        public string HttpWebRequestMethod { get; set; } = HttpMethodType.Post;

        /// <summary>
        /// BODY类型
        /// </summary>
        public BodyType bodyType { get; set; } = BodyType.none;

        /// <summary>
        /// 请求的Body类型为FormData
        /// </summary>
        //public MultipartFormDataContent Body_FormData { get; set; } = new MultipartFormDataContent();

        public Dictionary<string, string> Body_FormData { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 请求的Body类型为UrlEncoded(x-www-form-urlencoded)
        /// </summary>
      //  public FormUrlEncodedContent Body_UrlEncoded { get; set; } = new FormUrlEncodedContent(new Dictionary<string, string>());

        public Dictionary<string, string> Body_UrlEncoded { get; set; } = new Dictionary<string, string>(new Dictionary<string, string>());

        /// <summary>
        /// body中的raw
        /// </summary>

        public string Body_Raw { get; set; } = string.Empty;
        /// <summary>
        /// 请求的mediaType类型
        /// </summary>
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        public string RequestmediaType { get; set; } = System.Net.Mime.MediaTypeNames.Application.Json;
#else
         public string RequestmediaType { get; set; } =ComModel.CustomMediaTypeNames.Application.Json;
#endif
        /// <summary>
        /// 请求的超时设置public TimeSpan Timeout = System.Threading.Timeout.InfiniteTimeSpan;
        /// </summary>
       // public TimeSpan Timeout = System.Threading.Timeout.InfiniteTimeSpan;
        public TimeSpan Timeout =  TimeSpan.FromSeconds(60);

        /// <summary>
        /// 返回的请求状态代码
        /// </summary>
        public HttpStatusCode? ResponseStatusCode { get; set; }

        /// <summary>
        /// 返回的Response数据
        /// </summary>
        public object? ResponseObject { get; set; }

        /// <summary>
        /// 是否自动重定向
        /// </summary>
        public bool AllowAutoRedirect { get; set; }=true;

        ///// <summary>
        ///// 响应是否成功
        ///// </summary>
        /////public bool ResponseIsSuccess { get; set; } = false;

        /// <summary>
        /// 异步获取返回的字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public async Task<string> SendHttpRequestAsync(string url,Encoding? encoding =null)
        {
           // 异步获取返回的字符串

            byte[] responseByte = await SendHttpRequestAsByteAsync(url);

            if (encoding == null) { encoding = Encoding.UTF8; }
             

            string responseBody = encoding.GetString(responseByte);



            // 返回响应内容字符串  
            return responseBody;
        }
        /// <summary>
        /// 异步获取返回的byte
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<byte[]> SendHttpRequestAsByteAsync(string url)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.AllowAutoRedirect = AllowAutoRedirect;


            CookieContainer cookieContainer = new CookieContainer();//创建CookieContainer
            if (Requestcookies.Count > 0)
            {

                handler.CookieContainer = Requestcookies;


            }
            // 创建 HttpClient 实例  
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Clear();
            // client.DefaultRequestHeaders.Add("Accept-Charset", "utf-8");
            client.Timeout = Timeout;
            // 创建一个 HttpRequestMessage 实例
            HttpRequestMessage request = new HttpRequestMessage();
            // request.Headers.Add("Accept-Charset", "utf-8");
            // request.Method = HttpMethod; // 设置请求方法

            request.Method = new System.Net.Http.HttpMethod(HttpWebRequestMethod); // 设置请求方法

            if (queryParameters.Count > 0)
            {
                request.RequestUri = new Uri(url + "?" + CreateQueryString(queryParameters)); // 设置请求的 URI  
            }
            else
            {
                request.RequestUri = new Uri(url); // 设置请求的 URI  
            }



            #region 添加自定义请求头


            if (RequestHeaders.Count > 0)
            {
                foreach (KeyValuePair<string, string> RequestHeaderItem in RequestHeaders)
                {
                    client.DefaultRequestHeaders.Add(RequestHeaderItem.Key, RequestHeaderItem.Value);
                    request.Headers.Add(RequestHeaderItem.Key, RequestHeaderItem.Value);
                }
            }
            #endregion

            #region body构建

            /*formdata类型*/
            if (this.bodyType.Equals(BodyType.formdata))
            {
                if (Body_FormData.Count > 0)
                {
                    MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();

                    foreach (var item in Body_FormData)
                    {
                        multipartFormDataContent.Add(new StringContent(item.Value), item.Key);
                    }

                    request.Content = multipartFormDataContent;
                }
                //request.Content = this.Body_FormData;

            }

            /*urlencoded类型*/
            if (this.bodyType.Equals(BodyType.urlencoded))
            {
                // request.Content = this.Body_UrlEncoded;
                FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(Body_UrlEncoded!);
                request.Content = formUrlEncodedContent;
            }

            /*raw类型*/
            if (this.bodyType.Equals(BodyType.raw))
            {
                StringContent raw = new StringContent(Body_Raw, Encoding.UTF8, RequestmediaType); ; // 示例请求内容  
                request.Content = raw;
                //request.Content.Headers.ContentType = new MediaTypeHeaderValue(RequestmediaType);

            }




            #endregion



            
            HttpResponseMessage response = await client.SendAsync(request);

            ResponseStatusCode = response.StatusCode;
            // 确保请求成功  
            //response.EnsureSuccessStatusCode();

            byte[] responseByte = await response.Content.ReadAsByteArrayAsync();
            ResponseObject = response;
            //string responseBody = Encoding.UTF8.GetString(responseByte);
            this.ResponseHeaders = response.Headers;
            this.Responsecookies = handler.CookieContainer;


            handler.Dispose();
            client.Dispose();
            // 返回响应内容字符串  
            //ResponseIsSuccess = true;
            return responseByte;
        }
        /// <summary>
        /// 获取返回的字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string SendHttpRequest(string url, Encoding? encoding = null) 
        {
           
            string res = string.Empty;
            try
            {
                Task<string> RequestTask = this.SendHttpRequestAsync(url, encoding);
                RequestTask.Wait(); // 阻塞直到任务完成 
                res = RequestTask.Result;
               // ResponseIsSuccess = true;
            }
            catch (Exception ex)
            {
                res = ex.Message;
               // ResponseIsSuccess = false;
                //  throw;
            }
            return res;
        }

        /// <summary>
        /// 获取返回的byte
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public byte[] SendHttpRequestAsByte(string url)
        {
            Instance = this;
            byte[] res = new byte[] { };
            try
            {
                Task<byte[]> RequestTask = this.SendHttpRequestAsByteAsync(url);
                RequestTask.Wait(); // 阻塞直到任务完成 
                res = RequestTask.Result;
               // ResponseIsSuccess = true;

            }
            catch (Exception ex)
            {
                res = System.Text.Encoding.UTF8.GetBytes(ex.Message);
                //ResponseIsSuccess= false;
                //  throw;
            }
            return res;
        }

        /// <summary>
        /// 创建 Query 参数的字符串表示形式
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        // 创建 Query 参数的字符串表示形式
        public string CreateQueryString(Dictionary<string, string> parameters)
        {
            StringBuilder queryString = new StringBuilder();
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("&");
                }
                queryString.Append(parameter.Key).Append("=").Append(WebUtility.UrlEncode(parameter.Value));
            }
            return queryString.ToString();
        }

    }

}

#else


using PostManagerTool.Net;
using PostManagerTool.Net.ComModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
//using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;



namespace PostManagerTool.Net
{

    /// <summary>
    /// 
    /// </summary>
    public class PostManagerToolClient
    {
        /// <summary>
        /// 
        /// </summary>
        public PostManagerToolClient() {  }

        /// <summary>
        /// 最后一个启动的 PostManagerToolClient实例
        /// </summary>
        public static PostManagerToolClient? Instance { get; set; }
        /// <summary>
        /// queryParameters,构建请求的query拼接
        /// </summary>
        public Dictionary<string, string> queryParameters { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 请求的CookieContainer容器
        /// </summary>
        public CookieContainer Requestcookies { get; set; } = new CookieContainer();

        /// <summary>
        /// 响应的CookieContainer容器
        /// </summary>
        public CookieContainer Responsecookies { get; set; } = new CookieContainer();
        /// <summary>
        /// 请求头的键值对
        /// </summary>
        public Dictionary<string, string> RequestHeaders { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 响应的头文件
        /// </summary>
        public WebHeaderCollection? ResponseHeaders { get; set; }
        /// <summary>
        /// 请求的类型 POST GET DELETE ...
        /// </summary>
        public string HttpWebRequestMethod { get; set; } = HttpMethodType.Post;
        /// <summary>
        /// 
        /// </summary>
        public BodyType bodyType { get; set; } = BodyType.none;

        /// <summary>
        /// 
        /// </summary>
        public Version HttpWebRequestVersion = System.Net.HttpVersion.Version11;

        /// <summary>
        /// 请求的Body类型为FormData
        /// </summary>
        public Dictionary<string, string> Body_FormData { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 请求的Body类型为UrlEncoded(x-www-form-urlencoded)
        /// </summary>
        public Dictionary<string, string> Body_UrlEncoded { get; set; } = new Dictionary<string, string>(new Dictionary<string, string>());

        /// <summary>
        /// body中的raw
        /// </summary>

        public string Body_Raw { get; set; } = string.Empty;
        /// <summary>
        /// 请求的mediaType类型
        /// </summary>
        public string RequestmediaType { get; set; } = CustomMediaTypeNames.Application.Json;
        /// <summary>
        /// 请求的超时设置
        /// </summary>
        public int Timeout = 60;
        /// <summary>
        /// 
        /// </summary>
        public string Realsendurl { get; set; }=string.Empty;
        /// <summary>
        /// 返回的请求状态代码
        /// </summary>
        public HttpStatusCode? ResponseStatusCode { get; set; }

        /// <summary>
        /// 返回的Response数据
        /// </summary>
        public object? ResponseObject { get; set; }

        /// <summary>
        /// 是否自动重定向
        /// </summary>
        public bool AllowAutoRedirect { get; set; } = true;



        /// <summary>
        /// 获取返回的字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string SendHttpRequest(string url, Encoding encoding=null)
        {

           
            string responseContent = string.Empty;


            byte[] responseByte = SendHttpRequestAsByte(url);

            if (encoding == null) { encoding = Encoding.UTF8; }


            string responseBody = encoding.GetString(responseByte);


            return responseContent;
        }

        /// <summary>
        /// 获取返回的byte
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Byte[] SendHttpRequestAsByte(string url)
        {
            Instance = this;
            Realsendurl = url;

            string requestContent = string.Empty;
            //string responseContent = string.Empty;

            byte[] responsebyteArray = new byte[] { };

            if (queryParameters.Count > 0)
            {
                Realsendurl = url + "?" + CreateQueryString(queryParameters); // 设置请求的 URI  
            }

            string responseBody = string.Empty;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Realsendurl);


            //httpWebRequest.ProtocolVersion = System.Net.HttpVersion.Version10;
            httpWebRequest.ProtocolVersion = HttpWebRequestVersion;

            httpWebRequest.Method = HttpWebRequestMethod;
            httpWebRequest.ContentType = RequestmediaType;
            httpWebRequest.Timeout = Timeout * 1000;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.AllowAutoRedirect = AllowAutoRedirect;

            /*请求头*/
            if (RequestHeaders != null && RequestHeaders.Count > 0)
            {
                foreach (KeyValuePair<string, string> header in RequestHeaders)
                {
                    httpWebRequest.Headers[header.Key] = header.Value.ToString();
                }
            }

            /*请求cookies*/
            httpWebRequest.CookieContainer = Requestcookies;

            /*formdata类型*/
            if (this.bodyType.Equals(BodyType.formdata))
            {
                httpWebRequest.ContentType = CustomMediaTypeNames.Multipart.FormData;
                requestContent = string.Join("&", Body_FormData.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

            }

            /*urlencoded类型*/
            if (this.bodyType.Equals(BodyType.urlencoded))
            {
                httpWebRequest.ContentType = CustomMediaTypeNames.Application.FormUrlEncoded;
                requestContent = string.Join("&", Body_UrlEncoded.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

            }

            /*raw类型*/
            if (this.bodyType.Equals(BodyType.raw))
            {

                requestContent = Body_Raw;
                //request.Content.Headers.ContentType = new MediaTypeHeaderValue(RequestmediaType);

            }

            try
            {
                if (!string.IsNullOrWhiteSpace(requestContent))
                {
                    // 将 POST 数据写入请求流
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(requestContent);
                    httpWebRequest.ContentLength = byteArray.Length;
                    using (Stream dataStream = httpWebRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }
                }


                // 发送请求并获取响应
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();

                ResponseObject = response;

                // 获取 响应Cookie
                try
                {
                    ResponseStatusCode = response.StatusCode;
                    string cookieHeader = response.Headers["Set-Cookie"];

                    if (!string.IsNullOrWhiteSpace(cookieHeader))
                    {

                        Uri responseUri = response.ResponseUri;
                        Responsecookies.SetCookies(responseUri, cookieHeader);

                    }
                }
                catch (Exception)
                {

                    // throw;
                }

                // 获取响应头
                ResponseHeaders = response.Headers;


                using (Stream responseStream = response.GetResponseStream())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        responsebyteArray = memoryStream.ToArray();
                        responseStream.Close();
                        memoryStream.Close();
                        return responsebyteArray;   

                    }
                        
                  

                    //using (StreamReader reader = new StreamReader(responseStream))
                    //{


                    //    string responseData = reader.ReadToEnd();
                    //    responseContent = responseData;
                    //    // Console.WriteLine(responseData);
                    //}
                }


            }
            catch (Exception ex)
            {
                responsebyteArray= System.Text.Encoding.UTF8.GetBytes(ex.Message);
                return responsebyteArray;
                //throw;
            }



            //return responsebyteArray;
        }

        /// <summary>
        /// 异步获取返回的字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>


        public Task<string> SendHttpRequestAsync(string url)
        {
            return Task<string>.Factory.StartNew(() =>
            {
                return SendHttpRequest(url);
            });
        }

        /// <summary>
        /// 异步获取返回的byte
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<Byte[]> SendHttpRequestAsByteAsync(string url)
        {
            return Task<Byte[]>.Factory.StartNew(() =>
            {
                return SendHttpRequestAsByte(url);
            });
        }

        /// <summary>
        /// 拼接QueryString
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string CreateQueryString(Dictionary<string, string> parameters)
        {

            StringBuilder queryString = new StringBuilder();
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("&");
                }
                queryString.Append(parameter.Key).Append("=").Append(Uri.EscapeDataString(parameter.Value));
            }
            return queryString.ToString();


        }

   


    }

  

}

#endif
