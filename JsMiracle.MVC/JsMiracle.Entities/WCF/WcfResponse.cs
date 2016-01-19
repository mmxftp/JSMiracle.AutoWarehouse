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
        public object Data { get; set; }
    }

}
