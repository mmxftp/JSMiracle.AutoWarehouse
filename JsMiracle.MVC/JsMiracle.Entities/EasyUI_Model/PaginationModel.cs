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
        public object rows { get; private set; }

        public PaginationModel(object data, int dataRowCount)
        {
            total = dataRowCount;
            if (data != null)
            {
                var col = data as System.Collections.IList;

                if (total == 0)
                {
                    if (col != null)
                    {
                        total = col.Count;
                    }
                    else
                    {
                        var dt = data as System.Data.DataTable;
                        total = dt.Rows.Count;
                    }
                }
            }

            rows = data;
        }
    }
}