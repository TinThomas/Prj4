using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet.Models.Login;

namespace VareDatabase.DBContext
{
    public class WalletContext : DbContext
    {
        public WalletContext(DbContextOptions<WalletContext> options)
            : base(options)
        { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AddressEntity> Adresses { get; set; }
        public DbSet<WalletEntity> Wallets { get; set; }
        public DbSet<CardEntity> Cards { get; set; }

    }
}