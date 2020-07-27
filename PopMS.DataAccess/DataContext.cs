using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PopMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;

namespace PopMS.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DataContext(CS cs)
             : base(cs)
        {
        }
        public DbSet<area> areas { get; set; }
        public DbSet<area_location> area_Locations { get; set; }
        public DbSet<contract> contracts { get; set; }
        public DbSet<contract_pop> contract_Pops { get; set; }
        public DbSet<dc> dcs { get; set; }
        public DbSet<dept> depts { get; set; }
        public DbSet<inventory> inventories { get; set; }
        public DbSet<inventoryIn> inventoryIns { get; set; }
        public DbSet<inventoryOut> inventoryOuts { get; set; }
        public DbSet<order_pop> order_Pops { get; set; }
        public DbSet<pop> pops { get; set; }
        public DbSet<ship_pop> ship_Pops { get; set; }
        public DbSet<ship_pop_sum> ship_Pop_Sums { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<inv_record> inv_records { get; set; }
        public DbSet<pop_group> pop_Groups { get; set; }
        public DataContext(string cs, DBTypeEnum dbtype, string version=null)
             : base(cs, dbtype, version)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<pop>(e => e.HasIndex("PopIndex").IsUnique());
            modelBuilder.Entity<contract>(e => e.HasIndex("ContractID").IsUnique());
            modelBuilder.Entity<contract_pop>(e => e.HasKey(x=>new { x.ContractID,x.PopID}));
            base.OnModelCreating(modelBuilder);
        }
    }

    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("Server=10.66.148.88;Database=PopMS_db;uid=wmsuser;pwd=wmsuser", DBTypeEnum.SqlServer);
        }
    }

}
