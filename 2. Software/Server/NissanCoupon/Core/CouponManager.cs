using NissanCouponLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core
{
    public class CouponManager
    {
        private static List<CouponInfo> lstCurrentCoupon = new List<CouponInfo>();
        private static List<CounponOtp> lstCounponOtp = new List<CounponOtp>();
        private static Dictionary<int, string> dicDealer = new Dictionary<int, string>();
        private static DateTime LastReminDate = new DateTime(2000, 1, 1);

        public static int CurrentCouponCount
        {
            get
            {
                return lstCurrentCoupon.Count;
            }
        }

        public static int CurrentOtpCount
        {
            get
            {
                return lstCounponOtp.Count;
            }
        }

        public static void InitCouponManager()
        {
            UpdateCoupon();

            (new Task(() => CouponTick())).Start();
        }

        public static bool InsertNewCoupon(List<CouponInfo> NewCouponList)
        {
            try
            {
                lstCurrentCoupon.AddRange(NewCouponList);
                string GiftCode = string.Empty;

                foreach(var tmpCounpon in NewCouponList)
                {
                    string Message = string.Empty;

                    if (tmpCounpon.Category.ToLower().Contains("sales"))
                    {
                        if(tmpCounpon.Type.ToLower().Contains("survey"))
                        {
                            Message = string.Format("Cam on QK da tra loi khao sat cua Nissan. Ma qua tang {0}. Doi qua tai tat ca cac Dai ly Nissan, HSD {1}. LH 18006883",
                                tmpCounpon.GiftCode, tmpCounpon.ExpriedDate.ToString("dd/MM/yyyy"));
                        }

                        if (tmpCounpon.Type.ToLower().Contains("promotion"))
                        {
                            Message = string.Format("Tri an KH: Nissan gui tang QK ma KM dich vu {0} su dung cho xe {1} tai tat ca cac Dai ly 3S Nissan, HSD {2}. LH 18006883",
                                tmpCounpon.GiftCode, tmpCounpon.LicensePlateNumber, tmpCounpon.ExpriedDate.ToString("dd/MM/yyyy"));
                        }
                    }

                    if (tmpCounpon.Category.ToLower().Contains("service"))
                    {
                        if (tmpCounpon.Type.ToLower().Contains("survey"))
                        {
                            Message = string.Format("Cam on QK da tra loi khao sat cua Nissan. Ma KM dich vu {0} su dung cho xe {1} tai tat ca cac Dai ly 3S Nissan, HSD {2}. LH 18006883",
                                tmpCounpon.GiftCode, tmpCounpon.LicensePlateNumber, tmpCounpon.ExpriedDate.ToString("dd/MM/yyyy"));
                        }

                        if (tmpCounpon.Type.ToLower().Contains("promotion"))
                        {
                            Message = string.Format("Tri an KH: Nissan gui tang QK ma KM dich vu {0} su dung cho xe {1} tai tat ca cac Dai ly 3S Nissan, HSD {2}. LH 18006883",
                                tmpCounpon.GiftCode, tmpCounpon.LicensePlateNumber, tmpCounpon.ExpriedDate.ToString("dd/MM/yyyy"));
                        }
                    }

                    NissanCouponLibrary.Utils.Log.LogEvent("InsertNewCoupon", string.Format("Giftcode : {0}, BKS : {1}, Phone : {2}", 
                        GiftCode, tmpCounpon.LicensePlateNumber, tmpCounpon.PhoneNumber));

                    if (!string.IsNullOrEmpty(Message))
                    {
                        Utils.SMS.SendSMS(tmpCounpon.PhoneNumber, Message);
                    }
                    
                }

                return true;
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("InsertNewCoupon", "", ex.Message);
            }

            return false;
        }

        private static void UpdateCoupon()
        {
            try
            {
                lstCurrentCoupon = DataBase.CouponDAO.GetAllCoupon(new DateTime(2000,1,1), DateTime.Now).Where(x => x.IsValid().ReturnCode == 0).ToList();

                for (int i = lstCounponOtp.Count - 1; i > 0; i--)
                {
                    if (DateTime.Now.Day != lstCounponOtp[i].CreateTime.Day) lstCounponOtp.RemoveAt(i);
                }
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("UpdateCoupon", "", ex.Message);
            }
            

        }

        public static void CouponTick()
        {
            while(true)
            {
                CouponReminder();

                Task.Delay(1000).Wait();
            }
        }

        public static void CouponReminder()
        {
            try
            {
                if (LastReminDate.Date != DateTime.Now.Date)
                {
                    LastReminDate = DateTime.Now;

                    for (int i = lstCurrentCoupon.Count - 1; i > 0; i--)
                    {
                        if (lstCurrentCoupon[i].IsValid().ReturnCode != 0)
                        {
                            NissanCouponLibrary.Utils.Log.LogEvent("RemoveCoupon", string.Format("Giftcode : {0}, BKS : {1}, Phone : {2}",
                                lstCurrentCoupon[i].GiftCode, lstCurrentCoupon[i].LicensePlateNumber, lstCurrentCoupon[i].PhoneNumber));

                            lstCurrentCoupon.RemoveAt(i);
                            continue;
                        }

                        if (lstCurrentCoupon[i].ReminderDay.Date == DateTime.Now.Date && lstCurrentCoupon[i].ReminderDay.Month == DateTime.Now.Month
                            && lstCurrentCoupon[i].ReminderDay.Year == DateTime.Now.Year)
                        {
                            Utils.SMS.SendSMS(lstCurrentCoupon[i].PhoneNumber,
                                string.Format("Ma qua tang/KM dich vu {0} se het han su dung vao ngay {1}. Vui long den Dai ly Nissan de doi qua/su dung ma KM truoc khi het han. LH 18006883",
                                lstCurrentCoupon[i].GiftCode, lstCurrentCoupon[i].ExpriedDate.ToString("dd/MM/yyyy")));
                        }
                    }

                    Task.Delay(10000).Wait();
                }
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("CouponReminder", "", ex.Message);
            }
            
        }

        public static bool CreateCounponOtp(string GiftCode)
        {
            try
            {
                string OtpCode = (new Random()).Next(1000, 9999).ToString();
                var tmpCounpon = lstCurrentCoupon.Where(x => x.GiftCode == GiftCode).FirstOrDefault();
                string SMSMessage = string.Format("Ma OTP cua ban la {0}, ma co hieu luc trong ngay {1}",
                                        OtpCode, DateTime.Now.ToString("dd/MM/yyyy"));

                if (tmpCounpon == null) return false;
                
                for (int i = 0; i < lstCounponOtp.Count; i++)
                {
                    if(lstCounponOtp[i].GiftCode == GiftCode)
                    {
                        lstCounponOtp[i].Otp = OtpCode;
                        Utils.SMS.SendSMS(tmpCounpon.PhoneNumber, SMSMessage);
                        return true;
                    }
                }

                lstCounponOtp.Add(new CounponOtp
                {
                    Otp = OtpCode,
                    GiftCode = GiftCode,
                    CreateTime = DateTime.Now
                });

                Utils.SMS.SendSMS(tmpCounpon.PhoneNumber, SMSMessage);

                return true;
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("CreateCounponOtp", "", ex.Message);
            }

            return false;
        }

        public static bool CheckCounponOtp(string GiftCode, string OtpCode, string DealerName)
        {
            try
            {
                foreach(var tmpOtp in lstCounponOtp)
                {
                    if (tmpOtp.GiftCode == GiftCode && tmpOtp.Otp == OtpCode)
                    {
                        foreach(var tmpCounpon in lstCurrentCoupon)
                        {
                            if(tmpCounpon.GiftCode == GiftCode)
                            {
                                // Send SMS to customrer for comfirming
                                Utils.SMS.SendSMS(tmpCounpon.PhoneNumber,
                                    string.Format("Ma qua tang/ KM dich vu {0} da dang ky su dung tai dai ly {1} vao ngay {2}. LH 18006883",
                                    GiftCode, DealerName, DateTime.Now.ToString("dd/MM/yyyy")));


                                // Remove from current list and update to DB
                                lstCurrentCoupon.Remove(tmpCounpon);

                                tmpCounpon.RedeemedByDealer = DealerName;
                                tmpCounpon.RedeemedDate = DateTime.Now;
                                DataBase.CouponDAO.UpdateCounpon(tmpCounpon);
                                
                                lstCounponOtp.Remove(tmpOtp);
                                return true;
                            }                            
                        }                        
                    }
                }

            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("CheckCounponOtp", "", ex.Message);
            }

            return false;
        }
    }

    public class CounponOtp
    {
        public string GiftCode;
        public string Otp;
        public DateTime CreateTime;
    }
}
