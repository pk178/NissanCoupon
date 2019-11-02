using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NissanCouponLibrary.Entity
{
    [DataContract]
    public class UserLogin
    {
        [DataMember]
        public string UserName { set; get; }
        [DataMember]
        public string Password { set; get; }
    }

    [DataContract]
    public class LoginResult
    {
        public const int LOGIN_RESULT_OK = 0;
        public const int LOGIN_RESULT_INFO_ERROR = 1;       
        public const int LOGIN_RESULT_USER_WRONG_API_KEY = 2;

        [DataMember]
        public int Result { set; get; }
        [DataMember]
        public UserInfo User { set; get; }
    }
}
