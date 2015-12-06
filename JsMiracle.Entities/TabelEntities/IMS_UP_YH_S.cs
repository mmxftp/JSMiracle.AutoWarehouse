namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_UP_YH
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string YHID { get; set; }

        [Required]
        [StringLength(50)]
        public string YHM { get; set; }

        [Required]
        [StringLength(50)]
        public string MM { get; set; }

        [StringLength(150)]
        public string DY { get; set; }

        public DateTime? CJRQ { get; set; }

        public DateTime? XGRQ { get; set; }

        public int ZT { get; set; }
    }
}
