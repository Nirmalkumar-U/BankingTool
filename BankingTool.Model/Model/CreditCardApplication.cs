namespace BankingTool.Model
{
    public class CreditCardApplication : BaseSchema
    {
        public int ApplicationId { get; set; }
        public int AccountId { get; set; }
        public string CardType { get; set; }
        public decimal DesiredLimit { get; set; }
        public decimal? ApprovedLimit { get; set; }
        public CreditCardApplicationStatus Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string DecisionRemarks { get; set; }
    }
}
