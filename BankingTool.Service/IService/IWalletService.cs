using BankingTool.Model;

namespace BankingTool.Service.IService
{
    public interface IWalletService
    {
        Task<ResponseDto<bool>> CreateWalletAccount(string walletName, int amount);
    }
}
