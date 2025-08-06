using BankingTool.Model.Model;

namespace BankingTool.Repository.IRepository
{
    public interface IWalletRespository
    {
        Task<WalletAccount> GetLastWalletAccount();
        Task<bool> CreateWalletAccount(WalletAccount walletAccount, WalletTransaction walletTransaction, WalletTransactionDetail walletTransactionDetails);
    }
}
