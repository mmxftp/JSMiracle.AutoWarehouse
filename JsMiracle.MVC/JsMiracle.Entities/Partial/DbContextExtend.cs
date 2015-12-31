using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace JsMiracle.Entities.TabelEntities
{
    public static class DbContextExtend{

        /// <summary>
        /// 扩展方法 根据 sql反回datatable 
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="sql">sql脚本</param>
        /// <param name="parameters">sql参数</param>
        /// <returns></returns>
        public static DataTable SqlQueryForDataTatable(
            this Database db,
            string sql,
            SqlParameter[] parameters)
        {
            var conn = db.Connection as SqlConnection;

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn ;
            cmd.CommandText = sql;

            if (parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }




}