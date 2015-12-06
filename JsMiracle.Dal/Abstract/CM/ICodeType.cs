using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.CM
{
    /// <summary>
    /// 处理代码类型的接口
    /// </summary>
    public interface ICodeType : IDataLayer<IMS_CM_DMLX>
    {
        /// <summary>
        /// 根据代码类型编码得到代码类型
        /// </summary>
        /// <param name="lxdm">类型代码</param>
        /// <returns></returns>
        IMS_CM_DMLX GetEntity(string lxdm);
    }
}
