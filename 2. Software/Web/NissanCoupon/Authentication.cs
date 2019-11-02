using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NissanCoupon
{
    public enum AuthCode
    {
        User = 92,
        Coupon = 182,
    }

    public class AuthenticateAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly AuthCode? _authCode;

        public AuthenticateAttribute()
        {
        }

        public AuthenticateAttribute(AuthCode authCode)
        {
            _authCode = authCode;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var redirectPage = "/";
            var statusCode = HttpStatusCode.NotFound;
            if (HttpContext.Current.Session["Username"] == null)
            {
                redirectPage = "/Home/Login";
                statusCode = HttpStatusCode.Unauthorized;
            }
            else
            {
                if (HttpContext.Current.Session["Admin"] != null) return;
                if (_authCode == null) return;
                if (HttpContext.Current.Session["Permission"] != null && HttpContext.Current.Session["Permission"].ToString().Contains(((int)_authCode).ToString())) return;
                redirectPage = "/Home/Denied";
                statusCode = HttpStatusCode.Forbidden;
            }

            //if this is a AJAX call, don't redirect to login view, just return an Unauthorized code
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.Result = new HttpStatusCodeResult(statusCode);
            }
            else
            {
                //Don't execute current action by returning a RedirectResult to Login page
                var incommingUrl = HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString());

                //Don't use ReturnURL for POST request
                if (string.Equals("POST", HttpContext.Current.Request.RequestType, StringComparison.OrdinalIgnoreCase))
                    incommingUrl = string.Empty;

                var redirectUrl = string.Format("{0}?returnurl={1}", redirectPage, incommingUrl);
                filterContext.Result = new RedirectResult(redirectPage, false);
            }
        }
    }
}