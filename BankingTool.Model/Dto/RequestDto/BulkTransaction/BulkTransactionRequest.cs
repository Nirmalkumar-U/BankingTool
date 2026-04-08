using BankingTool.Model.Dto.RequestDtos;
using BankingTool.Model.Enums;

namespace BankingTool.Model.Dto.RequestDto.BulkTransaction
{
    public class BulkTransactionRequestObject
    {
        public IEnumerable<BulkTransactionRequest> Request { get; set; }
    }
    public class BulkTransactionRequest
    {
        public TransactionCategoryEnum TransactionCategory { get; set; }
        public BulkTransactionRequestAccount Account { get; set; }
        public BulkTransactionRequestTransaction Transaction { get; set; }
        public IEnumerable<BulkTransactionRequestBudgetAccount> BudgetAccount { get; set; }
    }
    public class BulkTransactionRequestAccount
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
    }
    public class BulkTransactionRequestTransaction
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionTagEnum TransactionTag { get; set; }
    }
    public class BulkTransactionRequestBudgetAccount : RequestId
    {
        public decimal Amount { get; set; }
    }
}
