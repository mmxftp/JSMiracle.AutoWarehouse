using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsMiracle.Entities.EasyUI_Model
{
    public class PaginationModel<T> where T : class, new()
    {
        public int total { get; set; }

        public IList<T> rows { get; set; }
    }
}