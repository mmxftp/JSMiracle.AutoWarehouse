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

namespace JsMiracle.WCF.VC.IOrderForm
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
                ,typeof(EnumHostSystemDocumentType)
                ,typeof(EnumBusinessType)
                ,typeof(EnumOrderFormState)
                ,typeof(EnumOrderDataState)
                ,typeof(IMS_VC_YWYS)
                ,typeof(IMS_VC_DJT)
                ,typeof(IMS_VC_DJH)
                ,typeof(V_IMS_VC_DJH)
                ,typeof(List<IMS_VC_DJT>)
                ,typeof(List<IMS_VC_DJH>)
                ,typeof(List<IMS_VC_YWYS>)
                ,typeof(List<V_IMS_VC_DJH>)
            };
        }
    }
}
