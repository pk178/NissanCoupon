using NissanCoupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NissanCoupon.Controllers
{
    public class HomeController : Controller
    {
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Denied()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            user.Password = Utils.DES.Encrypt(user.Password);
            var result = Api.Login(user);
            if (result != null && result.Result == 0)
            {
                Session["Username"] = result.User.UserName;
                if (result.User.Permission != null)
                    Session["Permission"] = string.Join(",", result.User.Permission);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "TenDangNhapHoacMatKhauKhongDung" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}