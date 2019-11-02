using log4net;
using Newtonsoft.Json;
using NissanCoupon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NissanCoupon.Controllers
{
    public class CouponController : Controller
    {
        private readonly ILog _logger = LogManager.GetLogger("NissanCoupon");

        [Authenticate(AuthCode.Coupon)]
        public ActionResult Index()
        {
            return View();
        }

        [Authenticate(AuthCode.Coupon)]
        public ActionResult GetAll(string from, string to)
        {
            if (string.IsNullOrEmpty(from))
                from = "01/01/0001";
            if (string.IsNullOrEmpty(to))
                to = "01/01/9999";
            from = from.Replace("/", "");
            to = to.Replace("/", "");
            var coupons = Api.GetAllCoupon(from, to);
            return Json(coupons, JsonRequestBehavior.AllowGet);
        }

        [Authenticate(AuthCode.Coupon)]
        public ActionResult Create()
        {
            return View();
        }

        [Authenticate(AuthCode.Coupon)]
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            List<int> lineErrors = new List<int>();
            if (file.ContentLength > 0)
            {
                try
                {
                    List<CouponInfo> list = new List<CouponInfo>();
                    var fileName = Path.GetFileName(file.FileName);
                    var uploadFolder = Server.MapPath("~/Uploads");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);
                    var path = Path.Combine(uploadFolder, fileName);
                    file.SaveAs(path);
                    string connectionString = "";
                    if (fileName.EndsWith("xls"))
                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;IMEX=1;\"";
                    else if (fileName.EndsWith("xlsx"))
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;IMEX=1;\"";
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        var schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        var sheet1 = schema.Rows[0]["TABLE_NAME"].ToString();
                        OleDbCommand command = new OleDbCommand(string.Format("SELECT * FROM [{0}]", sheet1), connection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);
                        DataTable table = dataset.Tables[0];
                        for (int i = 2; i < table.Rows.Count; i++)
                        {
                            if (table.Rows[i] == null) continue;
                            try
                            {
                                var coupon = new CouponInfo
                                {
                                    Category = table.Rows[i][0].ToString(),
                                    Type = table.Rows[i][1].ToString(),
                                    PromotionDate = table.Rows[i][2].ToString(),
                                    DealerName = table.Rows[i][3].ToString(),
                                    CustomerNumber = Convert.ToInt32(table.Rows[i][4].ToString()),
                                    CustomerName = table.Rows[i][5].ToString(),
                                    PhoneNumber = table.Rows[i][6].ToString(),
                                    VehicleModel = table.Rows[i][7].ToString(),
                                    ChassisNumber = table.Rows[i][8].ToString(),
                                    LicensePlateNumber = table.Rows[i][9].ToString(),
                                    GiftCode = "",
                                    ExpriedDate = table.Rows[i][11].ToString(),
                                    ReminderDay = DateTime.ParseExact(table.Rows[i][11].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(int.Parse(table.Rows[i][12].ToString()) * -1).ToString("dd/MM/yyyy"),
                                    EntitledServiceGift = table.Rows[i][13].ToString(),
                                    RedeemedDate = "01/01/0001",
                                    RedeemedByDealer = "",
                                    CampaignName = table.Rows[i][16].ToString()
                                };
                                list.Add(coupon);
                            }
                            catch (Exception e)
                            {
                                lineErrors.Add(i + 2);
                            }
                        }
                        connection.Close();
                    }
                    if (lineErrors.Count > 0)
                    {
                        return Json(new CouponUploadResult
                        {
                            ReturnCode = -1,
                            ReturnString = "Dữ liệu tải lên bị lỗi (các dòng " + string.Join(", ", lineErrors) + "). Vui lòng kiểm tra lại thông tin."
                        }, JsonRequestBehavior.AllowGet);
                    }
                    //_logger.Info(JsonConvert.SerializeObject(list));
                    var result = Api.UploadCouponData(list);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
            return Json(new CouponUploadResult
            {
                ReturnCode = -1,
                ReturnString = "Dữ liệu tải lên bị lỗi (file không đúng định dạng). Vui lòng kiểm tra lại thông tin."
            }, JsonRequestBehavior.AllowGet);
        }

        [Authenticate]
        public ActionResult CheckCoupon(string coupon)
        {
            var result = Api.CheckCoupon(coupon);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authenticate]
        public ActionResult CreateCouponOtp(string coupon)
        {
            var result = Api.CreateCouponOtp(coupon);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authenticate]
        public ActionResult CheckCouponOtp(string coupon, string otp)
        {
            var dealer = Api.GetAllUser().FirstOrDefault(u => u.UserName == Session["Username"].ToString()).Dealer;
            var result = Api.CheckCouponOtp(coupon, otp, dealer.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authenticate]
        public ActionResult Redeemed()
        {
            return View();
        }

        [Authenticate]
        public ActionResult GetRedeemed(string from, string to)
        {
            if (string.IsNullOrEmpty(from))
                from = "01/01/0001";
            if (string.IsNullOrEmpty(to))
                to = "01/01/9999";
            from = from.Replace("/", "");
            to = to.Replace("/", "");
            var dealer = Api.GetAllUser().FirstOrDefault(u => u.UserName == Session["Username"].ToString()).Dealer;
            var coupons = Api.GetDealerRedeemedList(dealer, from, to);
            return Json(coupons, JsonRequestBehavior.AllowGet);
        }
    }
}