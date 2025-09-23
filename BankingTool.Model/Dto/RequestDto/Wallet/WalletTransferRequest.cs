using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Wallet
{
    public class WalletTransferRequestObject
    {
        public WalletTransferRequest Request { get; set; }
    }
    public class WalletTransferRequest
    {
        public WalletTransferRequestWallet Wallet { get; set; }
        public WalletTransferRequestWalletTransaction WalletTransaction { get; set; }
        public List<WalletTransferRequestBudgetAccount> BudgetAccount { get; set; }
        public WalletTransferRequestAccount Account { get; set; }
    }
    public class WalletTransferRequestWallet : RequestId
    {
    }
    public class WalletTransferRequestWalletTransaction
    {
        public int Amount { get; set; }
        public string Description { get; set; }
        public string TransactionTag { get; set; }
        public DateTime TransactionDate { get; set; }
    }
    public class WalletTransferRequestBudgetAccount : RequestId
    {
        public int Amount { get; set; }
    }
    public class WalletTransferRequestAccount : RequestNullableId
    {
    }
}
