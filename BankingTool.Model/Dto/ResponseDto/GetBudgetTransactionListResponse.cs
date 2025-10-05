using BankingTool.Model.Enums;

namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetBudgetTransactionListResponse
    {
        public string BudgetName { get; set; }
        public string BudgetNumber { get; set; }
        public BudgetAccountStatusEnum BudgetStatus { get; set; }
        public decimal Balance { get; set; }
        public List<GetBudgetTransactionList> BudgetTransactionList { get; set; }
    }
    public class GetBudgetTransactionList
    {
        public string AccountName { get; set; }
        public string TransactionTag { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal? ReceiverAmount { get; set; }
        public decimal? SenderAmount { get; set; }
        public decimal StageBalance { get; set; }

    }
}
