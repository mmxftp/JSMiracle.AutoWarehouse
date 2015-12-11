using JsMiracle.Dal.Abstract.CM;
using JsMiracle.Entities;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
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

        public IList<IMS_CM_DM> GetCodeList(CodeTypeEnum codeType )
        {
            if (codeType == CodeTypeEnum.None)
                throw new JsMiracleException("codeType不得为空");

            var codeTypeStr = codeType.ToString();

            Expression<Func<IMS_CM_DM, bool>> filter
                = f => f.LXDM.Equals(codeTypeStr, StringComparison.CurrentCultureIgnoreCase);

            return this.GetAllEntites(filter);
        }


    }
}
