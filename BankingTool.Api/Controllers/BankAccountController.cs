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
            return HandleRequestAsync<CreateAccountRequestObject, bool>(
                model,
                Ruleset.LoginRequestRules,//todo
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
                Ruleset.LoginRequestRules,//todo
                () => _bankAccountService.GetBankDetailsDropDownWithoutCustomerAndAccountType(model.Request.Customer.Id.Value,
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
                Ruleset.LoginRequestRules,//todo
                () => _bankAccountService.IsCustomerHasCreditCardInThatBank(model.Request.Customer.Id.Value, model.Request.Bank.Id.Value)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<GetTransactionsListDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetTransactionsListDto>), 400)]
        public Task<IActionResult> TransactionsListForCustomer(TransactionsListForCustomerRequestObject model)
        {
            return HandleRequestAsync<TransactionsListForCustomerRequestObject, GetTransactionsListDto>(
                model,
                Ruleset.LoginRequestRules,//todo
                () => _bankAccountService.TransactionsListForCustomer(model.Request.Bank.Id.Value, model.Request.Account.Id.Value, model.Request.Customer.Id.Value)
            );
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> BankDropDownList(BankDropDownListRequestObject model)
        {
            return HandleRequestAsync<BankDropDownListRequestObject, bool>(
                model,
                Ruleset.LoginRequestRules,//todo
                () => _bankAccountService.BankDropDownList(model.Request.Customer.Id.Value)
            );
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> GetAccountTypeDropDownListByCustomerIdAndBankId(GetAccountTypeDropDownListRequestObject model)
        {
            return HandleRequestAsync<GetAccountTypeDropDownListRequestObject, bool>(
                model,
                Ruleset.LoginRequestRules,//todo
                () => _bankAccountService.GetAccountTypeDropDownListByCustomerIdAndBankId(model.Request.Customer.Id.Value,model.Request.Bank.Id.Value)
            );
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<TransferAmountInitialLoadDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TransferAmountInitialLoadDto>), 400)]
        public Task<IActionResult> GetTransferAmountInitialLoad(GetTransferAmountInitialLoadRequestObject model)
        {
            return HandleRequestAsync<GetTransferAmountInitialLoadRequestObject, TransferAmountInitialLoadDto>(
                model,
                Ruleset.LoginRequestRules,//todo
                () => _bankAccountService.GetTransferAmountInitialLoad(model.Request.Customer.Id.Value)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 400)]
        public Task<IActionResult> GetAccountBalance(GetAccountBalanceRequestObject model)
        {
            return HandleRequestAsync<GetAccountBalanceRequestObject, int>(
                model,
                Ruleset.LoginRequestRules,//todo
                () => _bankAccountService.GetAccountBalance(model.Request.Account.Id.Value)
            );
        }
    }
}
