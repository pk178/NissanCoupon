using log4net;
using Newtonsoft.Json;
using NissanCoupon.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NissanCoupon
{
    public class Api
    {
        private static readonly ILog _logger = LogManager.GetLogger("NissanCoupon");
        public static string BaseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
        public static string ApiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];

        public static LoginResult Login(UserLogin user)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(string.Format("/Login/{0}", ApiKey), Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(user);
                var response = client.Execute(request);
                return JsonConvert.DeserializeObject<LoginResult>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }

        public static bool UpdateUser(UserInfo user)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(string.Format("/UpdateUser/{0}", ApiKey), Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(user);
                var response = client.Execute(request);
                return bool.Parse(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public static List<UserInfo> GetAllUser()
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(string.Format("/GetAllUser/{0}", ApiKey), Method.GET);
                var response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<UserInfo>>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }

        public static CouponUploadResult UploadCouponData(List<CouponInfo> coupons)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(string.Format("/UploadCouponData/{0}", ApiKey), Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(coupons);
                var response = client.Execute(request);
                return JsonConvert.DeserializeObject<CouponUploadResult>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }

        public static List<CouponInfo> GetAllCoupon(string from, string to)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(string.Format("/GetAllCoupon/{0}/{1}/{2}", ApiKey, from, to), Method.GET);
                var response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<CouponInfo>>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }

        public static CouponInfo CheckCoupon(string coupon)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(string.Format("/CheckCoupon/{0}/{1}", coupon, ApiKey), Method.GET);
                var response = client.Execute(request);
                return JsonConvert.DeserializeObject<CouponInfo>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }

        public static bool CreateCouponOtp(string coupon)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(string.Format("/CreateCouponOtp/{1}/{0}", coupon, ApiKey), Method.GET);
                var response = client.Execute(request);
                return bool.Parse(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public static bool CheckCouponOtp(string coupon, string otp, string dealerName)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(string.Format("/CheckCounponOtp/{0}/{1}/{2}/{3}", ApiKey, coupon, otp, dealerName), Method.GET);
                var response = client.Execute(request);
                return bool.Parse(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public static List<DealerRedeemed> GetDealerRedeemedList(DealerInfo dealer, string from, string to)
        {
            try
            {
                var client = new RestClient(BaseUrl);
                var request = new RestRequest(string.Format("/GetDealerRedeemedList/{0}/{1}/{2}", ApiKey, from, to), Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(dealer);
                var response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<DealerRedeemed>>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }
    }
}