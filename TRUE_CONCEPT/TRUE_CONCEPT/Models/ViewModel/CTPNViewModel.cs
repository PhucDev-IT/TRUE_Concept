using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.ViewModel
{
    public class CTPNViewModel
    {
        public int IDProduct { get; set; }
        public string NameProduct { get; set; }
        public string ImgDemoProduct { get; set; }
        public float Quantity { get; set; }
        public double Price { get; set; }
        public double TotalMoney { get; set; }
     
    }
}