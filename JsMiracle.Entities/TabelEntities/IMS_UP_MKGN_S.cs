namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_UP_MKGN
    {
        public long ID { get; set; }

        public int MKID { get; set; }

        [Required]
        [StringLength(50)]
        public string GNMC { get; set; }

        [Required]
        [StringLength(300)]
        public string KJID { get; set; }

        [StringLength(200)]
        public string MS { get; set; }

        public int GNID { get; set; }

        [StringLength(255)]
        public string KZMC { get; set; }

        [StringLength(255)]
        public string HDMC { get; set; }
    }
}
