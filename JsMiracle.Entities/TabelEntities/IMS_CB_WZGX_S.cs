namespace JsMiracle.Entities.TabelEntities
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_CB_WZGX
    {
        public long ID { get; set; }

        [Required]
        [StringLength(64)]
        public string ZWZ { get; set; }

        [Required]
        [StringLength(64)]
        public string FWZ { get; set; }

        [Required]
        [StringLength(64)]
        public string LX { get; set; }

        public int X { get; set; }

        public int? Y { get; set; }

        public int Z { get; set; }

        public int KYZT { get; set; }

        [JsonIgnore]

        public virtual IMS_CB_WZ IMS_CB_WZ_S { get; set; }

        [JsonIgnore]

        public virtual IMS_CB_WZ IMS_CB_WZ_S1 { get; set; }
    }
}
