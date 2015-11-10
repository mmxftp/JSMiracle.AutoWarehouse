using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IModule
    {        

        IList<IMS_TB_Module> GetRootModule();

        IList<IMS_TB_Module> GetChildModuleList(int pageIndex,
            int pageSize, int parentid, out int totalCount);

        IMS_TB_Module Save(IMS_TB_Module module);

        int Remove(int id);

        IMS_TB_Module GetEntity(int id);

        IList<IMS_TB_Module> GetChildModuleList(int parentid);
    }
}
