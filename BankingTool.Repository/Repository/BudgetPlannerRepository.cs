using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BankingTool.Repository.Repository
{
    public class BudgetPlannerRepository(DataContext dataContext) : EntityRepository<BudgetPlanner>(dataContext), IBudgetPlannerRepository
    {
        public async Task<bool> CreateBudgetPlanner(BudgetPlanner budgetPlanner, List<BudgetItem> budgetItems)
        {
            using var transaction = dataContext.Database.BeginTransaction();
            try
            {
                budgetPlanner.UserId = dataContext.CustomerId;
                budgetPlanner.IsDeleted = false;

                await dataContext.BudgetPlanner.AddAsync(budgetPlanner);
                await dataContext.SaveChangesAsync();

                if (budgetItems != null && budgetItems.Count > 0)
                {
                    foreach (var item in budgetItems)
                    {
                        item.BudgetPlannerId = budgetPlanner.BudgetPlannerId;
                    }
                    await dataContext.BudgetItem.AddRangeAsync(budgetItems);
                    await dataContext.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateBudgetPlanner(BudgetPlanner budgetPlanner)
        {
            try
            {
                var existing = await dataContext.BudgetPlanner.FirstOrDefaultAsync(x => x.BudgetPlannerId == budgetPlanner.BudgetPlannerId);
                if (existing == null)
                    return false;

                existing.PlanName = budgetPlanner.PlanName;
                existing.Description = budgetPlanner.Description;
                existing.TotalBudget = budgetPlanner.TotalBudget;

                dataContext.BudgetPlanner.Update(existing);
                await dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteBudgetPlanner(int budgetPlannerId)
        {
            try
            {
                var budgetPlanner = await dataContext.BudgetPlanner.FirstOrDefaultAsync(x => x.BudgetPlannerId == budgetPlannerId);
                if (budgetPlanner == null)
                    return false;

                budgetPlanner.IsDeleted = true;
                dataContext.BudgetPlanner.Update(budgetPlanner);
                await dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<BudgetPlanner> GetBudgetPlannerById(int budgetPlannerId)
        {
            return await dataContext.BudgetPlanner
                .Include(x => x.BudgetItems)
                .FirstOrDefaultAsync(x => x.BudgetPlannerId == budgetPlannerId && x.IsDeleted == false);
        }

        public async Task<List<BudgetPlanner>> GetBudgetPlannerList()
        {
            var budgetPlanners = await dataContext.BudgetPlanner
                .Include(x => x.BudgetItems)
                .Where(x => x.UserId == dataContext.CustomerId && x.IsDeleted == false)
                .OrderByDescending(x => x.BudgetPlannerId)
                .ToListAsync();

            return budgetPlanners;
        }

        public async Task<BudgetPlanner> GetBudgetPlannerDetailById(int budgetPlannerId)
        {
            var budgetPlanner = await dataContext.BudgetPlanner
                .Include(x => x.BudgetItems)
                .FirstOrDefaultAsync(x => x.BudgetPlannerId == budgetPlannerId && x.UserId == dataContext.CustomerId && x.IsDeleted == false);

            return budgetPlanner;
        }
    }
}
