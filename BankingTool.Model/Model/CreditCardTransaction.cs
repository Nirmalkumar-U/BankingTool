namespace BankingTool.Model
{
    public class CreditCardTransaction : BaseSchema
    {
        public int TransactionId { get; set; }
        public int CreditCardId { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string MerchantName { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string ReferenceNumber { get; set; }

        // Navigation Properties
        public virtual CreditCard CreditCard { get; set; }
    }
}
