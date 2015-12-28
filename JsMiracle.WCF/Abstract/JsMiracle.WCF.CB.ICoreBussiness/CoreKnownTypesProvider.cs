using JsMiracle.Entities.EasyUI_Model;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Entities.View;
using JsMiracle.Entities.WCF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMiracle.WCF.CB.ICoreBussiness
{
    /// <summary>
    /// 用于WCF序列化为值的类型数组
    /// </summary>
    public static partial class CoreKnownTypesProvider
    {
        static Type[] GetKnownTypes(ICustomAttributeProvider knownTypeAttributeTarget)
        {
            //Type contractType = (Type)knownTypeAttributeTarget;
            //return contractType.GetGenericArguments();
            return new Type[] { 
                typeof(object[])           
                ,typeof(DataTable)
                ,typeof(InventoryPositionModule)
                ,typeof(IMS_CB_RQ)
                ,typeof(IMS_CB_RQLX)
                ,typeof(IMS_CB_WL)
                ,typeof(IMS_CB_WZ)                
                ,typeof(IMS_CB_WZGX)
                ,typeof(IMS_CB_WZLX)
                ,typeof(List<IMS_CB_RQ>)
                ,typeof(List<IMS_CB_RQLX>)
                ,typeof(List<IMS_CB_WL>)
                ,typeof(List<IMS_CB_WZ>)
                ,typeof(List<IMS_CB_WZGX>)           
                ,typeof(List<IMS_CB_WZLX>)
            };
        }
    }
}
