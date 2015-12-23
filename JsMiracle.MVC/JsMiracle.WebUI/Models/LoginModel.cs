using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JsMiracle.WebUI.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "员工号不可为空")]
        [Display(Name = "员工号")]
        public string UserID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不可为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}