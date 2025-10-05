namespace BankingTool.Model.Model
{
    public class WalletTransaction : BaseSchema
    {
        public int WalletTransactionId { get; set; }
        public string TransactionCategory { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionTime { get; set; }
        public string TransactionTag { get; set; }


        public virtual ICollection<WalletTransactionDetail> WalletTransactionDetail { get; set; }
    }
}
