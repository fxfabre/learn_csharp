﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestLinq
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TutosEntities : DbContext
    {
        public TutosEntities()
            : base("name=TutosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Couleurs> Couleurs { get; set; }
        public virtual DbSet<Marques> Marques { get; set; }
        public virtual DbSet<Ventes> Ventes { get; set; }
    }
}
