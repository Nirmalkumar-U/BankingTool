using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Budget
{
    public class GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestObject
    {
        public GetToBudgetDropdownListAndBalanceOfBudgetAccountRequest Request { get; set; }
    }
    public class GetToBudgetDropdownListAndBalanceOfBudgetAccountRequest
    {
        public GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestBudgetAccount BudgetAccount { get; set; }
    }
    public class GetToBudgetDropdownListAndBalanceOfBudgetAccountRequestBudgetAccount : RequestId
    {
    }
}
