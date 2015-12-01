using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsMiracle.Entities.EasyUI_Model
{
    public class PaginationModel
    {
        public int total { get; private set; }

        //public IList<dynamic> rows { get;private set; }
        public dynamic rows { get; private set; }

        public PaginationModel(dynamic data)
        {
            total = 0;
            if (data != null)
            {
                total = data.Count;
            }

            rows = data;
        }
    }


    //public class Test
    //{
    //    public int total { get; private set; }

    //    //public IList<dynamic> rows { get;private set; }
    //    public dynamic rows { get; private set; }

    //    public Test(dynamic data)
    //    {
    //        total = 0;
    //        if (data != null)
    //            total = data.Count;

    //        rows = data;
    //    }
    //}
}