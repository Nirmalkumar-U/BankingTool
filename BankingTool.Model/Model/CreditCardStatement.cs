namespace BankingTool.Model
{
    public class CreditCardStatement : BaseSchema
    {
        public int StatementId { get; set; }
        public int CreditCardId { get; set; }
        public DateTime BillingCycleStartDate { get; set; }
        public DateTime BillingCycleEndDate { get; set; }
        public DateTime StatementDate { get; set; }
        public decimal TotalCharges { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal Interest { get; set; }
        public decimal Fees { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal CardBalance { get; set; }
        public decimal MinimumDueAmount { get; set; }
        public DateTime DueDate { get; set; }
        public StatementStatus StatementStatus { get; set; }

        // Navigation Properties
        public virtual CreditCard CreditCard { get; set; }
        public virtual ICollection<CreditCardStatementDetail> StatementDetails { get; set; } = new List<CreditCardStatementDetail>();
    }
}
