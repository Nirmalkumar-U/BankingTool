using BankingTool.Model.Dto.RequestDto.BudgetPlanner;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class BudgetPlannerService(IBudgetPlannerRepository budgetPlannerRepository) : IBudgetPlannerService
    {
        private readonly IBudgetPlannerRepository _budgetPlannerRepository = budgetPlannerRepository;

        public async Task<bool> CreateBudgetPlanner(CreateBudgetPlannerRequest request)
        {
            try
            {
                if (request?.BudgetPlanner == null)
                    return false;

                var budgetPlanner = new BudgetPlanner
                {
                    PlanName = request.BudgetPlanner.PlanName,
                    Description = request.BudgetPlanner.Description,
                    TotalBudget = request.BudgetPlanner.TotalBudget
                };

                var budgetItems = request.BudgetPlanner.BudgetItems?.Select(x => new BudgetItem
                {
                    BudgetName = x.BudgetName,
                    AllocatedAmount = x.AllocatedAmount
                }).ToList() ?? new List<BudgetItem>();

                return await _budgetPlannerRepository.CreateBudgetPlanner(budgetPlanner, budgetItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateBudgetPlanner(UpdateBudgetPlannerRequest request)
        {
            try
            {
                if (request?.BudgetPlanner == null)
                    return false;

                var budgetPlanner = await _budgetPlannerRepository.GetBudgetPlannerDetailById(request.BudgetPlanner.BudgetPlannerId);

                return await _budgetPlannerRepository.UpdateBudgetPlanner(budgetPlanner);
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
                return await _budgetPlannerRepository.DeleteBudgetPlanner(budgetPlannerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<GetBudgetPlannerListResponse> GetBudgetPlannerList()
        {
            try
            {
                var budgetPlanners = await _budgetPlannerRepository.GetBudgetPlannerList();
                
                return new GetBudgetPlannerListResponse
                {
                    BudgetPlanners = budgetPlanners.Select(x => new GetBudgetPlannerList
                    {
                        BudgetPlannerId = x.BudgetPlannerId,
                        PlanName = x.PlanName,
                        Description = x.Description,
                        TotalBudget = x.TotalBudget,
                        AllocatedAmount = x.
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GetBudgetPlannerListResponse();
            }
        }

        public async Task<GetBudgetPlannerDetailResponse> GetBudgetPlannerDetail(int budgetPlannerId)
        {
            try
            {
                var budgetPlanner = await _budgetPlannerRepository.GetBudgetPlannerDetailById(budgetPlannerId);
                return new GetBudgetPlannerDetailResponse()
                {
                    BudgetPlannerId = budgetPlanner.BudgetPlannerId,
                    PlanName = budgetPlanner.PlanName,
                    Description = budgetPlanner.Description,
                    TotalBudget = budgetPlanner.TotalBudget,
                    BudgetItems = budgetPlanner.BudgetItems?.Select(x => new BudgetItemResponse
                    {
                        BudgetItemId = x.BudgetItemId,
                        BudgetName = x.BudgetName,
                        AllocatedAmount = x.AllocatedAmount
                    }).ToList() ?? new List<BudgetItemResponse>()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GetBudgetPlannerDetailResponse();
            }
        }
    }
}
