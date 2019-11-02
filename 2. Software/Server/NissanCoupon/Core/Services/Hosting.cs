using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core.Services
{
    public class Hosting
    {
        private static ServiceHost Webservices;
        private static bool ServiceState = false;
        public static bool IsOK
        {
            get
            {
                return !((Webservices == null) || (ServiceState == false));
            }
        }

        public static void HostWebService()
        {
            ServiceState = false;
            try
            {
                //Create a URI to serve as the base address
                Uri httpUrl = new Uri("http://127.0.0.1:2583/NSC");

                if (string.IsNullOrEmpty(ServerConfig.LinkWebService) == false)
                {
                    httpUrl = new Uri(ServerConfig.LinkWebService);
                }

                if (Webservices != null)
                {
                    Webservices.Close();
                    Webservices = null;
                }

                //Create ServiceHost
                Webservices = new WebServiceHost(typeof(NSVCoupon), httpUrl);
                WebHttpBinding binding = new WebHttpBinding();
                binding.MaxBufferPoolSize = Int32.MaxValue;
                binding.MaxBufferSize = Int32.MaxValue;
                binding.MaxReceivedMessageSize = Int32.MaxValue;

                //Add a service endpoint
                Webservices.AddServiceEndpoint(typeof(INSVCoupon), binding, "");

                //Enable metadata exchange
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                Webservices.Description.Behaviors.Add(smb);

                //Start the Service
                Webservices.Open();

                ServiceState = true;
            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("NissanCouponHosting", "", ex.Message);
                ServiceState = false;
            }
        }

        public static void CheckService()
        {
            try
            {
                if (Webservices == null || ServiceState == false)
                {
                    HostWebService();
                }

            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("CheckService", "", ex.Message);
            }


        }

        
    }
}
