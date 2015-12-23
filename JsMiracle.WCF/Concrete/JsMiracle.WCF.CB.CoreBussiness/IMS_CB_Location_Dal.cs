using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using JsMiracle.WCF.CB.ICoreBussiness;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.CB.CoreBussiness
{
    public class IMS_CB_Location_Dal : WcfDataLayerBase<IMS_CB_WZ>, ILocation
    {
        static IMS_CB_Location_Dal()
        {
            maxPosition = new InventoryPositionModule();
            using (var db = new Entities.TabelEntities.ORGModels())
            {
                maxPosition.MaxP = db.IMS_CB_WZ_S.Max(n => n.P);
                maxPosition.MaxL = db.IMS_CB_WZ_S.Max(n => n.L);
                maxPosition.MaxC = db.IMS_CB_WZ_S.Max(n => n.C);
            }
        }
        static readonly InventoryPositionModule maxPosition;

  

        #region protected Method

        protected override string GetKeyValue(IMS_CB_WZ entity)
        {
            return entity.WXBH;
        }

        protected override IMS_CB_WZ GetOldEntity(IMS_CB_WZ entity)
        {
            return FindEntityByKey(entity.WXBH);
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
        //public int InitLocation(int x, int y, int z)
        //{
        //    var rowCount = this.DbContext.IMS_CB_WZ_S.Count();
        //    if (rowCount > 0)
        //        throw new JsMiracleException("位置信息已存在不得初始化操作.");

        //    return 0;
        //}

        public DataTable GetLocationState(int p)
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
                           wz,
                           ExistsItem = v.RQBH != null
                       };

            DataTable dt = new DataTable();

            // 最大列为 层号 加 物理层号
            var dataColNumber = maxPosition.MaxL + 1;

            var dc = new DataColumn[dataColNumber];
            for (int i = 0; i < maxPosition.MaxL; i++)
            {
                dc[i] = new DataColumn((i + 1).ToString(), typeof(IMS_CB_WZ));
            }
            // 楼层列
            dc[maxPosition.MaxL] = new DataColumn("floor", typeof(string));
            dt.Columns.AddRange(dc);

            //int iBays = maxL;
            int iCol = 0;

            int floorNumber = maxPosition.MaxC;

            //定义一个与所要填充的表格式一样的行
            DataRow dr = dt.NewRow();

            //便历dataRows中的所有行为表中一行的数据
            foreach (var dataRow in data)
            {
                var wz = dataRow.wz;
                wz.ExistsItem = dataRow.ExistsItem;


                //将格位数转换成数组的位置
                iCol = wz.L;

                //行中每一格的值（数组从零开始,iCol从1开始）
                //dr[iCol - 1] = dataRow.ExistsItem;
                dr[iCol - 1] = wz;

                if (iCol == maxPosition.MaxL)
                {
                    dr["floor"] = floorNumber;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    floorNumber--;
                }
            }

            return dt;
        }

        //public int MaxP
        //{
        //    get { return maxP; }
        //}

        //public int MaxL
        //{
        //    get { return maxL; }
        //}

        //public int MaxC
        //{
        //    get { return maxC; }
        //}


        public InventoryPositionModule GetMaxPosition()
        {
            throw new NotImplementedException();
        }
    }
}
