using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.Common
{
    /// <summary>
    /// 第一信息发送短信接口类
    /// </summary>
    public class SendSMSClass
    {
        private readonly static string username = "anemptycup";
        private readonly static string pwd = "E2B9F852E5766B23FA02AAE1AC06";
        /// <summary>
        /// 发送短信息
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SendSMS(string phone,string content)
        {
            StringBuilder sms = new StringBuilder();
            sms.AppendFormat("name={0}", username);
            sms.AppendFormat("&pwd={0}", pwd);
            sms.AppendFormat("&content={0}", content);
            sms.AppendFormat("&mobile={0}", phone);
            sms.AppendFormat("&sign={0}", "诚信创客");
            sms.Append("&type=pt");
            string resp = PushToWeb("http://sms.1xinxi.cn/asmx/smsservice.aspx", sms.ToString(), Encoding.UTF8);
            string[] msg = resp.Split(',');
            if (msg[0] == "0")
            {
                return "s" + msg[1];
            }
            else
            {
                return "f" + msg[1];
            }
        }
        private static string PushToWeb(string weburl, string data, Encoding encode)
        {
            
            byte[] byteArray = encode.GetBytes(data);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(weburl));
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream newStream = webRequest.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();

            //接收返回信息：
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            StreamReader aspx = new StreamReader(response.GetResponseStream(), encode);
            return aspx.ReadToEnd();
        }
    }
}
