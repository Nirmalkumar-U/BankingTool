using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto;
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
        Task<List<GetBudgetList>> GetBudgetList();
        Task<List<GetBudgetTransactionList>> GetBudgetTransactionList(int budgetAccountId);
        Task<BudgetTransaction> GetLastBudgetTransaction(int BudgetAccountId);
        Task<bool> IsCustomerAsBudgetAccount(int customerId);
        Task<List<GetBudgetNotificationList>> GetBudgetNotificationList();
        Task<bool> AcceptBudgetTransaction(int BudgetAccountId, int BudgetNotificationId);
    }
}
