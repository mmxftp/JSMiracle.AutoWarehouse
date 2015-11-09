using JsMiracle.Dal.Abstract;
using JsMiracle.Dal.Models;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_TB_Permission_Dal : DataLayerBase<IMS_TB_Permission>
    {
        //public IMS_TB_Permission_Dal(IIMS_ORGEntities context)
        //    : base(context)
        //{
        //}

//        public List<PermissionModel> GetPermission(int roleid)
//        {
//            string sqlpermission = @"select m.ModuleID,m.ModuleName,m.ParentID,f.FunctionID,f.FunctionName 
//                            ,cast(case isnull(p.id,0) when 0 then 0 else 1 end as bit) as  ModulePermission  
//                            ,cast(case isnull(p1.id,0) when 0 then 0 else 1 end as bit) as  FunctionPermission 
//                            from IMS_TB_Module m 
//                            left join IMS_TB_Permission  p 
//	                            on p.ModuleID = m.moduleid and p.FunctionID is null and p.roleid= @rid 
//                            left join IMS_TB_ModuleFunction f on m.ModuleID = f.ModuleID 
//                            left join IMS_TB_Permission p1 on p1.FunctionID = f.FunctionID and p.roleid = @rid 
//                            where m.parentid!=-1";

//            var args = new DbParameter[] { 
//                new SqlParameter { ParameterName = "rid", Value = roleid }
//            };

//            var datas = base.objContext.ExecuteStoreQuery<PermissionModel>(sqlpermission
//                , args).ToList();

//            //var x = dal.ObjContext.CreateQuery(sqlpermission, new ObjectParameter("rid", roleid)).ToList();

//            string sqlRootModule = @"select moduleid,modulename,parentid 
//                                    from IMS_TB_Module where parentid = -1
//                                    order by sortid ";

//            var rootModuleList = base.objContext.ExecuteStoreQuery<PermissionModel>(sqlRootModule).ToList();

//            List<PermissionModel> pModuleList = new List<PermissionModel>();

//            foreach (var m in rootModuleList)
//            {
//                var x = from b in datas
//                        where b.Parentid == m.ModuleID
//                        select b;

//                pModuleList.Add(m);
//                pModuleList.AddRange(x);
//            }
//            return pModuleList;
//        }
    }
}
