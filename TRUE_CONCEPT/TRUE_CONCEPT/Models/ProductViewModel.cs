using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models
{
    public class ProductViewModel
    {
        [DisplayName("Mã sản phẩm")]
        public int ID { get; set; }

        [DisplayName("Tên")]
        public string NameProduct { get; set; }

        [DisplayName("Giá")]
        [Required(ErrorMessage = "Giá bán không được để trống")]
        public Nullable<double> Price { get; set; }

        [DisplayName("Ảnh")]
        public string Img_Url { get; set; }

        [DisplayName("Số lượng")]
        public double Quantity { get; set; }

        public double TotalMoney { get; set; }
    }
}