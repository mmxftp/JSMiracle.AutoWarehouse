using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CB.ICoreBussiness
{
    /// <summary>
    /// 处理位置的接口
    /// </summary>
    public interface ILocation : IDataLayer<IMS_CB_WZ>
    {

        DataTable GetLocationState(int p);

        InventoryPositionModule GetMaxPosition();

        //int MaxP { get; }

        //int MaxL { get; }

        //int MaxC { get; }

    }

    public class InventoryPositionModule
    {
        /// <summary>
        /// 最大列
        /// </summary>
        public int MaxP { set; get; }

        /// <summary>
        /// 最大列
        /// </summary>
        public int MaxL { set; get; }

        /// <summary>
        /// 最大层
        /// </summary>
        public int MaxC { set; get; }


    }



    public class WcfClientLocation : WcfDalClient<IMS_CB_WZ>, ILocation
    {
        const string ContactName = "ILocation";

        public WcfClientLocation()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

  

        public DataTable GetLocationState(int p)
        {
            return
                  WcfClient<ILocation>.UseService<DataTable>(
                  base.binding, base.remoteAddress,
                  n => n.GetLocationState(p));
        }

        public InventoryPositionModule GetMaxPosition()
        {
            return
                  WcfClient<ILocation>.UseService<InventoryPositionModule>(
                  base.binding, base.remoteAddress,
                  n => n.GetMaxPosition());
        }
    }
}
