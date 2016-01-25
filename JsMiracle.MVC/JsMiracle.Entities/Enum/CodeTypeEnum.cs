using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JsMiracle.Entities.Enum
{
    /// <summary>
    /// 代码类型
    /// </summary>
    [DataContract]
    public enum CodeTypeEnum
    {
        
        /// <summary>
        /// 空,未知类型
        /// </summary>
        [EnumMember]
        None = 0,

        /// <summary>
        /// 物料类型
        /// </summary>
        [EnumMember]
        ItemType = 1,

        /// <summary>
        /// 数据表名称
        /// </summary>
        [EnumMember]
        TableName = 2,

        /// <summary>
        /// 业务类型 （入/出库）
        /// </summary>
        [EnumMember]
        BusinessType=3,

        /// <summary>
        /// 上位单据类型
        /// </summary>
        [EnumMember]
        HostSystemDocumentType = 4,

        /// <summary>
        /// 单据状态
        /// </summary>
        [EnumMember]
        VH_STS = 5,

  
    }
}
