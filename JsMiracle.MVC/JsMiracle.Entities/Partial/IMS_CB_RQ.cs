using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.TabelEntities
{
    public interface IMS_CB_RQ_EX
    {
        [JsonIgnore]
        ICollection<IMS_CB_KC> IMS_CB_KC_S { get; set; }
    }

    partial class IMS_CB_RQ : IMS_CB_RQ_EX
    {
        /// <summary>
        /// 容器类型名称
        /// </summary>
        public string RQLX_Name { get; set; }
    }
}
