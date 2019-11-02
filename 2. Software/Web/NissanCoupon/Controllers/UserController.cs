using NissanCoupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NissanCoupon.Controllers
{
    public class UserController : Controller
    {
        [Authenticate(AuthCode.User)]
        public ActionResult Index()
        {
            return View();
        }

        [Authenticate(AuthCode.User)]
        public ActionResult GetDealers()
        {
            var dealers = new List<DealerInfo>
            {
                new DealerInfo{Region="Nationwide",Name="Nissan Viet Nam",Abbreviation="NVL",Type="NIL",Code="00"},
                new DealerInfo{Region="North",Name="Nissan Ha Dong",Abbreviation="NHD",Type="3S",Code="01"},
                new DealerInfo{Region="North",Name="Nissan Thang Long",Abbreviation="NTL",Type="3S",Code="02"},
                new DealerInfo{Region="North",Name="Nissan Long Bien",Abbreviation="NLB",Type="3S",Code="03"},
                new DealerInfo{Region="North",Name="Nissan Kinh Do",Abbreviation="NKD",Type="3S",Code="04"},
                new DealerInfo{Region="North",Name="Nissan Pham Van Dong",Abbreviation="NPVD",Type="3S",Code="05"},
                new DealerInfo{Region="North",Name="Nissan Le Van Luong",Abbreviation="NLVL",Type="1S",Code="06"},
                new DealerInfo{Region="North",Name="Nissan Bac Giang",Abbreviation="NBG",Type="3S",Code="07"},
                new DealerInfo{Region="North",Name="Nissan Lao Cai",Abbreviation="NLC",Type="3S",Code="08"},
                new DealerInfo{Region="North",Name="Nissan Ha Long",Abbreviation="NHL",Type="3S",Code="09"},
                new DealerInfo{Region="North",Name="Nissan Vinh Phuc",Abbreviation="NVP",Type="3S",Code="10"},
                new DealerInfo{Region="North",Name="Nissan Tay Ho",Abbreviation="NTO",Type="1S",Code="11"},
                new DealerInfo{Region="Central",Name="Nissan Thanh Hoa",Abbreviation="NTH",Type="3S",Code="12"},
                new DealerInfo{Region="Central",Name="Nissan Vinh",Abbreviation="NVI",Type="3S",Code="13"},
                new DealerInfo{Region="Central",Name="Nissan Dong Ha",Abbreviation="NDH",Type="3S",Code="14"},
                new DealerInfo{Region="Central",Name="Nissan Da Nang",Abbreviation="NDN",Type="3S",Code="15"},
                new DealerInfo{Region="Central",Name="Nissan Buon Ma Thuot",Abbreviation="NBMT",Type="3S",Code="16"},
                new DealerInfo{Region="Central",Name="Nissan Quy Nhon",Abbreviation="NQN",Type="3S",Code="17"},
                new DealerInfo{Region="Central",Name="Nissan Gia Lai",Abbreviation="NGL",Type="3S",Code="18"},
                new DealerInfo{Region="South",Name="Nissan Mien Tay",Abbreviation="NMT",Type="3S",Code="19"},
                new DealerInfo{Region="South",Name="Nissan Go Vap",Abbreviation="NGV",Type="3S",Code="20"},
                new DealerInfo{Region="South",Name="Nissan Sai Gon",Abbreviation="NSG",Type="3S",Code="21"},
                new DealerInfo{Region="South",Name="Nissan Bien Hoa",Abbreviation="NBH",Type="3S",Code="22"},
                new DealerInfo{Region="South",Name="Nissan Binh Duong",Abbreviation="NBD",Type="3S",Code="23"},
                new DealerInfo{Region="South",Name="Nissan Can Tho",Abbreviation="NCTH",Type="3S",Code="24"}
            };
            return Json(dealers, JsonRequestBehavior.AllowGet);
        }

        [Authenticate(AuthCode.User)]
        public ActionResult GetAll()
        {
            var users = Api.GetAllUser();
            users.ForEach(u => u.Password = Utils.DES.Decrypt(u.Password, true));
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [Authenticate(AuthCode.User)]
        [HttpPost]
        public ActionResult Update(UserInfo user)
        {
            user.Password = Utils.DES.Encrypt(user.Password);
            var result = Api.UpdateUser(user);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}