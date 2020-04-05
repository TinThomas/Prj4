using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace VareDatabase.Models
{
    public class ItemContext : DbContext
    {
        public DbSet<ItemDB> Item { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Item.db");
        }

        public class ItemDB
        {
            [Key]
            public int ItemId { get; set; }
            public string Type { get; set; }
        }
    }
}

