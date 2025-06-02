using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Budget
{
    public class ChangeBudgetAccountStatusRequestObject
    {
        public ChangeBudgetAccountStatusRequest Request { get; set; }
    }
    public class ChangeBudgetAccountStatusRequest
    {
        public ChangeBudgetAccountStatusRequestBudgetAccount BudgetAccount { get; set; }
    }
    public class ChangeBudgetAccountStatusRequestBudgetAccount : RequestId
    {
    }
}
