using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class BudgetController(IBudgetService budgetService, IValidatorService validatorService) : HandleRequest(validatorService)
    {
        private readonly IBudgetService _budgetService = budgetService;

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> CreateBudgetAccount(CreateBudgetAccountRequestObject model)
        {
            return HandleRequestAsync<CreateBudgetAccountRequestObject, bool>(
                model,
                ValidationRules.CreateBudgetAccountRequestObjectRules,
                () => _budgetService.CreateBudgetAccount(model.Request.BudgetAccount.BudgetName, model.Request.BudgetAccount.Amount)
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetListResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetListResponse>), 400)]
        public Task<IActionResult> GetBudgetList()
        {
            return HandleRequestAsync<bool?, GetBudgetListResponse>(
                null,
                null,
                () => _budgetService.GetBudgetList()
            );
        }
    }
}
