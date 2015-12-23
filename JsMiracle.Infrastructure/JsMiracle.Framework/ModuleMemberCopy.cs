using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMiracle.Framework
{

    public static class ModuleMemberCopy
    {
        /// <summary>
        /// 同类型数据赋值
        /// </summary>
        /// <param name="sourceData">数据源</param>
        /// <param name="targetData">需要设置值的对象</param> 
        /// <param name="copyCollection">是否copy集合类型(如是要序列化显示的实体,最好不要copy,会死循环的)</param>
        public static void SameValueCopier(object sourceData, object targetData, bool copyCollection = true)
        {

            PropertyInfo[] propertyInfoList = targetData.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                if (propertyInfo.MemberType != MemberTypes.Property)
                    continue;

                if (!copyCollection
                    && (propertyInfo.PropertyType.GetInterfaces().Any(n => n.Name == typeof(IEnumerable).Name))
                    && (propertyInfo.PropertyType != typeof(string)))
                    continue;

                // 只处理propertyInfo.MemberType == MemberTypes.Property 的情况
                string propertyName = propertyInfo.Name;
                object value = sourceData.GetType().GetProperty(propertyName).GetValue(sourceData, null);

                targetData.GetType().GetProperty(propertyName).SetValue(targetData, value, null);

            }
        }


        /// <summary>
        /// 不同类型数据赋值
        /// </summary>
        /// <param name="sourceData">数据源</param>
        /// <param name="targetData">需要设置值的对象</param> 
        public static void DiffValueCopier(object sourceData, object targetData)
        {

            PropertyInfo[] propertyInfoSetterList = targetData.GetType().GetProperties();
            PropertyInfo[] propertyInfoGetterList = targetData.GetType().GetProperties();

            foreach (PropertyInfo dataSetterPropertyInfo in propertyInfoSetterList)
            {
                if (dataSetterPropertyInfo.MemberType != MemberTypes.Property)
                    continue;

                // 只处理propertyInfo.MemberType == MemberTypes.Property 的情况

                string dataSetterPropertyName = dataSetterPropertyInfo.Name;

                foreach (PropertyInfo dataGetterPropertyInfo in propertyInfoGetterList)
                {
                    string dataGetterPropertyName = dataGetterPropertyInfo.Name;
                    object value = sourceData.GetType().GetProperty(dataGetterPropertyName).GetValue(sourceData, null);
                    if (dataSetterPropertyName != dataGetterPropertyName)
                        continue;

                    // 只处理属性名称相同的情况
                    targetData.GetType().GetProperty(dataSetterPropertyName).SetValue(targetData, value, null);
                    break;
                }

            } // PropertyInfo foreach end 
        }
    }

}
