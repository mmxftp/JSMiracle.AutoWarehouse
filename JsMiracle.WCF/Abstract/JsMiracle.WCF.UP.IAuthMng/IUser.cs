using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.WCF.UP.IAuthMng
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    [ServiceContract]
    public interface IUser : IDataLayer<IMS_UP_YH>
    {
        /// <summary>
        /// 得所有用户信息
        /// </summary>
        /// <param name="userNameFormatter">是否对用户名有特定格式要用</param>
        /// <returns></returns>
        [OperationContract]
        List<IMS_UP_YH> GetAllUserList(bool userNameFormatter = false);

        /// <summary>
        /// 得到数据实例
        /// </summary>
        /// <param name="userid">员工号</param>
        /// <returns></returns>
        [OperationContract]
        IMS_UP_YH GetEntityByYHBH(string userid);
    }


    public class WcfClientUser : WcfDalClient<IMS_UP_YH>, IUser
    {
        const string ContactName = "IUser";

        public WcfClientUser()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }


        public List<IMS_UP_YH> GetAllUserList(bool userNameFormatter = false)
        {
            return
                WcfClient<IUser>.UseService<List<IMS_UP_YH>>(
                    base.binding, base.remoteAddress,
                    n => n.GetAllUserList(userNameFormatter));
        }

        public IMS_UP_YH GetEntityByYHBH(string userid)
        {
            return
                 WcfClient<IUser>.UseService<IMS_UP_YH>(
                     base.binding, base.remoteAddress,
                     n => n.GetEntityByYHBH(userid));
        }
    }

    public class WcfConfigUser : WcfClientConfig<IMS_UP_YH>, IUser
    {
        const string ContactName = "IUser";

        public WcfConfigUser()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }


        public List<IMS_UP_YH> GetAllUserList(bool userNameFormatter = false)
        {
            return RequestFunc<object,List<IMS_UP_YH>>("GetAllUserList", userNameFormatter);
        }

        public IMS_UP_YH GetEntityByYHBH(string userid)
        {
            return RequestFunc<object, IMS_UP_YH>("GetEntityByYHBH", userid);
        }
    }
}
