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

        public readonly static string ConfigPath;

        static WCFServiceConfiguration()
        {
            try
            {
                bool isWeb = false;

                // 用当前使用的配置文件判断是否web项目
                if (string.Equals(
                    Path.GetFileName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
                    , "web.config", StringComparison.CurrentCultureIgnoreCase))
                {
                    isWeb = true;
                }

                var configFile = ConfigurationManager.AppSettings["ServiceConfiguration"];

                // web项目文件路径的特殊处理
                if (isWeb)
                {
                    if (!File.Exists(configFile))
                        configFile = System.Web.HttpContext.Current.Server.MapPath("~") +'\\' + configFile;
                }

                if (!string.IsNullOrEmpty(configFile)
                    && File.Exists(configFile))
                {
                    // 配置文件的所在目录，
                    // 配置内容中没有的路径，此目录要用于后台wcf服务文件载入用
                    ConfigPath = Path.GetDirectoryName(configFile);

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
