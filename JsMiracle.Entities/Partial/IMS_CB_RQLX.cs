using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.TabelEntities
{
    public interface IMS_CB_RQLX_Ex
    {
        [JsonIgnore]
        ICollection<IMS_CB_RQ> IMS_CB_RQ_S { get; set; }
    }

    partial class IMS_CB_RQLX:IMS_CB_RQLX_Ex
    {
    }
}
