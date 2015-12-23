using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Entities.TabelEntities
{
    public interface IMS_CB_WZLX_Ex
    {
        [Newtonsoft.Json.JsonIgnore]
        ICollection<IMS_CB_WZ> IMS_CB_WZ_S { get; set; }
    }

    partial class IMS_CB_WZLX : IMS_CB_WZLX_Ex
    {

    }
}
