using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NissanCouponLibrary.Utils
{
    public class SMS
    {
        public static bool SendSMS(string SendTo, string Message)
        {
            try
            {
                Dictionary<string, string> Params = new Dictionary<string, string>();
                Params.Add("user", "nissanvn");
                Params.Add("pass", "nissanvn12");
                Params.Add("sms", Message);
                Params.Add("senderName", "Nissan VN");
                Params.Add("phone", CorrectPhoneNumber(SendTo));
                Params.Add("isFlash", "false");
                Params.Add("isUnicode", "false");

                var Result = NissanCouponLibrary.Utils.SoapHelper.SendSOAPRequest(
                    "https://api.onesms.vn/wsPartners/Service.asmx",
                    "SendMT", "http://1sms.vn/", Params);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Result);

                return true;
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("SendSMS", string.Format("SendTo : {0}, Message : {1}", SendTo, Message), ex.Message);
            }

            return false;
        }

        public static string CorrectPhoneNumber(string PhoneNumber)
        {
            try
            {
                if(PhoneNumber.StartsWith("0"))
                {
                    return "84" + PhoneNumber.Substring(1);
                }

                if(PhoneNumber.StartsWith("+84"))
                {
                    return PhoneNumber.Substring(1);
                }
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("CorrectPhoneNumber", string.Format("PhoneNumber : {0}", PhoneNumber), ex.Message);
            }

            return PhoneNumber;
        }
    }
}
