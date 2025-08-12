namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetWalletTransactionListResponse
    {
        public string WalletName { get; set; }
        public string WalletNumber { get; set; }
        public string WalletStatus { get; set; }
        public int Balance { get; set; }
        public List<GetWalletTransactionList> WalletTransactionList { get; set; }
    }
    public class GetWalletTransactionList
    {
        public string TransactionTag { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public int? ReceiverAmount { get; set; }
        public int? SenderAmount { get; set; }
        public int StageBalance { get; set; }

    }
}
