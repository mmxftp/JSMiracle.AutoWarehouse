﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JsMiracle.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IIMS_ORGEntities : DbContext
    {
        public IIMS_ORGEntities()
            : base("name=IIMS_ORGEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<IMS_TB_Module> IMS_TB_Module { get; set; }
        public virtual DbSet<IMS_TB_ModuleFunction> IMS_TB_ModuleFunction { get; set; }
        public virtual DbSet<IMS_TB_Permission> IMS_TB_Permission { get; set; }
        public virtual DbSet<IMS_TB_RoleInfo> IMS_TB_RoleInfo { get; set; }
        public virtual DbSet<IMS_TB_UserInfo> IMS_TB_UserInfo { get; set; }
    }
}
