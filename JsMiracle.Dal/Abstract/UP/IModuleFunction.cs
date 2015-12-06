using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    public interface IModuleFunction : IDataLayer<IMS_UP_MKGN>
    {
        IList<IMS_UP_MKGN> GetModuleFunctionList(int moduleid);

        //IMS_UP_MoFn GetEntity(int id);


        //IMS_UP_MoFn Save(IMS_UP_MoFn module);

        //int Remove(int id);
    }
}
