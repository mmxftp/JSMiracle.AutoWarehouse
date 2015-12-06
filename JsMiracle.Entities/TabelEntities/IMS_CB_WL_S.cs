namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_CB_WL
    {
        public long ID { get; set; }

        [Required]
        [StringLength(64)]
        public string WLBH { get; set; }

        public int WLLX { get; set; }

        [Required]
        [StringLength(64)]
        public string WLMC { get; set; }

        [Required]
        [StringLength(128)]
        public string MS { get; set; }

        public int ABCHDZJ { get; set; }

        public int BZQ { get; set; }

        public int? ZXBZDW { get; set; }

        public long? ZDKCL { get; set; }

        public long ZXKCL { get; set; }

        public int ZLDJ { get; set; }

        public int WXDJ { get; set; }

        [StringLength(64)]
        public string TXM { get; set; }

        [Required]
        [StringLength(64)]
        public string CJR { get; set; }

        public DateTime CJSJ { get; set; }

        [StringLength(64)]
        public string XGR { get; set; }

        public DateTime? XGSJ { get; set; }

        [StringLength(256)]
        public string BZ { get; set; }
    }
}
