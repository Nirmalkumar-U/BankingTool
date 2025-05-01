namespace BankingTool.Model.Model
{
    public class TransactionDetail
    {
        public int TransactionDetailsId { get; set; }
        public int TransactionId { get; set; }
        public string TransactionRole { get; set; }
        public string TransactionType { get; set; }
        public int AccountId { get; set; }
        public int StageBalance { get; set; }
    }
}
