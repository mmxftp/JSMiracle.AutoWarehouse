using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IModuleFunction : IDataLayer<IMS_TB_ModuleFunction>
    {
        IList<IMS_TB_ModuleFunction> GetModuleFunctionList(int moduleid);

        //IMS_TB_ModuleFunction GetEntity(int id);


        //IMS_TB_ModuleFunction Save(IMS_TB_ModuleFunction module);

        //int Remove(int id);
    }
}
