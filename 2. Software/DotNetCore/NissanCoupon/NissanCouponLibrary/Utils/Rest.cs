﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

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
            StringBuilder StringData = new StringBuilder();

            try
            {
                int DataToRead = 0;

                byte[] DataBuffer = new byte[100];

                do
                {
                    DataToRead = Data.Read(DataBuffer, 0, 100);

                    if (DataToRead > 0)
                    {
                        for (int i = DataToRead; i < 100; i++) DataBuffer[i] = 0;

                        StringData.Append(ASCIIEncoding.UTF8.GetString(DataBuffer, 0, DataToRead));
                    }
                } while (DataToRead > 0);

                Log.LogEvent("GetObjectFromStream", StringData.ToString());

                return JsonConvert.DeserializeObject<T>(StringData.ToString());
            }
            catch (Exception ex)
            {
                Log.LogError("GetObjectFromStream", StringData.ToString(), ex.Message);
            }


            return default(T);
        }
    }
}