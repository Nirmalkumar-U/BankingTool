using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Wallet
{
    public class DepositWalletAmountRequestObject
    {
        public DepositWalletAmountRequest Request { get; set; }
    }
    public class DepositWalletAmountRequest
    {
        public DepositWalletAmountRequestWallet Wallet { get; set; }
        public DepositWalletAmountRequestWalletTransaction WalletTransaction { get; set; }
        public List<DepositWalletAmountRequestBudgetAccount> BudgetAccount { get; set; }
        public DepositWalletAmountRequestAccount Account { get; set; }
    }
    public class DepositWalletAmountRequestWallet : RequestId
    {
    }
    public class DepositWalletAmountRequestWalletTransaction
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
    public class DepositWalletAmountRequestBudgetAccount : RequestId
    {
        public decimal Amount { get; set; }
    }
    public class DepositWalletAmountRequestAccount : RequestNullableId
    {
        public bool IsFromAccount { get; set; }
    }
}
