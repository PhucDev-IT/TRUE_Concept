using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models
{
    public class HomeViewModel
    {
        public int QuantityNewOrders { get; set; }
        public double SumTotalMoneyStatistical { get; set; }

        public List<Order> LatestOrders { get; set; }

        public List<Product> newsProducts { get; set; }


    }
}