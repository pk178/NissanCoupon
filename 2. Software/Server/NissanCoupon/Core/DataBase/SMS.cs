using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core.DataBase
{
    public class SMS
    {
        public static bool UpdateSMS(string SendTo, string Message, string Result)
        {
            try
            {
                var collection = DAOManager._database.GetCollection<NissanCouponLibrary.Entity.SMSInfo>("SMSInfo");
                var filter = Builders<NissanCouponLibrary.Entity.SMSInfo>.Filter.Eq(x => x.Time, DateTime.Now);

                //Thực hiện lệnh update dữ liệu, và insert nếu chưa có
                var update = Builders<NissanCouponLibrary.Entity.SMSInfo>.Update
                    .Set(x => x.Time, DateTime.Now)
                    .Set(x => x.SendTo, SendTo)
                    .Set(x => x.Message, Message)
                    .Set(x => x.SendResult, Result);

                collection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });

                return true;
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("UpdateSMS", "", ex.Message);
            }

            return false;
        }

        public static List<NissanCouponLibrary.Entity.SMSInfo> GetFailSMS(DateTime From, DateTime To)
        {
            try
            {
                var collection = DAOManager._database.GetCollection<NissanCouponLibrary.Entity.SMSInfo>("SMSInfo");
                var Filter = Builders<NissanCouponLibrary.Entity.SMSInfo>.Filter.Eq(x => x.SendResult, "Quota not remain");
                return collection.Find(Filter).ToList().Where(x => (x.Time > From && x.Time < To)).ToList();
            }
            catch(Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("GetFailSMS", "", ex.Message);
            }

            return new List<NissanCouponLibrary.Entity.SMSInfo>();
        }
    }
}
