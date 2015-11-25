using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities
{
    partial class IMS_CB_WL : IModelBase
    {
        /// <summary>
        /// 物料类型名称
        /// </summary>
        public const string ItemTypeName = "wllx";

        /// <summary>
        /// 物料类型名称,用于界面显示
        /// </summary>
        public string WLTypeName { get; set; }
    }
}
