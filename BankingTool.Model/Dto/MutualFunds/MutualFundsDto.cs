namespace BankingTool.Model.Dto.MutualFunds
{
    public class MutualFundsDto
    {
        public string SchemeCode { get; set; }
        public string ISINDivPayout { get; set; }
        public string ISINGrowth { get; set; }
        public string ISINDivReinvestment { get; set; }
        public string SchemeName { get; set; }
        public decimal MutualFundValue { get; set; }
        public DateTime MutualFundDate { get; set; }
    }

}
