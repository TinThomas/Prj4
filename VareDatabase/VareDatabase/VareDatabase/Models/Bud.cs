using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace VareDatabase.Models
{
    public class BudContext : DbContext
    {
        public DbSet<BudDB> Bud { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Bud.db");
        }

        public class BudDB
        {
            public int userID_forLastBid { get; set; }
            public int price { get; set; }
            public int ÚuerID_forSeller { get; set; }
        }
    }
}

