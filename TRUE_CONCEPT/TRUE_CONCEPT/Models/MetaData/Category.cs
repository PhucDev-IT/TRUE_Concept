using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TRUE_CONCEPT.Models.MetaData
{
    public class Category
    {
        [DisplayName("Mã danh mục")]
        public int IDCategory { get; set; }

        [DisplayName("Tên danh mục")]
        public string NameCategory { get; set; }
    }
}