using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using VareDatabase.Models;

namespace VareDatabase.DBContext
{
    class VareDataModelContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlServer(@"Data Source=localhost,1433;Database=vareDatabase1_0;User ID=SA;Password=Password1!;");
        }

        public DbSet<ItemEntity> Item { get; set; }
        public DbSet<BudEntity> Bud { get; set; }
        public DbSet<BeskrivelseEntity> Beskrivelse { get; set; }
        public DbSet<TimeEntity> Tid { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemEntity>()
                .HasOne(a => a.Tid)
                .WithOne(b => b.Item)
                .HasForeignKey<TimeEntity>(e => e.Item);

            modelBuilder.Entity<ItemEntity>()
                .HasOne(a => a.Bud)
                .WithOne(b => b.Item)
                .HasForeignKey<BudEntity>(e => e.Item);

            modelBuilder.Entity<ItemEntity>()
                .HasOne(a => a.Beskrivelse)
                .WithOne(b => b.Item)
                .HasForeignKey<BeskrivelseEntity>(e => e.Item);
        }

    }
}
