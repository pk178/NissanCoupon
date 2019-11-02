using MongoDB.Driver;
using NissanCouponLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core.DataBase
{
    public class CouponDAO
    {
        public static CouponInfo CheckCoupon(string CouponCode)
        {
            try
            {
                var collection = DAOManager._database.GetCollection<CouponInfo>("CouponInfo");
                var Filter = Builders<NissanCouponLibrary.Entity.CouponInfo>.Filter.Empty;

                Filter = Filter & Builders<NissanCouponLibrary.Entity.CouponInfo>.Filter.Eq(x => x.GiftCode, NissanCouponLibrary.Utils.DES.Encrypt(CouponCode));
                //Filter = Filter | Builders<NissanCouponLibrary.Entity.CouponInfo>.Filter.Eq(x => x.ChassisNumber, CouponCode);
                //Filter = Filter | Builders<NissanCouponLibrary.Entity.CouponInfo>.Filter.Eq(x => x.LicensePlateNumber, CouponCode);

                var Return = collection.Find(Filter).ToList().FirstOrDefault();
                if(Return != null)
                {
                    Return.GiftCode = CouponCode;
                }
                else
                {
                    return new CouponInfo();
                }

                return Return;
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("CheckCoupon", CouponCode, ex.Message);
            }

            return new CouponInfo();
        }

        public static ReturnInfo IsCouponExist(List<CouponInfo> NewCoupon)
        {
            try
            {
                var AllCoupon = GetAllCoupon(new DateTime(2000, 1, 1), DateTime.Now);
                foreach(var tmpNewCoupon in NewCoupon)
                {
                    foreach(var tmpCouponData in AllCoupon)
                    {
                        if(tmpCouponData.CampaignName == tmpNewCoupon.CampaignName)
                        {
                            var CouponType = tmpCouponData.GetCouponType();

                            if ((tmpCouponData.LicensePlateNumber == tmpNewCoupon.LicensePlateNumber && 
                                (CouponType == CouponInfo.COUPON_TYPE_SERVICE_PROMOTION || CouponType == CouponInfo.COUPON_TYPE_SERVICE_SURVEY)) ||
                                (tmpCouponData.ChassisNumber == tmpNewCoupon.ChassisNumber &&
                                (CouponType == CouponInfo.COUPON_TYPE_SALE_PROMOTION || CouponType == CouponInfo.COUPON_TYPE_SALE_SURVEY)))
                            {
                                return new ReturnInfo
                                {
                                    ReturnCode = 1,
                                    ReturnString = string.Format("Coupon dành cho khách hàng {0}, BKS {1}, VIN {2} đã tồn tại trong chiến dịch '{3}'",
                                    tmpNewCoupon.PhoneNumber, tmpNewCoupon.LicensePlateNumber, tmpNewCoupon.ChassisNumber, tmpNewCoupon.CampaignName)
                                };
                            }

                        }

                        if (tmpCouponData.GiftCode == tmpNewCoupon.GiftCode)
                        {
                            return new ReturnInfo
                            {
                                ReturnCode = 1,
                                ReturnString = string.Format("Coupon dành cho khách hàng {0}, BKS {1}, VIN {2} đã tồn tại, trùng giftcode, vui lòng kiểm tra lại",
                                    tmpNewCoupon.PhoneNumber, tmpNewCoupon.LicensePlateNumber, tmpNewCoupon.ChassisNumber, tmpNewCoupon.CampaignName)
                            };
                        }
                    }
                }

                return new ReturnInfo
                {
                    ReturnCode = 0,
                    ReturnString = ""
                };
        }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("IsCouponExist", "", ex.Message);
            }

            return new ReturnInfo
            {
                ReturnCode = 255,
                ReturnString = "Có lỗi xảy ra, vui lòng kiểm tra lại"
            };
        }

        public static List<DealerRedeemed> GetCouponByDealer(DealerInfo Dealer, DateTime FromTime, DateTime ToTime)
        {
            try
            {
                var collection = DAOManager._database.GetCollection<CouponInfo>("CouponInfo");
                var Filter = Builders<CouponInfo>.Filter.And(
                        Builders<CouponInfo>.Filter.Gt(x => x.RedeemedDate, FromTime),
                        Builders<CouponInfo>.Filter.Lt(x => x.RedeemedDate, ToTime),
                        Builders<CouponInfo>.Filter.Eq(x => x.RedeemedByDealer, Dealer.Name));

                var RedeemedList = collection.Find(Filter).ToList();
                if (RedeemedList != null)
                {
                    for (int i = 0; i < RedeemedList.Count; i++)
                    {
                        RedeemedList[i].GiftCode = NissanCouponLibrary.Utils.DES.Decrypt(RedeemedList[i].GiftCode);
                    }

                    var Return = new List<DealerRedeemed>();
                    foreach (var tmpRedeemed in RedeemedList) Return.Add(tmpRedeemed.ToDealerRedeemed());
                    return Return;
                }

            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("IsCouponExist", "", ex.Message);
            }

            return new List<DealerRedeemed>();
        }

        public static List<CouponInfo> GetAllCoupon(DateTime FromTime, DateTime ToTime)
        {
            try
            {
                var collection = DAOManager._database.GetCollection<CouponInfo>("CouponInfo");
                var Filter = Builders<CouponInfo>.Filter.And(
                        Builders<CouponInfo>.Filter.Gt(x => x.PromotionDate, FromTime),
                        Builders<CouponInfo>.Filter.Lt(x => x.PromotionDate, ToTime));

                var Return = collection.Find(Filter).ToList();
                if (Return != null)
                {
                    for(int i = 0; i < Return.Count; i++)
                    {
                        Return[i].GiftCode = NissanCouponLibrary.Utils.DES.Decrypt(Return[i].GiftCode);
                    }

                    return Return;
                }
                
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("GetAllCoupon", "", ex.Message);
            }

            return new List<CouponInfo>();
        }

        public static bool UpdateCounpon(CouponInfo CounponToUpdate)
        {
            try
            {
                var collection = DAOManager._database.GetCollection<NissanCouponLibrary.Entity.CouponInfo>("CouponInfo");
                var EncryptGiftCode = NissanCouponLibrary.Utils.DES.Encrypt(CounponToUpdate.GiftCode);

                var filter = Builders<NissanCouponLibrary.Entity.CouponInfo>.Filter.Eq(x => x.GiftCode, EncryptGiftCode);

                //Thực hiện lệnh update dữ liệu, và insert nếu chưa có
                var update = Builders<NissanCouponLibrary.Entity.CouponInfo>.Update
                    .Set(x => x.Category, CounponToUpdate.Category)
                    .Set(x => x.Type, CounponToUpdate.Type)
                    .Set(x => x.CampaignName, CounponToUpdate.CampaignName)
                    .Set(x => x.PromotionDate, CounponToUpdate.PromotionDate)
                    .Set(x => x.DealerName, CounponToUpdate.DealerName)
                    .Set(x => x.CustomerNumber, CounponToUpdate.CustomerNumber)
                    .Set(x => x.CustomerName, CounponToUpdate.CustomerName)
                    .Set(x => x.PhoneNumber, CounponToUpdate.PhoneNumber)
                    .Set(x => x.VehicleModel, CounponToUpdate.VehicleModel)
                    .Set(x => x.ChassisNumber, CounponToUpdate.ChassisNumber)
                    .Set(x => x.LicensePlateNumber, CounponToUpdate.LicensePlateNumber)
                    .Set(x => x.GiftCode, EncryptGiftCode)
                    .Set(x => x.ExpriedDate, CounponToUpdate.ExpriedDate)
                    .Set(x => x.ReminderDay, CounponToUpdate.ReminderDay)
                    .Set(x => x.EntitledServiceGift, CounponToUpdate.EntitledServiceGift)
                    .Set(x => x.RedeemedDate, CounponToUpdate.RedeemedDate)
                    .Set(x => x.RedeemedByDealer, CounponToUpdate.RedeemedByDealer);


                collection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });

                return true;
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("UpdateCounpon", CounponToUpdate.GiftCode, ex.Message);
            }

            return false;
        }

        public static CouponInfo RedeemCoupon(string GiftCode, string DealerCode)
        {
            try
            {

            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("CheckCoupon", GiftCode, ex.Message);
            }

            return new CouponInfo();
        }
    }
}
