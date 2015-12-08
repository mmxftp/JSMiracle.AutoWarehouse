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
                var col = data as System.Collections.IList;

                if ( col != null)
                    total = col.Count;
                else
                {
                    var dt = data as System.Data.DataTable;
                    total = dt.Rows.Count;
                }
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