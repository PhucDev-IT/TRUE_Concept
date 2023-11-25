using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.ViewModel
{
    public class OrderViewModel
    {
       public List<ItemCartViewModel> ItemCarts { get; set; }
        public double Total { get; set; }

   
    }
}