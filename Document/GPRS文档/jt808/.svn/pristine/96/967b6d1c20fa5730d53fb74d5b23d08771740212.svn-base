using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Beetle.TJ808.BaseServer
{
    /// <summary>
    /// 数据提交服务器
    /// </summary>
    public class SendPosToServer
    {
        public const string PostMethod = "Post";
        public const string GetMethod = "Get";

        /// <summary>
        /// 查询设备授权码
        /// </summary>
        /// <param name="messageSim"></param>
        /// <param name="requestMethod"></param>
        /// <param name="connectTimeout"></param>
        /// <param name="readWriteTimeout"></param>
        /// <returns></returns>
        public static string QuerySig(string messageSim, string requestMethod = "Post", int connectTimeout = 20000, int readWriteTimeout = 20000)
        {
            var data = new Dictionary<string, string> { { "sim", messageSim } };
            var dataBytes = _ConvertKeyValuePairToBytes(data);
            var result = Send(Config.ServerUrl + "DataReceiver/QuerySig", new Dictionary<string, string>(), dataBytes, requestMethod, connectTimeout, readWriteTimeout);
            return GetContentString(result, Encoding.UTF8);
        }

        /// <summary>
        /// 提交坐标数据
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="requestMethod">Post/Get</param>
        /// <param name="connectTimeout">连接时限</param>
        /// <param name="readWriteTimeout">读取时限</param>
        /// <returns></returns>
        public static string SubmitPos(PostionDto dto, string requestMethod = "Post", int connectTimeout = 20000, int readWriteTimeout = 20000)
        {
            var data = new Dictionary<string, string> { { "Sim", dto.Sim }, { "Latitude", dto.Latitude }, { "Longitude", dto.Longitude }, { "Direction", dto.Direction }, { "Speed", dto.Speed }, { "Time", dto.Time }, { "Status", dto.Status } };
            var dataBytes = _ConvertKeyValuePairToBytes(data);
            var result = Send(Config.ServerUrl + "DataReceiver/Index", new Dictionary<string, string>(), dataBytes, requestMethod, connectTimeout, readWriteTimeout);
            return GetContentString(result, Encoding.UTF8);
        }

        /// <summary>
        /// 提交原始数据
        /// </summary>
        /// <param name="content"></param>
        /// <param name="requestMethod">Post/Get</param>
        /// <param name="connectTimeout">连接时限</param>
        /// <param name="readWriteTimeout">读取时限</param>
        /// <returns></returns>
        public static string SubmitBase(string content, string requestMethod = "Post", int connectTimeout = 20000, int readWriteTimeout = 20000)
        {
            var data = new Dictionary<string, string> { { "content", content } };
            var dataBytes = _ConvertKeyValuePairToBytes(data);
            var result = Send(Config.ServerUrl + "DataReceiver/BaseDataContent", new Dictionary<string, string>(), dataBytes, requestMethod, connectTimeout, readWriteTimeout);
            return GetContentString(result, Encoding.UTF8);
        }

        /// <summary>
        /// 数据提交服务器
        /// </summary>
        /// <param name="url">提交地址</param>
        /// <param name="cookies">cookies</param>
        /// <param name="data">数据</param>
        /// <param name="requestMethod">Post/Get</param>
        /// <param name="connectTimeout">连接时限</param>
        /// <param name="readWriteTimeout">读取时限</param>
        /// <returns></returns>
        private static WebResponse Send(string url, IDictionary<string, string> cookies, byte[] data, string requestMethod = "Post", int connectTimeout = 20000, int readWriteTimeout = 5000)
        {
            if (data.Length > 0 && GetMethod.Equals(requestMethod))
            {
                var requestString = Encoding.UTF8.GetString(data);
                var seperator = url.Contains("?") ? "&" : "?";
                url += seperator + requestString;
            }

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentLength = 0;
            request.Timeout = connectTimeout;
            request.ReadWriteTimeout = readWriteTimeout;
            _InitRequestContext(request, requestMethod);
            _InjectCookies(request, cookies);
            if (data.Length > 0 && PostMethod.Equals(requestMethod))
            {
                request.ContentLength = data.Length;
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                    requestStream.Flush();
                }
            }
            var response = request.GetResponse();
            return response;
        }

        private static byte[] _ConvertKeyValuePairToBytes(IDictionary<string, string> dataDictionary)
        {
            var keyValuePairStringList = new List<string>();

            foreach (var key in dataDictionary.Keys)
            {
                var itemString = string.Format("{0}={1}", key, dataDictionary[key]);
                keyValuePairStringList.Add(itemString);
            }
            string dataString = string.Join("&", keyValuePairStringList);
            var data = Encoding.UTF8.GetBytes(dataString);
            return data;
        }

        /// <summary>
        /// 封装
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestMethod"></param>
        private static void _InitRequestContext(HttpWebRequest request, string requestMethod = "Post")
        {
            request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
            request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
            request.UserAgent = "User-Agent: xuanyuansoft file fragment upload by http post.";
            request.KeepAlive = true;

            //上面的http头看情况而定，但是下面俩必须加  
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = requestMethod;
        }

        private static void _InjectCookies(HttpWebRequest request, IDictionary<string, string> cookies)
        {
            if (cookies != null && cookies.Count > 0)
            {
                request.CookieContainer = new CookieContainer();
                foreach (var key in cookies.Keys)
                {
                    var value = cookies[key];
                    var hostItems = request.Host.Split(new[] { ':' });
                    var cookie = new Cookie(key, value, "/", hostItems[0]) { Expires = DateTime.Now.AddDays(60) };

                    request.CookieContainer.Add(cookie);
                }
            }
        }
        /// <summary>
        /// 返回编码
        /// </summary>
        /// <param name="response">响应</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private static string GetContentString(WebResponse response, Encoding encoding)
        {
            byte[] responseBytes = GetContentBytes(response);
            var responseString = encoding.GetString(responseBytes);
            return responseString;
        }

        private static byte[] GetContentBytes(WebResponse response)
        {
            byte[] data;
            using (var buffer = new MemoryStream())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null) responseStream.CopyTo(buffer);
                }
                data = buffer.ToArray();
            }
            return data;
        }
    }
}