using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Abstract.UP;
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete.UP
{
    public class IMS_UP_RoMo_Dal : DataLayerBase<IMS_UP_JSMK>, IPermission
    {
        public IList<TreeModel> GetPermissionInfo(string roleid)
        {
            // 根节点
            var rootQueryable =
                this.DbContext.IMS_UP_MK_S.Where(n => n.FMKID == -1).OrderBy(n => n.PXID);

            List<TreeModel> data = new List<TreeModel>();
            TreeModel tm = new TreeModel();
            data.Add(tm);
            tm.id = -1;

            var perList = this.DbContext.IMS_UP_JSMK_S.Where(n => n.JSID == roleid);

            /// select * from  IMS_UP_Modu m
            /// left join IMS_UP_RoMo p
            /// on m.moduleid = p.moduleid  
            /// /*  perList.Where(n => n.RoleId == roleid); */  => and p.roleid = roleid
            /// order by m.sortid

            var moduleQueryable = from m in this.DbContext.IMS_UP_MK_S
                                  join p in perList.Where(n => n.GNID == -1)
                                  on m.MKID equals p.MKID into p_join
                                  from v in p_join.DefaultIfEmpty()
                                  where m.FMKID != -1
                                  orderby m.PXID
                                  select new
                                  {
                                      ModuleID = m.MKID,
                                      ModuleName = m.MKMC,
                                      ParentID = m.FMKID,
                                      RoleID = v.JSID,
                                      HasPermission = v.ID == null ? false : true
                                  };

            var funQueryable = from f in this.DbContext.IMS_UP_MKGN_S
                               join p in perList
                                on f.GNID equals p.GNID into p_join
                               from v in p_join.DefaultIfEmpty()
                               select new
                               {
                                   FunctionID = f.GNID,
                                   FunctionName = f.GNMC,
                                   ModuleID = f.MKID,
                                   RoleID = v.JSID,
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
                modeNode.id = root.MKID;
                modeNode.text = root.MKMC;
                tm.children.Add(modeNode);
                modeNode.attributes = new Dictionary<string, int>();
                modeNode.attributes.Add("parentid", -1);
                modeNode.attributes.Add("moduleid", root.MKID);
                modeNode.attributes.Add("functionid", -1);

                var childmodeList = moduleList.Where(n => n.ParentID == root.MKID);

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
                    cNode.attributes.Add("parentid", root.MKID);
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
                        fun.attributes.Add("parentid", root.MKID);
                        fun.attributes.Add("moduleid", cm.ModuleID);
                        fun.attributes.Add("functionid", f.FunctionID);
                        cNode.children.Add(fun);
                    }
                }
            }

            return data;
        }

        private void FillFunctionList(string roleid, int moduleid, ref List<IMS_UP_JSMK> permissionAllQueryable)
        {
            if (permissionAllQueryable == null)
                permissionAllQueryable = new List<IMS_UP_JSMK>();

            permissionAllQueryable.Add(new IMS_UP_JSMK()
            {
                MKID = moduleid,
                JSID = roleid,
                GNID = -1
            });

            var functionQuery = from f in this.DbContext.IMS_UP_MKGN_S
                                where f.MKID == moduleid
                                select f;

            foreach (var f in functionQuery)
            {
                permissionAllQueryable.Add(new IMS_UP_JSMK()
                {
                    JSID = roleid,
                    MKID = f.MKID,
                    GNID = f.GNID
                });
            }
        }

        public int SavePermission(bool check, int parentid, int moduleid, int functionid, string roleid)
        {
            var role = this.DbContext.IMS_UP_JS_S.Where(n => n.JSID == roleid);
            int effectRowCount = 0;

            if (role == null || role.Count() == 0)
                return effectRowCount;

            var perList = this.DbContext.IMS_UP_JSMK_S.Where(n => n.JSID == roleid);

            // 当前角色的所有权限
            var permissionExistsQueryable = from p in this.DbContext.IMS_UP_JSMK_S
                                            where p.JSID == roleid
                                            select p;

            //var permissionExistsQueryable = pQuery.ToList();

            // 所有需处理的权限
            List<IMS_UP_JSMK> permissionAllQueryable = new List<IMS_UP_JSMK>();

            // 根节点
            if (parentid == -1 && moduleid == -1 && functionid == -1)
            {
                var rootModuleQuery = this.DbContext.IMS_UP_MK_S.Where(n => n.FMKID == -1);

                foreach (var r in rootModuleQuery)
                {
                    var moduleQuery = from n in this.DbContext.IMS_UP_MK_S
                                      where n.FMKID == r.MKID
                                      select n;

                    foreach (var q in moduleQuery)
                    {
                        FillFunctionList(roleid, q.MKID, ref permissionAllQueryable);
                    }
                }
            }
            // 父节点
            if (parentid == -1 && moduleid != -1 && functionid == -1)
            {
                var moduleQuery = from n in this.DbContext.IMS_UP_MK_S
                                  where n.FMKID == moduleid
                                  select n;

                foreach (var q in moduleQuery)
                {
                    FillFunctionList(roleid, q.MKID, ref permissionAllQueryable);
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
                permissionAllQueryable.Add(new IMS_UP_JSMK()
                {
                    JSID = roleid,
                    MKID = moduleid,
                    GNID = functionid
                });
            }

            // 加权限的处理
            if (check)
            {
                foreach (var n in permissionAllQueryable)
                {
                    // 不存在此权限加入权限表
                    if (!permissionExistsQueryable.Any(p => p.MKID == n.MKID && p.GNID == n.GNID))
                    {
                        n.XGRQ = System.DateTime.Now;
                        this.DbContext.IMS_UP_JSMK_S.Add(n);
                        effectRowCount++;
                    }
                }

                // 提交数据库
                this.DbContext.SaveChanges();
            }
            else
            {
                effectRowCount += permissionAllQueryable.Count();

                // 因为permissionAllQueryable 为List<T>类型 , EF 中必须写在第一个from 语句中
                // join中写EF的DbSet<T>
                var removePermissionList = from a in permissionAllQueryable
                                           join p in this.DbContext.IMS_UP_JSMK_S
                                           on new
                                           {
                                               a.JSID,
                                               a.MKID,
                                               a.GNID
                                           }
                                           equals new
                                           {
                                               p.JSID,
                                               p.MKID,
                                               p.GNID
                                           }
                                           select p;

                this.DbContext.IMS_UP_JSMK_S.RemoveRange(removePermissionList);
                // 提交数据库
                this.DbContext.SaveChanges();
            }

            return effectRowCount;
        }


        public PermissionViewModule GetPermissionListByUserID(string userid)
        {
            //            select m.* from IMS_UP_ModuT m
            //left join IMS_UP_RoMoT. p on p.ModuleID = m.ModuleID  and p.FunctionID = -1
            //left join IMS_UP_RoUrSet r on p.RoleId = r.RoleID
            //where r.UserID ='0001'

            var modList = from m in this.DbContext.IMS_UP_MK_S
                          join p in this.DbContext.IMS_UP_JSMK_S.Where(n => n.GNID == -1)
                          on m.MKID equals p.MKID into v_per
                          from v in v_per.DefaultIfEmpty()
                          join r in this.DbContext.IMS_UP_JSYH_S
                          on v.JSID equals r.JSID into v_ret
                          from r in v_ret.DefaultIfEmpty()
                          where r.YHID == userid
                          select m;


            //select f.* from IMS_UP_MoFnT f
            //left join IMS_UP_RoMoT. p on p.ModuleID = f.ModuleID  and p.FunctionID = f.FunctionID
            //left join IMS_UP_RoUrSet r on p.RoleId = r.RoleID
            //where r.UserID ='0001'

            var funList = from f in this.DbContext.IMS_UP_MKGN_S
                          join p in this.DbContext.IMS_UP_JSMK_S
                          on new
                            {
                                mod = f.MKID,
                                fun = (int?)f.GNID
                            }
                          equals new
                            {
                                mod = p.MKID,
                                fun = p.GNID
                            }
                          into v_per
                          from v in v_per.DefaultIfEmpty()
                          join r in this.DbContext.IMS_UP_JSYH_S
                          on v.JSID equals r.JSID into v_ret
                          from r in v_ret.DefaultIfEmpty()
                          where r.YHID == userid
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
            var permissions = new PermissionViewModule(
                this.DbContext.IMS_UP_MK_S.ToList()
                , this.DbContext.IMS_UP_MKGN_S.ToList());

            return permissions;
        }


        public List<IMS_UP_JSMK> GetPermissionListByRoleID(string roleid)
        {
            if (string.IsNullOrEmpty(roleid))
                throw new JsMiracleException("roleid 不可为空");

            var data = this.DbContext.IMS_UP_JSMK_S.Where(n => n.JSID.Equals(roleid, StringComparison.CurrentCultureIgnoreCase));
            return data.ToList();
        }
    }
}
