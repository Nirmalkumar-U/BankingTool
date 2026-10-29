using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Model;

namespace BankingTool.Repository.IRepository
{
    public interface IBudgetPlannerRepository
    {
        Task<bool> CreateBudgetPlanner(BudgetPlanner budgetPlanner, List<BudgetItem> budgetItems);
        Task<bool> UpdateBudgetPlanner(BudgetPlanner budgetPlanner);
        Task<bool> UpdateBudgetPlanner(BudgetPlanner budgetPlanner, List<BudgetItem> budgetItems);
        Task<bool> DeleteBudgetPlanner(int budgetPlannerId);
        Task<BudgetPlanner> GetBudgetPlannerById(int budgetPlannerId);
        Task<List<BudgetPlanner>> GetBudgetPlannerList();
        Task<BudgetPlanner> GetBudgetPlannerDetailById(int budgetPlannerId);
    }
}
