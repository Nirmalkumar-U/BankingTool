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
    }
}
