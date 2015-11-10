using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using JsMiracle.Entities.EasyUI_Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_Permission_Dal : DataLayerBase, IPermission
    {
        public IList<TreeModel> GetPermissionInfo(string roleid)
        {
            // 根节点
            var rootQueryable =
                IMS_TB_Module.Where(n => n.ParentID == -1).OrderBy(n => n.SortID);

            List<TreeModel> data = new List<TreeModel>();
            TreeModel tm = new TreeModel();
            data.Add(tm);
            tm.id = -1;

            var perList = IMS_TB_Permission.Where(n => n.RoleId == roleid);

            /// select * from  IMS_TB_Module m
            /// left join IMS_TB_Permission p
            /// on m.moduleid = p.moduleid  
            /// /*  perList.Where(n => n.RoleId == roleid); */  => and p.roleid = roleid
            /// order by m.sortid

            var moduleQueryable = from m in IMS_TB_Module
                                  join p in perList
                                  on m.ModuleID equals p.ModuleID into p_join
                                  from v in p_join.DefaultIfEmpty()
                                  where m.ParentID != -1 && (v.FunctionID == null || v.FunctionID == -1)
                                  orderby m.SortID
                                  select new
                                  {
                                      ModuleID = m.ModuleID,
                                      ModuleName = m.ModuleName,
                                      ParentID = m.ParentID,
                                      RoleID = v.RoleId,
                                      HasPermission = v.ID == null ? false : true
                                  };

            var funQueryable = from f in IMS_TB_ModuleFunction
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

            foreach (var root in rootModuleList)
            {
                var modeNode = new TreeModel();
                modeNode.id = root.ModuleID;
                modeNode.text = root.ModuleName;
                tm.children.Add(modeNode);
                tm.attributes = new Dictionary<string, int>();
                tm.attributes.Add("functionid", -1);
                tm.attributes.Add("moduleid", root.ModuleID);

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
                    cNode.attributes.Add("functionid", -1);
                    cNode.attributes.Add("moduleid", cm.ModuleID);

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
                        fun.attributes.Add("functionid", f.FunctionID);
                        fun.attributes.Add("moduleid", -1);
                        cNode.children.Add(fun);
                    }
                }
            }

            return data;
        }

        public int SavePermission(bool check, int moduleid, int functionid, string roleid)
        {
            var role = IMS_TB_RoleInfo.Where(n => n.RoleID == roleid);
            int effectRowCount = 0;

            if (role == null || role.Count() == 0)
                return effectRowCount;


            var permissionQueryable = IMS_TB_Permission.Where(
                n => n.RoleId == roleid);

            if (moduleid != -1)
                permissionQueryable = permissionQueryable.Where(
                    n => n.ModuleID == moduleid);

            if (functionid != -1)
                permissionQueryable = permissionQueryable.Where(
                    n => n.FunctionID == functionid);


            var data = permissionQueryable.ToList();

            // 选中且数据表中数据不存在
            if (check && (data == null || data.Count == 0))
            {
                // 新增数据
                var ent = new IMS_TB_Permission()
                {
                    RoleId = roleid,
                    ModuleID = moduleid,
                    FunctionID = functionid,
                    LastModDate = System.DateTime.Now
                };

                DataLayerBase.Insert(this,ent);
                effectRowCount++;
            }
            // 去除选中找到数据
            else if (!check && data != null && data.Count > 0)
            {
                // 找到的数据全删除
                foreach (var d in data)
                {
                    DataLayerBase.Delete(this, d);
                    effectRowCount++;
                }
            }

            return effectRowCount;
        }
    }
}
