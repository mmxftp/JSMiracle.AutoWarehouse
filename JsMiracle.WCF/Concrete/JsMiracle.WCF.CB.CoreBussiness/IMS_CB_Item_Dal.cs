
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsMiracle.Framework;
using System.Linq.Dynamic;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.CB.ICoreBussiness;
using JsMiracle.WCF.WcfBaseService;
using JsMiracle.WCF.Interface;
using JsMiracle.Entities.WCF;

namespace JsMiracle.WCF.CB.CoreBussiness
{
    /// <summary>
    /// 物料
    /// </summary>
    public class IMS_CB_Item_Dal : WcfDataLayerBase<IMS_CB_WL>, IItem
    {
        public IMS_CB_WL GetEntityByWXBH(string wlbh)
        {
            return this.DbContext.IMS_CB_WL_S.Where(n => n.WLBH.Equals(wlbh)).FirstOrDefault();
        }


        public override List<IMS_CB_WL> GetDataByPageDynamic(int intPageIndex, int intPageSize, out int rowCount, string orderBy, string where, params object[] whereParams)
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
                IMS_CB_WL item = data.item;
                item.WLTypeName = data.MC;
                itemList.Add(item);
            }

            return itemList;
        }


        public List<IMS_CB_WL> GetAllList()
        {
            var t = this.DbContext.IMS_CB_WL_S.ToList().Select(
                n => new IMS_CB_WL { ID = n.ID, WLBH = n.WLBH, WLMC = n.WLMC });
            var data = t.ToList();
            return data;
        }
    }


    public class IMS_CB_Item_WCF : WcfDataServiceBase<IMS_CB_WL>, IWcfItem
    {
        IMS_CB_Item_Dal dal = new IMS_CB_Item_Dal();


        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();

            object[] objs;

            switch (req.Head.RequestMethodName)
            {
                case "GetEntityByWXBH":
                    objs = (object[])req.Body.Parameters;
                    res.Body.Data = dal.GetEntityByWXBH((string)objs[0]);
                    break;
                case "GetAllList":
                    res.Body.Data = dal.GetAllList();
                    break;

                default:
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }

        protected override IDataLayer<IMS_CB_WL> DataLayer
        {
            get { return dal; }
        }
    }
}
