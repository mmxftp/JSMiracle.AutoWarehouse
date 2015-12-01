using JsMiracle.Dal.Abstract.CB;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;

namespace JsMiracle.Dal.Concrete.CB
{
    public class IMS_CB_Container_Dal : DataLayerBase<IMS_CB_RQ>, IContainer
    {
        public override dynamic GetDataByPage(int intPageIndex, int intPageSize, out int rowCount
            , string orderBy, string where, params object[] whereParams)
        {
            rowCount = 0;

            int skipRows = (intPageIndex - 1) * intPageSize;

            var query = from rq in DbContext.IMS_CB_RQ_S
                        select new { rq, rq.IMS_CB_RQLX_S };

            if (!string.IsNullOrEmpty(where))
            {
                query = query.Where(where)
                            .OrderBy(orderBy)
                            .Skip(skipRows)
                            .Take(intPageSize);

                rowCount = query.Where(where).Count();
            }
            else
            {
                query = query.OrderBy(orderBy)
                            .Skip(skipRows)
                            .Take(intPageSize);

                rowCount = query.Count();
            }

            List<IMS_CB_RQ> rqList = new List<IMS_CB_RQ>();
            foreach (var row in query)
            {
                //var rq = new IMS_CB_RQ();
                var rq = row.rq;

                if ( row.IMS_CB_RQLX_S != null)                    
                    rq.RQLX_Name = string.Format("{0}({1})",row.IMS_CB_RQLX_S.LXBH,row.IMS_CB_RQLX_S.LXMC);

                rqList.Add(rq);
            }

            return rqList;
        }
    }
}
