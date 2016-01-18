using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMiracle.WCF.IBusinessOperations
{
    public static partial class BusinessKnownTypesProvider
    {
        static Type[] GetKnownTypes(ICustomAttributeProvider knownTypeAttributeTarget)
        {
            //Type contractType = (Type)knownTypeAttributeTarget;
            //return contractType.GetGenericArguments();
            return new Type[] { 
                typeof(object[]) 
                ,typeof(EnumHostSystemDocumentType)
                ,typeof(EnumBusinessType)
                ,typeof(IMS_VC_YWYS)
                ,typeof(IMS_VC_DJT)
                ,typeof(IMS_VC_DJH)
                ,typeof(List<IMS_VC_DJT>)
                ,typeof(List<IMS_VC_DJH>)
                ,typeof(List<IMS_VC_YWYS>)
            };
        }
    }
}
