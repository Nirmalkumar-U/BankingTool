using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.BulkTransaction;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class BulkTransactionController(IBulkTransactionService bulkTransaction,
        IValidatorService validatorService, ICacheProvider cacheProvider, IHttpContextAccessor httpContextAccessor) : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly IBulkTransactionService _bulkTransactionService = bulkTransaction;

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> BulkTransaction(BulkTransactionRequestObject model)
        {
            return HandleRequestAsync<BulkTransactionRequestObject, bool>(
                model,
                ValidationRules.BulkTransactionRequestRules,
                () => _bulkTransactionService.BulkTransaction(model)
            );
        }
    }
}
