using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.Budget;
using BankingTool.Model.Dto.RequestDto.Wallet;
using BankingTool.Model.Dto.ResponseDto;
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

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<GetWalletListResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetWalletListResponse>), 400)]
        public async Task<IActionResult> GetWalletList()
        {
            return await HandleRequestAsync<bool?, GetWalletListResponse>(
                null,
                null,
                () => _budgetService.GetWalletList()
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<GetWalletTransactionListResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetWalletTransactionListResponse>), 400)]
        public async Task<IActionResult> GetWalletTransactionList(GetWalletTransactionListRequestObject model)
        {
            return await HandleRequestAsync<GetWalletTransactionListRequestObject, GetWalletTransactionListResponse>(
                model,
                ValidationRules.GetWalletTransactionListRequestRules,
                () => _budgetService.GetWalletTransactionList(model.Request.Wallet.Id)
            );
        }

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
