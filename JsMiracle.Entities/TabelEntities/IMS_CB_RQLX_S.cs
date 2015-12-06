namespace JsMiracle.Entities.TabelEntities
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_CB_RQLX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IMS_CB_RQLX()
        {
            IMS_CB_RQ_S = new HashSet<IMS_CB_RQ>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Key]
        [StringLength(64)]
        public string LXBH { get; set; }

        [Required]
        [StringLength(64)]
        public string LXMC { get; set; }

        [StringLength(128)]
        public string MS { get; set; }

        [Required]
        [StringLength(64)]
        public string CZLX { get; set; }

        public int ZSCD { get; set; }

        public int ZSKD { get; set; }

        public int ZSGD { get; set; }

        public int ZSZL { get; set; }

        [Required]
        [StringLength(64)]
        public string KJLX { get; set; }

        public int YXCD { get; set; }

        public int YXKD { get; set; }

        public int YXGD { get; set; }

        public int YXCZ { get; set; }

        public int? GDDJ { get; set; }

        public int SDDJ { get; set; }

        public int WDDJ { get; set; }

        public int WXDJ { get; set; }

        public int FBDJ { get; set; }

        public int HXDJ { get; set; }

        public int FCDJ { get; set; }

        [StringLength(128)]
        public string TMYM { get; set; }

        [StringLength(64)]
        public string CJR { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMS_CB_RQ> IMS_CB_RQ_S { get; set; }
    }
}
