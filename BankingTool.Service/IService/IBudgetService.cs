using BankingTool.Model;

namespace BankingTool.Service.IService
{
    public interface IBudgetService
    {
        Task<ResponseDto<bool>> CreateBudgetAccount(string budgetName, int amount);
    }
}
