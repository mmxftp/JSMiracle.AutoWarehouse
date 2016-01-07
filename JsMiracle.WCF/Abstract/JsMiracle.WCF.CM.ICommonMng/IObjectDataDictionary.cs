using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.WCF.CM.ICommonMng
{
    /// <summary>
    /// IMS_CM_DXXX
    /// </summary>
    public interface IObjectDataDictionary : IDataLayer<IMS_CM_DXXX>
    {
        ///// <summary>
        ///// 根据表名得字段名
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <returns></returns>
        //IList<TableColumnsModule> GetColumns(string tablename);

        /// <summary>
        /// 保存表数据到对象表中
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="opUser">操作用户</param>
        void ReSaveObjectData(string tablename, string opUser);

        ///// <summary>
        ///// 删除对象代码对应的所有数据
        ///// </summary>
        ///// <param name="dxdm">对象代码</param>
        ///// <exception cref="JsMiracleException">对象已被使用不得删除</exception>
        //void DeleteObjectData(string dxdm);

    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CommonKnownTypesProvider))]
    public interface IWcfObjectDataDictionary : IWcfService
    {

    }

    //public class WcfClientObjectDataDictionary : WcfDalClient<IMS_CM_DXXX>, IObjectDataDictionary
    //{
    //    const string ContactName = "IObjectDataDictionary";

    //    public WcfClientObjectDataDictionary()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }

    //    public void ReSaveObjectData(string tablename, string opUser)
    //    {
    //        WcfClient<IObjectDataDictionary>.UseService(
    //            base.binding, base.remoteAddress,
    //            n => n.ReSaveObjectData(tablename, opUser));
    //    }
    //}


    public class WcfConfigObjectDataDictionary : WcfClientConfig<IMS_CM_DXXX, IWcfObjectDataDictionary>, IObjectDataDictionary
    {
        const string ContactName = "IObjectDataDictionary";

        public WcfConfigObjectDataDictionary()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public void ReSaveObjectData(string tablename, string opUser)
        {
            RequestAction<object[]>("ReSaveObjectData", new object[] { tablename, opUser });
        }
    }

}
