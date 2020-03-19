using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SignupController
{
    public class UserNContext : DbContext
    {
        public DbSet<UserN> UserNs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite("Data Source=UserNDB.db");
    }

    public class UserN
    {
        public int UserNId { get; set; }
        public string Email { get; set; }
        public string userName { get; set; }
    }
}
