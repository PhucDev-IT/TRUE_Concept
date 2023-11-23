using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRUE_CONCEPT.Models.ViewModel
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Full Name không được để trống")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải chứa ít nhất 6 ký tự")]
        [MaxLength(20, ErrorMessage = "Mật khẩu tối đa 20 ký tự")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Re - Password không được để trống")]
        public string RePassword { get; set; }
    }
}