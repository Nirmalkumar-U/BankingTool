namespace BankingTool.Model.Model
{
    public class UserInvestment
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; }
        public int SchemeId { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal UnitsPurchased { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal MutualFundAtPurchase { get; set; }

        public virtual Scheme Scheme { get; set; }
    }
}
