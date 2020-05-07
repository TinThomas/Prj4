using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet.Models.Login;

namespace VareDatabase.DBContext
{
    public class PassModelContext : DbContext
    {
        public PassModelContext(DbContextOptions<PassModelContext> options)
            : base(options)
        { }   

        public DbSet<PasswordEntity> Passwords { get; set; }
    }
}