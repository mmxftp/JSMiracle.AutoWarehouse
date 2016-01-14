using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsMiracle.WebUI.Areas.UP.Models
{
    public class ChangePasswordModule
    {
        public long ID { get; set; }

        public string UserID { get; set; }

        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string OldPasswordMD5 { get; set; }
    }
}