using Microsoft.EntityFrameworkCore;
using ThucTapW1.Entities;
using TT1.Entities;

namespace TT1.AppDbContexts
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Decentralization> Decentralizations { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ConfirmEmail> ConfirmEmails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = ADMIN-PC; database = ttw1; Trusted_Connection = true; TrustServerCertificate = true");
        }
    }
}
