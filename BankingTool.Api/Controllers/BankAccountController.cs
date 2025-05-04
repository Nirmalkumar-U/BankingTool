using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.Response;
using BankingTool.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class BankAccountController(IBankAccountService bankAccountService,
        IValidatorService validatorService) : HandleRequest(validatorService)
    {
        private readonly IBankAccountService _bankAccountService = bankAccountService;

        [HttpGet]
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
                () => _bankAccountService.TransactionsListForCustomer(model.Request.Bank.Id, model.Request.Account.AccountTypeId, model.Request.Customer.Id)
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> BankDropDownList()
        {
            return HandleRequestAsync<int?, bool>(
                null,
                null,
                _bankAccountService.BankDropDownList
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

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetTransferAmountInitialLoad()
        {
            return HandleRequestAsync<int?, bool>(
                null,
                null,
                () => _bankAccountService.GetTransferAmountInitialLoad()
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 400)]
        public Task<IActionResult> GetAccountBalance(GetAccountBalanceRequestObject model)
        {
            return HandleRequestAsync<GetAccountBalanceRequestObject, int>(
                model,
                ValidationRules.GetAccountBalanceRequestRules,
                () => _bankAccountService.GetAccountBalance(model.Request.Account.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> TransferAmount(TransferAmountRequestObject model)
        {
            return HandleRequestAsync<TransferAmountRequestObject, bool>(
                model,
                ValidationRules.TransferAmountRequestRules,
                () => _bankAccountService.TransferAmount(model.Request.Account.FromAccountId, model.Request.Account.ToAccountId,
                model.Request.Transaction.Amount, model.Request.Transaction.Description)
            );
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetSelfTransferInitialLoad()
        {
            return HandleRequestAsync<int?, bool>(
                null,
                null,
                _bankAccountService.GetSelfTransferInitialLoad
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetToAccountListExcludedByAccountId(GetToAccountListExcludedByAccountIdRequestObject model)
        {
            return HandleRequestAsync<GetToAccountListExcludedByAccountIdRequestObject, bool>(
                model,
                ValidationRules.GetToAccountListExcludedByAccountIdRequestRules,
                () => _bankAccountService.GetToAccountListExcludedByAccountId(model.Request.Account.Id)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> DepositAmount(DepositAmountRequestObject model)
        {
            return HandleRequestAsync<DepositAmountRequestObject, bool>(
                model,
                ValidationRules.DepositAmountRequestObjectRules,
                () => _bankAccountService.DepositAmount(model.Request.Account.Id, model.Request.Transaction.Amount, model.Request.Transaction.Description)
            );
        }
    }
}
