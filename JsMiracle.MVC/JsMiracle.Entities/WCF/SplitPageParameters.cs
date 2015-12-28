using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.WCF
{
    public class SplitPageParameters
    {

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public string Where { get; set; }

        public object[] WhereParams { get; set; }
    }


}
