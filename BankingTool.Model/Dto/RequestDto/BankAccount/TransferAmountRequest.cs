using BankingTool.Model.Dto.RequestDtos;
using BankingTool.Model.Enums;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class TransferAmountRequestObject
    {
        public TransferAmountRequest Request { get; set; }
    }
    public class TransferAmountRequest
    {
        public TransferAmountRequestAccount Account { get; set; }
        public TransferAmountRequestTransaction Transaction { get; set; }
        public List<TransferAmountRequestBudgetAccount> BudgetAccount { get; set; }
    }
    public class TransferAmountRequestAccount
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
    }
    public class TransferAmountRequestTransaction
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionTagEnum TransactionTag { get; set; }
    }
    public class TransferAmountRequestBudgetAccount : RequestId
    {
        public decimal Amount { get; set; }
    }
}
