namespace BankingTool.Model
{
    public class Transaction : BaseSchema
    {
        public int TransactionId { get; set; }
        public string TransactionCategory { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
