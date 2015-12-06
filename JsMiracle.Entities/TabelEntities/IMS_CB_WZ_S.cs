namespace JsMiracle.Entities.TabelEntities
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_CB_WZ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IMS_CB_WZ()
        {
            IMS_CB_WZGX_S = new HashSet<IMS_CB_WZGX>();
            IMS_CB_WZGX_S1 = new HashSet<IMS_CB_WZGX>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Key]
        [StringLength(64)]
        public string WXBH { get; set; }

        [Required]
        [StringLength(64)]
        public string WZMC { get; set; }

        [Required]
        [StringLength(64)]
        public string XSBQ { get; set; }

        [Required]
        [StringLength(64)]
        public string WZLX { get; set; }

        [Required]
        [StringLength(64)]
        public string CKID { get; set; }

        [Required]
        [StringLength(64)]
        public string QY { get; set; }

        public int XD { get; set; }

        public int P { get; set; }

        public int L { get; set; }

        public int C { get; set; }

        public int SD { get; set; }

        [Required]
        [StringLength(64)]
        public string WZ { get; set; }

        public int ABC { get; set; }

        public int WLZT { get; set; }

        public int FWZT { get; set; }

        public int ZYZT { get; set; }

        public int SDZT { get; set; }

        public int YDZT { get; set; }

        [Required]
        [StringLength(64)]
        public string JMLX { get; set; }

        [Required]
        [StringLength(64)]
        public string GNLX { get; set; }

        [Required]
        [StringLength(64)]
        public string CJR { get; set; }

        public DateTime CJSJ { get; set; }

        [Required]
        [StringLength(64)]
        public string JZ { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMS_CB_WZGX> IMS_CB_WZGX_S { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMS_CB_WZGX> IMS_CB_WZGX_S1 { get; set; }

        public virtual IMS_CB_WZLX IMS_CB_WZLX_S { get; set; }
    }
}
