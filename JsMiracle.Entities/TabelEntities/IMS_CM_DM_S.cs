namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_CM_DM
    {
        public long ID { get; set; }

        [Required]
        [StringLength(64)]
        public string LXDM { get; set; }

        [Required]
        [StringLength(64)]
        public string DM { get; set; }

        [Required]
        [StringLength(64)]
        public string MC { get; set; }

        public int SZ { get; set; }

        [Required]
        [StringLength(64)]
        public string CJR { get; set; }

        [StringLength(64)]
        public string XGR { get; set; }

        public DateTime? XGRQ { get; set; }

        [StringLength(64)]
        public string ZS { get; set; }
    }
}
