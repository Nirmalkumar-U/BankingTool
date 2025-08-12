using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto;

namespace BankingTool.Service.IService
{
    public interface IWalletService
    {
        Task<ResponseDto<bool>> CreateWalletAccount(string walletName, int amount);
        Task<ResponseDto<GetWalletListResponse>> GetWalletList();
        Task<ResponseDto<GetWalletTransactionListResponse>> GetWalletTransactionList(int walletId);
    }
}
