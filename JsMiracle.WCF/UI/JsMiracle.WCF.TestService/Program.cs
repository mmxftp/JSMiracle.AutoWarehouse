using JsMiracle.WCF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace JsMiracle.WCF.TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (WCFServiceConfiguration.cfgDic != null) {
                    foreach (var cfg in WCFServiceConfiguration.cfgDic)
                    {
                        cfg.Value.Open();
                    }
                }
                Console.WriteLine("wcf service started .... ");
                Console.WriteLine("Press enter will be closed  .... ");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();

            if (WCFServiceConfiguration.cfgDic != null)
            {
                foreach (var cfg in WCFServiceConfiguration.cfgDic)
                {
                    cfg.Value.Close();
                }
            }

        }
    }
}
