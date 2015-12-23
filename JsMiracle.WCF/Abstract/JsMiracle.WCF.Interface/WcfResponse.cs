using JsMiracle.Framework.Serialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.Interface
{
    public class WcfResponse
    {
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
        public string Data { get; set; }

        public void SetBody<T>(T t)
        {
            Data = XmlSerialization.SerializeString(t);
        }

        public T GetBody<T>()
        {
            return XmlSerialization.DeserializeString<T>(Data);
        }
    }

}
