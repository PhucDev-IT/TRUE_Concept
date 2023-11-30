using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.ViewModel
{
    public class ItemCartViewModel
    {
        public Product product { get; set; }
      
        public int Quantity { get; set; }
      
        public string nameCategory { get; set; }

        public double TotalMoney { get; set; }

    }
}