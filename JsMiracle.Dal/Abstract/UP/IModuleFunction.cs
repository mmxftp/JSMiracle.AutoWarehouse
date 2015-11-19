using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IModuleFunction : IDataLayer<IMS_UP_MoFn>
    {
        IList<IMS_UP_MoFn> GetModuleFunctionList(int moduleid);

        //IMS_UP_MoFn GetEntity(int id);


        //IMS_UP_MoFn Save(IMS_UP_MoFn module);

        //int Remove(int id);
    }
}
