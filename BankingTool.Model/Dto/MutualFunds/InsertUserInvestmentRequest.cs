namespace BankingTool.Model.Dto.MutualFunds
{
    public class InsertUserInvestmentRequest
    {
        public List<string> SchemeCodes { get; set; }
        public decimal InstallmentAmount { get; set; }
    }
}
