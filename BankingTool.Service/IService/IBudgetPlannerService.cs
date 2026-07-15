using BankingTool.Model.Dto.RequestDto.BudgetPlanner;
using BankingTool.Model.Dto.ResponseDto;

namespace BankingTool.Service.IService
{
    public interface IBudgetPlannerService
    {
        Task<bool> CreateBudgetPlanner(CreateBudgetPlannerRequest request);
        Task<bool> UpdateBudgetPlanner(UpdateBudgetPlannerRequest request);
        Task<bool> DeleteBudgetPlanner(int budgetPlannerId);
        Task<GetBudgetPlannerListResponse> GetBudgetPlannerList();
        Task<GetBudgetPlannerDetailResponse> GetBudgetPlannerDetail(int budgetPlannerId);
    }
}
