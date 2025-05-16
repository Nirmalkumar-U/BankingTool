namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetBudgetListResponse
    {
        public List<GetBudgetList> budgetList { get; set; }
    }
    public class GetBudgetList
    {
        public int BudgetAccountId { get; set; }
        public string BudgetName { get; set; }
        public string BudgetNumber { get; set; }
        public int Balance { get; set; }
        public string BudgetStatus { get; set; }

    }
}
