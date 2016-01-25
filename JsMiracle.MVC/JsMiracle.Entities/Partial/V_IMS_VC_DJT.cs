using JsMiracle.Entities.Enum;
using JsMiracle.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.TabelEntities
{
    partial class V_IMS_VC_DJT
    {
        /// <summary>
        /// 可修改
        /// </summary>
        public bool CanModify { get; set; }

        /// <summary>
        /// 是否可评审
        /// </summary>
        public bool CanAudit { get; set; }

        /// <summary>
        /// 可完成
        /// </summary>
        public bool CanComplete { get; set; }

        /// <summary>
        /// 可取消
        /// </summary>
        public bool CanCancel { get; set; }

        /// <summary>
        /// 可结束
        /// </summary>
        public bool CanClose { get; set; }
    }
}
