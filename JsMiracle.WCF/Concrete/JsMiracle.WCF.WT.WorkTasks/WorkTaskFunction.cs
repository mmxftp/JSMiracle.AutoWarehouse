using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.WT.WorkTasks
{
    public class WorkTaskFunction
    {
        /// <summary>
        /// 工作任务编号生成方法
        /// </summary>
        /// <returns></returns>
        public static string CreateNextTaskID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
