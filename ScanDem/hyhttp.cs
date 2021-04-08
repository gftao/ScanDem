using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;

namespace scandmo
{
    public class hyhttp
    {
            private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true; //总是接受     
            }
            public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding charset, string timeout)
            {
                HttpWebRequest request = null;
            //HttpWebResponse resp = null;
            try {
                //HTTPSQ请求  
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = DefaultUserAgent;
                request.Timeout = Convert.ToInt32(timeout) * 1000;//连接超时时间

                //如果需要POST数据     
                if (!(parameters == null || parameters.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}{1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}{1}", key, parameters[key]);
                        }
                        i++;
                    }
                    byte[] data = charset.GetBytes(buffer.ToString());

                    using (Stream stream = request.GetRequestStream())//写入
                    {
                        stream.Write(data, 0, data.Length);
                    }


                }
                /* try
                 {
                     resp = request.GetResponse() as HttpWebResponse;
                 }

                 catch (WebException webEx)
                 {
                     //webEx.StatusEx.Status是错误的状态
                     if (webEx.Status == WebExceptionStatus.Timeout)
                     {//这里写处理错误方法

                     }
                     //return request.GetResponse() as HttpWebResponse;//读取返回

                 }*/
                return request.GetResponse() as HttpWebResponse;//读取返回
            }catch (Exception)
            {
                return null;
            }
            
            }

            public static string HttpsPost(string url, string data, string timeout)
            {
                try
                {
                    Encoding encoding = Encoding.GetEncoding("utf-8");
                    IDictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("", data);
                    HttpWebResponse response = CreatePostHttpResponse(url, parameters, encoding, timeout);
                    //打印返回值  
                    Stream stream = response.GetResponseStream();   //获取响应的字符串流  
                    StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
                    string html = sr.ReadToEnd();   //从头读到尾，放到字符串html 
                    return html;
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            //return null;
        }
     }
 }
