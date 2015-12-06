using JsMiracle.Dal.Abstract.UP;
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete.UP
{
    public class IMS_UP_MoFn_Dal : DataLayerBase<IMS_UP_MKGN>, IModuleFunction
    {

        public IList<IMS_UP_MKGN> GetModuleFunctionList(int moduleid)
        {
            var data = this.DbContext.IMS_UP_MKGN_S.Where(n => n.MKID == moduleid);
            return data.ToList();
        }


        public override void SaveOrUpdate(IMS_UP_MKGN entity)
        {
            if (entity.GNID == 0)
            {
                //var itemCount = IMS_UP_MoFnT.Where(n => n.ModuleID == module.ModuleID).Count();
                // 得到同类的子项的记数加1
                //module.FunctionID = module.ModuleID * 100 + itemCount + 1;

                var moduleGroupNumber = entity.MKID * 100;

                var defaultFunctionID = moduleGroupNumber + 1;

                // 得到当前序号中断开的最小号码
                // 如:   1,2,3,4,6 , => 5
                //       1,2,3       => 4 
                var nextFunctionIDQueryable =
                    from a in this.DbContext.IMS_UP_MKGN_S.Where(n => n.MKID == entity.MKID)
                    join b in this.DbContext.IMS_UP_MKGN_S.Where(n => n.MKID == entity.MKID)
                    on (a.GNID + 1) % moduleGroupNumber + moduleGroupNumber equals b.GNID into left_Join
                    from v in left_Join.DefaultIfEmpty()
                    where v.ID == null && a.GNID >= defaultFunctionID
                    select a.GNID;

                if (nextFunctionIDQueryable.Count() > 0)
                    entity.GNID = (nextFunctionIDQueryable.Min() + 1) % moduleGroupNumber + moduleGroupNumber;
                else
                    entity.GNID = defaultFunctionID;
            }

            base.SaveOrUpdate(entity);
        }


    }
}
