using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.WCF.CB.ICoreBussiness;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CB.CoreBussiness
{
    /// <summary>
    /// 位置类型
    /// </summary>
    public class IMS_CB_LocationType_Dal : WcfDataLayerBase<IMS_CB_WZLX>, ILocationType
    {



        #region protected Method

        protected override string GetKeyValue(IMS_CB_WZLX entity)
        {
            return entity.LXDM;
        }

        protected override IMS_CB_WZLX GetOldEntity(IMS_CB_WZLX entity)
        {
            return FindEntityByKey(entity.LXDM);
        }

        protected override bool IsAddEntity(IMS_CB_WZLX entity)
        {
            return entity.ID == 0;
        }
        #endregion

    }


    public class IMS_CB_LocationType_WCF : WcfDataServiceBase<IMS_CB_WZLX>, IWcfLocationType
    {
        IMS_CB_LocationType_Dal dal = new IMS_CB_LocationType_Dal();


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

        protected override IDataLayer<IMS_CB_WZLX> DataLayer
        {
            get { return dal; }
        }
    }

}
