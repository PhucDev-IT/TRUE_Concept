using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace TRUE_CONCEPT.Models.MetaData
{
    public class ThueDuAn
    {
        [DisplayName("Mã dự án")]
        public int MaDuAn { get; set; }
        [DisplayName("Mã khách hàng")]
        public Nullable<int> IDCustomer { get; set; }
        [DisplayName("Họ tên")]
        public string nameCustomer { get; set; }
        [DisplayName("Địa chỉ")]
        public string AddressCustomer { get; set; }
        [DisplayName("Ngày thuê")]
        public Nullable<System.DateTime> NgayThueDuAn { get; set; }
        [DisplayName("Dự kiến hoàn thành")]
        public Nullable<System.DateTime> NgayDuKienHoanThanh { get; set; }
        [DisplayName("Ngân sách dự kiến")]
        public Nullable<double> NganSachDuKien { get; set; }
        [DisplayName("Yêu cầu")]
        public string YeuCauThietKe { get; set; }
        [DisplayName("Loại thiết kế")]
        public string KieuThietKe { get; set; }
        [DisplayName("Mô tả")]
        public string MoTaThem { get; set; }
        [DisplayName("Phí phát sinh")]
        public Nullable<double> PhiPhatSinh { get; set; }
        [DisplayName("Tổng tiền")]
        public Nullable<double> TongTien { get; set; }
    }
}