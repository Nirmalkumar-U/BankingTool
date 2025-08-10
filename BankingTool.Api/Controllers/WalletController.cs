using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.Wallet;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class WalletController(IWalletService budgetService, IValidatorService validatorService, ICacheProvider cacheProvider, IHttpContextAccessor httpContextAccessor)
        : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly IWalletService _budgetService = budgetService;

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> CreateWallet(CreateWalletRequestObject model)
        {
            return await HandleRequestAsync<CreateWalletRequestObject, bool>(
                model,
                ValidationRules.CreateWalletAccountRequestRules,
                () => _budgetService.CreateWalletAccount(model.Request.Wallet.WalletName, model.Request.Wallet.Amount)
            );
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(ResponseDto<GetBudgetListResponse>), 200)]
        //[ProducesResponseType(typeof(ResponseDto<GetBudgetListResponse>), 400)]
        //public async Task<IActionResult> GetBudgetList()
        //{
        //    return await HandleRequestAsync<bool?, GetBudgetListResponse>(
        //        null,
        //        null,
        //        () => _budgetService.GetBudgetList()
        //    );
        //}
        //[HttpPost]
        //[ProducesResponseType(typeof(ResponseDto<GetBudgetTransactionListResponse>), 200)]
        //[ProducesResponseType(typeof(ResponseDto<GetBudgetTransactionListResponse>), 400)]
        //public async Task<IActionResult> GetBudgetTransactionList(GetBudgetTransactionListRequestObject model)
        //{
        //    return await HandleRequestAsync<GetBudgetTransactionListRequestObject, GetBudgetTransactionListResponse>(
        //        model,
        //        ValidationRules.CreateGetBudgetTransactionListRequestObjectRules,
        //        () => _budgetService.GetBudgetTransactionList(model.Request.BudgetAccount.Id)
        //    );
        //}

        //[HttpPost]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        //public async Task<IActionResult> ChangeBudgetAccountStatus(ChangeBudgetAccountStatusRequestObject model)
        //{
        //    return await HandleRequestAsync<ChangeBudgetAccountStatusRequestObject, bool>(
        //        model,
        //        ValidationRules.CreateChangeBudgetAccountStatusRequestRules,
        //        () => _budgetService.ChangeBudgetAccountStatus(model.Request.BudgetAccount.Id)
        //    );
        //}
        //[HttpGet]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        //public async Task<IActionResult> GetBudgetDropdownList()
        //{
        //    return await HandleRequestAsync<bool?, bool>(
        //        null,
        //        null,
        //        () => _budgetService.GetBudgetDropdownList()
        //    );
        //}
        //[HttpPost]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        //[ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        //public async Task<IActionResult> SaveBudgetTransaction(SaveBudgetTransactionRequestObject model)
        //{
        //    return await HandleRequestAsync<SaveBudgetTransactionRequestObject, bool>(
        //        model,
        //        ValidationRules.SaveBudgetTransactionRequestRules,
        //        () => _budgetService.BudgetTransfer(model.Request.FromBudgetAccount.Id, model.Request.ToBudgetAccount, 
        //        model.Request.FromBudgetAccount.Amount, model.Request.FromBudgetAccount.Description)
        //    );
        //}
    }
}
