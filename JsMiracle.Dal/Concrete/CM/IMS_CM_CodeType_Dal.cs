using JsMiracle.Dal.Abstract.CM;
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete.CM
{
    public class IMS_CM_CodeType_Dal : DataLayerBase<IMS_CM_DMLX>, ICodeType
    {
        public IMS_CM_DMLX GetEntity(string lxdm)
        {
            var ent= this.DbContext.IMS_CM_DMLX_S.Where(n => n.LXDM.Equals(lxdm, StringComparison.CurrentCultureIgnoreCase));
            return ent.FirstOrDefault();
        }

    }
}
