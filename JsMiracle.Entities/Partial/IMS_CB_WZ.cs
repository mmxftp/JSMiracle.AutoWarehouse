using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.TabelEntities
{
    public interface IMS_CB_WZ_EX
    {
        [JsonIgnore]
        ICollection<IMS_CB_WZGX> IMS_CB_WZGX_S { get; set; }

        [JsonIgnore]
        ICollection<IMS_CB_WZGX> IMS_CB_WZGX_S1 { get; set; }
    }

    partial class IMS_CB_WZ :IMS_CB_WZ_EX
    {
        public bool ExistsItem { get; set; }
    }
}
