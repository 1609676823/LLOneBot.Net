
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Websocket.Client;


namespace LLOneBot.Net.Sessions
{
    /// <summary>
    /// LLOneBot对象
    /// </summary>
    public class LiteLoaderQQNTBot
    {
        /// <summary>
        /// 最后一个启动的LiteLoaderQQNTBot实例
        /// </summary>
        [JsonIgnore]
        public static LiteLoaderQQNTBot? Instance { get; set; }
        private WebsocketClient? _client;
        //  public static LiteLoaderQQNTBot Instance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LiteLoaderQQNTBot()
        {
            Instance = this;
            // InitializeIpaddress();
        }



        /// <summary>
        /// 收到Websocket所有消息
        /// </summary>
        public IObservable<ResponseMessage> AllWebsocketMessageReceived => _allWebsocketMessageReceived.AsObservable();

        private readonly Subject<ResponseMessage> _allWebsocketMessageReceived = new Subject<ResponseMessage>();


        /// <summary>
        /// 接收到未知类型的Websocket消息
        /// </summary>
        public IObservable<object> UnknownEventReceived => _unknownEventReceived.AsObservable();

        private readonly Subject<object> _unknownEventReceived = new();



        /// <summary>
        /// Websocket断开连接
        /// </summary>

        public IObservable<WebSocketCloseStatus> DisconnectionHappened => _disconnectionHappened.AsObservable();

        private readonly Subject<WebSocketCloseStatus> _disconnectionHappened = new();
        /// <summary>
        /// 接收meta_event元事件
        /// </summary>
        public IObservable<ResponseMessage> Meta_eventReceived => _meta_eventReceived.AsObservable();

        private readonly Subject<ResponseMessage> _meta_eventReceived = new();

        /// <summary>
        /// 接收message：消息事件
        /// </summary>


        public IObservable<Receivers.Message.MessageReceiverBase> MessageReceived => _messageReceived.AsObservable();



        private readonly Subject<Receivers.Message.MessageReceiverBase> _messageReceived = new();

        /// <summary>
        ///request：请求事件
        /// </summary>
        public IObservable<Receivers.Request.RequestReceiverBase> RequestReceived => _requestReceived.AsObservable();

        private readonly Subject<Receivers.Request.RequestReceiverBase> _requestReceived = new();


        /// <summary>
        ///notice：通知事件
        /// </summary>
        public IObservable<Receivers.Notice.NoticeReceiverBase> NoticeReceived => _noticeReceived.AsObservable();

        private readonly Subject<Receivers.Notice.NoticeReceiverBase> _noticeReceived = new();

        #region 构造类基础属性

        /// <summary>
        /// 是否包含Bot自身发送的消息(需要同时在插件中开启该功能)
        /// </summary>
        public bool IsContainsBotMessage { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip">服务端IP地址</param>
        /// <param name="httpport">HTTP服务监听端口</param>
        /// <param name="websocketPort">WebsocKet服务监听端口</param>
        /// <param name="accesstocken">accesstocken</param>

        public LiteLoaderQQNTBot(string ip, long httpport, long websocketPort, string accesstocken = "")
        {
            Ip = ip;
            HttpPort = httpport;
            WebsocKetPort = websocketPort;
            AccessTocken = accesstocken;
            InitializeIpaddress();
        }


        // private string _ip;
        /// <summary>
        /// 服务端IP地址
        /// </summary>
        public string? Ip { get; set; } = "127.0.0.1";

        ///// <summary>
        ///// 服务端IP地址
        ///// </summary>
        // public IPAddress Ipaddress { get; set; }
        /// <summary>
        /// HTTP服务监听端口
        /// </summary>
        public long HttpPort { get; set; } = 3000;
        /// <summary>
        /// WebsocKet服务监听端口
        /// </summary>
        public long WebsocKetPort { get; set; } = 3001;

        /// <summary>
        /// HTTP服务监听地址
        /// </summary>
        public string? HttpIpaddress { get; set; }
        /// <summary>
        /// websocKet服务监听地址
        /// </summary>
        public string? WebsocKetIpaddress { get; set; }
        /// <summary>
        /// Access Tocken
        /// </summary>
        public string? AccessTocken { get; set; }
        /// <summary>
        /// TimeoutSeconds
        /// </summary>
        public long TimeoutSeconds { get; set; } = 10;
        /// <summary>
        /// 初始化地址
        /// </summary>
        private void InitializeIpaddress()
        {
            //  if (string.IsNullOrWhiteSpace(Ip))
            {
                IPAddress Ipaddress = GetIPAddress(Ip!);

                string ip = string.Empty;
                try
                {
                    ip = Ipaddress.ToString();
                }
                catch (Exception)
                {
                    throw new Exception("IP地址格式不正确");
                    //throw;
                }
                if (!string.IsNullOrWhiteSpace(ip))
                {
                    if (HttpPort > 0)
                    {
                        HttpIpaddress = string.Format(@"http://{0}:{1}", ip, HttpPort);
                    }


                    if (WebsocKetPort > 0)
                    {
                        WebsocKetIpaddress = string.Format(@"ws://{0}:{1}", ip, WebsocKetPort);
                    }


                }
            }
        }
        /// <summary>
        /// 登录信息
        /// </summary>
        public Data.LoginInfo? LoginInfo { get; set; } = new Data.LoginInfo("");
        private string get_login_info()

        {


            string login_info = string.Empty;

            ComWebHelper.WebHelper webHelper = new ComWebHelper.WebHelper();

            webHelper.Timeout = TimeSpan.FromSeconds(TimeoutSeconds);

            webHelper.HttpMethod = HttpMethod.Get;
            string url = string.Format("{0}get_login_info", GetServerUrl(HttpIpaddress!));

            if (!string.IsNullOrWhiteSpace(AccessTocken))
            {
                webHelper.RequestHeaders.Add("Authorization", AccessTocken);
            }

            Task<string> RequestTask = webHelper.SendHttpRequestAsync(url);
            RequestTask.Wait(); // 阻塞直到任务完成 
            login_info = RequestTask.Result;
            return login_info;
        }

        private string GetServerUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return string.Empty;

                //throw new Exception("地址为空");
            }

            try
            {
                if (!url.EndsWith('/'))
                {
                    return url + "/";
                }
            }
            catch (Exception)
            {

                //throw;
            }

            return url;
        }
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private IPAddress GetIPAddress(string ip)
        {

            IPAddress iPAddress = null!;
            IPAddress.TryParse(ip, out iPAddress!);
            return iPAddress!;
        }
        #endregion


        /// <summary>
        /// 启动BOT连接
        /// </summary>
        /// <returns></returns>
        public async Task<Data.LoginInfo> StartBot()
        {
            Instance = this;
            InitializeIpaddress();
            try
            {
                string login_info = get_login_info();
                LoginInfo = new Data.LoginInfo(login_info);
            }
            catch (Exception ex)
            {

                throw new Exception("HTTP服务连接失败，请检查;" + ex.Message);
            }

            try
            {
                await StartWebsocketListenerAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Websocket服务连接失败，请检查;" + ex.Message);
            }

            return LoginInfo;


        }

        /// <summary>
        /// 启动websocket监听
        /// </summary>
        /// <returns></returns>
        private async Task StartWebsocketListenerAsync()

        {
            var uri = new Uri(WebsocKetIpaddress!);

            // var client = new ClientWebSocket();

            //  client.Options.SetRequestHeader("Authorization", AccessTocken);


            //连接到WebSocket服务器。
            // client.ConnectAsync(uri, CancellationToken.None);

            var clientFactory = new Func<Uri, CancellationToken, Task<WebSocket>>(async (uri, cancellationToken) =>
            {
                ClientWebSocket client = new ClientWebSocket();
                if (!string.IsNullOrWhiteSpace(AccessTocken))
                    client.Options.SetRequestHeader("authorization", AccessTocken);
                await client.ConnectAsync(uri, cancellationToken).ConfigureAwait(false);
                return client;
            });

            _client = new WebsocketClient(uri, null, clientFactory)
            {
                IsReconnectionEnabled = false,
            };
            await _client.StartOrFail();
            _client.DisconnectionHappened
                .Subscribe(x =>
                {
                    _disconnectionHappened.OnNext(x.CloseStatus ?? WebSocketCloseStatus.Empty);
                });

            //_client.MessageReceived
            //  .Where(message => message.MessageType == WebSocketMessageType.Text)
            //  .Subscribe(message =>

            _client.MessageReceived
                  .Where(message => message.MessageType == WebSocketMessageType.Text)
                  .Subscribe(message =>
                  {
                      var data = message?.Text;
                      if (string.IsNullOrWhiteSpace(data))
                          throw new InvalidDataException("Websocket数据响应错误！");
                      //ProcessWebSocketData(data);
                      ProcessWebSocketData(message!);

                  });



        }

        private async void testWebSocket()
        {
            ClientWebSocket clientWebSocket = new ClientWebSocket();
            Uri uri = new Uri("");


            // 构建请求头
            Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "Custom-Header", "Value" } // 添加自定义请求头
        };
            clientWebSocket.Options.SetRequestHeader("authorization", AccessTocken);

            await clientWebSocket.ConnectAsync(uri, CancellationToken.None);
            //var buffer = System.Text.Encoding.UTF8.GetBytes("");
            //var segment = new ArraySegment<byte>(buffer);
            //clientWebSocket.ReceiveAsync(buffer,CancellationToken.None);

            //byte[] receiveBuffer = new byte[1024];
            //var result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            //string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);


            while (clientWebSocket.State == WebSocketState.Open)
            {
                byte[] receiveBuffer = new byte[1024];
                var result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
                string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);

                // 处理接收到的消息
                Console.WriteLine("Received message: " + receivedMessage);
            }


        }

        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="responseMessage">WebSocket获取到的ResponseMessage</param>
        private void ProcessWebSocketData(ResponseMessage responseMessage)
        {


            _allWebsocketMessageReceived.OnNext(responseMessage);

            try
            {

                //JsonDocument jsonDocument = JsonDocument.Parse(responseMessage.Text!);
                //JsonElement root = jsonDocument.RootElement;
                //string post_type = Convert.ToString(root.GetProperty("post_type"))!;
                //string message_type = Convert.ToString(root.GetProperty("message_type"))!;

                JsonNode jsonNode = JsonNode.Parse(responseMessage.Text!)!;
                string post_type = Convert.ToString(jsonNode["post_type"])!;

                /*
                            message：消息事件
                            notice：通知事件
                            request：请求事件
                            meta_event：元事件
                 */

                bool message_sent = false;
                if ("message_sent".Equals(post_type, StringComparison.OrdinalIgnoreCase))

                {
                    if (IsContainsBotMessage)
                    {
                        message_sent = true;
                    }
                }
                else
                {
                    message_sent = false;
                }




                if ("message".Equals(post_type, StringComparison.OrdinalIgnoreCase) || message_sent)

                {
                    string message_type = Convert.ToString(jsonNode["message_type"])!;


                    try
                    {
                        if ("group".Equals(message_type, StringComparison.OrdinalIgnoreCase))
                        {


                            Receivers.Message.Group.GroupMessageReceiver groupMessageReceiver = JsonSerializer.Deserialize<Receivers.Message.Group.GroupMessageReceiver>(responseMessage.Text!)!;
                            groupMessageReceiver.Originaljson = responseMessage.Text!;
                            _messageReceived.OnNext(groupMessageReceiver);
                        }
                        else if ("private".Equals(message_type, StringComparison.OrdinalIgnoreCase))
                        {

                            Receivers.Message.Private.PrivateMessageReceiver privateMessageReceiver = JsonSerializer.Deserialize<Receivers.Message.Private.PrivateMessageReceiver>(responseMessage.Text!)!;
                            privateMessageReceiver.Originaljson = responseMessage.Text!;
                            _messageReceived.OnNext(privateMessageReceiver);

                        }
                        else

                        {
                            //  Receivers.Message.MessageReceiverBase messageReceiverBase = JsonSerializer.Deserialize<Receivers.Message.MessageReceiverBase>(responseMessage.Text!)!;
                            Receivers.Message.MessageReceiverBase messageReceiverBase = new Receivers.Message.MessageReceiverBase();
                            messageReceiverBase.ReceiveMessageType = Data.EventMessageType.Unknown;
                            messageReceiverBase.Originaljson = responseMessage.Text!;
                            _messageReceived.OnNext(messageReceiverBase);
                        }


                    }
                    catch (Exception)
                    {
                        //  Receivers.Message.MessageReceiverBase messageReceiverBase = JsonSerializer.Deserialize<Receivers.Message.MessageReceiverBase>(responseMessage.Text!)!;
                        Receivers.Message.MessageReceiverBase messageReceiverBase = new Receivers.Message.MessageReceiverBase();
                        messageReceiverBase.ReceiveMessageType = Data.EventMessageType.Unknown;
                        messageReceiverBase.Originaljson = responseMessage.Text!;
                        _messageReceived.OnNext(messageReceiverBase);
                        //throw;
                    }



                }

                else if ("notice".Equals(post_type, StringComparison.OrdinalIgnoreCase))

                {
                    try
                    {


                        string notice_type = Convert.ToString(jsonNode["notice_type"])!;

                        string sub_type = string.Empty;
                        try
                        {
                            sub_type = Convert.ToString(jsonNode["sub_type"])!;
                        }
                        catch (Exception)
                        {

                            // throw;
                        }

                        if ("group_upload".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Notice.GroupUploadReceiver groupUploadReceiver = JsonSerializer.Deserialize<Receivers.Notice.GroupUploadReceiver>(responseMessage.Text!)!;
                            groupUploadReceiver.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(groupUploadReceiver);

                        }
                        else if ("group_admin".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Notice.GroupAdminReceiver groupAdminReceiver = JsonSerializer.Deserialize<Receivers.Notice.GroupAdminReceiver>(responseMessage.Text!)!;
                            groupAdminReceiver.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(groupAdminReceiver);
                        }
                        else if ("group_decrease".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Notice.GroupDecreaseReceiver groupDecreaseReceiver = JsonSerializer.Deserialize<Receivers.Notice.GroupDecreaseReceiver>(responseMessage.Text!)!;
                            groupDecreaseReceiver.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(groupDecreaseReceiver);
                        }
                        else if ("group_increase".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Notice.GroupIncreaseReceiver groupIncreaseReceiver = JsonSerializer.Deserialize<Receivers.Notice.GroupIncreaseReceiver>(responseMessage.Text!)!;
                            groupIncreaseReceiver.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(groupIncreaseReceiver);
                        }
                        else if ("group_ban".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Notice.GroupBanReceiver groupBanReceiver = JsonSerializer.Deserialize<Receivers.Notice.GroupBanReceiver>(responseMessage.Text!)!;
                            groupBanReceiver.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(groupBanReceiver);
                        }
                        else if ("friend_add".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {

                            Receivers.Notice.FriendAddReceiver friendAddReceiver = JsonSerializer.Deserialize<Receivers.Notice.FriendAddReceiver>(responseMessage.Text!)!;
                            friendAddReceiver.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(friendAddReceiver);
                        }
                        else if ("group_recall".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Notice.GroupRecallReceiver groupRecallReceiver = JsonSerializer.Deserialize<Receivers.Notice.GroupRecallReceiver>(responseMessage.Text!)!;
                            groupRecallReceiver.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(groupRecallReceiver);
                        }

                        else if ("friend_recall".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Notice.FriendRecallReceiver friendRecallReceiver = JsonSerializer.Deserialize<Receivers.Notice.FriendRecallReceiver>(responseMessage.Text!)!;
                            friendRecallReceiver.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(friendRecallReceiver);
                        }
                        else if ("XX".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {

                        }
                        else if ("XX".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {

                        }
                        else if ("group_card".Equals(notice_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Notice.GroupCardReceiver groupCardReceiver = JsonSerializer.Deserialize<Receivers.Notice.GroupCardReceiver>(responseMessage.Text!)!;
                            groupCardReceiver.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(groupCardReceiver);
                        }


                        else
                        {
                            // Receivers.Notice.NoticeReceiverBase noticeReceiverBase = JsonSerializer.Deserialize<Receivers.Notice.NoticeReceiverBase>(responseMessage.Text!)!;
                            Receivers.Notice.NoticeReceiverBase noticeReceiverBase = new Receivers.Notice.NoticeReceiverBase();
                            noticeReceiverBase.Originaljson = responseMessage.Text!;
                            _noticeReceived.OnNext(noticeReceiverBase);
                        }

                    }

                    catch (Exception)
                    {
                        Receivers.Notice.NoticeReceiverBase noticeReceiverBase = new Receivers.Notice.NoticeReceiverBase();
                        noticeReceiverBase.Originaljson = responseMessage.Text!;
                        _noticeReceived.OnNext(noticeReceiverBase);

                        // throw;
                    }

                }

                else if ("request".Equals(post_type, StringComparison.OrdinalIgnoreCase))

                {

                    string request_type = Convert.ToString(jsonNode["request_type"])!;

                    try
                    {


                        if ("friend".Equals(request_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Request.FriendRequestReceiver friendRequestReceiver = JsonSerializer.Deserialize<Receivers.Request.FriendRequestReceiver>(responseMessage.Text!)!;
                            friendRequestReceiver.Originaljson = responseMessage.Text!;
                            _requestReceived.OnNext(friendRequestReceiver);
                        }

                        else if ("group".Equals(request_type, StringComparison.OrdinalIgnoreCase))
                        {
                            Receivers.Request.GroupRequestReceiver groupRequestReceiver = JsonSerializer.Deserialize<Receivers.Request.GroupRequestReceiver>(responseMessage.Text!)!;
                            groupRequestReceiver.Originaljson = responseMessage.Text!;
                            _requestReceived.OnNext(groupRequestReceiver);
                        }
                        else
                        {
                            // Receivers.Request.RequestReceiverBase requestReceiverBase = JsonSerializer.Deserialize<Receivers.Request.RequestReceiverBase>(responseMessage.Text!)!;
                            Receivers.Request.RequestReceiverBase requestReceiverBase = new Receivers.Request.RequestReceiverBase();
                            requestReceiverBase.Originaljson = responseMessage.Text!;
                            _requestReceived.OnNext(requestReceiverBase);
                        }
                    }
                    catch (Exception)
                    {
                        Receivers.Request.RequestReceiverBase requestReceiverBase = new Receivers.Request.RequestReceiverBase();
                        requestReceiverBase.Originaljson = responseMessage.Text!;
                        _requestReceived.OnNext(requestReceiverBase);
                        // throw;
                    }
                }

                else if ("meta_event".Equals(post_type, StringComparison.OrdinalIgnoreCase))

                {

                    _meta_eventReceived.OnNext(responseMessage);
                }

                else
                {
                    _unknownEventReceived.OnNext(responseMessage!);
                }


                //string message_type = Convert.ToString(root.GetProperty("message_type"))!;

                //string self_id = Convert.ToString(root.GetProperty("self_id"))!;
                //string user_id = Convert.ToString(root.GetProperty("user_id"))!;
                //string time = Convert.ToString(root.GetProperty("time"))!;
                //string raw_message = Convert.ToString(root.GetProperty("raw_message"))!;
                //string font = Convert.ToString(root.GetProperty("font"))!;
                //string sub_type = Convert.ToString(root.GetProperty("sub_type"))!;

            }
            catch (Exception)
            {
                _unknownEventReceived.OnNext(responseMessage!);
            }
            // Console.WriteLine(data);
        }


    }







}

