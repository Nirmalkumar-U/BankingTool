using BankingTool.Model.Enums;
using BankingTool.Model.Model;

namespace BankingTool.Model.Dto.InternalDto
{
    public class BulkTransactionDto
    {
        public TransactionCategoryEnum TransactionCategory { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public Transaction Transaction { get; set; }
        public TransactionDetail FromTransactionDetail { get; set; }
        public TransactionDetail ToTransactionDetail { get; set; }
        public BudgetNotification BudgetNotification { get; set; }
        public List<BudgetTransactionDto> BudgetTransaction { get; set; } = [];
    }
}
