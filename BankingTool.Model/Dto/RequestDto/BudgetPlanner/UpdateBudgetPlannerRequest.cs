namespace BankingTool.Model.Dto.RequestDto.BudgetPlanner
{
    public class UpdateBudgetPlannerRequestObject
    {
        public UpdateBudgetPlannerRequest Request { get; set; }
    }

    public class UpdateBudgetPlannerRequest
    {
        public UpdateBudgetPlannerRequestData BudgetPlanner { get; set; }
    }

    public class UpdateBudgetPlannerRequestData
    {
        public int BudgetPlannerId { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
        public decimal TotalBudget { get; set; }
        public List<BudgetItemData> BudgetItems { get; set; } = new List<BudgetItemData>();
    }
}
