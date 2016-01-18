using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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


        /// <summary>
        /// 得到所有物料信息 只有ID,WLBH,WLMC
        /// </summary>
        /// <returns></returns>
        List<IMS_CB_WL> GetAllList();
    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CoreKnownTypesProvider))]
    public interface IWcfItem : IWcfService
    {
    }

    public class WcfConfigItem : WcfClientConfig<IMS_CB_WL, IWcfItem>, IItem
    {
        const string ContactName = "IItem";

        public WcfConfigItem()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public IMS_CB_WL GetEntityByWXBH(string wlbh)
        {
            return RequestFunc<object[], IMS_CB_WL>("GetEntityByWXBH", new object[] { wlbh });
        }

        public List<IMS_CB_WL> GetAllList()
        {
            return RequestFunc<object[], List<IMS_CB_WL>>("GetAllList", new object[] { null });
        }
    }
}
