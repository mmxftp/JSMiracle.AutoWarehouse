using JsMiracle.Entities.WCF;
using JsMiracle.Framework;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace JsMiracle.WCF.BaseWcfClient
{
    public abstract class WcfClient<TChannel> : ClientBase<TChannel>, IDisposable
        where TChannel : class,IWcfService
    {
        protected static NetTcpBinding wcfBinding
            = new NetTcpBinding() ;

        static WcfClient() {

            wcfBinding.Security.Mode = SecurityMode.None;
            wcfBinding.ReceiveTimeout = TimeSpan.FromHours(1);
            wcfBinding.SendTimeout  = TimeSpan.FromHours(1);

            wcfBinding.MaxReceivedMessageSize = Int32.MaxValue;
            wcfBinding.MaxBufferSize = Int32.MaxValue;
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


        //public static TReturn UseService<TReturn>(
        //    Binding binding
        //    , EndpointAddress edpAddr
        //    , Func<TChannel, TReturn> func)
        //{
        //    //var chanFactory = new ChannelFactory<TChannel>("*");
        //    var chanFactory = new ChannelFactory<TChannel>(binding, edpAddr);
        //    TChannel channel = chanFactory.CreateChannel();
        //    ((IClientChannel)channel).Open();
        //    TReturn result = func(channel);
        //    try
        //    {
        //        ((IClientChannel)channel).Close();
        //    }
        //    catch
        //    {
        //        ((IClientChannel)channel).Abort();
        //    }
        //    return result;
        //}

        //public static void UseService(
        //   Binding binding
        //   , EndpointAddress edpAddr
        //   , Action<TChannel> func)
        //{
        //    //var chanFactory = new ChannelFactory<TChannel>("*");
        //    var chanFactory = new ChannelFactory<TChannel>(binding, edpAddr);
        //    TChannel channel = chanFactory.CreateChannel();
        //    ((IClientChannel)channel).Open();
        //    func(channel);
        //    try
        //    {
        //        ((IClientChannel)channel).Close();
        //    }
        //    catch
        //    {
        //        ((IClientChannel)channel).Abort();
        //    }
        //}

        /// <summary>
        /// 执行wcf方法并返回数据
        /// </summary>
        /// <typeparam name="P">参数类型</typeparam>
        /// <typeparam name="Return">返回类型</typeparam>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        /// <param name="serType"></param>
        /// <returns></returns>
        protected virtual Return RequestFunc<P, Return>(string methodName, P parameters)
        {
#if DEBUG
            //try
            //{
#endif
            WcfRequest req = new WcfRequest();
            req.Head.RequestMethodName = methodName;
            req.Head.ClassName = this.GetType().Name;
            if (parameters != null)
                req.Body.Parameters = parameters;

            var res = base.Channel.Request(req);
            if (res.Head.IsSuccess)
                return (Return)res.Body.Data;

            // 返回不是真 抛出异常
            throw new JsMiracleException(res.Head.Message);
#if DEBUG
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return default(Return);
            //}
#endif
        }



        /// <summary>
        /// 执行wcf方法没有返回数据
        /// </summary>
        /// <typeparam name="P">参数类型</typeparam>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        protected virtual void RequestAction<P>(string methodName, P parameters)
        {
            WcfRequest req = new WcfRequest();
            req.Head.RequestMethodName = methodName;
            req.Body.Parameters = parameters;

            var res = base.Channel.Request(req);
            if (res.Head.IsSuccess)
                return;

            // 返回不是真 抛出异常
            throw new JsMiracleException(res.Head.Message);
        }
    }

}
