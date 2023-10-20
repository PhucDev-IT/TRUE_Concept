//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TRUE_CONCEPT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ChiTietDuAns = new HashSet<ChiTietDuAn>();
            this.ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
            this.ItemCarts = new HashSet<ItemCart>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int ID { get; set; }
        public string NameProduct { get; set; }
        public Nullable<double> PriceOld { get; set; }
        public Nullable<double> NewPrice { get; set; }
        public string Unit { get; set; }
        public Nullable<double> Quantity { get; set; }
        public string Description { get; set; }
        public string Img_Url { get; set; }
        public string Status { get; set; }
        public Nullable<int> IDCategory { get; set; }
        public Nullable<double> VAT { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDuAn> ChiTietDuAns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemCart> ItemCarts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
