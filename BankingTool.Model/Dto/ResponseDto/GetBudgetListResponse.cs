using BankingTool.Model.Enums;

namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetBudgetListResponse
    {
        public List<GetBudgetList> BudgetList { get; set; }
    }
    public class GetBudgetList
    {
        public int BudgetAccountId { get; set; }
        public string BudgetName { get; set; }
        public string BudgetNumber { get; set; }
        public decimal Balance { get; set; }
        public BudgetAccountStatusEnum BudgetStatus { get; set; }

    }
}
