
using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.CM.ICommonMng;
using JsMiracle.WCF.Interface;
using JsMiracle.WCF.WcfBaseService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace JsMiracle.WCF.CM.CommonMng
{
    public class IMS_CM_DXXX_Dal : WcfDataLayerBase<IMS_CM_DXXX>, IObjectDataDictionary
    {

        public void ReSaveObjectData(string tablename, string opUser)
        {
            tablename = tablename.ToUpperInvariant();
            var columns = GetColumns(tablename);

            using (var tran = base.DbContext.Database.BeginTransaction())
            {
                try
                {
                    DeleteObjectData(tablename);
                    foreach (var col in columns)
                    {
                        var ent = new IMS_CM_DXXX()
                        {
                            DXDM = tablename,
                            DXZD = col.ColumnName,
                            ZDMC = col.ColumnNote,
                            XGR = opUser,
                            XGRQ = System.DateTime.Now,
                            ZDLX = col.ColumnType,
                        };
                        base.DbContext.IMS_CM_DXXX_S.Add(ent);
                    }
                    base.DbContext.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 删除对象对应的数据
        /// </summary>
        /// <param name="tablename">表名</param>
        private void DeleteObjectData(string tablename)
        {
            var ent = base.DbContext.IMS_CM_DM_S.Where(n => n.DM.Equals(tablename, StringComparison.CurrentCultureIgnoreCase)
                       && n.LXDM == CodeTypeEnum.TableName.ToString()).FirstOrDefault();


            if (ent != null)
            {
                if (base.DbContext.IMS_CM_YHDX_S.Any(n => n.DXDM == ent.DM))
                    throw new JsMiracleException("对应信息已被用户表(IMS_CM_YHDX_S)使用中不得删除");

                string filter = string.Format(" DXDM = \"{0}\" ", ent.DM);

                //var data =
                //    this.GetAllEntites(n => n.DXDM.Equals(ent.DM, StringComparison.CurrentCultureIgnoreCase));
                var data = this.GetAllEntitesByFilter(filter);

                base.DbContext.IMS_CM_DXXX_S.RemoveRange(data);
                //base.DbContext.SaveChanges();
            }
            else
                throw new JsMiracleException("表未定义，请先在IMS_CM_DM_S表中定义表名称");
        }



        private IList<TableColumnsModule> GetColumns(string tablename)
        {
            var sql = @"select c.name as columnName,p.value as columnNote, tp.name as columnType
                        from sys.tables t 
                        join sys.columns c on t.object_id = c.object_id 
                        left join sys.extended_properties p on p.major_id = c.object_id 
                                AND p.minor_id = c.column_id 
						left join systypes tp on c.system_type_id = tp.xtype
                        where t.name = @tablename
                            and tp.status = 0 ";

            var par = new SqlParameter()
            {
                ParameterName = "@tablename",
                Value = tablename,
            };

            var query = base.DbContext.Database.SqlQuery<TableColumnsModule>(sql, par);
            return query.ToList();
        }


    }


    public class IMS_CM_DXXX_WCF : WcfService<IMS_CM_DXXX>, IWcfObjectDataDictionary
    {
        IMS_CM_DXXX_Dal dal = new IMS_CM_DXXX_Dal();

        protected override WcfResponse RequestFun(WcfRequest req)
        {
            WcfResponse res = new WcfResponse();

            object[] objs;
            switch (req.Head.RequestMethodName)
            {
                case "ReSaveObjectData":
                    objs = (object[])req.Body.Parameters;
                    dal.ReSaveObjectData((string)objs[0], (string)objs[1]);
                    break;

                default:
                    return null;
            }

            res.Head.IsSuccess = true;
            return res;
        }

        protected override IDataLayer<IMS_CM_DXXX> DataLayer
        {
            get { return dal; }
        }
    }
}
