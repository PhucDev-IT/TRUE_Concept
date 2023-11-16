using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.MetaData
{
    public class Account
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Trường này là bắt buộc")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public string Password { get; set; }
        public string Decentralization { get; set; }
        public string Status { get; set; }
    }
}