using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.WCF.UP.IAuthMng
{
    /// <summary>
    /// 角色用户关系接口
    /// </summary>
    public interface IRoleUser : IDataLayer<IMS_UP_JSYH>
    {
        /// <summary>
        /// 根据角色得用户信息列表
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        //[OperationContract]
        List<IMS_UP_YH> GetUserList(string roleid);

        /// <summary>
        /// 保存用户角色关系
        /// </summary>
        /// <param name="roleid">角色</param>
        /// <param name="userid">用户</param>
        /// <returns></returns>
        [OperationContract]
        int SaveRoleUser(string roleid, string userid);

        /// <summary>
        /// 移除用户角色关系
        /// </summary>
        /// <param name="roleid">角色</param>
        /// <param name="userid">用户</param>
        /// <returns>移除数量</returns>
        [OperationContract]
        int RemoveRoleUser(string roleid, string userid);
    }


    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(AuthKnownTypesProvider))]
    public interface IWcfRoleUser : IWcfService
    {

    }


    public class WcfConfigRoleUser : WcfClientConfig<IMS_UP_JSYH, IWcfRoleUser>, IRoleUser
    {
        const string ContactName = "IRoleUser";

        public WcfConfigRoleUser()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public List<IMS_UP_YH> GetUserList(string roleid)
        {
            return RequestFunc<object[], List<IMS_UP_YH>>("GetUserList", new object[] { roleid });
        }

        public int SaveRoleUser(string roleid, string userid)
        {
            return RequestFunc<object[], int>("SaveRoleUser", new object[] { roleid, userid });
        }

        public int RemoveRoleUser(string roleid, string userid)
        {
            return RequestFunc<object[], int>("RemoveRoleUser", new object[] { roleid, userid });

        }
    }
}
