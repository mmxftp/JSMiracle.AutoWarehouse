using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CB.ICoreBussiness
{

    /// <summary>
    /// 处理物料信息的接口
    /// </summary>
    public interface IItem : IDataLayer<IMS_CB_WL>
    {
        /// <summary>
        /// 根据物料编号得到物料, 没有数据返回null
        /// </summary>
        /// <param name="wlbh">完整的物料编号</param>
        /// <returns></returns>
        IMS_CB_WL GetEntityByWXBH(string wlbh);
    }


    public class WcfClientItem : WcfDalClient<IMS_CB_WL>, IItem
    {
        const string ContactName = "IItem";

        public WcfClientItem()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }


        public IMS_CB_WL GetEntityByWXBH(string wlbh)
        {
            return
                    WcfClient<IItem>.UseService<IMS_CB_WL>(
                    base.binding, base.remoteAddress,
                    n => n.GetEntityByWXBH(wlbh));
        }
    }
}
