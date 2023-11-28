using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.MetaData
{
    public class Product
    {
        [DisplayName("Mã sản phẩm")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [DisplayName("Tên")]
        public string NameProduct { get; set; }

        [DisplayName("Giá cũ")]
        public Nullable<double> PriceOld { get; set; }

        [DisplayName("Giá hiện tại")]
        [Required(ErrorMessage = "Giá bán không được để trống")]
        public Nullable<double> NewPrice { get; set; }
        [DisplayName("Đơn vị")]
        public string Unit { get; set; }

        [DisplayName("Số lượng")]
        public double Quantity { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Ảnh")]
        public string ImgDemo { get; set; }

        public List<string> PreviewImages { get; set; }
        public string Status { get; set; }

        [DisplayName("Danh mục")]
        public Nullable<int> IDCategory { get; set; }

        public virtual Category Category { get; set; }
 
    }
}