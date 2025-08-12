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
    }
}
