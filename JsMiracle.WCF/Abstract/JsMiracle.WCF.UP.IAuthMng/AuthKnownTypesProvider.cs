using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Entities.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMiracle.WCF.UP.IAuthMng
{
    /// <summary>
    /// 用于WCF序列化为值的类型数组
    /// </summary>
    public static partial class AuthKnownTypesProvider
    {
        static Type[] GetKnownTypes(ICustomAttributeProvider knownTypeAttributeTarget)
        {
            //Type contractType = (Type)knownTypeAttributeTarget;
            //return contractType.GetGenericArguments();
            return new Type[] { 
                typeof(object[])
                ,typeof(PermissionViewModule)
                ,typeof(List<TreeModel>)
                ,typeof(TreeAttr)
                ,typeof(TreeModel)
                ,typeof(IMS_UP_YH)
                ,typeof(IMS_UP_JS)
                ,typeof(IMS_UP_JSMK)
                ,typeof(IMS_UP_MKGN)                
                ,typeof(IMS_UP_MK)
                ,typeof(List<IMS_UP_YH>)
                ,typeof(List<IMS_UP_JS>)
                ,typeof(List<IMS_UP_JSMK>)
                ,typeof(List<IMS_UP_MKGN>)
                ,typeof(List<IMS_UP_MK>)           
            };
        }
    }
}
