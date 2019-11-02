using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Models
{
    public class UserLogin
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }

    public class LoginResult
    {
        //public const int LOGIN_RESULT_OK = 0;
        //public const int LOGIN_RESULT_INFO_ERROR = 1;       
        //public const int LOGIN_RESULT_USER_WRONG_API_KEY = 2;

        public int Result { set; get; }
        public UserInfo User { set; get; }
    }
}
