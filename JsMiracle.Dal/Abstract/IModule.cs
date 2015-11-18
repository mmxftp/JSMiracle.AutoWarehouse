using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Abstract
{
    public interface IModule : IDataLayer<IMS_TB_Module>
    {        

        IList<IMS_TB_Module> GetRootModule();

        //IList<IMS_TB_Module> GetChildModuleList(int pageIndex,
        //    int pageSize, int parentid, out int totalCount);

        //IMS_TB_Module Save(IMS_TB_Module module);

        //int Remove(int id);

        /// <summary>
        /// 得到模块实例
        /// </summary>
        /// <param name="moduleid">模块编号不可重覆</param>
        /// <returns></returns>
        IMS_TB_Module GetEntityByModuleID(int moduleid);

        IList<IMS_TB_Module> GetChildModuleList(int parentid);
    }
}
