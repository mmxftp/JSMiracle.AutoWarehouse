using JsMiracle.Dal.Abstract.CB;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsMiracle.Framework;
using System.Linq.Dynamic;

namespace JsMiracle.Dal.Concrete.CB
{
    public class IMS_CB_Item_Dal : DataLayerBase<IMS_CB_WL>, IItem
    {
        public IMS_CB_WL GetEntity(string wlbh)
        {
            return this.DbContext.IMS_CB_WL_S.Where(n => n.WLBH.Equals(wlbh)).FirstOrDefault();
        }


        public override dynamic GetDataByPage(int intPageIndex, int intPageSize, out int rowCount, string orderBy, string where, params object[] whereParams)
        {
            rowCount = 0;

            int skipRows = (intPageIndex - 1) * intPageSize;

            var query = from item in this.DbContext.IMS_CB_WL_S
                        join code in this.DbContext.IMS_CM_DM_S
                        on new { key = item.WLLX, kv = IMS_CB_WL.ItemTypeName }
                        equals new { key = code.SZ, kv = code.LXDM } into v_wl
                        from v in v_wl.DefaultIfEmpty()
                        select new { item, v.MC };

            if (string.IsNullOrEmpty(where))
            {
                rowCount = query.Count();
                query = query.OrderBy(orderBy).Skip(skipRows).Take(intPageSize);
            }
            else
            {
                rowCount = query.Where(where, whereParams).Count();
                query = query.Where(where, whereParams).OrderBy(orderBy).Skip(skipRows).Take(intPageSize);
            }

            List<IMS_CB_WL> itemList = new List<IMS_CB_WL>();
            foreach (var data in query)
            {
                var item = data.item;
                item.WLTypeName = data.MC;
                itemList.Add(item);
            }

            return itemList;
        }


    }
}
