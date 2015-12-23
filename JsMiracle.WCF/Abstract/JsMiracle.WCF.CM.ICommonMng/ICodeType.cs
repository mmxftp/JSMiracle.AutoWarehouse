using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CM.ICommonMng
{
    /// <summary>
    /// 处理代码类型的接口
    /// </summary>
    public interface ICodeType : IDataLayer<IMS_CM_DMLX>
    {
        /// <summary>
        /// 根据代码类型编码得到代码类型
        /// </summary>
        /// <param name="lxdm">类型代码</param>
        /// <returns></returns>
        IMS_CM_DMLX GetEntity(string lxdm);
    }


    public class WcfClientCodeType : WcfDalClient<IMS_CM_DMLX>, ICodeType
    {
        const string ContactName = "ICodeType";

        public WcfClientCodeType()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }


        public IMS_CM_DMLX GetEntity(string lxdm)
        {
            return
                 WcfClient<ICodeType>.UseService<IMS_CM_DMLX>(
                 base.binding, base.remoteAddress,
                 n => n.GetEntity(lxdm));
        }
    }
}
