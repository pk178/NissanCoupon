using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NissanCouponLibrary.Entity;

namespace NissanCoupon.Core.Services
{
    public class NSVCoupon : INSVCoupon
    {
        #region User Process
        public List<UserInfo> GetAllUser(string ApiKey)
        {
            if (ApiKey != ServerConfig.ServicesApiKey) return new List<UserInfo>();

            return DataBase.User.GetAllUser();
        }

        public LoginResult Login(string ApiKey, Stream Data)
        {
            try
            {
                if (ApiKey != ServerConfig.ServicesApiKey) return new LoginResult
                {
                    Result = LoginResult.LOGIN_RESULT_USER_WRONG_API_KEY,
                    User = new UserInfo()
                };

                var LoginInfo = NissanCouponLibrary.Utils.Rest.GetObjectFromStream<UserLogin>(Data);
                var LoginedUser = DataBase.User.GetUser(LoginInfo.UserName, LoginInfo.Password);

                if (LoginedUser == null) return new LoginResult
                {
                    Result = LoginResult.LOGIN_RESULT_INFO_ERROR,
                    User = new UserInfo()
                };


                if (LoginedUser.IsValid()) return new LoginResult
                {
                    Result = LoginResult.LOGIN_RESULT_OK,
                    User = LoginedUser
                };

                return new LoginResult
                {
                    Result = LoginResult.LOGIN_RESULT_INFO_ERROR,
                    User = new UserInfo()
                };
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("Login", "", ex.Message);
            }

            return new LoginResult
            {
                Result = LoginResult.LOGIN_RESULT_INFO_ERROR,
                User = new UserInfo()
            };
        }
        
        public bool UpdateUser(string ApiKey, Stream Data)
        {
            var UserInfo = NissanCouponLibrary.Utils.Rest.GetObjectFromStream<UserInfo>(Data);

            if (ApiKey != ServerConfig.ServicesApiKey) return false;
            if (UserInfo.IsValid() == false) return false;

            return DataBase.User.UpdateUser(UserInfo);
        }

        #endregion User Process

        #region Coupon Process
        public CouponInfo CheckCoupon(string ApiKey, string Coupon)
        {
            if (ApiKey != ServerConfig.ServicesApiKey) return new CouponInfo();
            return DataBase.CouponDAO.CheckCoupon(Coupon);
        }

        public List<CouponInfo> GetAllCoupon(string ApiKey, string FromTime, string ToTime)
        {
            if (ApiKey != ServerConfig.ServicesApiKey) return new List<CouponInfo>();

            try
            {
                DateTime From = DateTime.ParseExact(FromTime, "ddMMyyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(ToTime, "ddMMyyyy", CultureInfo.InvariantCulture);

                if ((To - From).TotalDays >= 0)
                    return DataBase.CouponDAO.GetAllCoupon(From, To);
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("GetAllCoupon", "", ex.Message);
            }

            return new List<CouponInfo>();

        }

        public ReturnInfo UploadCouponData(string ApiKey, Stream Data)
        {
            try
            {
                if (ApiKey != ServerConfig.ServicesApiKey) return new ReturnInfo
                {
                    ReturnCode = 1,
                    ReturnString = string.Format("API không đúng")
                };

                var NewCounpon = NissanCouponLibrary.Utils.Rest.GetObjectFromStream<List<CouponInfo>>(Data);
                if (NewCounpon.Count > 0)
                {
                    // Check dealer, campaign and create gift code
                    foreach (var tmpCounpon in NewCounpon)
                    {
                        // Create gift code
                        DealerInfo tmpDealer = DataBase.DealerDAO.GetDealer(Name: tmpCounpon.DealerName);
                        if (tmpDealer == null) return new ReturnInfo { ReturnCode = 100, ReturnString = string.Format("Đại lý '{0}' không tồn tại trên hệ thống.", tmpCounpon.DealerName) };

                        if (tmpDealer.IsValid())
                        {
                            tmpCounpon.CreateGiftCode(tmpDealer);
                        }
                        else
                        {
                            return new ReturnInfo { ReturnCode = 101, ReturnString = string.Format("Thông tin đại lý không hợp lệ : '{0}'", tmpCounpon.DealerName) };
                        }

                        var tmpReturn = tmpCounpon.IsValid();
                        if (tmpReturn.ReturnCode != 0) return tmpReturn;
                    }

                    // Check coupon exist or not
                    var CheckCouponResult = DataBase.CouponDAO.IsCouponExist(NewCounpon);
                    if (CheckCouponResult.ReturnCode != 0)
                    {
                        return CheckCouponResult;
                    }

                    // If all of things are ok, update to DB
                    foreach (var tmpCounpon in NewCounpon)
                    {                        
                        DataBase.CouponDAO.UpdateCounpon(tmpCounpon);
                    }

                    CouponManager.InsertNewCoupon(NewCounpon);
                }

                return new ReturnInfo
                {
                    ReturnCode = 0,
                    ReturnString = string.Format("Upload dữ liệu thành công")
                };
    }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("UploadCouponData", "", ex.Message);
            }

            return new ReturnInfo
            {
                ReturnCode = 255,
                ReturnString = "Có lỗi xảy ra, vui lòng kiểm tra lại dữ liệu"
            };             
        }

        public bool CreateCouponOtp(string ApiKey, string Counpon)
        {
            return CouponManager.CreateCounponOtp(Counpon);
        }

        public bool CheckCounponOtp(string ApiKey, string Counpon, string Otp, string DealerName)
        {
            if (ApiKey != ServerConfig.ServicesApiKey) return false;

            return CouponManager.CheckCounponOtp(Counpon, Otp, DealerName);
        }

        public List<DealerRedeemed> GetDealerRedeemedList(string ApiKey, string FromTime, string ToTime, Stream Data)
        {
            try
            {
                if (ApiKey != ServerConfig.ServicesApiKey) return new List<DealerRedeemed>();

                DateTime From = DateTime.ParseExact(FromTime, "ddMMyyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(ToTime, "ddMMyyyy", CultureInfo.InvariantCulture);

                var Dealer = NissanCouponLibrary.Utils.Rest.GetObjectFromStream<DealerInfo>(Data);

                if ((To - From).TotalDays >= 0)
                    return DataBase.CouponDAO.GetCouponByDealer(Dealer, From, To);
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("GetDealerRedeemedList", "", ex.Message);
            }

            return new List<DealerRedeemed>();
        }
        #endregion Coupon Process
    }
}
