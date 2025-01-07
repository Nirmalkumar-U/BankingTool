using BankingTool.Model;
using Microsoft.EntityFrameworkCore;
using Action = BankingTool.Model.Action;

namespace BankingTool.Repository
{
    public class DataContext(DbContextOptions<DataContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Role> Role { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Action> Action { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RoleAccess> RoleAccess { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CreditScore> CreditScore { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Staff> Staff { get; set; }

        // SP
        public DbSet<GetActionsByUserIdDto> GetActionsByUserIdDto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(u => u.UserId);
            });

            modelBuilder.Entity<Action>(entity =>
            {
                entity.HasKey(a => a.ActionId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => ur.UserId);
            });

            modelBuilder.Entity<RoleAccess>(entity =>
            {
                entity.HasKey(ra => new { ra.RoleId, ra.ActionId });
            });
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(ur => ur.AccountId);
            });
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(ur => ur.CardId);
            });
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(ur => ur.CityId);
            });
            modelBuilder.Entity<CreditScore>(entity =>
            {
                entity.HasKey(ur => ur.CreditScoreId);
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(ur => ur.CustomerId);
            });
            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(ur => ur.StateId);
            });
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(ur => ur.TransactionId);
            });

            modelBuilder.Entity<GetActionsByUserIdDto>(e => e.HasNoKey());
            base.OnModelCreating(modelBuilder);
        }
    }
}
