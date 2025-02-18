using BankingTool.Model;
using BankingTool.Model.Dto.BankAccount;
using BankingTool.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BankAccountController(IBankAccountService bankAccountService) : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService = bankAccountService;

        [HttpGet]
        public async Task<IActionResult> GetCreateAccountInitialLoad()
        {
            var Result = await _bankAccountService.GetCreateAccountInitialLoad();
            return new OkObjectResult(Result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountDto model)
        {
            var Result = await _bankAccountService.CreateAccount(model);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> GetBankDetailsDropDownWithoutCustomerAndAccountType(int customerId, int accountTypeId)
        {
            var Result = await _bankAccountService.GetBankDetailsDropDownWithoutCustomerAndAccountType(customerId, accountTypeId);
            return new OkObjectResult(Result);
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
    }
}
