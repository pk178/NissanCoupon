using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NissanCouponLibrary.Entity
{
    [DataContract]
    public class ReturnInfo
    {
        [DataMember]
        public int ReturnCode;

        [DataMember]
        public string ReturnString;
    }
}
