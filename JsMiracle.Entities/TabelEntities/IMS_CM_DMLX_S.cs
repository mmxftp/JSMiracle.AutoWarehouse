namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_CM_DMLX
    {
        public long ID { get; set; }

        [Required]
        [StringLength(64)]
        public string LXDM { get; set; }

        [Required]
        [StringLength(64)]
        public string LXMC { get; set; }

        [StringLength(128)]
        public string LXMS { get; set; }

        [Required]
        [StringLength(64)]
        public string CJR { get; set; }

        public DateTime CJSJ { get; set; }

        [StringLength(64)]
        public string XGR { get; set; }

        [StringLength(256)]
        public string ZS { get; set; }
    }
}
