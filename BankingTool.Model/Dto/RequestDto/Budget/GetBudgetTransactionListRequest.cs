using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Budget
{
    public class GetBudgetTransactionListRequestObject
    {
        public GetBudgetTransactionListRequest Request { get; set; }
    }

    public class GetBudgetTransactionListRequest
    {
        public GetBudgetTransactionListRequestBudgetAccount BudgetAccount { get; set; }
    }

    public class GetBudgetTransactionListRequestBudgetAccount : RequestId
    {
    }
}
