using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet.Models.Login;

namespace VareDatabase.DBContext
{
    public class UserModelContext : DbContext
    {
        public UserModelContext(DbContextOptions<UserModelContext> options)
            : base(options)
        { }

        public DbSet<UsernameEntity> Users { get; set; }
    }
}