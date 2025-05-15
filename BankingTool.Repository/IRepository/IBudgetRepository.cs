using BankingTool.Model.Model;

namespace BankingTool.Repository.IRepository
{
    public interface IBudgetRepository
    {
        public Task<BudgetAccount> GetLastBudgetAccount();
        public Task<bool> CreateBudgetAccount(BudgetAccount budgetAccount, BudgetTransaction budgetTransaction, BudgetTransactionDetail budgetTransactionDetails);
        public Task<int> GetPrimaryAccountIdOfCustomer();
    }
}
