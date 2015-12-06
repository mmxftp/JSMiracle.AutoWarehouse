using JsMiracle.Dal.Abstract.CB;
using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete.CB
{
    public class IMS_CB_Location_Dal : DataLayerBase<IMS_CB_WZ>, ILocation
    {
        public IMS_CB_Location_Dal() : base(true) { }

        #region protected Method

        protected override string GetKeyValue(IMS_CB_WZ entity)
        {
            return entity.WXBH;
        }

        protected override IMS_CB_WZ GetOldEntity(IMS_CB_WZ entity)
        {
            return GetEntity(entity.WXBH);
        }

        protected override bool IsAddEntity(IMS_CB_WZ entity)
        {
            return entity.ID == 0;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        /// <exception cref="JsMiracleException"></exception>
        public int InitLocation(int x, int y, int z)
        {
            var rowCount = this.DbContext.IMS_CB_WZ_S.Count();
            if (rowCount > 0)
                throw new JsMiracleException("位置信息已存在不得初始化操作.");

            return 0;
        }


        public DataTable GetLocationState(int p, int maxL)
        {
            //             select FWZT,l,c,wxbh,rq.ID from ims_cb_wz_s wz 
            //left join IMS_CB_RQ_S rq on wz.WXBH = rq.DQWZ 
            //where p = 10
            //order by c desc,l

            //base.DbContext.Database.SqlQuery 


            var data = from wz in base.DbContext.IMS_CB_WZ_S
                       join rq in base.DbContext.IMS_CB_RQ_S
                       on wz.WXBH equals rq.DQWZ into v_temp
                       from v in v_temp.DefaultIfEmpty()
                       where wz.P == p
                       orderby wz.C descending, wz.L
                       select new
                       {
                           wz.WXBH,
                           wz.L,
                           wz.C,
                           ExistsItem = v.ID != null
                       };

            DataTable dt = new DataTable();
            var dc = new DataColumn[maxL];
            for (int i = 0; i < maxL; i++)
            {
                dc[i] = new DataColumn((i + 1).ToString(), typeof(string));
                dc[i].DefaultValue = "";
            }

            dt.Columns.AddRange(dc);

            //int iBays = maxL;
            int iCol = 0;

            //定义一个与所要填充的表格式一样的行
            DataRow dr = dt.NewRow();

            //便历dataRows中的所有行为表中一行的数据
            foreach (var dataRow in data)
            {
                //将格位数转换成数组的位置
                iCol = dataRow.L;

                //行中每一格的值（数组从零开始,iCol从1开始）
                dr[iCol - 1] = dataRow.ExistsItem;

                if (iCol == maxL)
                {
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
            }


            return dt;
        }
    }
}
