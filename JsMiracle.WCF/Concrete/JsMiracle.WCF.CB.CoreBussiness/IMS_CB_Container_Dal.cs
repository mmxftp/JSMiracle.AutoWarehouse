using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.CB.ICoreBussiness;
using JsMiracle.WCF.WcfBaseService;
using JsMiracle.WCF.Interface;
using JsMiracle.Entities.WCF;

namespace JsMiracle.WCF.CB.CoreBussiness
{
    public class IMS_CB_Container_Dal : WcfDataLayerBase<IMS_CB_RQ>, IContainer
    {
        public override List<IMS_CB_RQ> GetDataByPageDynamic(int intPageIndex, int intPageSize, out int rowCount
            , string orderBy, string where, params object[] whereParams)
        {
            rowCount = 0;

            int skipRows = (intPageIndex - 1) * intPageSize;

            var query = from rq in DbContext.IMS_CB_RQ_S
                        join rqlx in DbContext.IMS_CB_RQLX_S
                        on rq.LXBH equals rqlx.LXBH into view
                        from v in view.DefaultIfEmpty()
                        select new { rq, v.LXMC ,v.LXBH };

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

                rq.RQLX_Name = string.Format("{0}({1})", row.LXBH, row.LXMC);
                //if ( row.IMS_CB_RQLX_S != null)                    
                //    rq.RQLX_Name = string.Format("{0}({1})",row.IMS_CB_RQLX_S.LXBH,row.IMS_CB_RQLX_S.LXMC);

                rqList.Add(rq);
            }

            return rqList;
        }

        #region protected Method

        protected override string GetKeyValue(IMS_CB_RQ entity)
        {
            return entity.RQBH;
        }

        protected override IMS_CB_RQ GetOldEntity(IMS_CB_RQ entity)
        {
            return FindEntityByKey(entity.RQBH);
        }

        protected override bool IsAddEntity(IMS_CB_RQ entity)
        {
            return entity.ID == 0;
        }
        #endregion


    }



    public class IMS_CB_Container_WCF : WcfService<IMS_CB_RQ>, IWcfContainer
    {
        IMS_CB_Container_Dal dal = new IMS_CB_Container_Dal();

        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();
            switch (req.Head.RequestMethodName)
            {
                default:
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }

        protected override IDataLayer<IMS_CB_RQ> DataLayer
        {
            get { return dal; }
        }
    }
}
