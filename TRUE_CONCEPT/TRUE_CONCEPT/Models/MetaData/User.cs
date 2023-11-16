using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.MetaData
{
    public class User
    {
        [DisplayName("Mã khách hàng")]
        public int IDCustomer { get; set; }
        [DisplayName("Họ Tên")]
        public string FullName { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
    
        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        public string NumberPhone { get; set; }
        [DisplayName("Tài khoản")]
        public virtual Account Account { get; set; }
    }
}