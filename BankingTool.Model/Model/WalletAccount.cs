namespace BankingTool.Model.Model
{
    public class WalletAccount : BaseSchema
    {
        public int WalletAccountId { get; set; }
        public int CustomerId { get; set; }
        public string WalletName { get; set; }
        public string WalletNumber { get; set; }
        public int Balance { get; set; }
        public string WalletStatus { get; set; }

        // Optional navigation property
        public virtual Customer Customer { get; set; }
        public virtual ICollection<WalletTransactionDetail> WalletTransactionDetail { get; set; }
    }
}
