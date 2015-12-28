using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.WCF;
using JsMiracle.WCF.CB.ICoreBussiness;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CB.CoreBussiness
{
    public class IMS_CB_LocationRelationship_Dal:WcfDataLayerBase<IMS_CB_WZGX>,ILocationRelationship
    {

    }


    public class IMS_CB_LocationRelationship_WCF : WcfService<IMS_CB_WZGX>, IWcfLocationRelationship
    {
        IMS_CB_LocationRelationship_Dal dal = new IMS_CB_LocationRelationship_Dal();

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

        protected override IDataLayer<IMS_CB_WZGX> DataLayer
        {
            get { return dal; }
        }
    }
}
