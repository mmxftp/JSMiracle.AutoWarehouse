using JsMiracle.Entities.View;
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
    /// 处理权限的接口
    /// </summary>
    [ServiceContract]
    public interface IActionPermission
    {
        /// <summary>
        /// 得到所有需处理的权限集合
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PermissionViewModule GetAllPermission();

        /// <summary>
        /// 刷新权限缓存
        /// </summary>
        [OperationContract]
        void ResetCache();
    }

    public class WcfClientActionPermission : WcfClient<IActionPermission>, IActionPermission
    {
        const string ContactName = "IActionPermission";

        public WcfClientActionPermission()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        {

        }

        public PermissionViewModule GetAllPermission()
        {
            return base.Channel.GetAllPermission();
        }

        public void ResetCache()
        {
            base.Channel.ResetCache();
        }
    }
}
