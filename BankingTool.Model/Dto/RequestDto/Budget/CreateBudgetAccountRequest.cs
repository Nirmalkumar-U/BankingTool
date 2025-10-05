namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class CreateBudgetAccountRequestObject
    {
        public CreateBudgetAccountRequest Request { get; set; }
    }
    public class CreateBudgetAccountRequest
    {
        public CreateBudgetAccountRequestBudgetAccount BudgetAccount { get; set; }
    }
    public class CreateBudgetAccountRequestBudgetAccount
    {
        public string BudgetName { get; set; }
        public decimal Amount { get; set; }
    }
}
