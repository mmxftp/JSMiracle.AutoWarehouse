using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.CB.ICoreBussiness;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CB.CoreBussiness
{
    public class IMS_CB_ContainerType_Dal : WcfDataLayerBase<IMS_CB_RQLX>, IContainerType
    {

        #region protected Method

        protected override string GetKeyValue(IMS_CB_RQLX entity)
        {
            return entity.LXBH;
        }

        protected override IMS_CB_RQLX GetOldEntity(IMS_CB_RQLX entity)
        {
            return FindEntityByKey(entity.LXBH);
        }

        protected override bool IsAddEntity(IMS_CB_RQLX entity)
        {
            return entity.ID == 0;
        }
        #endregion

    }
}
