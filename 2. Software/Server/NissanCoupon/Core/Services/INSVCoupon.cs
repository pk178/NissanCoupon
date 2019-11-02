using NissanCouponLibrary.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core.Services
{
    [ServiceContract]
    public interface INSVCoupon
    {
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Login/{ApiKey}")]
        LoginResult Login(string ApiKey, Stream Data);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateUser/{ApiKey}")]
        bool UpdateUser(string ApiKey, Stream Data);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllUser/{ApiKey}")]
        List<UserInfo> GetAllUser(string ApiKey);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UploadCouponData/{ApiKey}")]
        ReturnInfo UploadCouponData(string ApiKey, Stream Data);
        
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CheckCoupon/{Coupon}/{ApiKey}")]
        CouponInfo CheckCoupon(string ApiKey, string Coupon);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllCoupon/{ApiKey}/{FromTime}/{ToTime}")]
        List<CouponInfo> GetAllCoupon(string ApiKey, string FromTime, string ToTime);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDealerRedeemedList/{ApiKey}/{FromTime}/{ToTime}")]
        List<DealerRedeemed> GetDealerRedeemedList(string ApiKey, string FromTime, string ToTime, Stream Data);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CreateCouponOtp/{ApiKey}/{Counpon}")]
        bool CreateCouponOtp(string ApiKey, string Counpon);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CheckCounponOtp/{ApiKey}/{Counpon}/{Otp}/{DealerName}")]
        bool CheckCounponOtp(string ApiKey, string Counpon, string Otp, string DealerName);
    }
}
