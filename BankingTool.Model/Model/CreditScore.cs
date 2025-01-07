namespace BankingTool.Model
{
    public class CreditScore : BaseSchema
    {
        public int CreditScoreId { get; set; }
        public int CustomerId { get; set; }
        public int? CreditScoreValue { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
