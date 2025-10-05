using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class BankAccountController(IBankAccountService bankAccountService,
        IValidatorService validatorService, ICacheProvider cacheProvider, IHttpContextAccessor httpContextAccessor) : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly IBankAccountService _bankAccountService = bankAccountService;

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> GetCreateAccountInitialLoad()
        {
            return await HandleRequestAsync<int?, bool>(
               null,
               null,
               _bankAccountService.GetCreateAccountInitialLoad,
               true
           );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> CreateAccount(CreateAccountRequestObject model)
        {
            return await HandleRequestAsync<CreateAccountRequestObject, bool>(
                model,
                ValidationRules.CreateAccountRequestRules,
                () => _bankAccountService.CreateAccount(model.Request)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> GetBankDetailsWithoutCustomerAndAccountType(GetBankDetailWithoutCustomerAndAccountTypeRequestObject model)
        {
            return await HandleRequestAsync<GetBankDetailWithoutCustomerAndAccountTypeRequestObject, bool>(
                model,
                ValidationRules.GetBankDetailWithoutCustomerAndAccountTypeRequestRules,
                () => _bankAccountService.GetBankDetailsDropDownWithoutCustomerAndAccountType(model.Request.Customer.Id,
                model.Request.Account.AccountTypeId)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> IsCustomerHasCreditCardInThatBank(IsCustomerHasCreditCardInThatBankRequestObject model)
        {
            return await HandleRequestAsync<IsCustomerHasCreditCardInThatBankRequestObject, bool>(
                model,
                ValidationRules.IsCustomerHasCreditCardInThatBankRequestRules,
                () => _bankAccountService.IsCustomerHasCreditCardInThatBank(model.Request.Customer.Id, model.Request.Bank.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<GetTransactionsListResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetTransactionsListResponse>), 400)]
        public async Task<IActionResult> TransactionsListForCustomer(TransactionsListForCustomerRequestObject model)
        {
            return await HandleRequestAsync<TransactionsListForCustomerRequestObject, GetTransactionsListResponse>(
                model,
                ValidationRules.TransactionsListForCustomerRequestRules,
                () => _bankAccountService.TransactionsListForCustomer(model.Request.Bank.Id, model.Request.Account.AccountTypeId, model.Request.Customer.Id)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<GetTransactionsListResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetTransactionsListResponse>), 400)]
        public async Task<IActionResult> TransactionList(TransactionListRequestObject model)
        {
            return await HandleRequestAsync<TransactionListRequestObject, GetTransactionsListResponse>(
                model,
                ValidationRules.TransactionListRequestObjectRules,
                () => _bankAccountService.TransactionsList(model.Request)
            );
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> BankDropDownList()
        {
            return await HandleRequestAsync<int?, bool>(
                null,
                null,
                _bankAccountService.BankDropDownList,
                true
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> GetAccountTypeDropDownListByCustomerIdAndBankId(GetAccountTypeDropDownListRequestObject model)
        {
            return await HandleRequestAsync<GetAccountTypeDropDownListRequestObject, bool>(
                model,
                ValidationRules.GetAccountTypeDropDownListRequestRules,
                () => _bankAccountService.GetAccountTypeDropDownListByCustomerIdAndBankId(model.Request.Customer.Id, model.Request.Bank.Id),
                true
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> GetTransferAmountInitialLoad()
        {
            return await HandleRequestAsync<int?, bool>(
                null,
                null,
                () => _bankAccountService.GetTransferAmountInitialLoad(),
                true
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<decimal>), 200)]
        [ProducesResponseType(typeof(ResponseDto<decimal>), 400)]
        public async Task<IActionResult> GetAccountBalance(GetAccountBalanceRequestObject model)
        {
            return await HandleRequestAsync<GetAccountBalanceRequestObject, decimal>(
                model,
                ValidationRules.GetAccountBalanceRequestRules,
                () => _bankAccountService.GetAccountBalance(model.Request.Account.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> TransferAmount(TransferAmountRequestObject model)
        {
            return await HandleRequestAsync<TransferAmountRequestObject, bool>(
                model,
                ValidationRules.TransferAmountRequestRules,
                () => _bankAccountService.TransferAmount(model.Request.Account.FromAccountId, model.Request.Account.ToAccountId,
                model.Request.Transaction.Amount, model.Request.Transaction.Description, model.Request.Transaction.TransactionDate,
                model.Request.Transaction.TransactionTag, model.Request.BudgetAccount)
            );
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> GetSelfTransferInitialLoad()
        {
            return await HandleRequestAsync<int?, bool>(
                null,
                null,
                _bankAccountService.GetSelfTransferInitialLoad
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> GetToAccountListExcludedByAccountId(GetToAccountListExcludedByAccountIdRequestObject model)
        {
            return await HandleRequestAsync<GetToAccountListExcludedByAccountIdRequestObject, bool>(
                model,
                ValidationRules.GetToAccountListExcludedByAccountIdRequestRules,
                () => _bankAccountService.GetToAccountListExcludedByAccountId(model.Request.Account.Id)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> DepositAmount(DepositAmountRequestObject model)
        {
            return await HandleRequestAsync<DepositAmountRequestObject, bool>(
                model,
                ValidationRules.DepositAmountRequestObjectRules,
                () => _bankAccountService.DepositAmount(model.Request.Account.Id, model.Request.Transaction.Amount,
                model.Request.Transaction.Description, model.Request.Transaction.TransactionDate, model.Request.BudgetAccount)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> CashWithdraw(CashWithdrawRequestObject model)
        {
            return await HandleRequestAsync<CashWithdrawRequestObject, bool>(
                model,
                ValidationRules.DepositAmountRequestObjectRules,
                () => _bankAccountService.CashWithdraw(model.Request.Account.Id, model.Request.Transaction.Amount,
                model.Request.Transaction.Description, model.Request.Transaction.TransactionDate, model.Request.BudgetAccount)
            );
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<GetDashBoardInitialLoadResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetDashBoardInitialLoadResponse>), 400)]
        public async Task<IActionResult> GetDashBoardInitialLoad()
        {
            return await HandleRequestAsync<int?, GetDashBoardInitialLoadResponse>(
                null,
                null,
                _bankAccountService.GetDashBoardInitialLoad
            );
        }
    }
}
