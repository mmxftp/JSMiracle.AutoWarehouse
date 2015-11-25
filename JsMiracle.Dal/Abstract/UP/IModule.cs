using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract.UP
{
    public interface IModule : IDataLayer<IMS_UP_MK>
    {

        IList<IMS_UP_MK> GetRootModule();

        //IList<IMS_UP_Modu> GetChildModuleList(int pageIndex,
        //    int pageSize, int parentid, out int totalCount);

        //IMS_UP_Modu Save(IMS_UP_Modu module);

        //int Remove(int id);

        /// <summary>
        /// 得到模块实例
        /// </summary>
        /// <param name="moduleid">模块编号不可重覆</param>
        /// <returns></returns>
        IMS_UP_MK GetEntityByModuleID(int moduleid);

        IList<IMS_UP_MK> GetChildModuleList(int parentid);
    }
}
