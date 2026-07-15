namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetBudgetPlannerListResponse
    {
        public List<GetBudgetPlannerList> BudgetPlanners { get; set; } = new List<GetBudgetPlannerList>();
    }

    public class GetBudgetPlannerList
    {
        public int BudgetPlannerId { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
        public decimal TotalBudget { get; set; }
        public int TotalItems { get; set; }
        public decimal AllocatedAmount { get; set; }
    }

    public class GetBudgetPlannerDetailResponse
    {
        public int BudgetPlannerId { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
        public decimal TotalBudget { get; set; }
        public List<BudgetItemResponse> BudgetItems { get; set; } = new List<BudgetItemResponse>();
    }

    public class BudgetItemResponse
    {
        public int BudgetItemId { get; set; }
        public string BudgetName { get; set; }
        public decimal AllocatedAmount { get; set; }
    }
}
