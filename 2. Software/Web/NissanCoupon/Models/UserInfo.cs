using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NissanCoupon.Models
{
    public class UserInfo
    {
        //public const int PERMISSION_EDIT_USER = 92;
        //public const int PERMISSION_EDIT_COUPON = 182;

        public string UserName { set; get; }
        public string FullName { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string CreatedDate { set; get; }
        public string LastLoginTime { set; get; }
        public string ModifiedDate { set; get; }
        public DealerInfo Dealer { set; get; }
        public bool Enable { set; get; }
        public List<int> Permission{ set; get; }
    }
}
