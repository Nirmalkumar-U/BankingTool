using BankingTool.Model.Dto.BankAccount;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Service.IService
{
    public interface IBankAccountService
    {
        Task<ResponseDto<bool>> GetCreateAccountInitialLoad();
        Task<ResponseDto<bool>> CreateAccount(CreateAccountRequest model);
        Task<ResponseDto<bool>> GetBankDetailsDropDownWithoutCustomerAndAccountType(int customerId, int accountTypeId);
        Task<ResponseDto<bool>> IsCustomerHasCreditCardInThatBank(int customerId, int bankId);
        Task<ResponseDto<GetTransactionsListDto>> TransactionsListForCustomer(int bankId, int accountTypeId, int customerId);
        Task<ResponseDto<bool>> BankDropDownList(int customerId);
        Task<ResponseDto<bool>> GetAccountTypeDropDownListByCustomerIdAndBankId(int customerId, int bankId);
        Task<ResponseDto<bool>> GetTransferAmountInitialLoad(int customerId);
        Task<ResponseDto<int>> GetAccountBalance(int accountId);
    }
}
