using BankingTool.Model.Dto.BankAccount;
using BankingTool.Model;

namespace BankingTool.Service.IService
{
    public interface IBankAccountService
    {
        Task<ResponseDto<CreateAccountInitialLoadDto>> GetCreateAccountInitialLoad();
        Task<ResponseDto<bool>> CreateAccount(CreateAccountDto model);
    }
}
