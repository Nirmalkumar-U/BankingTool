using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Budget
{
    public class RejectBudgetTransactionRequestObject
    {
        public RejectBudgetTransactionRequest Request { get; set; }
    }
    public class RejectBudgetTransactionRequest
    {
        public RejectBudgetTransactionRequestBudgetNotification BudgetNotification { get; set; }
    }
    public class RejectBudgetTransactionRequestBudgetNotification : RequestId
    {
    }
}
