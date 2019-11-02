using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon
{
    public class ServerConfig
    {
        public const string Version = "0.0.1";
        public static string LinkWebService { set; get; }
        public static string ServicesApiKey { set; get; }

        public static void LoadConfig()
        {
            LinkWebService = Properties.Settings.Default.LinkWebService;
            ServicesApiKey = "92f80bc8-fba0-4624-821e-c7d433bb7868";

        }

        public static void SaveConfig()
        {

            Properties.Settings.Default.Save();

        }
    }
}
