namespace BankingTool.Model
{
    public class Transaction : BaseSchema
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
        public int StageBalance { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
