using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Models
{
    public class SMSInfo
    {
        public string SendTo { set; get; }
        public string Message { set; get; }
        public string SendResult { set; get; }
        public string Time { set; get; }
    }
}
