namespace BankingTool.Model.Model
{
    public class WalletTransactionDetail
    {
        public int WalletTransactionDetailId { get; set; }
        public int WalletTransactionId { get; set; }
        public int WalletAccountId { get; set; }
        public string TransactionRole { get; set; }
        public string TransactionType { get; set; }
        public decimal StageBalance { get; set; }

        // Optional navigation properties
        public virtual WalletTransaction WalletTransaction { get; set; }
        public virtual WalletAccount WalletAccount { get; set; }
    }

}
