using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    /// <summary>
    /// 角色信息接口
    /// </summary>
    public interface IRole : IDataLayer<IMS_UP_JS>
    {
        /// <summary>
        /// 得所有角色
        /// </summary>
        /// <returns></returns>
        IList<IMS_UP_JS> GetAllRole();

    }
}
