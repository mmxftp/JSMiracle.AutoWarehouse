using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Entities.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMiracle.Wcf.VC.IOrderForm
{
    /// <summary>
    /// 用于WCF序列化为值的类型数组
    /// </summary>
    public static partial class OrderKnownTypesProvider
    {
        static Type[] GetKnownTypes(ICustomAttributeProvider knownTypeAttributeTarget)
        {
            //Type contractType = (Type)knownTypeAttributeTarget;
            //return contractType.GetGenericArguments();
            return new Type[] { 
                typeof(object[]) 
                ,typeof(IMS_VC_DJT)
                ,typeof(IMS_VC_DJH)
                ,typeof(List<IMS_VC_DJT>)
                ,typeof(List<IMS_VC_DJH>)
            };
        }
    }
}
