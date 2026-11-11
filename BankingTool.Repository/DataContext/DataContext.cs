using System.Security.Claims;
using System.Text.Json;
using BankingTool.Model;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Dto.SPDto;
using BankingTool.Model.Model;
using BankingTool.Repository.EntityConfiguration;
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
        public readonly string UserName;
        public readonly int StaffId;
        public readonly int CustomerId;
        public DataContext(DbContextOptions<DataContext> dbContextOptions, IHttpContextAccessor httpContextAccessor) : base(dbContextOptions)
        {
            if (httpContextAccessor.HttpContext.User.Claims.Any())
            {
                List<ClaimDto> claims = JsonSerializer.Deserialize<List<ClaimDto>>(httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == nameof(ClaimTypes.Email))?.Value);
                UserId = Convert.ToInt32(claims.FirstOrDefault(x => x.Key == AppClaimTypes.UserId).Value);
                RoleId = Convert.ToInt32(claims.FirstOrDefault(x => x.Key == AppClaimTypes.RoleId).Value);
                StaffId = Convert.ToInt32(claims.FirstOrDefault(x => x.Key == AppClaimTypes.StaffId)?.Value);
                CustomerId = Convert.ToInt32(claims.FirstOrDefault(x => x.Key == AppClaimTypes.CustomerId).Value);
                UserEmail = claims.FirstOrDefault(x => x.Key == AppClaimTypes.EmailId).Value;
                UserName = claims.FirstOrDefault(x => x.Key == AppClaimTypes.UserName).Value;
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
        public DbSet<TransactionDetail> TransactionDetail { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<CodeValue> CodeValue { get; set; }
        public DbSet<BudgetAccount> BudgetAccount { get; set; }
        public DbSet<BudgetTransaction> BudgetTransaction { get; set; }
        public DbSet<BudgetTransactionDetail> BudgetTransactionDetail { get; set; }
        public DbSet<BudgetNotification> BudgetNotification { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<CalendarEvent> CalendarEvent { get; set; }

        public DbSet<WalletAccount> WalletAccount { get; set; }
        public DbSet<WalletTransaction> WalletTransaction { get; set; }
        public DbSet<WalletTransactionDetail> WalletTransactionDetail { get; set; }
        public DbSet<Scheme> Scheme { get; set; }
        public DbSet<MutualFunds> MutualFunds { get; set; }
        public DbSet<UserInvestment> UserInvestment { get; set; }
        public DbSet<BudgetPlanner> BudgetPlanner { get; set; }
        public DbSet<BudgetItem> BudgetItem { get; set; }

        // Credit Card Feature
        public DbSet<CreditCard> CreditCard { get; set; }
        public DbSet<CreditCardApplication> CreditCardApplication { get; set; }
        public DbSet<CreditCardTransaction> CreditCardTransaction { get; set; }
        public DbSet<CreditCardStatement> CreditCardStatement { get; set; }
        public DbSet<CreditCardStatementDetail> CreditCardStatementDetail { get; set; }
        public DbSet<CreditCardFee> CreditCardFee { get; set; }

        // SP
        public DbSet<GetActionsByUserIdDto> GetActionsByUserIdDto { get; set; }
        public DbSet<GetTransactionsListResponseTransactionList> GetTransactionsListResponseTransactionList { get; set; }
        public DbSet<GetSenderAccount> GetSenderAccount { get; set; }
        public DbSet<GetReceiverAccount> GetReceiverAccount { get; set; }
        public DbSet<GetBudgetTransactionList> GetBudgetTransactionList { get; set; }
        public DbSet<GetWalletTransactionList> GetWalletTransactionList { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tables
            modelBuilder.ApplyConfiguration(new CodeValueConfiguration());
            modelBuilder.ApplyConfiguration(new StateConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ActionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleAccessConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CreditScoreConfiguration());
            modelBuilder.ApplyConfiguration(new BankConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionDetailConfiguration());
            modelBuilder.ApplyConfiguration(new BudgetAccountConfiguration());
            modelBuilder.ApplyConfiguration(new BudgetTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new BudgetTransactionDetailConfiguration());
            modelBuilder.ApplyConfiguration(new BudgetNotificationConfiguration());
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            modelBuilder.ApplyConfiguration(new CalendarEventConfiguration());
            modelBuilder.ApplyConfiguration(new WalletAccountConfiguration());
            modelBuilder.ApplyConfiguration(new WalletTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new WalletTransactionDetailConfiguration());
            modelBuilder.ApplyConfiguration(new SchemeConfiguration());
            modelBuilder.ApplyConfiguration(new MutualFundsConfiguration());
            modelBuilder.ApplyConfiguration(new UserInvestmentConfiguration());
            modelBuilder.ApplyConfiguration(new BudgetPlannerConfiguration());
            modelBuilder.ApplyConfiguration(new BudgetItemConfiguration());

            // Credit Card Configurations
            modelBuilder.ApplyConfiguration(new CreditCardApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCardConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCardTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCardStatementConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCardStatementDetailConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCardFeeConfiguration());

            //SPs
            modelBuilder.Entity<GetActionsByUserIdDto>(e => e.HasNoKey());
            modelBuilder.Entity<GetTransactionsListResponseTransactionList>(e => e.HasNoKey());
            modelBuilder.Entity<GetBudgetTransactionList>(e => e.HasNoKey());
            modelBuilder.Entity<GetSenderAccount>(e => e.HasNoKey());
            modelBuilder.Entity<GetReceiverAccount>(e => e.HasNoKey());
            modelBuilder.Entity<GetWalletTransactionList>(e => e.HasNoKey());

            base.OnModelCreating(modelBuilder);
        }
    }
}
