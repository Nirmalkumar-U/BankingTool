using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.Wallet;
using BankingTool.Model.Dto.ResponseDto;

namespace BankingTool.Service.IService
{
    public interface IWalletService
    {
        Task<ResponseDto<bool>> CreateWalletAccount(string walletName, int amount);
        Task<ResponseDto<GetWalletListResponse>> GetWalletList();
        Task<ResponseDto<GetWalletTransactionListResponse>> GetWalletTransactionList(int walletId);
        Task<ResponseDto<bool>> ChangeWalletStatus(int walletId);
        Task<ResponseDto<bool>> DepositWalletAmount(int walletId, int amount, string description, DateTime transactionDate, List<DepositWalletAmountRequestBudgetAccount> budgetAccounts,
            bool isFromAccount, int? accountId);
        Task<ResponseDto<bool>> GetWalletDepositInitialLoad();
        Task<ResponseDto<bool>> GetWalletTransferInitialLoad();
    }
}
