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
    
    public partial class ItemCart
    {
        public int ID { get; set; }
        public int IDProduct { get; set; }
        public Nullable<double> Quantity { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Product Product { get; set; }
    }
}
