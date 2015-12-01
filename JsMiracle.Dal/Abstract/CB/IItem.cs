using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.CB
{

    /// <summary>
    /// 处理物料信息的接口
    /// </summary>
    public interface IItem : IDataLayer<IMS_CB_WL>
    {
        /// <summary>
        /// 根据物料编号得到物料, 没有数据返回null
        /// </summary>
        /// <param name="wlbh">完整的物料编号</param>
        /// <returns></returns>
        IMS_CB_WL GetEntity(string wlbh);
    }
}
