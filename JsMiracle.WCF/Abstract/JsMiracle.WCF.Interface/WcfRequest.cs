using JsMiracle.Framework.Serialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.Interface
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
        public string Parameters { get; set; }

        public void SetParameters<T>(T t)
        {
            Parameters = XmlSerialization.SerializeString(t);
        }

        public T GetParameters<T>()
        {
            return XmlSerialization.DeserializeString<T>(Parameters);
        }
    }
}
