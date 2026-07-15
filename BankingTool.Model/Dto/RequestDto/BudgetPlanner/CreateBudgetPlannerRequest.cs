namespace BankingTool.Model.Dto.RequestDto.BudgetPlanner
{
    public class CreateBudgetPlannerRequestObject
    {
        public CreateBudgetPlannerRequest Request { get; set; }
    }

    public class CreateBudgetPlannerRequest
    {
        public CreateBudgetPlannerRequestData BudgetPlanner { get; set; }
    }

    public class CreateBudgetPlannerRequestData
    {
        public string PlanName { get; set; }
        public string Description { get; set; }
        public decimal TotalBudget { get; set; }
        public List<BudgetItemData> BudgetItems { get; set; } = new List<BudgetItemData>();
    }

    public class BudgetItemData
    {
        public string BudgetName { get; set; }
        public decimal AllocatedAmount { get; set; }
    }
}
