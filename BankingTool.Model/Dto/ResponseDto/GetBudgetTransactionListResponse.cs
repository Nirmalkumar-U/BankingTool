namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetBudgetTransactionListResponse
    {
        public string BudgetName { get; set; }
        public string BudgetNumber { get; set; }
        public string BudgetStatus { get; set; }
        public int Balance { get; set; }
        public List<GetBudgetTransactionList> BudgetTransactionList { get; set; }
    }
    public class GetBudgetTransactionList
    {
        public string AccountName { get; set; }
        public string TransactionTag { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public int? ReceiverAmount { get; set; }
        public int? SenderAmount { get; set; }
        public int StageBalance { get; set; }

    }
}
