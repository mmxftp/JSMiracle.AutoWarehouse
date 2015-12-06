namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Model1")
        {
        }

        public virtual DbSet<IMS_CB_KC> IMS_CB_KC_S { get; set; }
        public virtual DbSet<IMS_CB_RQ> IMS_CB_RQ_S { get; set; }
        public virtual DbSet<IMS_CB_RQLX> IMS_CB_RQLX_S { get; set; }
        public virtual DbSet<IMS_CB_WL> IMS_CB_WL_S { get; set; }
        public virtual DbSet<IMS_CB_WZ> IMS_CB_WZ_S { get; set; }
        public virtual DbSet<IMS_CB_WZGX> IMS_CB_WZGX_S { get; set; }
        public virtual DbSet<IMS_CB_WZLX> IMS_CB_WZLX_S { get; set; }
        public virtual DbSet<IMS_CM_DM> IMS_CM_DM_S { get; set; }
        public virtual DbSet<IMS_CM_DMLX> IMS_CM_DMLX_S { get; set; }
        public virtual DbSet<IMS_UP_JS> IMS_UP_JS_S { get; set; }
        public virtual DbSet<IMS_UP_JSMK> IMS_UP_JSMK_S { get; set; }
        public virtual DbSet<IMS_UP_JSYH> IMS_UP_JSYH_S { get; set; }
        public virtual DbSet<IMS_UP_MK> IMS_UP_MK_S { get; set; }
        public virtual DbSet<IMS_UP_MKGN> IMS_UP_MKGN_S { get; set; }
        public virtual DbSet<IMS_UP_YH> IMS_UP_YH_S { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IMS_CB_KC>()
                .Map(n => n.ToTable("IMS_CB_KC_S"))
                .Property(e => e.KQSL)
                .HasPrecision(2, 0);

            modelBuilder.Entity<IMS_CB_KC>()
                // .Map(n => n.ToTable("IMS_CB_KC_S"))
                .Property(e => e.KYSL)
                .HasPrecision(2, 0);

            modelBuilder.Entity<IMS_CB_RQ>()
                .Map(n => n.ToTable("IMS_CB_RQ_S"))
                .HasMany(e => e.IMS_CB_KC_S)
                .WithRequired(e => e.IMS_CB_RQ_S)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IMS_CB_WZ>()
                .Map(n => n.ToTable("IMS_CB_WZ_S"))
                .HasMany(e => e.IMS_CB_WZGX_S)
                .WithRequired(e => e.IMS_CB_WZ_S)
                .HasForeignKey(e => e.ZWZ)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IMS_CB_WZ>()
                //.Map(n => n.ToTable("IMS_CB_WZ_S"))
                .HasMany(e => e.IMS_CB_WZGX_S1)
                .WithRequired(e => e.IMS_CB_WZ_S1)
                .HasForeignKey(e => e.FWZ)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IMS_CB_WZLX>()
                .Map(n => n.ToTable("IMS_CB_WZLX_S"))
                .Property(e => e.KJLX)
                .IsUnicode(false);

            modelBuilder.Entity<IMS_CB_WZLX>()
                //.Map(n => n.ToTable("IMS_CB_WZLX_S"))
                .HasMany(e => e.IMS_CB_WZ_S)
                .WithRequired(e => e.IMS_CB_WZLX_S)
                .HasForeignKey(e => e.WZLX)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IMS_CB_RQLX>()
                .Map(n => n.ToTable("IMS_CB_RQLX_S"));

            modelBuilder.Entity<IMS_CB_WZGX>()
                .Map(n => n.ToTable("IMS_CB_WZGX_S"));

            modelBuilder.Entity<IMS_CB_WL>()
                .Map(n => n.ToTable("IMS_CB_WL_S"));


            modelBuilder.Entity<IMS_UP_JS>()
                .Map(n => n.ToTable("IMS_UP_JS_S"));

            modelBuilder.Entity<IMS_UP_JSMK>()
                .Map(n => n.ToTable("IMS_UP_JSMK_S"));

            modelBuilder.Entity<IMS_UP_JSYH>()
                .Map(n => n.ToTable("IMS_UP_JSYH_S"));

            modelBuilder.Entity<IMS_UP_MK>()
                .Map(n => n.ToTable("IMS_UP_MK_S"));

            modelBuilder.Entity<IMS_UP_MKGN>()
                .Map(n => n.ToTable("IMS_UP_MKGN_S"));

            modelBuilder.Entity<IMS_UP_YH>()
                .Map(n => n.ToTable("IMS_UP_YH_S"));

            modelBuilder.Entity<IMS_CM_DM>()
                .Map(n => n.ToTable("IMS_CM_DM_S"));

            modelBuilder.Entity<IMS_CM_DMLX>()
                .Map(n => n.ToTable("IMS_CM_DMLX_S"));
        }
    }
}
