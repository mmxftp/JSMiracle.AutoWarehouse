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

        /// <summary>
        /// 得到模块实例
        /// </summary>
        /// <param name="id">表主键</param>
        /// <param name="moduleid">模块编号不可重覆</param>
        /// <returns></returns>
        /// <exception cref="Exception">id 和moduleid 参数中必须有一个参数有值</exception>
        IMS_TB_Module GetEntity(int? id, int moduleid=-1);

        IList<IMS_TB_Module> GetChildModuleList(int parentid);
    }
}
