using BankingTool.Model.Dto.BankAccount;
using BankingTool.Model;

namespace BankingTool.Service.IService
{
    public interface IBankAccountService
    {
        Task<ResponseDto<CreateAccountInitialLoadDto>> GetCreateAccountInitialLoad();
        Task<ResponseDto<bool>> CreateAccount(CreateAccountDto model);
        Task<ResponseDto<List<DropDownDto>>> GetBankDetailsDropDownWithoutCustomerAndAccountType(int customerId, int accountTypeId);
        Task<ResponseDto<bool>> IsCustomerHasCreditCardInThatBank(int customerId, int bankId);
        Task<ResponseDto<GetTransactionsListDto>> TransactionsListForCustomer(int bankId, int accountTypeId, int customerId);
        Task<ResponseDto<List<DropDownDto>>> BankDropDownList(int customerId);
        Task<ResponseDto<List<DropDownDto>>> GetAccountTypeDropDownListByCustomerIdAndBankId(int customerId, int bankId);
        Task<ResponseDto<TransferAmountInitialLoadDto>> GetTransferAmountInitialLoad(int customerId);
        Task<ResponseDto<int>> GetAccountBalance(int accountId);
    }
}
