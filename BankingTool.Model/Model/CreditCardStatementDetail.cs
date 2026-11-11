namespace BankingTool.Model
{
    public class CreditCardStatementDetail : BaseSchema
    {
        public int StatementDetailId { get; set; }
        public int StatementId { get; set; }
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }

        // Navigation Properties
        public virtual CreditCardStatement Statement { get; set; }
        public virtual CreditCardTransaction Transaction { get; set; }
    }
}
