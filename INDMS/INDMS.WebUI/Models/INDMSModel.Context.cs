﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace INDMS.WebUI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class INDMSEntities : DbContext
    {
        public INDMSEntities()
            : base("name=INDMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GeneralBook> GeneralBooks { get; set; }
        public virtual DbSet<GuideLine> GuideLines { get; set; }
        public virtual DbSet<ParameterMaster> ParameterMasters { get; set; }
        public virtual DbSet<PolicyLetter> PolicyLetters { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Standard> Standards { get; set; }
        public virtual DbSet<StandingOrder> StandingOrders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Drawing> Drawings { get; set; }
        public virtual DbSet<QAP> QAPs { get; set; }
        public virtual DbSet<MovementOrder> MovementOrders { get; set; }
        public virtual DbSet<Firm> Firms { get; set; }
        public virtual DbSet<POGeneration> POGenerations { get; set; }
        public virtual DbSet<AdminCorrespondence> AdminCorrespondences { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Photograph> Photographs { get; set; }
        public virtual DbSet<TYMemo> TYMemoes { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<FCLDetail> FCLDetails { get; set; }
        public virtual DbSet<FCL> FCLs { get; set; }
    }
}
