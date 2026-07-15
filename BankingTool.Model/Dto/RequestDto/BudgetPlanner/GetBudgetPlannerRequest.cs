namespace BankingTool.Model.Dto.RequestDto.BudgetPlanner
{
    public class GetBudgetPlannerListRequest
    {
    }

    public class GetBudgetPlannerByIdRequest
    {
        public int BudgetPlannerId { get; set; }
    }

    public class DeleteBudgetPlannerRequest
    {
        public int BudgetPlannerId { get; set; }
    }
}
