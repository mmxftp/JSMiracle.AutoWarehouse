using JsMiracle.Entities.Enum;
using JsMiracle.Framework;
using JsMiracle.Framework.Serialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.WCF
{

    public class WcfRequest
    {


        public WcfRequest()
        {
            Head = new WcfRequestHead();
            Body = new WcfRequestBody();
        }

        public WcfRequestHead Head { get; set; }

        public WcfRequestBody Body { get; set; }

    }

    public class WcfRequestHead
    {
        public string ClassName { get; set; }

        public string RequestMethodName { get; set; }
    }


    public class WcfRequestBody
    {
        //public WcfRequestBody()
        //    : this(SerializedType.Json)
        //{ }

        //public WcfRequestBody(SerializedType serType)
        //{
        //    DataSerializedType = serType;
        //}

        /// <summary>
        /// 数据序列化类型
        /// </summary>
        //public SerializedType DataSerializedType { get; set; }

        public object Parameters { get; set; }

        //public void SetParameters<T>(T t)
        //{
        //    switch (DataSerializedType)
        //    {
        //        case SerializedType.Json:
        //            Parameters = JsonSerialization.Serialize(t);
        //            break;
        //        case SerializedType.Xml:
        //            Parameters = XmlSerialization.SerializeString(t);
        //            break;

        //        default:
        //            throw new JsMiracleException("未处理的序列化类型" + DataSerializedType.ToString());
        //    }
        //}

        //public T GetParameters<T>()
        //{

        //    T data;

        //    switch (DataSerializedType)
        //    {
        //        case SerializedType.Json:
        //            data = JsonSerialization.Deserialize<T>(Parameters);
        //            break;
        //        case SerializedType.Xml:
        //            data = XmlSerialization.DeserializeString<T>(Parameters);
        //            break;

        //        default:
        //            throw new JsMiracleException("未处理的序列化类型" + DataSerializedType.ToString());
        //    }

        //    return data;
        //}
    }
}
