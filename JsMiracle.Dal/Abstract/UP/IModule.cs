using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IModule : IDataLayer<IMS_UP_Modu>
    {        

        IList<IMS_UP_Modu> GetRootModule();

        //IList<IMS_UP_Modu> GetChildModuleList(int pageIndex,
        //    int pageSize, int parentid, out int totalCount);

        //IMS_UP_Modu Save(IMS_UP_Modu module);

        //int Remove(int id);

        /// <summary>
        /// 得到模块实例
        /// </summary>
        /// <param name="moduleid">模块编号不可重覆</param>
        /// <returns></returns>
        IMS_UP_Modu GetEntityByModuleID(int moduleid);

        IList<IMS_UP_Modu> GetChildModuleList(int parentid);
    }
}
