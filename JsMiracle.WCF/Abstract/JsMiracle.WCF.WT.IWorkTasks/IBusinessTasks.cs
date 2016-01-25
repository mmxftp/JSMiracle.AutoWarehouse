using JsMiracle.Entities;
using JsMiracle.Entities.TabelEntities;
using JsMiracle.WCF.BaseWcfClient;
using JsMiracle.WCF.Config;
using JsMiracle.WCF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace JsMiracle.WCF.WT.IWorkTasks
{
    /// <summary>
    /// 业务类工作任务
    /// </summary>
    public interface IBusinessTasks : IDataLayer<IMS_WT_YWRW>
    {
        /// <summary>
        /// 根据单据行id得任务信息
        /// </summary>
        /// <param name="djhid">单据行ID</param>
        /// <returns>实体</returns>
        IMS_WT_YWRW GetTaskByDJHID(long djhid);

        /// <summary>
        /// 组盘入库
        /// </summary>
        /// <param name="id">业务表主键</param>
        /// <param name="userid">操作人</param>
        /// <param name="sku">物料ID</param>
        /// <param name="zpsl">组盘数量</param>
        /// <param name="rqbh">容器编号</param>
        void ZPIn(long id,string userid, long sku, decimal zpsl, string rqbh);

        /// <summary>
        /// 组盘出库
        /// </summary>
        /// <param name="id">业务表主键</param>
        /// <param name="userid">操作人</param>
        /// <param name="sku">物料ID</param>
        /// <param name="zpsl">组盘数量</param>
        /// <param name="rqbh">容器编号</param>
        void ZPOut(long id, string userid, long sku, decimal zpsl, string rqbh);
    }

    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(TasksKnownTypesProvider))]
    public interface IWcfBusinessTasks : IWcfService
    {

    }

    public class WcfConfigBusinessTasks : WcfClientConfig<IMS_WT_YWRW, IWcfBusinessTasks>, IBusinessTasks
    {
        const string ContactName = "IBusinessTasks";

        public WcfConfigBusinessTasks()
            : base(WCFServiceConfiguration.cfgDic[ContactName].GetEndPointAddress())
        { }


        public IMS_WT_YWRW GetTaskByDJHID(long djhid)
        {
            return base.RequestFunc<object[], IMS_WT_YWRW>("GetTaskByDJHID", new object[] { djhid });
        }


        public void ZPIn(long id, string userid, long sku, decimal zpsl, string rqbh)
        {
            base.RequestAction<object[]>("ZPIn", new object[] { id, userid, sku, zpsl, rqbh });
        }


        public void ZPOut(long id, string userid, long sku, decimal zpsl, string rqbh)
        {
            base.RequestAction<object[]>("ZPOut", new object[] { id, userid, sku, zpsl, rqbh });
        }
    }
}
