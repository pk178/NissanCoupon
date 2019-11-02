using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Core.DataBase
{
    class DAOManager
    {
        public static IMongoClient _client;
        public static IMongoDatabase _database;

        public static bool DataBaseOk = false;


        /// <summary>
        /// Khởi tạo cơ sở dữ liệu MongoDB
        /// </summary>
        /// <returns></returns>
        public static bool Init()
        {
            try
            {
                //_client = new MongoClient(string.Format("{0}", "mongodb://127.0.0.1:7117"));
                _client = new MongoClient(string.Format("{0}", "mongodb://171.244.49.26:7117"));
                _database = _client.GetDatabase("NissanCoupon");

                CreateIndexes();

                DataBaseOk = true;
            }
            catch (Exception ex)
            {
               NissanCouponLibrary.Utils.Log.LogError("InitMongoDB", "Khởi tạo MongoDB", ex.Message);

                return false;
            }

            return true;
        }
        /// <summary>
        /// Tạo và kiểm tra key
        /// </summary>
        private static void CreateIndexes()
        {

            //var collection = _database.GetCollection<BsonDocument>("CouponData");
            //var keys = Builders<BsonDocument>.IndexKeys.Ascending("Key");
            //collection.Indexes.CreateOne(keys);

            //collection = _database.GetCollection<BsonDocument>("UserInfo");
            //keys = Builders<BsonDocument>.IndexKeys.Ascending("LoginName");
            //collection.Indexes.CreateOne(keys);

        }
    }
}
