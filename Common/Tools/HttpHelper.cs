using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;

namespace Common.Tools
{
    public class MyClass
    {
        public string Url { get; set; }
        public string ContentType { get; set; }
        public string Method { get; set; }
        public string Accept { get; set; }
        public string Referer { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public string Postdata { get; set; }
        public bool KeepAlive { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public string Response_SessionID { get; set; }
    }

    public class HttpHelper
    {
        public static HttpHelper _HttpHelper = new HttpHelper();

        public string SendAsyncHttp(ref MyClass mc)
        {
            string returnStr = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(mc.Url);
                request.Referer = mc.Referer;
                request.Method = mc.Method;
                request.Accept = mc.Accept;
                request.ContentType = mc.ContentType;
                request.Headers = mc.Headers;
                request.KeepAlive = mc.KeepAlive;

                if (mc.CookieContainer!=null)
                    request.CookieContainer = mc.CookieContainer;

                if (mc.Accept.Contains("image"))
                {
                    request.AllowWriteStreamBuffering = true;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    request.MaximumResponseHeadersLength = -1;
                }

                if (mc.Method == "POST")
                {
                    // 发送数据
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    byte[] data = encoding.GetBytes(mc.Postdata);
                    request.ContentLength = data.Length;
                    request.ContentType = mc.ContentType;
                    Stream myStream = request.GetRequestStream();
                    myStream.Write(data, 0, data.Length);
                    myStream.Close();
                }

                // 接受响应
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();

                if (mc.Accept.Contains("image"))
                {
                    #region 获取图片流保存到本地
                    //var filePath = Environment.CurrentDirectory;
                    var filePath = "E:\\IMG\\";
                    var fileName = DateTime.Now.ToLongDateString() + ".jpg";
                    if (!Directory.Exists(filePath)) //判断是否存在某个文件夹
                    {
                        Directory.CreateDirectory(filePath); //创建文件夹
                    }
                    if (!File.Exists(filePath + fileName)) //图片文件的全路径字符串
                    {
                        File.Create(filePath + fileName);
                    }

                    List<byte> list = new List<byte>();
                    while (true)
                    {
                        int data = stream.ReadByte();
                        if (data == -1)
                            break;
                        list.Add((byte)data);
                    }
                    File.WriteAllBytes(filePath + fileName, list.ToArray());
                    returnStr = filePath + fileName;
                    stream.Close();
                    #endregion
                }
                else
                {
                    string buffer = "", line;
                    StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("UTF-8"));
                    while ((line = reader.ReadLine()) != null)
                    {
                        buffer += line + "\r\n";
                    }
                    returnStr = buffer;
                    stream.Close();
                }

                // 获取结果SessionID值
                if (response.Headers.AllKeys.Contains("Set-Cookie"))
                    mc.Response_SessionID = GetSessionId(response.Headers["Set-Cookie"]);

                // 关闭响应
                response.Close();
                mc.CookieContainer = request.CookieContainer; //保存cookies
                return returnStr;
            }
            catch(Exception ex)
            {
                throw new WebException("http请求异常:" + ex);
            }
        }

        private string GetSessionId(string Cookie)
        {
            string[] strArray = Cookie.Split(';');
            foreach (var item in strArray)
            {
                if (item.Contains("ASP.NET_SessionId")) 
                {
                    string[] itemArray = item.Split('=');
                    return itemArray[1];
                }
            }
            return string.Empty;
        }

        public string GetDataByGet(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                return client.GetStringAsync(url).Result;
            }
            catch {
                return null;
            }
        }

        public string GetDataByPost(string url, object entity)
        {
            try
            {
                HttpClient client = new HttpClient();
                return client.PostAsJsonAsync(url, entity).Result.Content.ReadAsStringAsync().Result;
            }
            catch {
                return null;
            }
        }

        /// <summary>
        /// 通过Post获取
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postData">数据</param>
        /// <param name="encode">页面编码</param>
        /// <returns></returns>
        public string GetDataByPostForm(string url, string postData)
        {
            WebClient client = new WebClient();
            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0");
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers.Add("ContentLength", postData.Length.ToString());
            client.Encoding = System.Text.Encoding.UTF8;
            try
            {
                return client.UploadString(url, postData);
            }
            catch
            {
                throw new WebException("http请求异常");
            }
        }
    }
}
