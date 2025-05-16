using BankingTool.Model;
using BankingTool.Model.Model;

namespace BankingTool.Repository.IRepository
{
    public interface IBudgetRepository
    {
        Task<BudgetAccount> GetLastBudgetAccount();
        Task<bool> CreateBudgetAccount(BudgetAccount budgetAccount, BudgetTransaction budgetTransaction, BudgetTransactionDetail budgetTransactionDetails);
        Task<int> GetPrimaryAccountIdOfCustomer();
        Task<BudgetAccount> GetBudgetAccountById(int budgetAccountId);
        Task<List<DropDownDto>> GetBudgetAccountDropdown();
    }
}
