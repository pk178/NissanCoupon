using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCouponLibrary.Entity
{
    public class SMSInfo
    {
        public BsonObjectId _id;
        public string SendTo { set; get; }

        public string Message { set; get; }

        public string SendResult { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Time { set; get; }
    }
}
