using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.MVC
{
    public class IMS_ZP
    {
        /// <summary>
        /// 订单行主键ID 
        /// </summary>
        public long DJH_ID { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string DJBH { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public string HH { get; set; }

  
        /// <summary>
        /// 物料 ID
        /// </summary>
        public long SKU { get; set; }

        /// <summary>
        /// 物料信息( 物料名称+ 物料编号)
        /// </summary>
        public string WLMC { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string PCBH { get; set; }

        /// <summary>
        /// 单据数量
        /// </summary>
        public decimal ZPSL { get; set; }

        /// <summary>
        /// 容器编号
        /// </summary>
        public string RQBH { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int ZT { get; set; }

        /// <summary>
        /// 销售订单号
        /// </summary>
        public string XSDDH { get; set; }
    }
}
