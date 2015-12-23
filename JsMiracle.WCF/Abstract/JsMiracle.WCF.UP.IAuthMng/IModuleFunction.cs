using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.WCF.UP.IAuthMng
{
    /// <summary>
    /// 模块功能信息
    /// </summary>
    [ServiceContract]
    public interface IModuleFunction : IDataLayer<IMS_UP_MKGN>
    {
        /// <summary>
        /// 根据模块号得功能信息
        /// </summary>
        /// <param name="moduleid">模块编号</param>
        /// <returns></returns>
        [OperationContract]
        List<IMS_UP_MKGN> GetModuleFunctionList(int moduleid);

    }


    public class WcfClientModuleFunction : WcfDalClient<IMS_UP_MKGN>, IModuleFunction
    {
        const string ContactName = "IModuleFunction";

        public WcfClientModuleFunction()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public List<IMS_UP_MKGN> GetModuleFunctionList(int moduleid)
        {
            return
                  WcfClient<IModuleFunction>.UseService<List<IMS_UP_MKGN>>(
                  base.binding, base.remoteAddress,
                  n => n.GetModuleFunctionList(moduleid));
        }
    }


    public class WcfConfigModuleFunction : WcfClientConfig<IMS_UP_MKGN>, IModuleFunction
    {
        const string ContactName = "IModuleFunction";

        public WcfConfigModuleFunction()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }



        public List<IMS_UP_MKGN> GetModuleFunctionList(int moduleid)
        {
            return RequestFunc<object, List<IMS_UP_MKGN>>("GetModuleFunctionList", moduleid);
        }
    }

}
