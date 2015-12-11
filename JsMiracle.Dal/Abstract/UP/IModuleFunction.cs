using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    /// <summary>
    /// 模块功能信息
    /// </summary>
    public interface IModuleFunction : IDataLayer<IMS_UP_MKGN>
    {
        /// <summary>
        /// 根据模块号得功能信息
        /// </summary>
        /// <param name="moduleid">模块编号</param>
        /// <returns></returns>
        IList<IMS_UP_MKGN> GetModuleFunctionList(int moduleid);

    }
}
