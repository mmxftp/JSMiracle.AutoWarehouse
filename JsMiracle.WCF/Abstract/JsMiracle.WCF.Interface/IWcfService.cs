using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.WCF.Interface
{
    [ServiceContract]
    public interface IWcfService 
    {
        [OperationContract]
        WcfResponse Request(WcfRequest req);
    }

    //[ServiceContract]
    //public interface IWcfBaseService
    //{
    //    [OperationContract]
    //    WcfResponse Request(WcfRequest req);
    //}
}
