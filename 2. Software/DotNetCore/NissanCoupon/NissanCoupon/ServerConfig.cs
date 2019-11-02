using System;
using System.Collections.Generic;
using System.Text;

namespace NissanCoupon
{
    public class ServerConfig
    {
        public const string Version = "0.0.1";
        public static string LinkWebService { set; get; }
        public static string ServicesApiKey { set; get; }

        public static void LoadConfig()
        {
            ServicesApiKey = "92f80bc8-fba0-4624-821e-c7d433bb7868";
            LinkWebService = string.Empty;
        }

        public static void SaveConfig()
        {


        }
    }
}
