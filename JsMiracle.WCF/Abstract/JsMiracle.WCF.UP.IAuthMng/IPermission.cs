using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
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
    /// 权限验证
    /// </summary>
    [ServiceContract]
    public interface IPermission : IDataLayer<IMS_UP_JSMK>
    {
        /// <summary>
        /// 按角色得到权限信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [OperationContract]
        List<TreeModel> GetPermissionInfo(string roleid);

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="check">加入(check==true)/移除(check==false) 权限</param>
        /// <param name="parentid">父节点 没有值填(-1)</param>
        /// <param name="moduleid">模块节点 没有值填(-1)</param>
        /// <param name="functionid">功能节点 没有值填(-1)</param>
        /// <param name="roleid">角色编号</param>
        /// <returns></returns>
        [OperationContract]
        int SavePermission(bool check, int parentid, int moduleid, int functionid, string roleid);

        /// <summary>
        /// 按用户id得权限
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [OperationContract]
        PermissionViewModule GetPermissionListByUserID(string userid);

        /// <summary>
        /// 得所有权限信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PermissionViewModule GetAllPermission();

        /// <summary>
        /// 按角色得权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [OperationContract]
        List<IMS_UP_JSMK> GetPermissionListByRoleID(string roleid);

    }

    public class WcfClientPermission : WcfDalClient<IMS_UP_JSMK>, IPermission
    {
        const string ContactName = "IPermission";

        public WcfClientPermission()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        //public IList<IMS_UP_JS> GetAllRole()
        //{
        //    return
        //            WcfClient<IRole>.UseService<IList<IMS_UP_JS>>(
        //            base.binding, base.remoteAddress,
        //            n => n.GetAllRole());
        //}

        public List<TreeModel> GetPermissionInfo(string roleid)
        {
            return
                    WcfClient<IPermission>.UseService<List<TreeModel>>(
                    base.binding, base.remoteAddress
                    , n => n.GetPermissionInfo(roleid));
        }

        public int SavePermission(bool check, int parentid, int moduleid, int functionid, string roleid)
        {
            return
                  WcfClient<IPermission>.UseService<int>(
                  base.binding, base.remoteAddress
                  , n => n.SavePermission(check, parentid, moduleid, functionid, roleid));
        }

        public PermissionViewModule GetPermissionListByUserID(string userid)
        {
            return
                  WcfClient<IPermission>.UseService<PermissionViewModule>(
                  base.binding, base.remoteAddress
                  , n => n.GetPermissionListByUserID(userid));
        }

        public PermissionViewModule GetAllPermission()
        {
            return
                 WcfClient<IPermission>.UseService<PermissionViewModule>(
                 base.binding, base.remoteAddress
                 , n => n.GetAllPermission());
        }

        public List<IMS_UP_JSMK> GetPermissionListByRoleID(string roleid)
        {
            return
             WcfClient<IPermission>.UseService<List<IMS_UP_JSMK>>(
             base.binding, base.remoteAddress
             , n => n.GetPermissionListByRoleID(roleid));
        }


    }


    public class WcfConfigPermission : WcfClientConfig<IMS_UP_JSMK>, IPermission
    {
        const string ContactName = "IPermission";

        public WcfConfigPermission()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }

        public List<TreeModel> GetPermissionInfo(string roleid)
        {
            return RequestFunc<object, List<TreeModel>>("GetPermissionInfo", roleid);
        }

        public int SavePermission(bool check, int parentid, int moduleid, int functionid, string roleid)
        {
            return RequestFunc<object[], int>("SavePermission"
                , new object[]{
                    check,parentid,moduleid,functionid,roleid
                    });
        }

        public PermissionViewModule GetPermissionListByUserID(string userid)
        {
            return RequestFunc<object, PermissionViewModule>(
                "GetPermissionListByUserID", userid);
        }

        public PermissionViewModule GetAllPermission()
        {
            return RequestFunc<object, PermissionViewModule>(
            "GetAllPermission", null);
        }

        public List<IMS_UP_JSMK> GetPermissionListByRoleID(string roleid)
        {
            return RequestFunc<object, List<IMS_UP_JSMK>>(
                "GetPermissionListByRoleID", roleid);
        }
    }
}
