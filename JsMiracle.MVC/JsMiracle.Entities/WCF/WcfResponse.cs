using JsMiracle.Entities.Enum;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.Framework;
using JsMiracle.Framework.Serialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.Entities.WCF
{
 
    public class WcfResponse
    {
        //public WcfResponse()
        //    : this(SerializedType.Json)
        //{ }

        public WcfResponse()
        {
            Head = new WcfResponseHeader();
            Body = new WcfResponseBody();
        }

        public WcfResponseHeader Head { get; set; }

        public WcfResponseBody Body { get; set; }


    }

    public class WcfResponseHeader
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public int Return { get; set; }
    }


    public class WcfResponseBody
    {
        //public WcfResponseBody()
        //    : this(SerializedType.Json)
        //{
        //}

        //public WcfResponseBody(SerializedType serType)
        //{
        //    DataSerializedType = serType;
        //}

        /// <summary>
        /// 数据序列化类型
        /// </summary>
        //public SerializedType DataSerializedType { get; set; }
        public object Data { get; set; }

        //public void SetBody<T>(T t)
        //{
        //    //Data = XmlSerialization.SerializeString(t);
        //    Data = JsonSerialization.Serialize(t);

        //    switch (DataSerializedType)
        //    {
        //        case SerializedType.Json:
        //            Data = JsonSerialization.Serialize(t);
        //            break;
        //        case SerializedType.Xml:
        //            Data = XmlSerialization.SerializeString(t);
        //            break;

        //        default:
        //            throw new JsMiracleException("未处理的序列化类型" + DataSerializedType.ToString());
        //    }
        //}

        //public T GetBody<T>()
        //{
        //    //return XmlSerialization.DeserializeString<T>(Data);
        //    //return JsonSerialization.Deserialize<T>(Data);

        //    T data;

        //    switch (DataSerializedType)
        //    {
        //        case SerializedType.Json:
        //            data = JsonSerialization.Deserialize<T>(Data);
        //            break;
        //        case SerializedType.Xml:
        //            data = XmlSerialization.DeserializeString<T>(Data);
        //            break;

        //        default:
        //            throw new JsMiracleException("未处理的序列化类型" + DataSerializedType.ToString());
        //    }

        //    return data;
        //}
    }

}
