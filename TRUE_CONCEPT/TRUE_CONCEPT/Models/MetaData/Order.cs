using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.MetaData
{ 
    public class Order
    {
        [DisplayName("Mã hóa đơn")]
        public int IDOrder { get; set; }
        [DisplayName("Mã khách hàng")]
        public Nullable<int> IDCustomer { get; set; }
        [DisplayName("Họ tên")]
        public string nameCustomer { get; set; }
        public string InforShipment { get; set; }
        public Nullable<double> FeeShipment { get; set; }
        [DisplayName("Ngày đặt hàng")]
        public Nullable<System.DateTime> OrderDate { get; set; }
        [DisplayName("Trạng thái")]
        public string Status { get; set; }
        [DisplayName("Thành tiền")]
        public Nullable<double> ThanhTien { get; set; }

    }
}