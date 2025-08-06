using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class WalletRespository(DataContext dataContext) : EntityRepository<WalletAccount>(dataContext), IWalletRespository
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
        private void UpdateBudgetAccount(WalletAccount model)
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
        #endregion Private
    }
}
