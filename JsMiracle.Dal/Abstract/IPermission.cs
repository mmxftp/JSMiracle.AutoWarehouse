using JsMiracle.Dal.Models;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IPermission:IDataLayer<IMS_TB_Permission>
    {
        List<PermissionModel> GetPermission(int roleid);



    }
}
