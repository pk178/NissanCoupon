using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Models
{
    public class DealerInfo
    {
        public string Region { set; get; }
        public string Name { set; get; }
        public string Abbreviation { set; get; }
        public string Type { set; get; }
        public string Code { set; get; }
    }
}
