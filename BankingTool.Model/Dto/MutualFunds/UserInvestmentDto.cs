namespace BankingTool.Model.Dto.MutualFunds
{
    public class UserInvestmentDto
    {
        public int InvestmentId { get; set; }
        public string SchemeCode { get; set; }
        public string SchemeName { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal UnitsPurchased { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal MutualFundAtPurchase { get; set; }
        public decimal MutualFundCurrentValue { get; set; }
    }
}
