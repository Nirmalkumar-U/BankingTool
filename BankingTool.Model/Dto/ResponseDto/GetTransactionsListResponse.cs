using BankingTool.Model.Enums;

namespace BankingTool.Model.Dto.Response
{
    public class GetTransactionsListResponse
    {
        public List<GetTransactionsListResponseTransactionList> TransactionsList { get; set; }
        public GetTransactionsListResponseAccountInfo AccountInfo { get; set; }
        public List<GetTransactionsListResponseCardInfo> CardInfo { get; set; }
    }
    public class GetTransactionsListResponseTransactionList
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public decimal StageBalance { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public string TransactionCategory { get; set; }
        public string FromAccountId { get; set; } 
        public string ToAccountId { get; set; }
        public string TransactionTag { get; set; }
    }
    public class GetTransactionsListResponseAccountInfo
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }
    }
    public class GetTransactionsListResponseCardInfo
    {
        public CardTypeEnum CardType { get; set; }
        public string CardNumber { get; set; }
        public string BankName { get; set; }
        public string HolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVV { get; set; }
        public decimal? BalanceLimit { get; set; }
    }
}
