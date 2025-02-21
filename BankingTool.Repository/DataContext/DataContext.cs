using BankingTool.Model;
using BankingTool.Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Action = BankingTool.Model.Action;

namespace BankingTool.Repository
{
    public class DataContext : DbContext
    {
        public readonly int UserId;
        public readonly int RoleId;
        public readonly string UserEmail;
        public readonly int RoleLevel;
        public DataContext(DbContextOptions<DataContext> dbContextOptions, IHttpContextAccessor httpContextAccessor) : base(dbContextOptions)
        {
            if (httpContextAccessor.HttpContext.User.Claims.Any())
            {
                this.UserId = Convert.ToInt32(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == AppClaimTypes.UserId)?.Value);
                this.RoleId = Convert.ToInt32(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == AppClaimTypes.RoleId)?.Value);
                this.RoleLevel = Convert.ToInt32(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == AppClaimTypes.RoleLevel)?.Value);
                this.UserEmail = httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == AppClaimTypes.EmailId)?.Value;
            }
        }

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
        public DbSet<Bank> Bank { get; set; }
        public DbSet<CodeValue> CodeValue { get; set; }

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
                entity.Property(e => e.State).HasColumnName("State").HasColumnType("int");
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
            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(ur => ur.BankId);
            });
            modelBuilder.Entity<CodeValue>(entity =>
            {
                entity.HasKey(ur => ur.CodeValueId);
            });

            modelBuilder.Entity<GetActionsByUserIdDto>(e => e.HasNoKey());
            base.OnModelCreating(modelBuilder);
        }
    }
}
