using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.EasyUI_Model
{
    public class ExtResult
    {
        public bool success { get; set; }
        public string msg { get; set; }

        /// <summary>
        /// 影响行数
        /// </summary>
        public int effectRowCount { get; set; }

        public object parentid { get; set; }

        public object id { get; set; }
    }
}
