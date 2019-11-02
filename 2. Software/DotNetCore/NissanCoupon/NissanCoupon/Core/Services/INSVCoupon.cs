using NissanCouponLibrary.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core.Services
{
    public interface INSVCoupon
    {
        LoginResult Login(string ApiKey, Stream Data);

        bool UpdateUser(string ApiKey, Stream Data);

        List<UserInfo> GetAllUser(string ApiKey);

        ReturnInfo UploadCouponData(string ApiKey, Stream Data);
        
        CouponInfo CheckCoupon(string ApiKey, string Coupon);

        List<CouponInfo> GetAllCoupon(string ApiKey, string FromTime, string ToTime);

        List<DealerRedeemed> GetDealerRedeemedList(string ApiKey, string FromTime, string ToTime, Stream Data);

        bool CreateCouponOtp(string ApiKey, string Counpon);

        bool CheckCounponOtp(string ApiKey, string Counpon, string Otp, string DealerName);
    }
}
