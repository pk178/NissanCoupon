using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NissanCouponLibrary.Entity
{
    [DataContract]
    public class DealerRedeemed
    {
        [DataMember]
        public string CustomerName { set; get; }

        [DataMember]
        public string PhoneNumber { set; get; }

        [DataMember]
        public string VehicleModel { set; get; }

        [DataMember]
        public string ChassisNumber { set; get; }

        [DataMember]
        public string LicensePlateNumber { set; get; }

        [DataMember]
        public string GiftCode { set; get; }

        [IgnoreDataMember]
        public DateTime ExpriedDate { set; get; }

        [DataMember(Name = "ExpriedDate")]
        public string ExpriedDateStr
        {
            get { return ExpriedDate.ToString("dd/MM/yyyy"); }
            set { ExpriedDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
        }

        [DataMember]
        public string EntitledServiceGift { set; get; }

        [IgnoreDataMember]
        public DateTime RedeemedDate { set; get; }

        [DataMember(Name = "RedeemedDate")]
        public string RedeemedDateStr
        {
            get { return RedeemedDate.ToString("dd/MM/yyyy"); }
            set { RedeemedDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
        }

        [DataMember]
        public string RedeemedByDealer { set; get; }
    }
}
