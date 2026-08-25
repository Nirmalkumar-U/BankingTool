using BankingTool.Model;
using BankingTool.Model.Dto.MutualFunds;
using BankingTool.Model.Model;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MutualFundsController(IMutualFundsService mutualFundsService,
        IValidatorService validatorService, ICacheProvider cacheProvider, IHttpContextAccessor httpContextAccessor) : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly IMutualFundsService _mutualFundsService = mutualFundsService;

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<MutualFundsDto>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<MutualFundsDto>>), 400)]
        public Task<IActionResult> GetAllMutualFunds()
        {
            return HandleRequestAsync<bool?, List<MutualFundsDto>>(
                null,
                null,
                () => _mutualFundsService.GetAllMutualFundsFromExternal()
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<MutualFundsDto>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<MutualFundsDto>>), 400)]
        public Task<IActionResult> GetAllMutualFundsInDB()
        {
            return HandleRequestAsync<bool?, List<MutualFundsDto>>(
                null,
                null,
                () => _mutualFundsService.GetAllMutualFundsInDB()
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<UserInvestmentDto>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<UserInvestmentDto>>), 400)]
        public Task<IActionResult> GetAllUserInvestment()
        {
            return HandleRequestAsync<bool?, List<UserInvestmentDto>>(
                null,
                null,
                () => _mutualFundsService.GetAllUserInvestment()
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> SaveSchemes([FromBody] List<string> codes)
        {
            return HandleRequestAsync<List<string>, bool>(
                null,
                null,
                () => _mutualFundsService.InsertSchemes(codes)
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> InsertUserInvestment([FromBody] InsertUserInvestmentRequest request)
        {
            return HandleRequestAsync<InsertUserInvestmentRequest, bool>(
                null,
                null,
                () => _mutualFundsService.InsertUserInvestment(request.SchemeCodes, request.InstallmentAmount)
            );
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<MutualFundsDto>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<MutualFundsDto>>), 400)]
        public Task<IActionResult> InsertDailyMutualFundValue()
        {
            return HandleRequestAsync<bool?, bool>(
                null,
                null,
                () => _mutualFundsService.InsertDailyMutualFundValue()
            );
        }
    }
}
