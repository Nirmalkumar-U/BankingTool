using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.BankAccount;
using BankingTool.Model.Dto.RequestDtos;
using BankingTool.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BankAccountController(IBankAccountService bankAccountService,
        IValidatorService validatorService) : HandleRequest(validatorService)
    {
        private readonly IBankAccountService _bankAccountService = bankAccountService;

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<CreateAccountInitialLoadDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<CreateAccountInitialLoadDto>), 400)]
        public Task<IActionResult> GetCreateAccountInitialLoad()
        {
            return HandleRequestAsync<int?, CreateAccountInitialLoadDto>(
                null,
                null,
                _bankAccountService.GetCreateAccountInitialLoad
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> CreateAccount(CreateAccountRequestObject model)
        {
            return HandleRequestAsync<CreateAccountRequest, bool>(
                model.CreateAccountRequest,
                Ruleset.LoginRequestRules,//todo
                () => _bankAccountService.CreateAccount(model.CreateAccountRequest)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetBankDetailsWithoutCustomerAndAccountType(GetBankDetailWithoutCustomerAndAccountTypeRequestObject model)
        {
            return HandleRequestAsync<GetBankDetailWithoutCustomerAndAccountTypeRequest, bool>(
                model.GetBankDetailWithoutCustomerAndAccountTypeRequest,
                Ruleset.LoginRequestRules,//todo
                () => _bankAccountService.GetBankDetailsDropDownWithoutCustomerAndAccountType(model.GetBankDetailWithoutCustomerAndAccountTypeRequest.Customer.CustomerId,
                model.GetBankDetailWithoutCustomerAndAccountTypeRequest.Account.AccountTypeId)
            );
        }

        [HttpGet]
        public async Task<IActionResult> IsCustomerHasCreditCardInThatBank(int customerId, int bankId)
        {
            var Result = await _bankAccountService.IsCustomerHasCreditCardInThatBank(customerId, bankId);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> TransactionsListForCustomer(int bankId, int accountTypeId, int customerId)
        {
            var Result = await _bankAccountService.TransactionsListForCustomer(bankId, accountTypeId, customerId);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> BankDropDownList(int customerId)
        {
            var Result = await _bankAccountService.BankDropDownList(customerId);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountTypeDropDownListByCustomerIdAndBankId(int customerId, int bankId)
        {
            var Result = await _bankAccountService.GetAccountTypeDropDownListByCustomerIdAndBankId(customerId, bankId);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> GetTransferAmountInitialLoad(int customerId)
        {
            var Result = await _bankAccountService.GetTransferAmountInitialLoad(customerId);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountBalance(int accountId)
        {
            var Result = await _bankAccountService.GetAccountBalance(accountId);
            return new OkObjectResult(Result);
        }
    }
}
