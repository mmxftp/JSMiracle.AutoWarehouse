using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.View
{
    /// <summary>
    /// 表字段信息
    /// </summary>
    public class TableColumnsModule
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段注释
        /// </summary>
        public string ColumnNote { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }
    }
}
