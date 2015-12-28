
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.WCF.CM.ICommonMng;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CM.CommonMng
{
    public class IMS_CM_CodeType_Dal : WcfDataLayerBase<IMS_CM_DMLX>, ICodeType
    {
        public IMS_CM_DMLX GetEntity(string lxdm)
        {
            var ent = this.DbContext.IMS_CM_DMLX_S.Where(n => n.LXDM.Equals(lxdm, StringComparison.CurrentCultureIgnoreCase));
            return ent.FirstOrDefault();
        }

    }


    public class IMS_CM_CodeType_WCF : WcfService<IMS_CM_DMLX>, IWcfCodeType
    {
        IMS_CM_CodeType_Dal dal = new IMS_CM_CodeType_Dal();

        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();
            object[] objs;
            switch (req.Head.RequestMethodName)
            {
                case "GetEntity":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetEntity(objs[0]);
                    break;
                default:
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }

        protected override IDataLayer<IMS_CM_DMLX> DataLayer
        {
            get { return dal; }
        }
    }
}
