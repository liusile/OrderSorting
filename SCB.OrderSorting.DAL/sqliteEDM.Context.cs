﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCB.OrderSorting.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OrderSortingDBEntities : DbContext
    {
        public OrderSortingDBEntities()
            : base("name=OrderSortingDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ConfigParamers> ConfigParamers { get; set; }
        public virtual DbSet<Countrys> Countrys { get; set; }
        public virtual DbSet<LatticeOrdersCache> LatticeOrdersCache { get; set; }
        public virtual DbSet<LatticeSetting> LatticeSetting { get; set; }
        public virtual DbSet<OldOrderInfo> OldOrderInfo { get; set; }
        public virtual DbSet<OldOrderSortingLog> OldOrderSortingLog { get; set; }
        public virtual DbSet<OrderInfo> OrderInfo { get; set; }
        public virtual DbSet<OrderSortingLog> OrderSortingLog { get; set; }
        public virtual DbSet<PackingLog> PackingLog { get; set; }
        public virtual DbSet<Posttypes> Posttypes { get; set; }
        public virtual DbSet<SolutionCountry> SolutionCountry { get; set; }
        public virtual DbSet<SolutionPostType> SolutionPostType { get; set; }
        public virtual DbSet<SortingSolutions> SortingSolutions { get; set; }
        public virtual DbSet<LoginLog> LoginLog { get; set; }
        public virtual DbSet<OldPackingLog> OldPackingLog { get; set; }
        public virtual DbSet<PostArea> PostArea { get; set; }
        public virtual DbSet<SolutionPostArea> SolutionPostArea { get; set; }
        public virtual DbSet<SolutionZipType> SolutionZipType { get; set; }
        public virtual DbSet<ZipType> ZipType { get; set; }
    }
}
