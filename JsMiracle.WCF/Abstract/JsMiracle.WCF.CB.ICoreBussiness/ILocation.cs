using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.WCF.CB.ICoreBussiness
{
    /// <summary>
    /// 处理位置的接口
    /// </summary>
    public interface ILocation : IDataLayer<IMS_CB_WZ>
    {
        /// <summary>
        /// 得到储位信息(json序列化完成的字符串，直接用于页面显示)
        /// </summary>
        /// <param name="p">排</param>
        /// <returns></returns>
        string GetLocationState(int p);

        /// <summary>
        /// 得最大位置
        /// </summary>
        /// <returns></returns>
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

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CoreKnownTypesProvider))]
    public interface IWcfLocation : IWcfService
    {

    }

    //public class WcfClientLocation : WcfDalClient<IMS_CB_WZ>, ILocation
    //{
    //    const string ContactName = "ILocation";

    //    public WcfClientLocation()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }



    //    public string GetLocationState(int p)
    //    {
    //        return
    //              WcfClient<ILocation>.UseService<string>(
    //              base.binding, base.remoteAddress,
    //              n => n.GetLocationState(p));
    //    }

    //    public InventoryPositionModule GetMaxPosition()
    //    {
    //        return
    //              WcfClient<ILocation>.UseService<InventoryPositionModule>(
    //              base.binding, base.remoteAddress,
    //              n => n.GetMaxPosition());
    //    }
    //}


    public class WcfConfigLocation : WcfClientConfig<IMS_CB_WZ, IWcfLocation>, ILocation
    {
        const string ContactName = "ILocation";

        public WcfConfigLocation()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }



        public string GetLocationState(int p)
        {
            return RequestFunc<object[], string>("GetLocationState", new object[] { p });
        }

        public InventoryPositionModule GetMaxPosition()
        {
            return RequestFunc<object[], InventoryPositionModule>("GetMaxPosition", new object[]{ null});
        }
    }
}
