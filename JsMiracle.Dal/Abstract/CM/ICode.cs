using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.CM
{
    public interface ICode : IDataLayer<IMS_CM_DM>
    {
        IList<IMS_CM_DM> GetCodeList(string codeType);
    }
}
