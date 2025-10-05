using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.Wallet;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Enums;

namespace BankingTool.Service.IService
{
    public interface IWalletService
    {
        Task<ResponseDto<bool>> CreateWalletAccount(string walletName, decimal amount);
        Task<ResponseDto<GetWalletListResponse>> GetWalletList();
        Task<ResponseDto<GetWalletTransactionListResponse>> GetWalletTransactionList(int walletId);
        Task<ResponseDto<bool>> ChangeWalletStatus(int walletId);
        Task<ResponseDto<bool>> DepositWalletAmount(int walletId, decimal amount, string description, DateTime transactionDate, List<DepositWalletAmountRequestBudgetAccount> budgetAccounts,
            bool isFromAccount, int? accountId);
        Task<ResponseDto<bool>> GetWalletDepositInitialLoad();
        Task<ResponseDto<bool>> GetWalletTransferInitialLoad();
        Task<ResponseDto<bool>> WalletTransfer(int walletId, int? accountId, decimal amount, string description, TransactionTagEnum transactionTag,
            DateTime transactionDate, List<WalletTransferRequestBudgetAccount> budgetAccounts);
        Task<ResponseDto<decimal>> GetWalletBalance(int accountId);
    }
}
