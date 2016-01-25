using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.TabelEntities
{
    partial class IMS_WT_YWRW
    {
        /// <summary>
        /// 物料信息( 物料名称+ 物料编号)
        /// </summary>
        public string WLMC { get; set; }


        /// <summary>
        /// 容器编号
        /// </summary>
        public string RQBH { get; set; }

        /// <summary>
        /// 组盘数量
        /// </summary>
        public decimal ZPSL { get; set; }

    }
}
