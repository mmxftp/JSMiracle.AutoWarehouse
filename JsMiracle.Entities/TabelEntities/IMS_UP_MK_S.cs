namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_UP_MK
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string MKMC { get; set; }

        [StringLength(300)]
        public string URL { get; set; }

        public DateTime? CJRQ { get; set; }

        [StringLength(200)]
        public string MS { get; set; }

        public int MKID { get; set; }

        public int FMKID { get; set; }

        public int PXID { get; set; }

        [StringLength(255)]
        public string KZMC { get; set; }

        [StringLength(255)]
        public string HDMC { get; set; }
    }
}
