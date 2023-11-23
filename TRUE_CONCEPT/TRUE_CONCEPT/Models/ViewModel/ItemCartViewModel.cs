using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.ViewModel
{
    public class ItemCartViewModel
    {
        public int IdProduct { get; set; }

        public string NameProduct { get; set; }

        public double quantity { get; set; }
        public double currentPrice { get; set; }

        public string ImageDemo { get; set; }
        public string unit { get; set; }
        public string nameCategory { get; set; }
    }
}