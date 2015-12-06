namespace JsMiracle.Entities.TabelEntities
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMS_CB_RQ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IMS_CB_RQ()
        {
            IMS_CB_KC_S = new HashSet<IMS_CB_KC>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [StringLength(64)]
        public string LXBH { get; set; }

        [Key]
        [StringLength(64)]
        public string RQBH { get; set; }

        [Required]
        [StringLength(64)]
        public string SMTM { get; set; }

        [Required]
        [StringLength(64)]
        public string DQWZ { get; set; }

        public int DQCD { get; set; }

        public int DQZL { get; set; }

        public int? DQKD { get; set; }

        public int SDZT { get; set; }

        public int ZYZT { get; set; }

        public int WZZT { get; set; }

        public int SYZT { get; set; }

        public int SYCS { get; set; }

        public DateTime ZCSJ { get; set; }

        [Required]
        [StringLength(64)]
        public string ZCWZ { get; set; }

        [StringLength(256)]
        public string BZ { get; set; }

        [JsonIgnore]

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMS_CB_KC> IMS_CB_KC_S { get; set; }

        public virtual IMS_CB_RQLX IMS_CB_RQLX_S { get; set; }
    }
}
