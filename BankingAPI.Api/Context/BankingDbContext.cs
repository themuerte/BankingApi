using Microsoft.EntityFrameworkCore;
using BankingAPI.Api.Models;

namespace BankingAPI.Api.Data
{
    public class BankingDbContext : DbContext
    {
         public BankingDbContext(DbContextOptions<BankingDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClientModel> Clients => Set<ClientModel>();
        public DbSet<AccountBankModel>   Accounts  => Set<AccountBankModel>();
        public DbSet<TransactionsModel>  Transactions => Set<TransactionsModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Client -> Account
            modelBuilder.Entity<AccountBankModel>()
                        .HasOne(a => a.Client)
                        .WithMany(c => c.Accounts)
                        .HasForeignKey(a => a.ClientId)
                        .OnDelete(DeleteBehavior.Cascade);

            // Account -> Transaction
            modelBuilder.Entity<TransactionsModel>()
                        .HasOne(t => t.AccountBank)
                        .WithMany(a => a.Transactions)
                        .HasForeignKey(t => t.AccountBankId)
                        .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<AccountBankModel>()
                        .HasIndex(a => a.AccountNumber)
                        .IsUnique();
        }
    }
}