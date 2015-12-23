using JsMiracle.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.TabelEntities
{
    partial class IMS_CB_WL 
    {
        /// <summary>
        /// 物料类型名称
        /// </summary>
        public readonly static string ItemTypeName = CodeTypeEnum.ItemType.ToString();

 
        /// <summary>
        /// 物料类型名称,用于界面显示
        /// </summary>
        public string WLTypeName { get; set; }
    }
}
