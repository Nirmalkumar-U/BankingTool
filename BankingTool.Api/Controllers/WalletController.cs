using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.RequestDto.Budget;
using BankingTool.Model.Dto.RequestDto.Wallet;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Service.IService;
using BankingTool.Service.Service;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class WalletController(IWalletService walletService, IValidatorService validatorService, ICacheProvider cacheProvider, IHttpContextAccessor httpContextAccessor)
        : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly IWalletService _walletService = walletService;

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> CreateWallet(CreateWalletRequestObject model)
        {
            return await HandleRequestAsync<CreateWalletRequestObject, bool>(
                model,
                ValidationRules.CreateWalletAccountRequestRules,
                () => _walletService.CreateWalletAccount(model.Request.Wallet.WalletName, model.Request.Wallet.Amount)
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
                () => _walletService.GetWalletList()
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
                () => _walletService.GetWalletTransactionList(model.Request.Wallet.Id)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> ChangeWalletStatus(ChangeWalletStatusRequestObject model)
        {
            return await HandleRequestAsync<ChangeWalletStatusRequestObject, bool>(
                model,
                ValidationRules.CreateChangeWalletStatusRequestRules,
                () => _walletService.ChangeWalletStatus(model.Request.Wallet.Id)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> DepositWalletAmount(DepositWalletAmountRequestObject model)
        {
            return await HandleRequestAsync<DepositWalletAmountRequestObject, bool>(
                model,
                ValidationRules.DepositWalletAmountRequestObjectRules,
                () => _walletService.DepositWalletAmount(model.Request.Wallet.Id, model.Request.WalletTransaction.Amount,
                model.Request.WalletTransaction.Description, model.Request.WalletTransaction.TransactionDate,
                model.Request.BudgetAccount, model.Request.Account.IsFromAccount, model.Request.Account.Id)
            );
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> GetWalletDepositInitialLoad()
        {
            return await HandleRequestAsync<int?, bool>(
                null,
                null,
                _walletService.GetWalletDepositInitialLoad
            );
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> GetWalletTransferInitialLoad()
        {
            return await HandleRequestAsync<int?, bool>(
                null,
                null,
                _walletService.GetWalletTransferInitialLoad
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> WalletTransfer(WalletTransferRequestObject model)
        {
            return await HandleRequestAsync<WalletTransferRequestObject, bool>(
                model,
                ValidationRules.WalletTransferRequestRules,
                () => _walletService.WalletTransfer(model.Request.Wallet.Id, model.Request.Account.Id, model.Request.WalletTransaction.Amount,
                model.Request.WalletTransaction.Description, model.Request.WalletTransaction.TransactionTag, model.Request.WalletTransaction.TransactionDate,
                model.Request.BudgetAccount)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 400)]
        public async Task<IActionResult> GetWalletBalance(GetWalletBalanceRequestObject model)
        {
            return await HandleRequestAsync<GetWalletBalanceRequestObject, int>(
                model,
                ValidationRules.GetWalletBalanceRequestRules,
                () => _walletService.GetWalletBalance(model.Request.Account.Id)
            );
        }
    }
}
