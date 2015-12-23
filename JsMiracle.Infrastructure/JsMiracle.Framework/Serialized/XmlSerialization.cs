using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JsMiracle.Framework.Serialized
{
    public static class XmlSerialization //: ISerialized
    {

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static T DeserializeString<T>(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(sr);
            }
        }

        public static T DeserializeFile<T>(string filepath)
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                var xmlSerializer = new  XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(reader);
            }
        }

        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string SerializeString<T>(T obj)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(sw, obj);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static void SerializeFile<T>(string filepath, T obj)
        {
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(sw, obj);
            }
        }
        #endregion
    }
}
