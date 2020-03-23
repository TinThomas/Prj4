using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SignupController
{
    public class PassContext : DbContext
    {
        public DbSet<PassDB> Passes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite("Data Source=Password.db");

    }
    public class PassDB
    {
        [Key]
        public int UserId { get; set; }
        public int Password { get; set; }
    }
}
