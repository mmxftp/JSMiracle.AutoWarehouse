using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Models
{
    public class PermissionModel 
    {
        public int ModuleID { get; set; }

        public string ModuleName { get; set; }
        public int Parentid { get; set; }

        public int? FunctionID { get; set; }

        public string FunctionName { get; set; }

        public bool ModulePermission { get; set; }
        public bool FunctionPermission { get; set; }
    }
}
