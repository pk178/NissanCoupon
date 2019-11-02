using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Models
{
    public class CouponRedeemResult
    {
        //public const int RESULT_CODE_OK = 0;
        //public const int RESULT_CODE_EXPRIED = 1;
        //public const int RESULT_CODE_REDEEMED = 2;

        public int ResultCode { set; get; }
        public string RedeemMessage { set; get; }
    }

    public class CouponCheckResult
    {
        //public const int RESULT_CODE_OK = 0;
        //public const int RESULT_CODE_EXPRIED = 1;
        //public const int RESULT_CODE_REDEEMED = 2;
        //public const int RESULT_CODE_NOT_EXIST = 3;

        public int ResultCode;
        public CouponInfo Coupon;
    }

    public class CouponUploadResult
    {
        public int ReturnCode { set; get; }
        public string ReturnString { set; get; }
    }

    public class CouponInfo
    {
        //public const int COUPON_TYPE_SALE_SURVEY = 1;
        //public const int COUPON_TYPE_SALE_PROMOTION = 2;
        //public const int COUPON_TYPE_SERVICE_SURVEY = 3;
        //public const int COUPON_TYPE_SERVICE_PROMOTION = 4;
        //public const int COUPON_TYPE_UNKNOW = 255;

        public string Category { set; get; }
        public string Type { set; get; }
        public string PromotionDate { set; get; }
        public string DealerName { set; get; }
        public int CustomerNumber { set; get; }
        public string CustomerName { set; get; }
        public string PhoneNumber { set; get; }
        public string VehicleModel { set; get; }
        public string ChassisNumber { set; get; }
        public string LicensePlateNumber { set; get; }
        public string GiftCode { set; get; }
        public string ExpriedDate { set; get; }
        public string ReminderDay { set; get; }
        public string EntitledServiceGift { set; get; }
        public string RedeemedDate { set; get; }
        public string RedeemedByDealer { set; get; }
        public string CampaignName { set; get; }
    }

    public class DealerRedeemed
    {
        public string CustomerName { set; get; }
        public string PhoneNumber { set; get; }
        public string VehicleModel { set; get; }
        public string ChassisNumber { set; get; }
        public string LicensePlateNumber { set; get; }
        public string GiftCode { set; get; }
        public string ExpriedDate { set; get; }
        public string EntitledServiceGift { set; get; }
        public string RedeemedDate { set; get; }
        public string RedeemedByDealer { set; get; }
    }
}
