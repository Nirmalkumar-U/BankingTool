using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.RequestDto.Budget;
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
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetTransactionListResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetTransactionListResponse>), 400)]
        public Task<IActionResult> GetBudgetTransactionList(GetBudgetTransactionListRequestObject model)
        {
            return HandleRequestAsync<GetBudgetTransactionListRequestObject, GetBudgetTransactionListResponse>(
                model,
                ValidationRules.CreateGetBudgetTransactionListRequestObjectRules,
                () => _budgetService.GetBudgetTransactionList(model.Request.BudgetAccount.Id)
            );
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetNotificationListResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetNotificationListResponse>), 400)]
        public Task<IActionResult> GetBudgetNotificationList()
        {
            return HandleRequestAsync<bool?, GetBudgetNotificationListResponse>(
                null,
                null,
                () => _budgetService.GetBudgetNotificationList()
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> AcceptBudgetTransaction(AcceptBudgetTransactionRequestObject model)
        {
            return HandleRequestAsync<AcceptBudgetTransactionRequestObject, bool>(
                model,
                ValidationRules.CreateAcceptBudgetTransactionRequestRules,
                () => _budgetService.AcceptBudgetTransaction(model.Request.BudgetAccount.Id, model.Request.BudgetNotification.Id)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> RejectBudgetTransaction(RejectBudgetTransactionRequestObject model)
        {
            return HandleRequestAsync<RejectBudgetTransactionRequestObject, bool>(
                model,
                ValidationRules.CreateRejectBudgetTransactionRequestRules,
                () => _budgetService.RejectBudgetTransaction(model.Request.BudgetNotification.Id)
            );
        }
    }
}
