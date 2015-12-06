namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_UP_JSMK
    {
        public long ID { get; set; }

        [Required]
        [StringLength(36)]
        public string JSID { get; set; }

        public int MKID { get; set; }

        public int? GNID { get; set; }

        public DateTime? XGRQ { get; set; }
    }
}
