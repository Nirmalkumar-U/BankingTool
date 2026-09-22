namespace BankingTool.Model.Dto.MutualFunds
{
    public class UserInvestmentProfileDto
    {
        public int InvestmentId { get; set; }
        public int SchemeId { get; set; }
        public string SchemeCode { get; set; }
        public string SchemeName { get; set; }

        public decimal TotalUnitsPurchased { get; set; }
        public decimal InvestmentCost { get; set; }
        public decimal CurrentValue { get; set; }

        public decimal UnrealisedGain { get; set; }
        public decimal RealisedGain { get; set; }
        public decimal AbsoluteReturnPercent { get; set; }
        public double XirrPercent { get; set; }
        public decimal OneDayChange { get; set; }

        public DateTime InvestedDate { get; set; }
    }
}
