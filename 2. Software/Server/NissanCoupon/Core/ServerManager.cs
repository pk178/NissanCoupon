using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core
{
    public class ServerManager
    {
        public static void ServerInit()
        {
            ServerConfig.LoadConfig();
            DataBase.DAOManager.Init();
            Services.Hosting.HostWebService();
            CouponManager.InitCouponManager();

            //NissanCouponLibrary.Utils.SMS.SendSMS("0356053721", "Khởi tạo server lúc : " + DateTime.Now);

            (new Task(() => ServerManagerTick())).Start();
        }

        private static void ServerManagerTick()
        {
            while(true)
            {
                Services.Hosting.CheckService();

                Task.Delay(1000).Wait();
            }
        }
    }
}
