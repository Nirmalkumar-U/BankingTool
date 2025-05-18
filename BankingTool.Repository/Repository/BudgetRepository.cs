using BankingTool.Model;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class BudgetRepository(DataContext dataContext) : EntityRepository<BudgetAccount>(dataContext), IBudgetRepository
    {
        public async Task<BudgetAccount> GetLastBudgetAccount()
        {
            if (dataContext.BudgetAccount.Any())
                return await dataContext.BudgetAccount.OrderByDescending(a => a.BudgetAccountId).FirstOrDefaultAsync();
            return null;
        }
        public async Task<bool> CreateBudgetAccount(BudgetAccount budgetAccount, BudgetTransaction budgetTransaction, BudgetTransactionDetail budgetTransactionDetails)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                budgetAccount.CustomerId = dataContext.CustomerId;
                var budgetAccountId = InsertBudgetAccount(budgetAccount);
                var budgetTransactionId = InsertBudgetTransaction(budgetTransaction);
                budgetTransactionDetails.BudgetTransactionId = budgetTransactionId.Value;
                budgetTransactionDetails.BudgetAccountId = budgetAccountId.Value;
                var budgetTransactionDetailId = InsertBudgetTransactionDetail(budgetTransactionDetails);
                sqlTransaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlTransaction.Rollback();
                return false;
            }
        }
        public async Task<int> GetPrimaryAccountIdOfCustomer()
        {
            var primaryAccount = await dataContext.Customer.Include(x => x.Account)
                                            .Where(x => x.CustomerId == dataContext.CustomerId
                                                     && x.Account.Any(a => a.AccountNumber == x.PrimaryAccountNumber))
                                            .Select(x => x.Account.FirstOrDefault(a => a.AccountNumber == x.PrimaryAccountNumber))
                                            .FirstOrDefaultAsync();

            return primaryAccount?.AccountId ?? 0;
        }
        public async Task<BudgetAccount> GetBudgetAccountById(int budgetAccountId)
        {
            var budgetAccount = await dataContext.BudgetAccount
                .FirstOrDefaultAsync(x => x.BudgetAccountId == budgetAccountId);
            return budgetAccount;
        }
        public async Task<List<DropDownDto>> GetBudgetAccountDropdown()
        {
            var budgetAccount = await dataContext.BudgetAccount
                .Where(x => x.CustomerId == dataContext.CustomerId && x.BudgetStatus == "Active" && x.IsDeleted == false)
                .Select(x => new DropDownDto
                {
                    Key = x.BudgetAccountId,
                    Value = x.BudgetName,
                    IsSelected = false
                }).ToListAsync();
            return budgetAccount;
        }
        public async Task<List<GetBudgetList>> GetBudgetList()
        {
            var budgetAccountList = await dataContext.BudgetAccount
                .Where(x => x.CustomerId == dataContext.CustomerId && x.IsDeleted == false)
                .Select(x => new GetBudgetList
                {
                    BudgetAccountId = x.BudgetAccountId,
                    BudgetName = x.BudgetName,
                    BudgetNumber = x.BudgetNumber,
                    Balance = x.Balance,
                    BudgetStatus = x.BudgetStatus
                }).ToListAsync();
            return budgetAccountList;
        }
        public async Task<List<GetBudgetTransactionList>> GetBudgetTransactionList(int budgetAccountId)
        {
            var sql = "EXEC GetBudgetAccountTransactions @BudgetAccountId";
            var parameters = new[]
                            {
                                new Microsoft.Data.SqlClient.SqlParameter("@BudgetAccountId", budgetAccountId.ToString())
                            };

            var transactions = await dataContext.Set<GetBudgetTransactionList>()
                    .FromSqlRaw(sql, parameters)
                    .ToListAsync();

            return transactions;
        }
        public async Task<BudgetTransaction> GetLastBudgetTransaction(int BudgetAccountId)
        {
            return await (from t in dataContext.BudgetTransaction
                          join td in dataContext.BudgetTransactionDetail on t.BudgetTransactionId equals td.BudgetTransactionId
                          where td.BudgetAccountId == BudgetAccountId
                          orderby t.TransactionTime descending
                          select t).FirstOrDefaultAsync();
        }
        public async Task AcceptBudgetTransaction(int BudgetAccountId, int BudgetNotificationId)
        {
            DateTime transferedTime = DateTime.Now;
            string transactionCatagory = TransactionCatagory.Transfer;
            int amount = 100;
            string description = "Transfer";
            string transactionTag = TransactionTag.Transfer;
            string transactionType = TransactionType.Credit;
            string transactionRole = TransactionRole.Receiver;
            int accountId = 1;

            var budgetTransactionAboveTransferedTime = await dataContext.BudgetTransaction
                .Include(x => x.BudgetTransactionDetail)
                .Where(x => x.TransactionTime > transferedTime)
                .OrderBy(x => x.TransactionTime)
                .ToListAsync();
            if (budgetTransactionAboveTransferedTime == null || budgetTransactionAboveTransferedTime.Count == 0)
            {
                var lastBudgetAccountTransaction = await GetLastBudgetTransaction(BudgetAccountId);
                var budgetTransactionDate = transferedTime;
                if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction.TransactionTime.Date == budgetTransactionDate.Date)
                {
                    budgetTransactionDate = lastBudgetAccountTransaction.TransactionTime;
                    budgetTransactionDate = budgetTransactionDate.AddMinutes(5);
                }
                var budgetAccount = await GetBudgetAccountById(BudgetAccountId);
                budgetAccount.Balance = transactionRole == TransactionRole.Receiver ? budgetAccount.Balance + amount : budgetAccount.Balance - amount;
                var budgetTransaction = new BudgetTransaction()
                {
                    Amount = amount,
                    Description = description,
                    TransactionTime = budgetTransactionDate,
                    TransactionCategory = transactionCatagory,
                    TransactionTag = transactionTag
                };
                var budgetTransactionDetails = new BudgetTransactionDetail()
                {
                    TransactionType = transactionType,
                    StageBalance = budgetAccount.Balance,
                    TransactionRole = transactionRole,
                    AccountId = accountId,
                    BudgetAccountId = budgetAccount.BudgetAccountId
                };
                UpdateBudgetAccount(budgetAccount);
                var budgetTransactionId = InsertBudgetTransaction(budgetTransaction);
                budgetTransactionDetails.BudgetTransactionId = budgetTransactionId.Value;
                var budgetTransactionDetailId = InsertBudgetTransactionDetail(budgetTransactionDetails);
            }
            else
            {
                var previousTransaction = dataContext.BudgetTransaction
                    .Include(x => x.BudgetTransactionDetail)
                    .Where(t => t.TransactionTime < transferedTime)
                    .OrderByDescending(t => t.TransactionTime)
                    .FirstOrDefault();

                var lastTransactionAmount = previousTransaction.Amount;
                var lastTransactionStageBalance = previousTransaction.BudgetTransactionDetail.FirstOrDefault().StageBalance;

                BudgetTransaction budgetTransaction = new BudgetTransaction
                {
                    TransactionCategory = transactionCatagory,
                    Amount = amount,
                    Description = description,
                    TransactionTime = transferedTime,
                    TransactionTag = transactionTag
                };
                var budgetTransactionId = InsertBudgetTransaction(budgetTransaction);
                BudgetTransactionDetail budgetTransactionDetail = new BudgetTransactionDetail
                {
                    TransactionType = transactionType,
                    StageBalance = transactionRole == TransactionRole.Receiver ? lastTransactionStageBalance + amount : lastTransactionStageBalance - amount,
                    TransactionRole = transactionRole,
                    AccountId = accountId,
                    BudgetTransactionId = budgetTransactionId.Value,
                };
                var budgetTransactionDetailId = InsertBudgetTransactionDetail(budgetTransactionDetail);
                int currentStageBalance = budgetTransactionDetail.StageBalance;

                foreach (var budget in budgetTransactionAboveTransferedTime)
                {
                    var updateBudgetTransactionDetail = budget.BudgetTransactionDetail.FirstOrDefault();
                    updateBudgetTransactionDetail.StageBalance = updateBudgetTransactionDetail.TransactionRole == TransactionRole.Receiver ?
                        currentStageBalance + budget.Amount : currentStageBalance - budget.Amount;
                    Update(updateBudgetTransactionDetail);
                    currentStageBalance = updateBudgetTransactionDetail.StageBalance;
                }
                var budgetAccount = await GetBudgetAccountById(BudgetAccountId);
                budgetAccount.Balance = currentStageBalance;
                UpdateBudgetAccount(budgetAccount);
            }
        }

        public int? InsertBudgetAccount(BudgetAccount BudgetAccount)
        {
            BudgetAccount.CreatedBy = dataContext.UserEmail;
            BudgetAccount.CreatedDate = DateTime.Now;
            BudgetAccount.ModifiedBy = null;
            BudgetAccount.ModifiedDate = null;
            BudgetAccount.IsDeleted = false;
            var isInserted = Insert(BudgetAccount);
            return isInserted ? BudgetAccount.BudgetAccountId : null;
        }
        public void UpdateBudgetAccount(BudgetAccount BudgetAccount)
        {
            BudgetAccount.ModifiedBy = dataContext.UserEmail;
            BudgetAccount.ModifiedDate = DateTime.Now;
            Update(BudgetAccount);
        }
        public int? InsertBudgetTransaction(BudgetTransaction BudgetTransaction)
        {
            BudgetTransaction.CreatedBy = dataContext.UserEmail;
            BudgetTransaction.CreatedDate = DateTime.Now;
            BudgetTransaction.ModifiedBy = null;
            BudgetTransaction.ModifiedDate = null;
            BudgetTransaction.IsDeleted = false;
            var isInserted = Insert(BudgetTransaction);
            return isInserted ? BudgetTransaction.BudgetTransactionId : null;
        }
        public int? InsertBudgetTransactionDetail(BudgetTransactionDetail BudgetTransactionDetail)
        {
            var isInserted = Insert(BudgetTransactionDetail);
            return isInserted ? BudgetTransactionDetail.BudgetTransactionDetailId : null;
        }
        public void UpdateBudgetTransactionDetail(BudgetTransactionDetail model)
        {
            Update(model);
        }
    }
}
