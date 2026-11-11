namespace BankingTool.Model
{
    public class CreditCardFee : BaseSchema
    {
        public int FeeId { get; set; }
        public int CreditCardId { get; set; }
        public string FeeType { get; set; }
        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string FeeReason { get; set; }

        // Navigation Properties
        public virtual CreditCard CreditCard { get; set; }
    }
}
