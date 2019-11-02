using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NissanCouponLibrary.Utils
{
    public class Rest
    {
        public const int GET_REQUEST = 0;
        public const int POST_REQUEST = 1;

        public static T CreateRequest<T>(string URL, int RequestType, object Body = null)
        {
            try
            {
                Uri url = new Uri(string.Format(URL.Replace(" ", "%20")));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                if (RequestType == POST_REQUEST)
                {
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.Accept = "application/octet-stream";

                    byte[] dataBuffer = new byte[0];
                    if (Body != null)
                    {
                        var Debug = JsonConvert.SerializeObject(Body);
                        dataBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Body));
                    }

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(dataBuffer, 0, dataBuffer.Length);
                    }
                }
                else
                {
                    request.Method = "GET";
                }

                string result;
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                    }
                }

                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                Log.LogError("GetData", URL, ex.Message);
            }

            return default(T);
        }

        public static T GetObjectFromStream<T>(Stream Data)
        {
            string strData = string.Empty;

            try
            {                
                using (StreamReader sr = new StreamReader(Data, Encoding.UTF8))
                {
                    strData = sr.ReadToEnd();
                    Log.LogEvent("GetObjectFromStream", strData);

                    return JsonConvert.DeserializeObject<T>(strData);
                }
               
                
            }
            catch (Exception ex)
            {
                Log.LogError("GetObjectFromStream", strData, ex.Message);
            }


            return default(T);
        }
    }
}
