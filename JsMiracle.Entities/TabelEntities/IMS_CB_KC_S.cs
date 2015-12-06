namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_CB_KC
    {
        public long ID { get; set; }

        [Required]
        [StringLength(64)]
        public string RQBH { get; set; }

        [Required]
        [StringLength(64)]
        public string WKBH { get; set; }

        public decimal KQSL { get; set; }

        public decimal KYSL { get; set; }

        [Required]
        [StringLength(64)]
        public string PCBH { get; set; }

        [Required]
        [StringLength(64)]
        public string SYZ { get; set; }

        [Required]
        [StringLength(64)]
        public string BZXS { get; set; }

        public int ZLZT { get; set; }

        public int ZJZT { get; set; }

        public int SDZT { get; set; }

        public int KYZT { get; set; }

        [Required]
        [StringLength(64)]
        public string RKRY { get; set; }

        public DateTime RKSJ { get; set; }

        public DateTime? ZHXGSJ { get; set; }

        [StringLength(256)]
        public string BZ { get; set; }

        public virtual IMS_CB_RQ IMS_CB_RQ_S { get; set; }
    }
}
