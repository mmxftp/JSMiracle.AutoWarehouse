namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_UP_JS
    {
        public long ID { get; set; }

        [StringLength(36)]
        public string JSID { get; set; }

        [StringLength(50)]
        public string JSMC { get; set; }

        public DateTime? CJRQ { get; set; }

        [StringLength(200)]
        public string MS { get; set; }
    }
}
