using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IModule : IDataLayer<IMS_UP_MK>
    {
        /// <summary>
        /// 得所有根节点信息
        /// </summary>
        /// <returns></returns>
        IList<IMS_UP_MK> GetRootModule();

        /// <summary>
        /// 得到模块实例
        /// </summary>
        /// <param name="moduleid">模块编号不可重覆</param>
        /// <returns></returns>
        IMS_UP_MK GetEntityByModuleID(int moduleid);

        /// <summary>
        /// 根据父节点得子节点信息
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        IList<IMS_UP_MK> GetChildModuleList(int parentid);
    }
}
