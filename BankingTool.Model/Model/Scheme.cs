namespace BankingTool.Model.Model
{
    public class Scheme
    {
        public int SchemeId { get; set; }
        public string SchemeCode { get; set; }
        public string ISINDivPayout { get; set; }
        public string ISINGrowth { get; set; }
        public string ISINDivReinvestment { get; set; }
        public string SchemeName { get; set; }

        public virtual ICollection<MutualFunds> MutualFunds { get; set; }
        public virtual ICollection<UserInvestment> UserInvestments { get; set; }
    }
}
