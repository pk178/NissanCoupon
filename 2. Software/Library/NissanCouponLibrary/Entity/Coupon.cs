using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NissanCouponLibrary.Entity
{
    [DataContract]
    public class CouponRedeemResult
    {
        public const int RESULT_CODE_OK = 0;
        public const int RESULT_CODE_EXPRIED = 1;
        public const int RESULT_CODE_REDEEMED = 2;

        [DataMember]
        public int ResultCode { set; get; }

        [DataMember]
        public string RedeemMessage { set; get; }
    }

    [DataContract]
    public class CouponCheckResult
    {
        public const int RESULT_CODE_OK = 0;
        public const int RESULT_CODE_EXPRIED = 1;
        public const int RESULT_CODE_REDEEMED = 2;
        public const int RESULT_CODE_NOT_EXIST = 3;

        [DataMember]
        public int ResultCode;

        [DataMember]
        public CouponInfo Coupon;
    }


    [DataContract]
    public class CouponInfo
    {
        public const int COUPON_TYPE_SALE_SURVEY = 1;
        public const int COUPON_TYPE_SALE_PROMOTION = 2;
        public const int COUPON_TYPE_SERVICE_SURVEY = 3;
        public const int COUPON_TYPE_SERVICE_PROMOTION = 4;
        public const int COUPON_TYPE_UNKNOW = 255;

        public BsonObjectId _id;

        [DataMember]
        public string Category { set; get; }

        [DataMember]
        public string Type { set; get; }

        [DataMember]
        public string CampaignName { set; get; }

        [IgnoreDataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime PromotionDate { set; get; }

        [DataMember(Name = "PromotionDate")]
        public string PromotionDateStr
        {
            get { return PromotionDate.ToString("dd/MM/yyyy"); }
            set
            {
                try
                {
                    PromotionDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    PromotionDate = new DateTime(2000,1,1);
                }
            }
        }

        [DataMember]
        public string DealerName { set; get; }

        [DataMember]
        public int CustomerNumber { set; get; }

        [DataMember]
        public string CustomerName { set; get; }

        [DataMember]
        public string PhoneNumber { set; get; }

        [DataMember]
        public string VehicleModel { set; get; }

        [DataMember]
        public string ChassisNumber { set; get; }

        [DataMember]
        public string LicensePlateNumber { set; get; }

        [DataMember]
        public string GiftCode { set; get; }

        [IgnoreDataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ExpriedDate { set; get; }

        [DataMember(Name = "ExpriedDate")]
        public string ExpriedDateStr
        {
            get { return ExpriedDate.ToString("dd/MM/yyyy"); }
            set
            {
                try
                {
                    ExpriedDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    ExpriedDate = new DateTime(2000, 1, 1);
                }
            }
        }

        [IgnoreDataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ReminderDay { set; get; }

        [DataMember(Name = "ReminderDay")]
        public string ReminderDayStr
        {
            get { return ReminderDay.ToString("dd/MM/yyyy"); }
            set
            {
                try
                {
                    ReminderDay = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    ReminderDay = new DateTime(2000, 1, 1);
                }
            }
        }

        [DataMember]
        public string EntitledServiceGift { set; get; }

        [IgnoreDataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RedeemedDate { set; get; }

        [DataMember(Name = "RedeemedDate")]
        public string RedeemedDateStr
        {
            get { return RedeemedDate.ToString("dd/MM/yyyy"); }
            set
            {
                try
                {
                    RedeemedDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    RedeemedDate = new DateTime(2000, 1, 1);
                }
            }
        }

        [DataMember]
        public string RedeemedByDealer { set; get; }

        public CouponInfo()
        {
            Category = string.Empty;
            Type = string.Empty;
        }

        public bool IsExpried()
        {
            try
            {
                if ((DateTime.Now - ExpriedDate).TotalDays < 0) return true;
                return false;
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("IsExpried", "", ex.Message);
            }

            return false;
        }

        public bool IsRedeem()
        {
            try
            {
                if ((DateTime.Now - RedeemedDate).TotalDays < 0) return true;
                if (string.IsNullOrEmpty(RedeemedByDealer)) return true;
                return false;
            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("IsExpried", "", ex.Message);
            }

            return true;
        }

        public ReturnInfo IsValid()
        {
            try
            {
                var CouponType = GetCouponType();

                if ((DateTime.Now - ExpriedDate).TotalDays > 0) return new ReturnInfo {ReturnCode = 1, ReturnString = "Coupon đã quá hạn, ngày hết hạn : " + ExpriedDateStr};                
                if (!string.IsNullOrEmpty(RedeemedByDealer)) return new ReturnInfo { ReturnCode = 2, ReturnString = string.Format("Coupon đã được sử dụng bởi đại lý '{0}'", RedeemedByDealer)};
                if (CouponType == COUPON_TYPE_UNKNOW) return new ReturnInfo { ReturnCode = 3, ReturnString = string.Format("Category/Type không hợp lệ : {0}/{1}", Category, Type) };
                if (string.IsNullOrEmpty(CampaignName)) return new ReturnInfo { ReturnCode = 4, ReturnString = "Tên chiến dịch không được để trống" };
                if (string.IsNullOrEmpty(CustomerName)) return new ReturnInfo { ReturnCode = 5, ReturnString = "Tên khách hàng không được để trống" };
                if (string.IsNullOrEmpty(VehicleModel)) return new ReturnInfo { ReturnCode = 6, ReturnString = "Loại xe không được để trống" };
                if ((CouponType == COUPON_TYPE_SALE_PROMOTION || CouponType == COUPON_TYPE_SALE_SURVEY) && string.IsNullOrEmpty(ChassisNumber))
                    return new ReturnInfo { ReturnCode = 6, ReturnString = "Số khung không được để trống" };

                if ((CouponType == COUPON_TYPE_SERVICE_PROMOTION || CouponType == COUPON_TYPE_SERVICE_SURVEY) && string.IsNullOrEmpty(LicensePlateNumber))
                    return new ReturnInfo { ReturnCode = 6, ReturnString = "Biển số không được để trống" };

                if (string.IsNullOrEmpty(EntitledServiceGift)) return new ReturnInfo { ReturnCode = 9, ReturnString = "Dịch vụ/quà tặng không được để trống" };
                if (!Regex.IsMatch(PhoneNumber, @"^(\+84|84|0)+([0-9]{3})+([0-9]{6})\b")) return new ReturnInfo { ReturnCode = 10, ReturnString = "Số điện thoại không hợp lệ : " + PhoneNumber };
                if (PromotionDate.Year == 2000) return new ReturnInfo { ReturnCode = 11, ReturnString = "Ngày khảo sát không hợp lệ : " + PromotionDateStr };

                return new ReturnInfo { ReturnCode = 0, ReturnString = "Coupon hợp lệ" }; ;
            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("IsExpried", "", ex.Message);
            }

            return new ReturnInfo { ReturnCode = 255, ReturnString = "Coupon không hợp lệ, vui lòng kiểm tra lại" }; ;
        }

        public int GetCouponType()
        {
            try
            {
                if (Category.ToLower().Equals("sales"))
                {
                    if (Type.ToLower().Equals("survey"))
                    {
                        return COUPON_TYPE_SALE_SURVEY;
                    }

                    if (Type.ToLower().Equals("promotion"))
                    {
                        return COUPON_TYPE_SALE_PROMOTION;
                    }
                }

                if (Category.ToLower().Equals("service"))
                {
                    if (Type.ToLower().Equals("survey"))
                    {
                        return COUPON_TYPE_SERVICE_SURVEY;
                    }

                    if (Type.ToLower().Equals("promotion"))
                    {
                        return COUPON_TYPE_SERVICE_PROMOTION;
                    }
                }
            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("GetCouponType", "", ex.Message);
            }

            return COUPON_TYPE_UNKNOW;
        }

        public bool CreateGiftCode(DealerInfo Dealer)
        {
            string tmpGift = string.Empty;

            try
            {
                switch(GetCouponType())
                {
                    case COUPON_TYPE_SALE_SURVEY:
                        tmpGift = "SS";
                        break;
                    case COUPON_TYPE_SALE_PROMOTION:
                        tmpGift = "SP";
                        break;
                    case COUPON_TYPE_SERVICE_SURVEY:
                        tmpGift = "AS";
                        break;
                    case COUPON_TYPE_SERVICE_PROMOTION:
                        tmpGift = "AP";
                        break;
                }

                tmpGift += string.Format("{0:00}{1:00}", PromotionDate.Year - 2000, PromotionDate.Month);
                tmpGift += Dealer.Code;
                tmpGift += string.Format("{0:000}", CustomerNumber);
                tmpGift += PhoneNumber.Substring(PhoneNumber.Length - 3);

                this.GiftCode = tmpGift;
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("CreateGiftCode", "", ex.Message);
            }

            return false;
        }

        public DealerRedeemed ToDealerRedeemed()
        {
            try
            {
                return new DealerRedeemed
                {
                    ChassisNumber = this.ChassisNumber,
                    CustomerName = this.CustomerName,
                    EntitledServiceGift = this.EntitledServiceGift,
                    ExpriedDate = this.ExpriedDate,
                    GiftCode = this.GiftCode,
                    LicensePlateNumber = this.LicensePlateNumber,
                    PhoneNumber = this.PhoneNumber,
                    RedeemedByDealer = this.RedeemedByDealer,
                    RedeemedDate = this.RedeemedDate,
                    VehicleModel = this.VehicleModel
                };

            }
            catch(Exception ex)
            {
                Utils.Log.LogError("ConvertToDealerRedeemed", "", ex.Message);
            }

            return new DealerRedeemed();
        }
    }
}
