using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.CM
{
    /// <summary>
    /// 处理代码的接口
    /// </summary>
    public interface ICode : IDataLayer<IMS_CM_DM>
    {
        /// <summary>
        /// 得到代码类型对应的所有代码信息的集合
        /// </summary>
        /// <param name="codeType">代码类型jf</param>
        /// <returns></returns>
        IList<IMS_CM_DM> GetCodeList(string codeType);
    }
}
