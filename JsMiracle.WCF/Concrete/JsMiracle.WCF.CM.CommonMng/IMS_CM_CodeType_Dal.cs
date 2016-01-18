
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.CM.ICommonMng;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CM.CommonMng
{
    /// <summary>
    /// 代码类型
    /// </summary>
    public class IMS_CM_CodeType_Dal : WcfDataLayerBase<IMS_CM_DMLX>, ICodeType
    {

        protected override bool IsAddEntity(IMS_CM_DMLX entity)
        {
            if (entity.ID != 0)
            {
                if (base.DbContext.IMS_CM_DMLX_S.Any(n => n.ID != entity.ID && n.LXDM == entity.LXDM))
                    throw new JsMiracleException("代码类型重覆不得修改");
            }
            else
            {
                if (base.DbContext.IMS_CM_DMLX_S.Any(n=>n.LXDM == entity.LXDM))
                    throw new JsMiracleException("代码类型重覆不得修改");
            }

            return base.IsAddEntity(entity);
        }

        public IMS_CM_DMLX GetEntityBylxdm(string lxdm)
        {
            var ent = this.DbContext.IMS_CM_DMLX_S.Where(n => n.LXDM.Equals(lxdm, StringComparison.CurrentCultureIgnoreCase));
            return ent.FirstOrDefault();
        }
    }


    public class IMS_CM_CodeType_WCF : WcfDataServiceBase<IMS_CM_DMLX>, IWcfCodeType
    {
        IMS_CM_CodeType_Dal dal = new IMS_CM_CodeType_Dal();

        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();
            object[] objs;
            switch (req.Head.RequestMethodName)
            {
                case "GetEntityBylxdm":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetEntityBylxdm((string)objs[0]);
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
