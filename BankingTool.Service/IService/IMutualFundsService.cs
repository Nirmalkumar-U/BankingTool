using BankingTool.Model;
using BankingTool.Model.Dto.MutualFunds;
using BankingTool.Model.Model;

namespace BankingTool.Service.IService
{
    public interface IMutualFundsService
    {
        Task<ResponseDto<List<MutualFundsDto>>> GetAllMutualFundsFromExternal();
        Task<ResponseDto<List<MutualFundsDto>>> GetMutualFundsByCodeFromExternal(List<string> codes);
        Task<ResponseDto<bool>> InsertSchemes(List<string> codes);
        Task<ResponseDto<List<UserInvestmentDto>>> GetAllUserInvestment();
        Task<ResponseDto<bool>> InsertUserInvestment(List<string> codes, decimal installmentAmount);
        Task<ResponseDto<List<MutualFundsDto>>> GetAllMutualFundsInDB();
        Task<ResponseDto<bool>> InsertDailyMutualFundValue();
        Task<ResponseDto<List<UserInvestmentProfileDto>>> GetUserInvestmentProfile();
    }
}
