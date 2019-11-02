using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NissanCouponLibrary.Entity
{
    [DataContract]
    public class DealerInfo
    {
        public BsonObjectId _id;

        [DataMember]
        public string Region { set; get; }

        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public string Abbreviation { set; get; }

        [DataMember]
        public string Type { set; get; }

        [DataMember]
        public string Code { set; get; }

        public DealerInfo()
        {
            Region = "UNKNOW";
            Name = "UNKNOW";
            Abbreviation = "UNKNOW";
            Type = "UNKNOW";
            Code = "UNKNOW";
        }

        public bool IsValid()
        {
            try
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Code)) return false;

                int tmpCode = int.Parse(Code);
                if (tmpCode < 0 || tmpCode > 24) return false;

                return true;
            }
            catch (Exception ex)
            {
                Utils.Log.LogError("Dealer IsValid", "", ex.Message);
            }

            return false;
        }
    }
}
