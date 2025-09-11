using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Model;

namespace BankingTool.Repository.IRepository
{
    public interface IWalletRespository
    {
        Task<WalletAccount> GetLastWalletAccount();
        Task<bool> CreateWalletAccount(WalletAccount walletAccount, WalletTransaction walletTransaction, WalletTransactionDetail walletTransactionDetails);
        Task<List<GetWalletList>> GetWalletList();
        Task<WalletAccount> GetWalletById(int walletId);
        Task<List<GetWalletTransactionList>> GetWalletTransactionList(int walletId);
        Task<bool> ChangeWalletStatus(int walletId);
        Task<WalletTransaction> GetLastWalletTransaction(int walletAccountId);
        Task<WalletAccount> GetWalletAccount(int walletAccountId);
        bool WalletDeposit(WalletAccount account, WalletTransaction transaction, WalletTransactionDetail transactionDetail,
            bool isBudgetAccountAttached, List<BudgetTransactionDto> budgetTransactionDetail);
        Task<List<DropDownDto>> GetFromWalletAccountListByCustomerId();
    }
}
