namespace BankingTool.Model.Dto.BankAccount
{
    public class TransactionsListDto
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
        public string TransactionType { get; set; }
        public int StageBalance { get; set; }
    }
}
