using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Budget
{
    public class AcceptBudgetTransactionRequestObject
    {
        public AcceptBudgetTransactionRequest Request { get; set; }
    }
    public class AcceptBudgetTransactionRequest
    {
        public List<AcceptBudgetTransactionRequestBudgetAcccount> BudgetAccount { get; set; }
        public AcceptBudgetTransactionRequestBudgetNotification BudgetNotification { get; set; }
    }
    public class AcceptBudgetTransactionRequestBudgetAcccount : RequestId
    {
        public int Amount { get; set; }
    }
    public class AcceptBudgetTransactionRequestBudgetNotification : RequestId
    {
    }
}
