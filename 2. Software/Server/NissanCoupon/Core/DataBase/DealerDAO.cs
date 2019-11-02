using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core.DataBase
{
    public class DealerDAO
    {
        public static NissanCouponLibrary.Entity.DealerInfo GetDealer(string Name = "", string Abbreviation = "", string Code = "")
        {
            try
            {
                if(string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Abbreviation) && string.IsNullOrEmpty(Code))
                {
                    return new NissanCouponLibrary.Entity.DealerInfo();
                }

                var collection = DAOManager._database.GetCollection<NissanCouponLibrary.Entity.DealerInfo>("DealerInfo");
                var Filter = Builders<NissanCouponLibrary.Entity.DealerInfo>.Filter.Empty;

                if (!string.IsNullOrEmpty(Name))
                    Filter = Filter & Builders<NissanCouponLibrary.Entity.DealerInfo>.Filter.Eq(x => x.Name, Name);

                if (!string.IsNullOrEmpty(Abbreviation))
                    Filter = Filter & Builders<NissanCouponLibrary.Entity.DealerInfo>.Filter.Eq(x => x.Abbreviation, Abbreviation);

                if (!string.IsNullOrEmpty(Code))
                    Filter = Filter & Builders<NissanCouponLibrary.Entity.DealerInfo>.Filter.Eq(x => x.Code, Code);

                return collection.Find(Filter).ToList().First();

            }
            catch (Exception ex)
            {
                NissanCouponLibrary.Utils.Log.LogError("GetDealer", "", ex.Message);
            }

            return null;
        }
    }
}
