using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.MetaData
{
    public class BaoCaoNhap
    {
        [DisplayName("Mã phiếu nhập")]
        public int MaPhieuNhap { get; set; }
        [DisplayName("Ngày nhập")]
        public Nullable<System.DateTime> NgayNhap { get; set; }
        [DisplayName("Tổng tiền")]
        public Nullable<double> TongTien { get; set; }
        [DisplayName("Giảm giá")]
        public Nullable<double> GiamGia { get; set; }
        [DisplayName("Thành tiền")]
        public Nullable<double> ThanhTien { get; set; }
    }
}