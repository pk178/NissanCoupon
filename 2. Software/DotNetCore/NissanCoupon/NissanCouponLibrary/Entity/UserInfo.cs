using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NissanCouponLibrary.Entity
{
    [DataContract]
    public class UserInfo
    {
        public const int PERMISSION_EDIT_USER = 92; // Create new, delete, disable, change password
        public const int PERMISSION_EDIT_COUPON = 182;

        public BsonObjectId _id;

        [DataMember]
        public string UserName { set; get; }

        [DataMember]
        public string FullName { set; get; }

        [DataMember]
        public string Password { set; get; }

        [DataMember]
        public string Email { set; get; }

        [DataMember]
        public string PhoneNumber { set; get; }

        [IgnoreDataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { set; get; }

        [DataMember(Name = "CreatedDate")]
        public string CreatedDateStr
        {
            get { return CreatedDate.ToString("dd/MM/yyyy"); }
            set
            {
                try
                {
                    CreatedDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    CreatedDate = DateTime.Now;
                }
            }
        }

        [IgnoreDataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LastLoginTime { set; get; }

        [DataMember(Name = "LastLoginTime")]
        public string LastLoginTimeStr
        {
            get { return LastLoginTime.ToString("dd/MM/yyyy"); }
            set
            {
                try
                {
                    LastLoginTime = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    LastLoginTime = new DateTime(2000, 1, 1);
                }
                
            }
        }

        [IgnoreDataMember]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ModifiedDate { set; get; }

        [DataMember(Name = "ModifiedDate")]
        public string ModifiedDateStr
        {
            get { return ModifiedDate.ToString("dd/MM/yyyy"); }
            set
            {
                try
                {
                    ModifiedDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    ModifiedDate = new DateTime(2000, 1, 1);
                }
                
            }
        }

        [DataMember]
        public DealerInfo Dealer { set; get; }

        [DataMember]
        public bool Enable { set; get; }

        [DataMember]
        public List<int> Permission{ set; get; }

        public UserInfo()
        {
            CreatedDate = new DateTime(2000, 1, 1);
            LastLoginTime = new DateTime(2000, 1, 1);
            ModifiedDate = new DateTime(2000, 1, 1);
            Enable = false;
        }

        public bool IsValid()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || !Enable) return false;
                if (!Dealer.IsValid()) return false;

                return true;
            }
            catch (Exception ex)
            {
                Utils.Log.LogError("User IsValid", "", ex.Message);
            }

            return false;
        }
    }
}
