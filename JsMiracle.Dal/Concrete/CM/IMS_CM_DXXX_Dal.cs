using JsMiracle.Dal.Abstract.CM;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace JsMiracle.Dal.Concrete.CM
{
    public class IMS_CM_DXXX_Dal : DataLayerBase<IMS_CM_DXXX>, IObjectDataDictionary
    {

        public void ReSaveObjectData(string tablename, string opUser)
        {
            var dxdm = "";
            DeleteObjectData(tablename, out dxdm);

            var columns = GetColumns(tablename);

            using (var tran = base.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var col in columns)
                    {
                        var ent = new IMS_CM_DXXX()
                        {
                            DXDM = dxdm,
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
        /// <param name="dxdm">对象代码</param>
        private void DeleteObjectData(string tablename, out string dxdm)
        {
            var ent = base.DbContext.IMS_CM_DM_S.Where(n => n.MC.Equals(tablename)
                       && n.LXDM == CodeTypeEnum.TableName.ToString()).FirstOrDefault();

            dxdm = "";

            if (ent != null)
            {
                dxdm = ent.DM;

                if (base.DbContext.IMS_CM_YHDX_S.Any(n => n.DXDM == ent.DM))
                    throw new JsMiracleException("对应信息已被用户表(IMS_CM_YHDX_S)使用中不得删除");

                var data =
                    this.GetAllEntites(n => n.DXDM.Equals(ent.DM, StringComparison.CurrentCultureIgnoreCase));

                base.DbContext.IMS_CM_DXXX_S.RemoveRange(data);
                base.DbContext.SaveChanges();
            }
        }



        private IList<TableColumnsModule> GetColumns(string tablename)
        {
            var sql = @"select c.name as columnName,p.value as columnNote, tp.name as columnType from sys.tables t 
                        join sys.columns c on t.object_id = c.object_id 
                        left join sys.extended_properties p on p.major_id = c.object_id 
                                AND p.minor_id = c.column_id 
						left join systypes tp on c.system_type_id = tp.xtype
                        where t.name = @tablename ";

            var par = new SqlParameter()
            {
                ParameterName = "@tablename",
                Value = tablename,
            };

            var query = base.DbContext.Database.SqlQuery<TableColumnsModule>(sql, par);
            return query.ToList();
        }

  
    }
}
