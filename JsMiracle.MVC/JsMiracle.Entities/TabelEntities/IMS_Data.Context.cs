﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JsMiracle.Entities.TabelEntities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ORGModels : DbContext
    {
        public ORGModels()
            : base("name=ORGModels")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
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
        public virtual DbSet<IMS_UP_MKGN> IMS_UP_MKGN_S { get; set; }
        public virtual DbSet<IMS_UP_YH> IMS_UP_YH_S { get; set; }
        public virtual DbSet<IMS_CM_DXXX> IMS_CM_DXXX_S { get; set; }
        public virtual DbSet<IMS_CM_YHDX> IMS_CM_YHDX_S { get; set; }
        public virtual DbSet<IMS_CM_XTCS> IMS_CM_XTCS_S { get; set; }
        public virtual DbSet<IMS_VC_DJH> IMS_VC_DJH_S { get; set; }
        public virtual DbSet<IMS_VC_DJT> IMS_VC_DJT_S { get; set; }
        public virtual DbSet<IMS_VC_YWYS> IMS_VC_YWYS_S { get; set; }
        public virtual DbSet<IMS_WN_TZ> IMS_WN_TZ_S { get; set; }
        public virtual DbSet<IMS_WN_TZLX> IMS_WN_TZLX_S { get; set; }
        public virtual DbSet<IMS_WT_BYRW> IMS_WT_BYRW_S { get; set; }
        public virtual DbSet<IMS_WT_CWRW> IMS_WT_CWRW_S { get; set; }
        public virtual DbSet<IMS_WT_DZRW> IMS_WT_DZRW_S { get; set; }
        public virtual DbSet<IMS_WT_XTJD> IMS_WT_XTJD_S { get; set; }
        public virtual DbSet<IMS_WT_YWRW> IMS_WT_YWRW_S { get; set; }
        public virtual DbSet<IMS_WT_ZXLJ> IMS_WT_ZXLJ_S { get; set; }
        public virtual DbSet<IMS_WT_ZXZ> IMS_WT_ZXZ_S { get; set; }
        public virtual DbSet<IMS_UP_MK> IMS_UP_MK_S { get; set; }
    }
}
