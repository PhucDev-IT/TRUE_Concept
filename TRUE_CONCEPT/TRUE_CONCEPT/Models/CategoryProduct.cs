using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models
{
    public class CategoryProduct
    {
        public List<Category> Categories { get; set; }
        public List<Product> ListProducts { get; set; }
    }
}