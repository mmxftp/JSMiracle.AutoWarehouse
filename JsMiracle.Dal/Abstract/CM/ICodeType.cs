using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.CM
{
    public interface ICodeType : IDataLayer<IMS_CM_DMLX>
    {
        IMS_CM_DMLX GetEntity(string lxdm);
    }
}
