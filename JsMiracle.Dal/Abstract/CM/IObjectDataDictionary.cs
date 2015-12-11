using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.CM
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectDataDictionary : IDataLayer<IMS_CM_DXXX>
    {
        ///// <summary>
        ///// 根据表名得字段名
        ///// </summary>
        ///// <param name="tablename">表名</param>
        ///// <returns></returns>
        //IList<TableColumnsModule> GetColumns(string tablename);

        /// <summary>
        /// 保存表数据到对象表中
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="opUser">操作用户</param>
        void ReSaveObjectData(string tablename, string opUser);

        ///// <summary>
        ///// 删除对象代码对应的所有数据
        ///// </summary>
        ///// <param name="dxdm">对象代码</param>
        ///// <exception cref="JsMiracleException">对象已被使用不得删除</exception>
        //void DeleteObjectData(string dxdm);

    }
}
