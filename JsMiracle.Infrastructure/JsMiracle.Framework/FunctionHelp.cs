using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Framework
{
    public static class FunctionHelp
    {
        /// <summary>
        /// 根据枚举的数值得到枚举类型的内容
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">数值</param>
        /// <returns>枚举值</returns>
        public static T GetEnum<T>(long value) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }


        /// <summary>
        /// 根据枚举的数值得到枚举类型的内容
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">枚举值字符串</param>
        /// <returns>枚举值</returns>
        public static T GetEnum<T>(string value) where T : struct
        {
            T enumValue;
            if (Enum.TryParse<T>(value, true, out enumValue))
                return enumValue;

            return default(T);
        }
    }
}
