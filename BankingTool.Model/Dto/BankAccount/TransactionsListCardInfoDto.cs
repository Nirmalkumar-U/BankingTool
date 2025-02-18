namespace BankingTool.Model.Dto.BankAccount
{
    public class TransactionsListCardInfoDto
    {
        public string CardType { get; set; }
        public string BankName { get; set; }
        public string HolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVV { get; set; }
        public int? BalanceLimit { get; set; }
    }
}
