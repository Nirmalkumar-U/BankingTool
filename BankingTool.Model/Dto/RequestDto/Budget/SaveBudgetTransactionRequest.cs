using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Budget
{
    public class SaveBudgetTransactionRequestObject
    {
        public SaveBudgetTransactionRequest Request { get; set; }
    }
    public class SaveBudgetTransactionRequest
    {
        public SaveBudgetTransactionRequestFromBudgetAccount FromBudgetAccount { get; set; }
        public List<SaveBudgetTransactionRequestToBudgetAccount> ToBudgetAccount { get; set; }
    }
    public class SaveBudgetTransactionRequestFromBudgetAccount : RequestId
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
    public class SaveBudgetTransactionRequestToBudgetAccount : RequestId
    {
        public decimal Amount { get; set; }
    }
}
