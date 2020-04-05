using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Timers;

namespace VareDatabase.Models
{
    public class TidContext : DbContext
    {
        public DbSet<TidDB> Tid { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Tid.db");
        }
    }

    public class TidDB
    {
        public Timer experation { get; set; }
        public string timeOfCreation { get; set; }
    }
}
