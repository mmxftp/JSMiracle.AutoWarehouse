namespace JsMiracle.Entities.TabelEntities
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_CB_WZLX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IMS_CB_WZLX()
        {
            IMS_CB_WZ_S = new HashSet<IMS_CB_WZ>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Key]
        [StringLength(64)]
        public string LXDM { get; set; }

        [Required]
        [StringLength(64)]
        public string LXMC { get; set; }

        [StringLength(128)]
        public string MS { get; set; }

        [Required]
        [StringLength(64)]
        public string KJLX { get; set; }

        public int YXCD { get; set; }

        public int YXKD { get; set; }

        public int YXGD { get; set; }

        public int ZDCZ { get; set; }

        public int? GDDJ { get; set; }

        public int SDDJ { get; set; }

        public int WXDJ { get; set; }

        public int FBDJ { get; set; }

        public int HXDJ { get; set; }

        public int FCDJ { get; set; }

        public int WDDJ { get; set; }

        [Required]
        [StringLength(64)]
        public string CJR { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMS_CB_WZ> IMS_CB_WZ_S { get; set; }
    }
}
