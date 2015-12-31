using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JsMiracle.Entities.Enum
{
    /// <summary>
    /// 序列化类型
    /// </summary>
    [DataContract]
    public enum SerializedType
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        Json = 1,

        [EnumMember]
        Xml = 2
    }
}
