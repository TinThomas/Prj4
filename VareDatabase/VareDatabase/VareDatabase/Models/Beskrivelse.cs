using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace VareDatabase.Models
{
    public class BeskrivelseContext : DbContext
    {
        public DbSet<BeskrivelseDB> Beskrivelse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Beskrivelse.db");
        }
    }

    public class BeskrivelseDB
    {
        public string imageOfItem { get; set; }
        public string descriptionOfItem { get; set; }
        public string title { get; set; }
    }
}
