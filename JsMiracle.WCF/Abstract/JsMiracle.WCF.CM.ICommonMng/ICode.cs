using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
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
    /// 处理代码的接口
    /// </summary>
    public interface ICode : IDataLayer<IMS_CM_DM>
    {
        /// <summary>
        /// 得到代码类型对应的所有代码信息的集合
        /// </summary>
        /// <param name="codeType">代码类型</param>
        /// <returns></returns>
        List<IMS_CM_DM> GetCodeList(CodeTypeEnum codeType);
    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CommonKnownTypesProvider))]
    public interface IWcfCode : IWcfService
    {

    }
    //public class WcfClientCode : WcfDalClient<IMS_CM_DM>,ICode
    //{
    //    const string ContactName = "ICode";

    //    public WcfClientCode()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }

    //    public List<IMS_CM_DM> GetCodeList(CodeTypeEnum codeType)
    //    {
    //        return
    //              WcfClient<ICode>.UseService<List<IMS_CM_DM>>(
    //              base.binding, base.remoteAddress,
    //              n => n.GetCodeList(codeType));
    //    }

    //}


    public class WcfConfigCode : WcfClientConfig<IMS_CM_DM, IWcfCode>, ICode
    {
        const string ContactName = "ICode";

        public WcfConfigCode()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public List<IMS_CM_DM> GetCodeList(CodeTypeEnum codeType)
        {
            return RequestFunc<object[], List<IMS_CM_DM>>("GetCodeList", new object[] { codeType });
        }
    }
}
