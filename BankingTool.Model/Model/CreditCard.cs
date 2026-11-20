namespace BankingTool.Model
{
    public class CreditCard : BaseSchema
    {
        public int CreditCardId { get; set; }

        public int AccountId { get; set; }

        // Application Details
        public string CardType { get; set; } = null!;
        public decimal RequestedLimit { get; set; }
        public decimal? ApprovedLimit { get; set; }

        public CreditCardStatus CardStatus { get; set; } = CreditCardStatus.Active!;

        public DateTime ApplicationDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public string ApprovedBy { get; set; }
        public string DecisionRemarks { get; set; }

        // Card Details
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string CardHolderName { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Financial Details
        public decimal? CreditLimit { get; set; }
        public decimal LimitUsed { get; set; }
        public decimal? AvailableCredit { get; set; }

        // Navigation
        public virtual Account Account { get; set; } = null!;

        // Navigation Properties
        public virtual ICollection<CreditCardTransaction> Transactions { get; set; } = new List<CreditCardTransaction>();
        public virtual ICollection<CreditCardStatement> Statements { get; set; } = new List<CreditCardStatement>();
        public virtual ICollection<CreditCardFee> Fees { get; set; } = new List<CreditCardFee>();
    }
}
