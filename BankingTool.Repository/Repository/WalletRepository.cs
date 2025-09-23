using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class WalletRepository(DataContext dataContext) : EntityRepository<WalletAccount>(dataContext), IWalletRepository
    {
        public async Task<WalletAccount> GetLastWalletAccount()
        {
            if (dataContext.WalletAccount.Any())
                return await dataContext.WalletAccount.OrderByDescending(a => a.WalletAccountId).FirstOrDefaultAsync();
            return null;
        }
        public async Task<bool> CreateWalletAccount(WalletAccount walletAccount, WalletTransaction walletTransaction, WalletTransactionDetail walletTransactionDetails)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                walletAccount.CustomerId = dataContext.CustomerId;
                var budgetAccountId = InsertWalletAccount(walletAccount);
                var budgetTransactionId = InsertWalletTransaction(walletTransaction);
                walletTransactionDetails.WalletTransactionId = budgetTransactionId.Value;
                walletTransactionDetails.WalletAccountId = budgetAccountId.Value;
                var budgetTransactionDetailId = InsertWalletTransactionDetail(walletTransactionDetails);
                sqlTransaction.Commit();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlTransaction.Rollback();
                return await Task.FromResult(false);
            }
        }
        public async Task<List<GetWalletList>> GetWalletList()
        {
            return await dataContext.WalletAccount
                .Where(x => x.CustomerId == dataContext.CustomerId && x.IsDeleted == false)
                .Select(x => new GetWalletList
                {
                    WalletId = x.WalletAccountId,
                    WalletName = x.WalletName,
                    WalletNumber = x.WalletNumber,
                    Balance = x.Balance,
                    WalletStatus = x.WalletStatus
                }).ToListAsync();
        }
        public async Task<WalletAccount> GetWalletById(int walletId)
        {
            return await dataContext.WalletAccount
                .FirstOrDefaultAsync(x => x.WalletAccountId == walletId);
        }
        public async Task<List<GetWalletTransactionList>> GetWalletTransactionList(int walletId)
        {
            var sql = "EXEC GetWalletAccountTransactions @WalletId";
            var parameters = new[]
                            {
                                new Microsoft.Data.SqlClient.SqlParameter("@WalletId", walletId.ToString())
                            };

            var transactions = await dataContext.Set<GetWalletTransactionList>()
                    .FromSqlRaw(sql, parameters)
                    .ToListAsync();

            return transactions;
        }
        public async Task<bool> ChangeWalletStatus(int walletId)
        {
            var walletAccount = await dataContext.WalletAccount.Where(x => x.WalletAccountId == walletId).FirstOrDefaultAsync();
            if (walletAccount != null)
            {
                walletAccount.WalletStatus = walletAccount.WalletStatus == WalletStatus.Active ? WalletStatus.InActive : WalletStatus.Active;
                UpdateWalletAccount(walletAccount);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<WalletTransaction> GetLastWalletTransaction(int walletAccountId)
        {
            return await (from t in dataContext.WalletTransaction
                          join td in dataContext.WalletTransactionDetail on t.WalletTransactionId equals td.WalletTransactionId
                          where td.WalletAccountId == walletAccountId
                          orderby t.TransactionTime descending
                          select t).FirstOrDefaultAsync();
        }
        public async Task<WalletAccount> GetWalletAccount(int walletAccountId)
        {
            return await dataContext.WalletAccount.FirstOrDefaultAsync(x => x.WalletAccountId == walletAccountId);
        }
        public bool SaveTransactions(WalletTransactionDto walletDetails, List<BudgetTransactionDto> budgetTransactionDetail, AccountTransactionsDto accountDetails)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                //Wallet
                if (walletDetails != null)
                {
                    UpdateWalletAccount(walletDetails.WalletAccount);
                    var transactionId = InsertWalletTransaction(walletDetails.WalletTransaction);
                    walletDetails.WalletTransactionDetail.WalletTransactionId = transactionId.Value;
                    InsertWalletTransactionDetail(walletDetails.WalletTransactionDetail);
                }

                //Budget
                if (budgetTransactionDetail != null && budgetTransactionDetail.Count != 0)
                {
                    foreach (var budgetAccount in budgetTransactionDetail)
                    {
                        UpdateBudgetAccount(budgetAccount.BudgetAccount);
                        var budgetTransactionId = InsertBudgetTransaction(budgetAccount.BudgetTransaction);
                        budgetAccount.BudgetTransactionDetail.BudgetTransactionId = budgetTransactionId.Value;
                        var budgetTransactionDetailId = InsertBudgetTransactionDetail(budgetAccount.BudgetTransactionDetail);
                    }
                }

                //Account
                if (accountDetails != null)
                {
                    UpdateAccount(accountDetails.Account);
                    var accountTransactionId = InsertTransaction(accountDetails.Transaction);
                    accountDetails.TransactionDetail.TransactionId = accountTransactionId.Value;
                    InsertTransactionDetail(accountDetails.TransactionDetail);
                }

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
        public async Task<int> GetWalletBalance(int accountId)
        {
            return await (from a in dataContext.WalletAccount
                          where a.WalletAccountId == accountId && a.WalletStatus == WalletStatus.Active
                          select a.Balance).FirstOrDefaultAsync();
        }
        public async Task<List<DropDownDto>> GetFromWalletAccountListByCustomerId()
        {
            return await (from a in dataContext.WalletAccount
                          join c in dataContext.Customer on a.CustomerId equals c.CustomerId
                          join u in dataContext.Users on c.UserId equals u.UserId
                          where a.CustomerId == dataContext.CustomerId && a.WalletStatus == WalletStatus.Active && a.IsDeleted == false && u.IsDeleted == false
                          select new DropDownDto
                          {
                              Key = a.WalletAccountId,
                              Value = a.WalletName + " | " + a.WalletNumber
                          }).ToListAsync();
        }
        public async Task<List<DropDownDto>> GetToWalletAccountListByCustomerId()
        {
            return await (from a in dataContext.WalletAccount
                          join c in dataContext.Customer on a.CustomerId equals c.CustomerId
                          join u in dataContext.Users on c.UserId equals u.UserId
                          where a.CustomerId != dataContext.CustomerId && a.WalletStatus == WalletStatus.Active && a.IsDeleted == false && u.IsDeleted == false
                          select new DropDownDto
                          {
                              Key = a.WalletAccountId,
                              Value = a.WalletName + " | " + a.WalletNumber
                          }).ToListAsync();
        }
        #region Private
        private int? InsertWalletAccount(WalletAccount model)
        {
            model.CreatedBy = dataContext.UserEmail;
            model.CreatedDate = DateTime.Now;
            model.ModifiedBy = null;
            model.ModifiedDate = null;
            model.IsDeleted = false;
            var isInserted = Insert(model);
            return isInserted ? model.WalletAccountId : null;
        }
        private void UpdateWalletAccount(WalletAccount model)
        {
            model.ModifiedBy = dataContext.UserEmail;
            model.ModifiedDate = DateTime.Now;
            Update(model);
        }
        private int? InsertWalletTransaction(WalletTransaction model)
        {
            model.CreatedBy = dataContext.UserEmail;
            model.CreatedDate = DateTime.Now;
            model.ModifiedBy = null;
            model.ModifiedDate = null;
            model.IsDeleted = false;
            var isInserted = Insert(model);
            return isInserted ? model.WalletTransactionId : null;
        }
        private int? InsertWalletTransactionDetail(WalletTransactionDetail model)
        {
            var isInserted = Insert(model);
            return isInserted ? model.WalletTransactionDetailId : null;
        }
        private void UpdateBudgetAccount(BudgetAccount model)
        {
            model.ModifiedBy = dataContext.UserEmail;
            model.ModifiedDate = DateTime.Now;
            Update(model);
        }
        private int? InsertBudgetTransaction(BudgetTransaction model)
        {
            model.CreatedBy = dataContext.UserEmail;
            model.CreatedDate = DateTime.Now;
            model.ModifiedBy = null;
            model.ModifiedDate = null;
            model.IsDeleted = false;
            var isInserted = Insert(model);
            return isInserted ? model.BudgetTransactionId : null;
        }
        private int? InsertBudgetTransactionDetail(BudgetTransactionDetail model)
        {
            var isInserted = Insert(model);
            return isInserted ? model.BudgetTransactionDetailId : null;
        }
        public void UpdateAccount(Account account)
        {
            account.ModifiedBy = dataContext.UserEmail;
            account.ModifiedDate = DateTime.Now;
            Update(account);
        }
        public int? InsertTransaction(Transaction transaction)
        {
            transaction.CreatedBy = dataContext.UserEmail;
            transaction.CreatedDate = DateTime.Now;
            transaction.ModifiedBy = null;
            transaction.ModifiedDate = null;
            transaction.IsDeleted = false;
            var isInserted = Insert(transaction);
            return isInserted ? transaction.TransactionId : null;
        }
        public void InsertTransactionDetail(TransactionDetail transactionDetail)
        {
            var isInserted = Insert(transactionDetail);
        }
        #endregion Private
    }
}
