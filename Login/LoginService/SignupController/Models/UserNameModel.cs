using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SignupController
{
    public class UserNContext : DbContext
    {
        public DbSet<UserNDB> UserNs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite("Data Source=UserName.db");
    }

    public class UserNDB
    {
        [Key]
        public int UserNId { get; set; }
        [EmailAddress(ErrorMessage = "Must be a valid Email address")]
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
