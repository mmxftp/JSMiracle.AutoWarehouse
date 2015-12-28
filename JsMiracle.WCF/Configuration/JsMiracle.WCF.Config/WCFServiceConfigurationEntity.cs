using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Xml.Serialization;

namespace JsMiracle.WCF.Config
{
    [XmlRoot("configuration")]
    public class WCFServiceConfigurationEntity
    {
        [XmlArrayAttribute("Services")]
        public WcfService[] Services { get; set; }
    }


    [Serializable]
    public class WcfService
    {
        [XmlAttribute]
        public string ID { get; set; }

        public string ServiceID { get; set; }

        /// <summary>
        /// WCF 服务所在的文件
        /// </summary>
        public string ServiceLibraryFile { get; set; }

        /// <summary>
        /// WCF 接口所在的文件
        /// </summary>
        public string ClientLibraryFile { get; set; }

        /// <summary>
        /// 访问wcf地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 服务完整名称(命名空间 + 类名称)
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 接口完整名称 (命名空间 + 接口名称 )
        /// </summary>
        public string Contract { get; set; }

        public string BehaviorHttpGetUrl { get; set; }

        public EndpointAddress GetEndPointAddress()
        {
            return new EndpointAddress(Address);
        }

        private ServiceHost host;

        public void Open()
        {
            try
            {
                var interfaceTypes = Assembly.LoadFrom(ClientLibraryFile);
                var contract = interfaceTypes.GetType(Contract);

                var service = Assembly.LoadFrom(ServiceLibraryFile);
                var implTypes = service.GetType(ServiceName);

                Uri address = new Uri(Address);
                var bind = new NetTcpBinding();
                //var bind = MetadataExchangeBindings.CreateMexTcpBinding();
                bind.Security.Mode = SecurityMode.None;
                bind.ReceiveTimeout = TimeSpan.FromHours(1);
                bind.OpenTimeout = TimeSpan.FromHours(1);
                bind.SendTimeout = TimeSpan.FromHours(1);
                bind.MaxReceivedMessageSize = Int32.MaxValue;
                bind.MaxBufferSize = Int32.MaxValue;

                host = new ServiceHost(implTypes);
                //host.BaseAddresses.
                host.AddServiceEndpoint(contract, bind, address);


                #region 加入报错调试信息
                // 加入报错调试信息
                var metaDataPublishBehavior = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    HttpGetUrl = new Uri(BehaviorHttpGetUrl)
                };
                var debugBehavior = new ServiceDebugBehavior
                {
                    IncludeExceptionDetailInFaults = true
                };
                host.Description.Behaviors.Add(metaDataPublishBehavior);

                //You got to remove the default 'ServiceDeubgBehavior' before you can add one.
                host.Description.Behaviors.RemoveAll<ServiceDebugBehavior>();

                host.Description.Behaviors.Add(debugBehavior);
                #endregion

                host.Open();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void Close()
        {
            try
            {

                if (host != null)
                    host.Close();
            }
            catch
            {
                if (host != null)
                    host.Abort();
            }
        }

    }

}
