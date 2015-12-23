
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.CM.ICommonMng;
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
            var ent= this.DbContext.IMS_CM_DMLX_S.Where(n => n.LXDM.Equals(lxdm, StringComparison.CurrentCultureIgnoreCase));
            return ent.FirstOrDefault();
        }

    }
}
