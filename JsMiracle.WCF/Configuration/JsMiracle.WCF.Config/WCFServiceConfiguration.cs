using JsMiracle.Framework.Log;
using JsMiracle.Framework.Serialized;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace JsMiracle.WCF.Config
{
    public class WCFServiceConfiguration
    {
        public readonly static Dictionary<string, WcfService> cfgDic;

        static WCFServiceConfiguration()
        {
            try
            {
                var configFile = ConfigurationManager.AppSettings["ServiceConfiguration"];
                if (!string.IsNullOrEmpty(configFile)
                    && File.Exists(configFile))
                {
                    var wcfConfig =
                        XmlSerialization.DeserializeFile<WCFServiceConfigurationEntity>(configFile);

                    if (wcfConfig != null)
                    {
                        cfgDic = new Dictionary<string, WcfService>();

                        foreach (var service in wcfConfig.Services)
                        {
                            cfgDic.Add(service.ServiceID, service);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ILog log = new Net4Log();
                //var log = WindsorContaineFactory.GetContainer().Resolve<ILog>();
                log.WriteLog(LogEnum.WcfConfig, LogLevel.ERROR, ex.Message +'|'+ ex.StackTrace);
                throw;
            }
        }

    }
}
