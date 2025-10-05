using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.RequestDto.Budget;
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
        Task<bool> AcceptBudgetTransaction(List<AcceptBudgetTransactionRequestBudgetAcccount> BudgetAccount, int BudgetNotificationId);
        Task<bool> RejectBudgetTransaction(int BudgetNotificationId);
        Task<bool> ChangeBudgetAccountStatus(int budgetAccountId);
        Task<BudgetNotification> GetBudgetNotification(int BudgetNotificationId);
        Task<decimal> GetBudgetAccountBalance(int budgetAccountId);
        Task<List<DropDownDto>> GetBudgetAccountDropdownExceptBudgetAccountId(int budgetAccountId);
        Task<bool> SaveBudgetTransfer(BudgetAccount fromBudgetAccount, BudgetTransaction budgetTransaction,
           BudgetTransactionDetail fromBudgetTransactionDetail, List<BudgetTransactionDto> budgetAccountList);
    }
}
