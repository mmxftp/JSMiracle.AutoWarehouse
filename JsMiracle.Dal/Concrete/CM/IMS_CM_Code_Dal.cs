using JsMiracle.Dal.Abstract.CM;
using JsMiracle.Entities;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JsMiracle.Dal.Concrete.CM
{
    public class IMS_CM_Code_Dal : DataLayerBase<IMS_CM_DM>, ICode
    {

        public IList<IMS_CM_DM> GetCodeList(string codeType)
        {
            if (string.IsNullOrEmpty(codeType))
                throw new JsMiracleException("codeType不得为空");

            Expression<Func<IMS_CM_DM, bool>> filter
                = f => f.LXDM.Equals(codeType, StringComparison.CurrentCultureIgnoreCase);

            return this.GetAllEntites(filter);
        }
    }
}
