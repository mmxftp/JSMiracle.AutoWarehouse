
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using JsMiracle.WCF.Interface;
using System.ServiceModel;

namespace JsMiracle.WCF.UP.IAuthMng
{
    /// <summary>
    /// 用户管理的接口
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// 最小密码长度
        /// </summary>
        [OperationContract]
        int MinPasswordLength();

        /// <summary>
        /// 验证用户登录是否成功
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [OperationContract]
        bool ValidateUser(string userID, string password);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="email">电子邮箱</param>
        [OperationContract]
        void CreateUser(string userID, string userName, string password, string email);

        /// <summary>
        /// 改变密码
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        [OperationContract]
        bool ChangePassword(string userID, string oldPassword, string newPassword);
    }


    //public class WcfClientMembershipService : WcfClient<IMembershipService>, IMembershipService
    //{
    //    const string ContactName = "IMembershipService";

    //    public WcfClientMembershipService()
    //        : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
    //    { }


    //    public int MinPasswordLength()
    //    {
    //        return base.Channel.MinPasswordLength();
    //    }

    //    public bool ValidateUser(string userID, string password)
    //    {
    //        return base.Channel.ValidateUser(userID, password);
    //    }

    //    public void CreateUser(string userID, string userName, string password, string email)
    //    {
    //        base.Channel.CreateUser(userID, userName, password, email);
    //    }

    //    public bool ChangePassword(string userID, string oldPassword, string newPassword)
    //    {
    //        return base.Channel.ChangePassword(userID, oldPassword, newPassword);
    //    }
    //}
    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(AuthKnownTypesProvider))]
    public interface IWcfMembershipService : IWcfService
    {

    }

    public class WcfConfigMembershipService
        : WcfClientConfig<IMS_UP_YH, IWcfMembershipService>, IMembershipService
    {
        const string ContactName = "IMembershipService";

        public WcfConfigMembershipService()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public int MinPasswordLength()
        {
            return RequestFunc<object[], int>("MinPasswordLength", new object[] { null });
        }

        public bool ValidateUser(string userID, string password)
        {
            return RequestFunc<object[], bool>("ValidateUser"
                , new object[] { userID, password });
        }

        public void CreateUser(string userID, string userName, string password, string email)
        {
            RequestAction<object[]>("ValidateUser"
                , new object[] { userID, userName, password, email });
        }

        public bool ChangePassword(string userID, string oldPassword, string newPassword)
        {
            return RequestFunc<object[], bool>("ChangePassword"
             , new object[] { userID, oldPassword, newPassword });
        }
    }
}
