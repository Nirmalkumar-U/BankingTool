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
        public async Task<IActionResult> GetBankDetailsByWithoutCustomerIdAndAccountTypeDropDown(int customerId, int accountTypeId)
        {
            var Result = await _bankAccountService.GetBankDetailsByWithoutCustomerIdAndAccountTypeDropDown(customerId, accountTypeId);
            return new OkObjectResult(Result);
        }
        [HttpGet]
        public async Task<IActionResult> IsCustomerHasCreditCardInThatBank(int customerId, int bankId)
        {
            var Result = await _bankAccountService.IsCustomerHasCreditCardInThatBank(customerId, bankId);
            return new OkObjectResult(Result);
        }
    }
}
