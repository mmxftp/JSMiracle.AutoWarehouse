using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.TabelEntities
{
    partial class V_IMS_VC_DJH
    {
        /// <summary>
        /// 可修改
        /// </summary>
        public bool CanModify { get; set; }

        /// <summary>
        /// 是否可释放
        /// </summary>
        public bool CanRelease { get; set; }

        /// <summary>
        /// 可完成
        /// </summary>
        public bool CanComplete { get; set; }

        /// <summary>
        /// 可取消
        /// </summary>
        public bool CanCancel { get; set; }

        /// <summary>
        /// 是否可组盘
        /// </summary>
        public bool CanZP { get; set; }

    }
}
