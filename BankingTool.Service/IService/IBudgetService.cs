using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto;

namespace BankingTool.Service.IService
{
    public interface IBudgetService
    {
        Task<ResponseDto<bool>> CreateBudgetAccount(string budgetName, int amount);
        Task<ResponseDto<GetBudgetListResponse>> GetBudgetList();
        Task<ResponseDto<GetBudgetTransactionListResponse>> GetBudgetTransactionList(int budgetAccountId);
        Task<ResponseDto<GetBudgetNotificationListResponse>> GetBudgetNotificationList();
        Task<ResponseDto<bool>> AcceptBudgetTransaction(int BudgetAccountId, int BudgetNotificationId);
    }
}
