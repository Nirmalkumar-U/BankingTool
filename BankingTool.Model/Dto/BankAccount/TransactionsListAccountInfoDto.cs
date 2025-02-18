namespace BankingTool.Model.Dto.BankAccount
{
    public class TransactionsListAccountInfoDto
    {
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public int Balance { get; set; }
        public string AccountType { get; set; }
    }
}
