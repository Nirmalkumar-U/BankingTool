namespace BankingTool.Model
{
    public class CreditCard : BaseSchema
    {
        public int CreditCardId { get; set; }
        public int AccountId { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string CardHolderName { get; set; }
        public string CardType { get; set; }
        public CreditCardStatus Status { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal AvailableCredit { get; set; }

        // Navigation Properties
        public virtual ICollection<CreditCardTransaction> Transactions { get; set; } = new List<CreditCardTransaction>();
        public virtual ICollection<CreditCardStatement> Statements { get; set; } = new List<CreditCardStatement>();
        public virtual ICollection<CreditCardFee> Fees { get; set; } = new List<CreditCardFee>();
    }
}
