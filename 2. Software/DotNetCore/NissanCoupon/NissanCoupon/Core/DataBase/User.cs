using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core.DataBase
{
    public class User
    {
        public static List<NissanCouponLibrary.Entity.UserInfo> GetAllUser()
        {
            var Result = new List<NissanCouponLibrary.Entity.UserInfo>();

            try
            {
                var collection = DAOManager._database.GetCollection<NissanCouponLibrary.Entity.UserInfo>("UserInfo");
                var documents = collection.Find(x => string.IsNullOrEmpty(x.UserName) == false).ToList();

                foreach (var tmp in documents)
                {
                    Result.Add(tmp);
                }
            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("GetAllUser", "", ex.Message);
            }

            return Result;
        }

        public static NissanCouponLibrary.Entity.UserInfo GetUser(string UserName, string Password)
        {

            try
            {
                var collection = DAOManager._database.GetCollection<NissanCouponLibrary.Entity.UserInfo>("UserInfo");
                return collection.Find(x => x.UserName == UserName && x.Password == Password).ToList().FirstOrDefault();

            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("GetUser", "", ex.Message);
            }

            return null;
        }

        public static bool UpdateUser(NissanCouponLibrary.Entity.UserInfo info)
        {
            try
            {
                var collection = DAOManager._database.GetCollection<NissanCouponLibrary.Entity.UserInfo>("UserInfo");

                var filter = Builders<NissanCouponLibrary.Entity.UserInfo>.Filter.Eq(x => x.UserName, info.UserName);

                //Thực hiện lệnh update dữ liệu, và insert nếu chưa có
                var update = Builders<NissanCouponLibrary.Entity.UserInfo>.Update
                    .Set(x => x.UserName, info.UserName)
                    .Set(x => x.Password, info.Password)
                    .Set(x => x.FullName, info.FullName)
                    .Set(x => x.Email, info.Email)
                    .Set(x => x.PhoneNumber, info.PhoneNumber)
                    .Set(x => x.Permission, info.Permission)
                    .Set(x => x.Dealer, info.Dealer)
                    .Set(x => x.Enable, info.Enable)
                    .Set(x => x.LastLoginTime, info.LastLoginTime)
                    .Set(x => x.CreatedDate, info.CreatedDate)
                    .Set(x => x.ModifiedDate, info.ModifiedDate);


                collection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });

                return true;
            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("UpdateUser", "", ex.Message);
            }

            return false;
        }

    }
}
