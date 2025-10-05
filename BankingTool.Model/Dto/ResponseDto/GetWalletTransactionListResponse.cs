using BankingTool.Model.Enums;

namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetWalletTransactionListResponse
    {
        public string WalletName { get; set; }
        public string WalletNumber { get; set; }
        public WalletStatusEnum WalletStatus { get; set; }
        public decimal Balance { get; set; }
        public List<GetWalletTransactionList> WalletTransactionList { get; set; }
    }
    public class GetWalletTransactionList
    {
        public string TransactionTag { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal? ReceiverAmount { get; set; }
        public decimal? SenderAmount { get; set; }
        public decimal StageBalance { get; set; }
    }
}
