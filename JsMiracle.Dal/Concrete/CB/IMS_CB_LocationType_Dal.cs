using JsMiracle.Dal.Abstract.CB;
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete.CB
{
    public class IMS_CB_LocationType_Dal : DataLayerBase<IMS_CB_WZLX>, ILocationType
    {



        #region protected Method

        protected override string GetKeyValue(IMS_CB_WZLX entity)
        {
            return entity.LXDM;
        }

        protected override IMS_CB_WZLX GetOldEntity(IMS_CB_WZLX entity)
        {
            return GetEntity(entity.LXDM);
        }

        protected override bool IsAddEntity(IMS_CB_WZLX entity)
        {
            return entity.ID == 0;
        }
        #endregion


        //protected override IList<IMS_CB_WZLX> GetPageQuery(IQueryable<IMS_CB_WZLX> query)
        //{



        //    return base.GetPageQuery(query);
        //}
    }
}
