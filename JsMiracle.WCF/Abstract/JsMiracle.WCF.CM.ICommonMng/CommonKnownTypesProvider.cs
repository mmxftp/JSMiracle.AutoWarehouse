using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Entities.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMiracle.WCF.CM.ICommonMng
{
    /// <summary>
    /// 用于WCF序列化为值的类型数组
    /// </summary>
    public static partial class CommonKnownTypesProvider
    {
        static Type[] GetKnownTypes(ICustomAttributeProvider knownTypeAttributeTarget)
        {
            //Type contractType = (Type)knownTypeAttributeTarget;
            //return contractType.GetGenericArguments();
            return new Type[] { 
                typeof(object[])
                ,typeof(CodeTypeEnum)
                ,typeof(IMS_CM_DM)
                ,typeof(IMS_CM_DMLX)
                ,typeof(IMS_CM_DXXX)
                ,typeof(IMS_CM_YHDX)      
                ,typeof(List<IMS_CM_DM>)
                ,typeof(List<IMS_CM_DMLX>)
                ,typeof(List<IMS_CM_DXXX>)
                ,typeof(List<IMS_CM_YHDX>)  
            };
        }
    }
}
