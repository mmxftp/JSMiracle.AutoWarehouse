using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace JsMiracle.WCF.BaseWcfClient
{
    public abstract class WcfClient<TChannel> : ClientBase<TChannel>, IDisposable where TChannel : class
    {
        protected static NetTcpBinding wcfBinding
            = new NetTcpBinding() ;

        static WcfClient() {

            wcfBinding.Security.Mode = SecurityMode.None;
            //    var bind = new NetTcpBinding();
            ////var bind = MetadataExchangeBindings.CreateMexTcpBinding();
            //bind.Security.Mode = SecurityMode.None;
        }

        protected Binding binding;
        protected EndpointAddress remoteAddress;

        protected WcfClient(EndpointAddress edpAddr)
            : base(wcfBinding, edpAddr) { this.binding = wcfBinding; this.remoteAddress = edpAddr; }

        protected WcfClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress) { this.binding = wcfBinding; this.remoteAddress = remoteAddress; }

        void IDisposable.Dispose()
        {
            try
            {
                this.Close();
            }
            catch
            {
                this.Abort();
            }
        }


        public static TReturn UseService<TReturn>(
            Binding binding
            , EndpointAddress edpAddr
            , Func<TChannel, TReturn> func)
        {
            //var chanFactory = new ChannelFactory<TChannel>("*");
            var chanFactory = new ChannelFactory<TChannel>(binding, edpAddr);
            TChannel channel = chanFactory.CreateChannel();
            ((IClientChannel)channel).Open();
            TReturn result = func(channel);
            try
            {
                ((IClientChannel)channel).Close();
            }
            catch
            {
                ((IClientChannel)channel).Abort();
            }
            return result;
        }

        public static void UseService(
           Binding binding
           , EndpointAddress edpAddr
           , Action<TChannel> func)
        {
            //var chanFactory = new ChannelFactory<TChannel>("*");
            var chanFactory = new ChannelFactory<TChannel>(binding, edpAddr);
            TChannel channel = chanFactory.CreateChannel();
            ((IClientChannel)channel).Open();
            func(channel);
            try
            {
                ((IClientChannel)channel).Close();
            }
            catch
            {
                ((IClientChannel)channel).Abort();
            }
        }
    }

}
