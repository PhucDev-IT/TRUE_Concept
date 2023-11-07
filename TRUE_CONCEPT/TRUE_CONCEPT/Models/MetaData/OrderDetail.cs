using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.MetaData
{
    public class OrderDetail
    {
        [DisplayName("Mã hóa đơn")]
        public int IDOrder { get; set; }
        [DisplayName("Mã sản phẩm")]
        public int IDProduct { get; set; }
        [DisplayName("Số lượng")]
        public Nullable<double> Quantity { get; set; }
        [DisplayName("Giá bán")]
        public Nullable<double> Price { get; set; }
        [DisplayName("Thuế")]
        public Nullable<double> VAT { get; set; }
        [DisplayName("Tổng tiền")]
        public Nullable<double> TotalMoney { get; set; }
    }
}