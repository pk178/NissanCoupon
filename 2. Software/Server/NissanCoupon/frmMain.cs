using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace NissanCoupon
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            Core.ServerManager.ServerInit();

            lbVersion.Text = "Phiên bản phần mềm : " + ServerConfig.Version;
            
        }

        private void TestSMS_Click(object sender, EventArgs e)
        {
            //var tmpUser = new NissanCouponLibrary.Entity.UserInfo();
            //tmpUser.CreatedDate = DateTime.Now;
            //tmpUser.Dealer = new NissanCouponLibrary.Entity.DealerInfo
            //{
            //    Abbreviation = string.Empty,
            //    Code = "00",
            //    Name = "Nationwide",
            //    Region = "",
            //    Type = ""
            //};
            //tmpUser.Email = "phamkhanh.hut@gmail.com";
            //tmpUser.Enable = true;
            //tmpUser.FullName = "Pham Duc Khanh";
            //tmpUser.Password = NissanCouponLibrary.Utils.DES.Encrypt("123@123a");
            //tmpUser.Permission = new List<int> { NissanCouponLibrary.Entity.UserInfo.PERMISSION_EDIT_COUPON, NissanCouponLibrary.Entity.UserInfo.PERMISSION_EDIT_USER };
            //tmpUser.PhoneNumber = "0356053721";
            //tmpUser.UserName = "khanhpd";

            //Core.DataBase.User.UpdateUser(tmpUser);

        }

        private void SystemInfoTick_Tick(object sender, EventArgs e)
        {
            if (Core.Services.Hosting.IsOK)
                lbServicesState.Text = "Trạng thái services : OK";
            else
                lbServicesState.Text = "Trạng thái services : Failure";

            lbTotalCoupon.Text = "Số khuyến mãi hiện tại : " + Core.CouponManager.CurrentCouponCount;
            lbCurrentOtpCount.Text = "Số OTP đang mở : " + Core.CouponManager.CurrentOtpCount;
        }

        private void btnLoadSMS_Click(object sender, EventArgs e)
        {
            var FailSMS = Core.DataBase.SMS.GetFailSMS(dtSMSFromDate.Value, dtSMSToDate.Value);

            txtFindSMSResult.Text = string.Format("Tìm thấy {0} SMS đã gửi thất bại từ ngày {1} đến ngày {2}", FailSMS.Count,
                dtSMSFromDate.Value.ToString("dd/MM/yyyy"), dtSMSToDate.Value.ToString("dd/MM/yyyy"));
        }

        private void btnResendSMS_Click(object sender, EventArgs e)
        {
            var FailSMS = Core.DataBase.SMS.GetFailSMS(dtSMSFromDate.Value, dtSMSToDate.Value);
            
            Array.ForEach<NissanCouponLibrary.Entity.SMSInfo>(FailSMS.ToArray(), x => Utils.SMS.SendSMS(x.SendTo, x.Message));

            MessageBox.Show("Đã gửi lại thành công", "Thông báo");
        }

        private void btnSendCancelCouponInfo_Click(object sender, EventArgs e)
        {
            //var AllCoupon = Core.DataBase.CouponDAO.GetAllCoupon(new DateTime(2000,1,1), DateTime.Now);
            //var CancelCoupon = AllCoupon.Where(x => x.CampaignName == "SSI-1907-03");
            //var KeepCoupon = AllCoupon.Where(x => x.CampaignName == "SSI-1907-02");

            //Dictionary<string, string> CancelSMS = new Dictionary<string, string>();

            //foreach(var tmpCancelCoupon in CancelCoupon)
            //{
            //    foreach(var tmpKeepCoupon in KeepCoupon)
            //    {
            //        if(tmpCancelCoupon.PhoneNumber == tmpKeepCoupon.PhoneNumber)
            //        {
            //            CancelSMS.Add(tmpKeepCoupon.PhoneNumber,
            //                string.Format("Do loi he thong, ma qua tang {0} khong con gia tri. Vui long su dung ma {1} de doi qua tai dai ly Nissan, HSD 11/02/2020 LH 18006883",
            //                tmpCancelCoupon.GiftCode, tmpKeepCoupon.GiftCode));
            //            break;
            //        }
            //    }
            //}

            //var DialogResult = MessageBox.Show(string.Format("Tìm thấy {0} mã khuyến mại để nhắn tin hủy, bạn có muốn tiếp tục không ?", CancelSMS.Count), "Thông báo", MessageBoxButtons.OKCancel);
            //if(DialogResult == DialogResult.OK)
            //{
            //    foreach(var tmpCancelSMS in CancelSMS)
            //    {
            //        Utils.SMS.SendSMS(tmpCancelSMS.Key, tmpCancelSMS.Value);
            //    }

            //}

            var AllCoupon = Core.DataBase.CouponDAO.GetAllCoupon(new DateTime(2000, 1, 1), DateTime.Now);

            Dictionary<string, string> CancelSMS = new Dictionary<string, string>();
            CancelSMS.Add("AS190723001925", "Do loi he thong, ma qua tang AS190723001925 khong con gia tri. Vui long su dung ma AS190823001925 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190723002947", "Do loi he thong, ma qua tang AS190723002947 khong con gia tri. Vui long su dung ma AS190823002947 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190723003182", "Do loi he thong, ma qua tang AS190723003182 khong con gia tri. Vui long su dung ma AS190823003182 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190723004898", "Do loi he thong, ma qua tang AS190723004898 khong con gia tri. Vui long su dung ma AS190823004898 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190723005671", "Do loi he thong, ma qua tang AS190723005671 khong con gia tri. Vui long su dung ma AS190823005671 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190723006379", "Do loi he thong, ma qua tang AS190723006379 khong con gia tri. Vui long su dung ma AS190823006379 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190707007303", "Do loi he thong, ma qua tang AS190707007303 khong con gia tri. Vui long su dung ma AS190807009303 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190707008138", "Do loi he thong, ma qua tang AS190707008138 khong con gia tri. Vui long su dung ma AS190807010138 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190707009383", "Do loi he thong, ma qua tang AS190707009383 khong con gia tri. Vui long su dung ma AS190807011383 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190722010010", "Do loi he thong, ma qua tang AS190722010010 khong con gia tri. Vui long su dung ma AS190822013010 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190722011218", "Do loi he thong, ma qua tang AS190722011218 khong con gia tri. Vui long su dung ma AS190822014218 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190722012072", "Do loi he thong, ma qua tang AS190722012072 khong con gia tri. Vui long su dung ma AS190822015072 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190722013308", "Do loi he thong, ma qua tang AS190722013308 khong con gia tri. Vui long su dung ma AS190822016308 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190716014830", "Do loi he thong, ma qua tang AS190716014830 khong con gia tri. Vui long su dung ma AS190816018830 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190716015700", "Do loi he thong, ma qua tang AS190716015700 khong con gia tri. Vui long su dung ma AS190816019700 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190716016879", "Do loi he thong, ma qua tang AS190716016879 khong con gia tri. Vui long su dung ma AS190816020879 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190716017700", "Do loi he thong, ma qua tang AS190716017700 khong con gia tri. Vui long su dung ma AS190816021700 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190716018171", "Do loi he thong, ma qua tang AS190716018171 khong con gia tri. Vui long su dung ma AS190816022171 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190716019152", "Do loi he thong, ma qua tang AS190716019152 khong con gia tri. Vui long su dung ma AS190816023152 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190716020729", "Do loi he thong, ma qua tang AS190716020729 khong con gia tri. Vui long su dung ma AS190816024729 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190714021789", "Do loi he thong, ma qua tang AS190714021789 khong con gia tri. Vui long su dung ma AS190814029789 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190714022398", "Do loi he thong, ma qua tang AS190714022398 khong con gia tri. Vui long su dung ma AS190814030398 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190714023345", "Do loi he thong, ma qua tang AS190714023345 khong con gia tri. Vui long su dung ma AS190814031345 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190718024677", "Do loi he thong, ma qua tang AS190718024677 khong con gia tri. Vui long su dung ma AS190818033677 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190718025129", "Do loi he thong, ma qua tang AS190718025129 khong con gia tri. Vui long su dung ma AS190818034129 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190720026747", "Do loi he thong, ma qua tang AS190720026747 khong con gia tri. Vui long su dung ma AS190820035747 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190720027437", "Do loi he thong, ma qua tang AS190720027437 khong con gia tri. Vui long su dung ma AS190820036437 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190720028883", "Do loi he thong, ma qua tang AS190720028883 khong con gia tri. Vui long su dung ma AS190820037883 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190720029474", "Do loi he thong, ma qua tang AS190720029474 khong con gia tri. Vui long su dung ma AS190820038474 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190701030886", "Do loi he thong, ma qua tang AS190701030886 khong con gia tri. Vui long su dung ma AS190801041886 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190701031996", "Do loi he thong, ma qua tang AS190701031996 khong con gia tri. Vui long su dung ma AS190801042996 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190701032676", "Do loi he thong, ma qua tang AS190701032676 khong con gia tri. Vui long su dung ma AS190801043676 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190701033812", "Do loi he thong, ma qua tang AS190701033812 khong con gia tri. Vui long su dung ma AS190801044812 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190701034899", "Do loi he thong, ma qua tang AS190701034899 khong con gia tri. Vui long su dung ma AS190801045899 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190701035686", "Do loi he thong, ma qua tang AS190701035686 khong con gia tri. Vui long su dung ma AS190801046686 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190701036968", "Do loi he thong, ma qua tang AS190701036968 khong con gia tri. Vui long su dung ma AS190801047968 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190701037999", "Do loi he thong, ma qua tang AS190701037999 khong con gia tri. Vui long su dung ma AS190801048999 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190709038862", "Do loi he thong, ma qua tang AS190709038862 khong con gia tri. Vui long su dung ma AS190809051862 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190709039186", "Do loi he thong, ma qua tang AS190709039186 khong con gia tri. Vui long su dung ma AS190809052186 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190804056494", "Do loi he thong, ma qua tang AS190804056494 khong con gia tri. Vui long su dung ma AS190704040494 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190704041020", "Do loi he thong, ma qua tang AS190704041020 khong con gia tri. Vui long su dung ma AS190804057020 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190704042594", "Do loi he thong, ma qua tang AS190704042594 khong con gia tri. Vui long su dung ma AS190804058594 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190704043282", "Do loi he thong, ma qua tang AS190704043282 khong con gia tri. Vui long su dung ma AS190804059282 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190704044332", "Do loi he thong, ma qua tang AS190704044332 khong con gia tri. Vui long su dung ma AS190804060332 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190704045225", "Do loi he thong, ma qua tang AS190704045225 khong con gia tri. Vui long su dung ma AS190804061225 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190704046675", "Do loi he thong, ma qua tang AS190704046675 khong con gia tri. Vui long su dung ma AS190804062675 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190704047658", "Do loi he thong, ma qua tang AS190704047658 khong con gia tri. Vui long su dung ma AS190804063658 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703048684", "Do loi he thong, ma qua tang AS190703048684 khong con gia tri. Vui long su dung ma AS190803069684 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703049411", "Do loi he thong, ma qua tang AS190703049411 khong con gia tri. Vui long su dung ma AS190803070411 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703050007", "Do loi he thong, ma qua tang AS190703050007 khong con gia tri. Vui long su dung ma AS190803071007 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703051058", "Do loi he thong, ma qua tang AS190703051058 khong con gia tri. Vui long su dung ma AS190803072058 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190803073168", "Do loi he thong, ma qua tang AS190803073168 khong con gia tri. Vui long su dung ma AS190703052168 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703053430", "Do loi he thong, ma qua tang AS190703053430 khong con gia tri. Vui long su dung ma AS190803074430 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703054618", "Do loi he thong, ma qua tang AS190703054618 khong con gia tri. Vui long su dung ma AS190803075618 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703055118", "Do loi he thong, ma qua tang AS190703055118 khong con gia tri. Vui long su dung ma AS190803076118 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703056868", "Do loi he thong, ma qua tang AS190703056868 khong con gia tri. Vui long su dung ma AS190803077868 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703057560", "Do loi he thong, ma qua tang AS190703057560 khong con gia tri. Vui long su dung ma AS190803078560 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703058680", "Do loi he thong, ma qua tang AS190703058680 khong con gia tri. Vui long su dung ma AS190803079680 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703059692", "Do loi he thong, ma qua tang AS190703059692 khong con gia tri. Vui long su dung ma AS190803080692 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703060696", "Do loi he thong, ma qua tang AS190703060696 khong con gia tri. Vui long su dung ma AS190803081696 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190703061877", "Do loi he thong, ma qua tang AS190703061877 khong con gia tri. Vui long su dung ma AS190803082877 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190708062889", "Do loi he thong, ma qua tang AS190708062889 khong con gia tri. Vui long su dung ma AS190808093889 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190708063346", "Do loi he thong, ma qua tang AS190708063346 khong con gia tri. Vui long su dung ma AS190808094346 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190708064515", "Do loi he thong, ma qua tang AS190708064515 khong con gia tri. Vui long su dung ma AS190808095515 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190708065608", "Do loi he thong, ma qua tang AS190708065608 khong con gia tri. Vui long su dung ma AS190808096608 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190719066938", "Do loi he thong, ma qua tang AS190719066938 khong con gia tri. Vui long su dung ma AS190819100938 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190719067964", "Do loi he thong, ma qua tang AS190719067964 khong con gia tri. Vui long su dung ma AS190819101964 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190719068992", "Do loi he thong, ma qua tang AS190719068992 khong con gia tri. Vui long su dung ma AS190819102992 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190719069222", "Do loi he thong, ma qua tang AS190719069222 khong con gia tri. Vui long su dung ma AS190819103222 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190719070450", "Do loi he thong, ma qua tang AS190719070450 khong con gia tri. Vui long su dung ma AS190819104450 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190719071138", "Do loi he thong, ma qua tang AS190719071138 khong con gia tri. Vui long su dung ma AS190819105138 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190719072028", "Do loi he thong, ma qua tang AS190719072028 khong con gia tri. Vui long su dung ma AS190819106028 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190705073105", "Do loi he thong, ma qua tang AS190705073105 khong con gia tri. Vui long su dung ma AS190805112105 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190705074333", "Do loi he thong, ma qua tang AS190705074333 khong con gia tri. Vui long su dung ma AS190805113333 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190705075737", "Do loi he thong, ma qua tang AS190705075737 khong con gia tri. Vui long su dung ma AS190805114737 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190705076168", "Do loi he thong, ma qua tang AS190705076168 khong con gia tri. Vui long su dung ma AS190805115168 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190705077998", "Do loi he thong, ma qua tang AS190705077998 khong con gia tri. Vui long su dung ma AS190805116998 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190705078855", "Do loi he thong, ma qua tang AS190705078855 khong con gia tri. Vui long su dung ma AS190805117855 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190717079334", "Do loi he thong, ma qua tang AS190717079334 khong con gia tri. Vui long su dung ma AS190817122334 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190717080709", "Do loi he thong, ma qua tang AS190717080709 khong con gia tri. Vui long su dung ma AS190817123709 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190721081520", "Do loi he thong, ma qua tang AS190721081520 khong con gia tri. Vui long su dung ma AS190821125520 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190721082705", "Do loi he thong, ma qua tang AS190721082705 khong con gia tri. Vui long su dung ma AS190821126705 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190721083518", "Do loi he thong, ma qua tang AS190721083518 khong con gia tri. Vui long su dung ma AS190821127518 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190721084818", "Do loi he thong, ma qua tang AS190721084818 khong con gia tri. Vui long su dung ma AS190821128818 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190721085823", "Do loi he thong, ma qua tang AS190721085823 khong con gia tri. Vui long su dung ma AS190821129823 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190712086039", "Do loi he thong, ma qua tang AS190712086039 khong con gia tri. Vui long su dung ma AS190812135039 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190712087190", "Do loi he thong, ma qua tang AS190712087190 khong con gia tri. Vui long su dung ma AS190812136190 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190712088218", "Do loi he thong, ma qua tang AS190712088218 khong con gia tri. Vui long su dung ma AS190812137218 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702089181", "Do loi he thong, ma qua tang AS190702089181 khong con gia tri. Vui long su dung ma AS190802139181 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702090068", "Do loi he thong, ma qua tang AS190702090068 khong con gia tri. Vui long su dung ma AS190802140068 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702091177", "Do loi he thong, ma qua tang AS190702091177 khong con gia tri. Vui long su dung ma AS190802141177 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702092658", "Do loi he thong, ma qua tang AS190702092658 khong con gia tri. Vui long su dung ma AS190802142658 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702093971", "Do loi he thong, ma qua tang AS190702093971 khong con gia tri. Vui long su dung ma AS190802143971 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702094314", "Do loi he thong, ma qua tang AS190702094314 khong con gia tri. Vui long su dung ma AS190802144314 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702095006", "Do loi he thong, ma qua tang AS190702095006 khong con gia tri. Vui long su dung ma AS190802145006 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702097139", "Do loi he thong, ma qua tang AS190702097139 khong con gia tri. Vui long su dung ma AS190802147139 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702098090", "Do loi he thong, ma qua tang AS190702098090 khong con gia tri. Vui long su dung ma AS190802148090 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190702099662", "Do loi he thong, ma qua tang AS190702099662 khong con gia tri. Vui long su dung ma AS190802149662 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190713100594", "Do loi he thong, ma qua tang AS190713100594 khong con gia tri. Vui long su dung ma AS190813156594 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190713101962", "Do loi he thong, ma qua tang AS190713101962 khong con gia tri. Vui long su dung ma AS190813157962 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190713102564", "Do loi he thong, ma qua tang AS190713102564 khong con gia tri. Vui long su dung ma AS190813158564 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190713103627", "Do loi he thong, ma qua tang AS190713103627 khong con gia tri. Vui long su dung ma AS190813159627 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190713104875", "Do loi he thong, ma qua tang AS190713104875 khong con gia tri. Vui long su dung ma AS190813160875 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190713105133", "Do loi he thong, ma qua tang AS190713105133 khong con gia tri. Vui long su dung ma AS190813161133 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190713106111", "Do loi he thong, ma qua tang AS190713106111 khong con gia tri. Vui long su dung ma AS190813162111 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190713107199", "Do loi he thong, ma qua tang AS190713107199 khong con gia tri. Vui long su dung ma AS190813163199 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190710108999", "Do loi he thong, ma qua tang AS190710108999 khong con gia tri. Vui long su dung ma AS190810171999 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190710109777", "Do loi he thong, ma qua tang AS190710109777 khong con gia tri. Vui long su dung ma AS190810172777 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190715110941", "Do loi he thong, ma qua tang AS190715110941 khong con gia tri. Vui long su dung ma AS190815176941 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190715111200", "Do loi he thong, ma qua tang AS190715111200 khong con gia tri. Vui long su dung ma AS190815177200 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190715112954", "Do loi he thong, ma qua tang AS190715112954 khong con gia tri. Vui long su dung ma AS190815178954 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190715113001", "Do loi he thong, ma qua tang AS190715113001 khong con gia tri. Vui long su dung ma AS190815179001 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190715114068", "Do loi he thong, ma qua tang AS190715114068 khong con gia tri. Vui long su dung ma AS190815180068 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190724115165", "Do loi he thong, ma qua tang AS190724115165 khong con gia tri. Vui long su dung ma AS190824186165 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");
            CancelSMS.Add("AS190724116348", "Do loi he thong, ma qua tang AS190724116348 khong con gia tri. Vui long su dung ma AS190824187348 de doi qua tai dai ly Nissan, HSD 25/01/2020 LH 18006883");


            var DialogResult = MessageBox.Show(string.Format("Bạn có muốn nhắn tin hủy tới {0} số điện thoại không ?", CancelSMS.Count), "Thông báo", MessageBoxButtons.OKCancel);
            if (DialogResult != DialogResult.OK)
            {
                return;
            }

            foreach (var tmpCancelSMS in CancelSMS)
            {
                foreach(var tmpCoupon in AllCoupon)
                {
                    if(tmpCoupon.GiftCode == tmpCancelSMS.Key)
                    {
                        Utils.SMS.SendSMS(tmpCoupon.PhoneNumber, tmpCancelSMS.Value);
                        tmpCoupon.RedeemedByDealer = "Canceled";
                        Core.DataBase.CouponDAO.UpdateCounpon(tmpCoupon);
                        break;
                    }
                }
                //
            }

            MessageBox.Show("Đã gửi xong nhé", "Thông báo");
        }

        private void btnChangeCustomerName_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> ChangeCustomerName = new Dictionary<string, string>();
            ChangeCustomerName.Add("AS190823007709", "Cty Tnhh Mtv Thư Thùy");
            ChangeCustomerName.Add("AS190807009303", "Trần Đoàn  Nam");
            ChangeCustomerName.Add("AS190807010138", "Phạm Quang Hải");
            ChangeCustomerName.Add("AS190807011383", "Vũ Nguyễn Bình");
            ChangeCustomerName.Add("AS190807012166", "Nhữ Huy Thanh");
            ChangeCustomerName.Add("AS190822013010", "Công Ty TNHH MTV Thương Mại Dịch Vụ Sản Xuất Hoàng Khoa");
            ChangeCustomerName.Add("AS190822014218", "Nguyễn Công Dực");
            ChangeCustomerName.Add("AS190822015072", "Lưu Xuân Nghiêm");
            ChangeCustomerName.Add("AS190822016308", "Nguyễn Khánh Hòa");
            ChangeCustomerName.Add("AS190822017126", "CHI NHÁNH CTY TNHH TM DV XNK BÙI NGUYỄN GIA PHÁT");
            ChangeCustomerName.Add("AS190816020879", "Anh Thụy");
            ChangeCustomerName.Add("AS190816021700", "Trần Văn Mạnh");
            ChangeCustomerName.Add("AS190816022171", "Phạm Văn Tiến");
            ChangeCustomerName.Add("AS190816023152", "Anh Thi");
            ChangeCustomerName.Add("AS190816024729", "Nguyễn Trình Chính ");
            ChangeCustomerName.Add("AS190816025409", "Châu Đương");
            ChangeCustomerName.Add("AS190816027454", "Lưu Anh Dũng");
            ChangeCustomerName.Add("AS190814029789", "Hồ Thị Phi / Anh Hòa");
            ChangeCustomerName.Add("AS190814030398", "Thân Trọng Phương");
            ChangeCustomerName.Add("AS190814031345", "CTY TNHH MTV Hùng Hoa Huy Hoàng");
            ChangeCustomerName.Add("AS190814032099", "Công ty TNHH Hoàng Hà BQC (Hoàng Hà)");
            ChangeCustomerName.Add("AS190818034129", "Anh Thọ");
            ChangeCustomerName.Add("AS190820035747", "Phạm Quang Hiếu");
            ChangeCustomerName.Add("AS190820036437", "NGUYỄN MẬU HUÂN");
            ChangeCustomerName.Add("AS190820037883", "Nguyễn Tài Đức");
            ChangeCustomerName.Add("AS190820038474", "Phạm Thanh Long");
            ChangeCustomerName.Add("AS190820039897", "TRẦN THANH TRUYỀN ");
            ChangeCustomerName.Add("AS190801041886", "Nguyễn Văn Tiến");
            ChangeCustomerName.Add("AS190801043676", "Ngô Xuân Thêm");
            ChangeCustomerName.Add("AS190801044812", "Nguyễn Văn Nam");
            ChangeCustomerName.Add("AS190801045899", "Nguyễn Hữu Việt");
            ChangeCustomerName.Add("AS190801047968", "Trần Việt Hà");
            ChangeCustomerName.Add("AS190801049393", "Phạm Tuấn Anh");
            ChangeCustomerName.Add("AS190801050799", "Trịnh Nhân Nghĩa");
            ChangeCustomerName.Add("AS190809051862", "Bùi Thị Nhuần");
            ChangeCustomerName.Add("AS190809052186", "Phạm Việt Cường");
            ChangeCustomerName.Add("AS190809053258", "Trần Thế Trung");
            ChangeCustomerName.Add("AS190809055312", "Bùi Ngọc Đức");
            ChangeCustomerName.Add("AS190804057020", "Hà Thế Mạnh");
            ChangeCustomerName.Add("AS190804058594", "Nguyễn Mạnh Hùng");
            ChangeCustomerName.Add("AS190804059282", "Nguyễn Minh Tân");
            ChangeCustomerName.Add("AS190804060332", "Lê Đức Anh");
            ChangeCustomerName.Add("AS190804061225", "Nguyễn Mạnh Tùng");
            ChangeCustomerName.Add("AS190804062675", "Lê Minh Giang");
            ChangeCustomerName.Add("AS190804063658", "Cty TNHH MTV Vận tải Hàng Hóa Đường Sắt");
            ChangeCustomerName.Add("AS190804064555", "BQL Bình Đẳng Giới Tính Lào Cai");
            ChangeCustomerName.Add("AS190804065424", "Cty TNHH Đầu Tư Phát Triển Tùng Bách");
            ChangeCustomerName.Add("AS190804066888", "Trần Văn Đạt");
            ChangeCustomerName.Add("AS190804067963", "Cty CP Dược Phẩm Gia Nguyễn");
            ChangeCustomerName.Add("AS190804068389", "Hoa Anh Tuấn");
            ChangeCustomerName.Add("AS190803069684", "HOÀNG VĂN LƯU");
            ChangeCustomerName.Add("AS190803071007", "Đỗ An Toàn");
            ChangeCustomerName.Add("AS190803072058", "CHU HỮU THẮNG");
            ChangeCustomerName.Add("AS190803074430", "Nguyễn Quang Tuyến");
            ChangeCustomerName.Add("AS190803075618", "Phạm Tiến Thuấn");
            ChangeCustomerName.Add("AS190803076118", "Đinh Văn Hùng");
            ChangeCustomerName.Add("AS190803077868", "Nguyễn Xuân Đức");
            ChangeCustomerName.Add("AS190803078560", "Mai Thanh Sơn");
            ChangeCustomerName.Add("AS190803080692", "Đồng Văn Lương");
            ChangeCustomerName.Add("AS190803081696", "Trần Văn Hải");
            ChangeCustomerName.Add("AS190803082877", "Lê Xuân Nghĩa");
            ChangeCustomerName.Add("AS190803083913", "VŨ VĂN KHOA");
            ChangeCustomerName.Add("AS190803084474", "Nguyễn Tuấn Tú");
            ChangeCustomerName.Add("AS190803085623", "A Hoạt");
            ChangeCustomerName.Add("AS190803086959", "Hoàng Lê Huy");
            ChangeCustomerName.Add("AS190803087358", "Nguyễn Đình Duy");
            ChangeCustomerName.Add("AS190803088772", "Nguyễn Văn Phi");
            ChangeCustomerName.Add("AS190803089348", "Nguyễn Công Sinh");
            ChangeCustomerName.Add("AS190803090888", "NGUYỄN QUỐC VIỆT");
            ChangeCustomerName.Add("AS190803091275", "Lê Tuấn Anh");
            ChangeCustomerName.Add("AS190808093889", "Trần Việt Dũng");
            ChangeCustomerName.Add("AS190808094346", "Lưu Đức Vương");
            ChangeCustomerName.Add("AS190808097166", "Viettel Lào Cai");
            ChangeCustomerName.Add("AS190808098333", "Phạm Quốc Huy");
            ChangeCustomerName.Add("AS190808099699", "Hoàng Thị Thanh");
            ChangeCustomerName.Add("AS190819100938", "PHAN VĂN HÙNG");
            ChangeCustomerName.Add("AS190819101964", "Trần Ngọc Lân");
            ChangeCustomerName.Add("AS190819102992", "Công ty Cổ phần Tô Thành Phát");
            ChangeCustomerName.Add("AS190819104450", "HUỲNH SƠN TRUNG");
            ChangeCustomerName.Add("AS190819105138", "Nguyễn Thị Kim Hồng");
            ChangeCustomerName.Add("AS190819106028", "Công Ty Cổ Phần Kỹ Thuật Ánh Minh");
            ChangeCustomerName.Add("AS190819107824", "CÔNG TY TNHH QUỐC TẾ THỦY ĐỘNG LỰC");
            ChangeCustomerName.Add("AS190819108772", "CÔNG TY TNHH THƯƠNG MẠI THẠCH LAM");
            ChangeCustomerName.Add("AS190819109118", "Lê Doãn Tú");
            ChangeCustomerName.Add("AS190819110238", "Công ty TNHH Một Thành Viên Quốc Phát");
            ChangeCustomerName.Add("AS190819111676", "LÊ VĂN MẠNH");
            ChangeCustomerName.Add("AS190805113333", "PHAN HỮU QUỲNH");
            ChangeCustomerName.Add("AS190805114737", "NGUYỄN BẨY");
            ChangeCustomerName.Add("AS190805115168", "NGÔ VĂN THỌ");
            ChangeCustomerName.Add("AS190805116998", "TRẦN ANH TUẤN");
            ChangeCustomerName.Add("AS190805117855", "NGUYỄN TUẤN ANH");
            ChangeCustomerName.Add("AS190805118828", "PHÙNG MINH HƯNG");
            ChangeCustomerName.Add("AS190805119521", "Nguyễn Văn Tuấn");
            ChangeCustomerName.Add("AS190805120649", "THÁI BÁ DIỆP");
            ChangeCustomerName.Add("AS190817122334", "Lê Phúc Nguyên");
            ChangeCustomerName.Add("AS190817123709", "Lê Văn Huy");
            ChangeCustomerName.Add("AS190817124995", "Công ty TNHH Thanh Thảnh");
            ChangeCustomerName.Add("AS190821125520", "CÔNG TY TNHH NGÂN ANH");
            ChangeCustomerName.Add("AS190821127518", "Công ty TNHH Dược Phẩm Tiền Giang");
            ChangeCustomerName.Add("AS190821128818", "Mai Văn Thư");
            ChangeCustomerName.Add("AS190821129823", "Công Ty TNHH Thương Mại Dịch Vụ Phú Thái");
            ChangeCustomerName.Add("AS190821130236", "Võ Thanh Tâm");
            ChangeCustomerName.Add("AS190821131166", "Lưu Tuấn Anh");
            ChangeCustomerName.Add("AS190821132279", "Lê Thị Khánh Chi");
            ChangeCustomerName.Add("AS190821133160", "Đinh Đức Thủy");
            ChangeCustomerName.Add("AS190821134466", "Công Ty TNHH Tm - SX Két Sắt Hồ Gia");
            ChangeCustomerName.Add("AS190812135039", "Nguyễn Trọng Dũng");
            ChangeCustomerName.Add("AS190812137218", "Lê Văn Hoàn");
            ChangeCustomerName.Add("AS190812138373", "NGUYỄN TUẤN ANH");
            ChangeCustomerName.Add("AS190802139181", "Phạm Văn Cao");
            ChangeCustomerName.Add("AS190802140068", "Phạm Đức Ngọc");
            ChangeCustomerName.Add("AS190802141177", "Trịnh Văn Thụy");
            ChangeCustomerName.Add("AS190802142658", "Lê Sỹ Vinh");
            ChangeCustomerName.Add("AS190802143971", "Trương Minh Thiết");
            ChangeCustomerName.Add("AS190802144314", "Cao Hữu Tình");
            ChangeCustomerName.Add("AS190802145006", "Trần Minh Ngọc ");
            ChangeCustomerName.Add("AS190802146526", "Nguyễn Hữu Dũng");
            ChangeCustomerName.Add("AS190802147139", "Nguyễn Văn Nghĩa ");
            ChangeCustomerName.Add("AS190802148090", "Nguyễn Mạnh Hùng");
            ChangeCustomerName.Add("AS190802149662", "Vũ Văn Thắng");
            ChangeCustomerName.Add("AS190802150699", "Nguyễn Văn Xuyên");
            ChangeCustomerName.Add("AS190802151102", "Hồ Minh Hân");
            ChangeCustomerName.Add("AS190802153868", "Nguyễn Viết Mạnh");
            ChangeCustomerName.Add("AS190802154230", "Đinh Văn Thế");
            ChangeCustomerName.Add("AS190802155708", "Trần Hoàng Anh");
            ChangeCustomerName.Add("AS190813156594", "Nguyễn Đức Dũng");
            ChangeCustomerName.Add("AS190813157962", "Nguyễn Xuân Thái");
            ChangeCustomerName.Add("AS190813158564", "Lê Văn Vỹ");
            ChangeCustomerName.Add("AS190813159627", "Hồ Sỹ Thọ");
            ChangeCustomerName.Add("AS190813161133", "Nguyễn Văn Sơn");
            ChangeCustomerName.Add("AS190813162111", "Trương Nam Nhân");
            ChangeCustomerName.Add("AS190813164149", "Hồ Đức Vinh");
            ChangeCustomerName.Add("AS190813165999", "Nguyễn Cảnh Phong");
            ChangeCustomerName.Add("AS190813166506", "Trần Thị Hường");
            ChangeCustomerName.Add("AS190813167322", "Võ Văn Tú");
            ChangeCustomerName.Add("AS190813168234", "Nguyễn Văn Lệnh");
            ChangeCustomerName.Add("AS190813169666", "Nguyễn Duy Dũng");
            ChangeCustomerName.Add("AS190813170636", "Võ Văn Quyền");
            ChangeCustomerName.Add("AS190810171999", "NGUYỄN MINH ĐỨC");
            ChangeCustomerName.Add("AS190810172777", "Hà Đức Trọng");
            ChangeCustomerName.Add("AS190810173523", "Lưu Thế Trung");
            ChangeCustomerName.Add("AS190810174156", "NGUYỄN CÔNG HẢI");
            ChangeCustomerName.Add("AS190810175129", "Chị vượng");
            ChangeCustomerName.Add("AS190815176941", "TRẦN VĂN THÀNH");
            ChangeCustomerName.Add("AS190815177200", "MR. BỬU");
            ChangeCustomerName.Add("AS190815178954", "MR. PHÚC");
            ChangeCustomerName.Add("AS190815180068", "MR. PHƯỚC");
            ChangeCustomerName.Add("AS190815181725", "NGUYỄN ĐÔNG");
            ChangeCustomerName.Add("AS190815182145", "MR. KHIÊM");
            ChangeCustomerName.Add("AS190815183096", "NGUYỄN QUỐC HUY");
            ChangeCustomerName.Add("AS190815185236", "NGUYỄN TRƯỜNG TRANG");
            ChangeCustomerName.Add("AS190824187348", "Trần Hoàng Nhi");
            ChangeCustomerName.Add("AS190824189209", "Lê Hoàng Rang");
            ChangeCustomerName.Add("AS190824190168", "Lương Thị Thu Mai");

            List<NissanCouponLibrary.Entity.CouponInfo> UpdateCoupon = new List<NissanCouponLibrary.Entity.CouponInfo>();
            foreach (var tmpChanging in ChangeCustomerName)
            {
                var tmpCoupon = Core.DataBase.CouponDAO.CheckCoupon(tmpChanging.Key);
                
                if(tmpCoupon != null)
                {
                    tmpCoupon.CustomerName = tmpChanging.Value;
                    UpdateCoupon.Add(tmpCoupon);                    
                }                
            }

            if (UpdateCoupon.Count > 0)
            {
                var Confirm = MessageBox.Show(string.Format("Bạn có muốn update cho {0} khách hàng không ?", UpdateCoupon.Count), "Xác nhận", MessageBoxButtons.YesNo);
                if (Confirm == DialogResult.Yes)
                {
                    UpdateCoupon.ForEach(x => Core.DataBase.CouponDAO.UpdateCounpon(x));

                    MessageBox.Show("Cập nhật thành công", "Thông báo");
                }
            }
        }
    }
}
