namespace BankingTool.Model.Model
{
    public class MutualFunds
    {
        public int MutualFundId { get; set; }
        public int SchemeId { get; set; }
        public decimal MutualFundValue { get; set; }
        public DateTime MutualFundDate { get; set; }

        public virtual Scheme Scheme { get; set; }
    }

}
