using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Entities.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMiracle.WCF.WT.IWorkTasks
{
    /// <summary>
    /// 用于WCF序列化为值的类型数组
    /// </summary>
    public static partial class TasksKnownTypesProvider
    {
        static Type[] GetKnownTypes(ICustomAttributeProvider knownTypeAttributeTarget)
        {
            //Type contractType = (Type)knownTypeAttributeTarget;
            //return contractType.GetGenericArguments();
            return new Type[] { 
                typeof(object[]) 
                ,typeof(IMS_WT_BYRW)
                ,typeof(IMS_WT_DZRW)
                ,typeof(IMS_WT_CWRW)
                ,typeof(IMS_WT_XTJD)
                ,typeof(IMS_WT_YWRW)
                ,typeof(IMS_WT_ZXLJ)
                ,typeof(IMS_WT_ZXZ)

                ,typeof(List<IMS_WT_BYRW>)
                ,typeof(List<IMS_WT_DZRW>)
                ,typeof(List<IMS_WT_CWRW>)
                ,typeof(List<IMS_WT_XTJD>)
                ,typeof(List<IMS_WT_YWRW>)
                ,typeof(List<IMS_WT_ZXLJ>)
                ,typeof(List<IMS_WT_ZXZ>)
            };
        }
    }
}
