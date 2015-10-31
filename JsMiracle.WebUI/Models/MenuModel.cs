using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsMiracle.WebUI.Models
{
    public class MenuModel
    {
        public int? ID { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int? Parentid { get; set; } 
    }
}