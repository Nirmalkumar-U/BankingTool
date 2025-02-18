namespace BankingTool.Model.Dto.BankAccount
{
    public class GetTransactionsListDto
    {
        public List<TransactionsListDto> TransactionsList { get; set; }
        public TransactionsListAccountInfoDto AccountInfo { get; set; }
        public List<TransactionsListCardInfoDto> CardInfo { get; set; }
    }
}
