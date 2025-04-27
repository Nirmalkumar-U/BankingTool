using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDtos;
using BankingTool.Model.Dto.Response;
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
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetCreateAccountInitialLoad()
        {
            return HandleRequestAsync<int?, bool>(
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
            return HandleRequestAsync<CreateAccountRequestObject, bool>(
                model,
                ValidationRules.CreateAccountRequestRules,
                () => _bankAccountService.CreateAccount(model.Request)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetBankDetailsWithoutCustomerAndAccountType(GetBankDetailWithoutCustomerAndAccountTypeRequestObject model)
        {
            return HandleRequestAsync<GetBankDetailWithoutCustomerAndAccountTypeRequestObject, bool>(
                model,
                ValidationRules.GetBankDetailWithoutCustomerAndAccountTypeRequestRules,
                () => _bankAccountService.GetBankDetailsDropDownWithoutCustomerAndAccountType(model.Request.Customer.Id,
                model.Request.Account.AccountTypeId)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> IsCustomerHasCreditCardInThatBank(IsCustomerHasCreditCardInThatBankRequestObject model)
        {
            return HandleRequestAsync<IsCustomerHasCreditCardInThatBankRequestObject, bool>(
                model,
                ValidationRules.IsCustomerHasCreditCardInThatBankRequestRules,
                () => _bankAccountService.IsCustomerHasCreditCardInThatBank(model.Request.Customer.Id, model.Request.Bank.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<GetTransactionsListResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetTransactionsListResponse>), 400)]
        public Task<IActionResult> TransactionsListForCustomer(TransactionsListForCustomerRequestObject model)
        {
            return HandleRequestAsync<TransactionsListForCustomerRequestObject, GetTransactionsListResponse>(
                model,
                ValidationRules.TransactionsListForCustomerRequestRules,
                () => _bankAccountService.TransactionsListForCustomer(model.Request.Bank.Id, model.Request.Account.Id, model.Request.Customer.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> BankDropDownList(BankDropDownListRequestObject model)
        {
            return HandleRequestAsync<BankDropDownListRequestObject, bool>(
                model,
                ValidationRules.BankDropDownListRequestRules,
                () => _bankAccountService.BankDropDownList(model.Request.Customer.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetAccountTypeDropDownListByCustomerIdAndBankId(GetAccountTypeDropDownListRequestObject model)
        {
            return HandleRequestAsync<GetAccountTypeDropDownListRequestObject, bool>(
                model,
                ValidationRules.GetAccountTypeDropDownListRequestRules,
                () => _bankAccountService.GetAccountTypeDropDownListByCustomerIdAndBankId(model.Request.Customer.Id, model.Request.Bank.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetTransferAmountInitialLoad(GetTransferAmountInitialLoadRequestObject model)
        {
            return HandleRequestAsync<GetTransferAmountInitialLoadRequestObject, bool>(
                model,
                ValidationRules.GetTransferAmountInitialLoadRequestRules,
                () => _bankAccountService.GetTransferAmountInitialLoad(model.Request.Customer.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 400)]
        public Task<IActionResult> GetAccountBalance(GetAccountBalanceRequestObject model)
        {
            return HandleRequestAsync<GetAccountBalanceRequestObject, int>(
                model,
                ValidationRules.LoginRequestRules,//todo
                () => _bankAccountService.GetAccountBalance(model.Request.Account.Id)
            );
        }
    }
}
