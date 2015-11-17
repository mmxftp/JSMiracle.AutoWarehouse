using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.View;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_Permission_Dal : IIMS_ORGEntities, IPermission
    {
        public IList<TreeModel> GetPermissionInfo(string roleid)
        {
            // 根节点
            var rootQueryable =
                IMS_TB_ModuleSet.Where(n => n.ParentID == -1).OrderBy(n => n.SortID);

            List<TreeModel> data = new List<TreeModel>();
            TreeModel tm = new TreeModel();
            data.Add(tm);
            tm.id = -1;

            var perList = IMS_TB_PermissionSet.Where(n => n.RoleId == roleid);

            /// select * from  IMS_TB_Module m
            /// left join IMS_TB_Permission p
            /// on m.moduleid = p.moduleid  
            /// /*  perList.Where(n => n.RoleId == roleid); */  => and p.roleid = roleid
            /// order by m.sortid

            var moduleQueryable = from m in IMS_TB_ModuleSet
                                  join p in perList.Where(n => n.FunctionID == -1)
                                  on m.ModuleID equals p.ModuleID into p_join
                                  from v in p_join.DefaultIfEmpty()
                                  where m.ParentID != -1
                                  orderby m.SortID
                                  select new
                                  {
                                      ModuleID = m.ModuleID,
                                      ModuleName = m.ModuleName,
                                      ParentID = m.ParentID,
                                      RoleID = v.RoleId,
                                      HasPermission = v.ID == null ? false : true
                                  };

            var funQueryable = from f in IMS_TB_ModuleFunctionSet
                               join p in perList
                                on f.FunctionID equals p.FunctionID into p_join
                               from v in p_join.DefaultIfEmpty()
                               select new
                               {
                                   FunctionID = f.FunctionID,
                                   FunctionName = f.FunctionName,
                                   ModuleID = f.ModuleID,
                                   RoleID = v.RoleId,
                                   HasPermission = v.ID == null ? false : true
                               };

            var rootModuleList = rootQueryable.ToList();
            var moduleList = moduleQueryable.ToList();
            var funList = funQueryable.ToList();

            if (rootModuleList == null || moduleList == null || rootModuleList.Count == 0 || moduleList.Count == 0)
                return null;

            tm.text = "权限模块";
            tm.children = new List<TreeModel>();
            tm.attributes = new Dictionary<string, int>();
            tm.attributes.Add("parentid", -1);
            tm.attributes.Add("moduleid", -1);
            tm.attributes.Add("functionid", -1);

            foreach (var root in rootModuleList)
            {
                var modeNode = new TreeModel();
                modeNode.id = root.ModuleID;
                modeNode.text = root.ModuleName;
                tm.children.Add(modeNode);
                modeNode.attributes = new Dictionary<string, int>();
                modeNode.attributes.Add("parentid", -1);
                modeNode.attributes.Add("moduleid", root.ModuleID);
                modeNode.attributes.Add("functionid", -1);

                var childmodeList = moduleList.Where(n => n.ParentID == root.ModuleID);

                if (childmodeList == null || childmodeList.Count() == 0)
                    continue;

                modeNode.children = new List<TreeModel>();
                foreach (var cm in childmodeList)
                {
                    var cNode = new TreeModel();
                    cNode.id = cm.ModuleID;
                    cNode.text = cm.ModuleName;
                    modeNode.children.Add(cNode);
                    cNode.children = new List<TreeModel>();
                    cNode.attributes = new Dictionary<string, int>();
                    cNode.attributes.Add("parentid", root.ModuleID);
                    cNode.attributes.Add("moduleid", cm.ModuleID);
                    cNode.attributes.Add("functionid", -1);

                    var functionList = funList.Where(n => n.ModuleID == cm.ModuleID);
                    if (functionList == null || functionList.Count() == 0)
                    {
                        // 没有子节点,时当前选中状态取数据表中的值 
                        cNode.@checked = cm.HasPermission;
                        continue;
                    }

                    foreach (var f in functionList)
                    {
                        var fun = new TreeModel();
                        fun.id = f.FunctionID;
                        fun.text = f.FunctionName;
                        fun.@checked = f.HasPermission;
                        fun.attributes = new Dictionary<string, int>();
                        fun.attributes.Add("parentid", root.ModuleID);
                        fun.attributes.Add("moduleid", cm.ModuleID);
                        fun.attributes.Add("functionid", f.FunctionID);
                        cNode.children.Add(fun);
                    }
                }
            }

            return data;
        }

        private void FillFunctionList(string roleid, int moduleid, ref List<IMS_TB_Permission> permissionAllQueryable)
        {
            if (permissionAllQueryable == null)
                permissionAllQueryable = new List<IMS_TB_Permission>();

            permissionAllQueryable.Add(new IMS_TB_Permission()
            {
                ModuleID = moduleid,
                RoleId = roleid,
                FunctionID = -1
            });

            var functionQuery = from f in IMS_TB_ModuleFunctionSet
                                where f.ModuleID == moduleid
                                select f;

            foreach (var f in functionQuery)
            {
                permissionAllQueryable.Add(new IMS_TB_Permission()
                {
                    RoleId = roleid,
                    ModuleID = f.ModuleID,
                    FunctionID = f.FunctionID
                });
            }
        }

        public int SavePermission(bool check, int parentid, int moduleid, int functionid, string roleid)
        {
            var role = IMS_TB_RoleInfoSet.Where(n => n.RoleID == roleid);
            int effectRowCount = 0;

            if (role == null || role.Count() == 0)
                return effectRowCount;

            var perList = IMS_TB_PermissionSet.Where(n => n.RoleId == roleid);

            // 当前角色的所有权限
            var permissionExistsQueryable = from p in IMS_TB_PermissionSet
                                            where p.RoleId == roleid
                                            select p;

            //var permissionExistsQueryable = pQuery.ToList();

            // 所有需处理的权限
            List<IMS_TB_Permission> permissionAllQueryable = new List<IMS_TB_Permission>();

            // 根节点
            if (parentid == -1 && moduleid == -1 && functionid == -1)
            {
                var rootModuleQuery = IMS_TB_ModuleSet.Where(n => n.ParentID == -1);

                foreach (var r in rootModuleQuery)
                {
                    var moduleQuery = from n in IMS_TB_ModuleSet
                                      where n.ParentID == r.ModuleID
                                      select n;

                    foreach (var q in moduleQuery)
                    {
                        FillFunctionList(roleid, q.ModuleID, ref permissionAllQueryable);
                    }
                }
            }
            // 父节点
            if (parentid == -1 && moduleid != -1 && functionid == -1)
            {
                var moduleQuery = from n in IMS_TB_ModuleSet
                                  where n.ParentID == moduleid
                                  select n;

                foreach (var q in moduleQuery)
                {
                    FillFunctionList(roleid, q.ModuleID, ref permissionAllQueryable);
                }
            }
            // 第一级子节点
            else if (parentid != -1 && moduleid != -1 && functionid == -1)
            {
                FillFunctionList(roleid, moduleid, ref permissionAllQueryable);
            }
            // 第二级子节点
            else if (parentid != -1 && moduleid != -1 && functionid != -1)
            {
                permissionAllQueryable.Add(new IMS_TB_Permission()
                {
                    RoleId = roleid,
                    ModuleID = moduleid,
                    FunctionID = functionid
                });
            }

            // 加权限的处理
            if (check)
            {
                foreach (var n in permissionAllQueryable)
                {
                    // 不存在此权限加入权限表
                    if (!permissionExistsQueryable.Any(p => p.ModuleID == n.ModuleID && p.FunctionID == n.FunctionID))
                    {
                        n.LastModDate = System.DateTime.Now;
                        IMS_TB_PermissionSet.Add(n);
                        effectRowCount++;
                    }
                }

                // 提交数据库
                SaveChanges();
            }
            else
            {
                effectRowCount += permissionAllQueryable.Count();

                // 因为permissionAllQueryable 为List<T>类型 , EF 中必须写在第一个from 语句中
                // join中写EF的DbSet<T>
                var removePermissionList = from a in permissionAllQueryable
                                           join p in IMS_TB_PermissionSet
                                           on new
                                           {
                                               a.RoleId,
                                               a.ModuleID,
                                               a.FunctionID
                                           }
                                           equals new
                                           {
                                               p.RoleId,
                                               p.ModuleID,
                                               p.FunctionID
                                           }
                                           select p;

                IMS_TB_PermissionSet.RemoveRange(removePermissionList);
                // 提交数据库
                SaveChanges();
            }

            return effectRowCount;
        }


        public PermissionViewModule GetPermissionListByUserID(string userid)
        {
            //            select m.* from IMS_TB_ModuleSet m
            //left join IMS_TB_PermissionSet p on p.ModuleID = m.ModuleID  and p.FunctionID = -1
            //left join IMS_TB_RoleUserSet r on p.RoleId = r.RoleID
            //where r.UserID ='0001'

            var modList = from m in IMS_TB_ModuleSet
                          join p in IMS_TB_PermissionSet.Where(n => n.FunctionID == -1)
                          on m.ModuleID equals p.ModuleID into v_per
                          from v in v_per.DefaultIfEmpty()
                          join r in IMS_TB_RoleUserSet
                          on v.RoleId equals r.RoleID into v_ret
                          from r in v_ret.DefaultIfEmpty()
                          where r.UserID == userid
                          select m;


            //select f.* from IMS_TB_ModuleFunctionSet f
            //left join IMS_TB_PermissionSet p on p.ModuleID = f.ModuleID  and p.FunctionID = f.FunctionID
            //left join IMS_TB_RoleUserSet r on p.RoleId = r.RoleID
            //where r.UserID ='0001'

            var funList = from f in IMS_TB_ModuleFunctionSet
                          join p in IMS_TB_PermissionSet
                          on new
                            {
                                mod = f.ModuleID,
                                fun = (int?)f.FunctionID
                            }
                          equals new
                            {
                                mod = p.ModuleID,
                                fun = p.FunctionID
                            }
                          into v_per
                          from v in v_per.DefaultIfEmpty()
                          join r in IMS_TB_RoleUserSet
                          on v.RoleId equals r.RoleID into v_ret
                          from r in v_ret.DefaultIfEmpty()
                          where r.UserID == userid
                          select f;

            // 不分层的权限处理
            var permissions = new PermissionViewModule(modList.ToList(), funList.ToList());
            //permissions.Modules = modList.ToList();
            //permissions.Functions = funList.ToList();

            return permissions;
            // 分层的权限处理
            //var perList = new List<PermissionViewModule>();

            //foreach (var m in modList)
            //{
            //    var p = new PermissionViewModule();
            //    p.Module = m;

            //    p.Functions = funList.Where(n => n.ModuleID == m.ModuleID).ToList();
            //    perList.Add(p);
            //}

            //return perList;
        }


        public PermissionViewModule GetAllPermission()
        {
             //if (Cache.GetApplicationCache("actionpermission") == null)

            //cache.SetApplicationCache(CacheAllPermission, data);
            //(IList<ActionPermission>)Cache.GetApplicationCache("actionpermission");

            var permissions = new PermissionViewModule(IMS_TB_ModuleSet.ToList(), IMS_TB_ModuleFunctionSet.ToList());
            //permissions.Modules = IMS_TB_ModuleSet.ToList();
            //permissions.Functions = IMS_TB_ModuleFunctionSet.ToList();
            return permissions;
        }
    }
}
