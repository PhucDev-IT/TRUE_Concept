using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.MetaData
{
    public class ChiTietPhieuNhap
    {
        [DisplayName("Mã phiếu nhập")]
        public int MaPhieuNhap { get; set; }
        [DisplayName("Mã sản phẩm")]
        public int IDProduct { get; set; }
        [DisplayName("Số lượng")]
        public Nullable<double> Quantity { get; set; }
        [DisplayName("VAT")]
        public Nullable<double> VAT { get; set; }
        [DisplayName("Giá")]
        public Nullable<double> Price { get; set; }
        [DisplayName("Tổng tiền")]
        public Nullable<double> TONGTIEN { get; set; }
    }
}