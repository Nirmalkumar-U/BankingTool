using BankingTool.Api.Filter;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.BudgetPlanner;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class BudgetPlannerController(IBudgetPlannerService budgetPlannerService, IValidatorService validatorService, ICacheProvider cacheProvider, IHttpContextAccessor httpContextAccessor)
        : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly IBudgetPlannerService _budgetPlannerService = budgetPlannerService;

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> CreateBudgetPlanner(CreateBudgetPlannerRequestObject model)
        {
            return await HandleRequestAsync<CreateBudgetPlannerRequestObject, bool>(
                model,
                null,
                async () => {
                    var result = await _budgetPlannerService.CreateBudgetPlanner(model.Request);
                    return new ResponseDto<bool> { Response = result, Status = result, StatuCode = 200 };
                }
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> UpdateBudgetPlanner(UpdateBudgetPlannerRequestObject model)
        {
            return await HandleRequestAsync<UpdateBudgetPlannerRequestObject, bool>(
                model,
                null,
                async () => {
                    var result = await _budgetPlannerService.UpdateBudgetPlanner(model.Request);
                    return new ResponseDto<bool> { Response = result, Status = result, StatuCode = 200 };
                }
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public async Task<IActionResult> DeleteBudgetPlanner(DeleteBudgetPlannerRequest model)
        {
            return await HandleRequestAsync<DeleteBudgetPlannerRequest, bool>(
                model,
                null,
                async () => {
                    var result = await _budgetPlannerService.DeleteBudgetPlanner(model.BudgetPlannerId);
                    return new ResponseDto<bool> { Response = result, Status = result, StatuCode = 200 };
                }
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetPlannerListResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetPlannerListResponse>), 400)]
        public async Task<IActionResult> GetBudgetPlannerList()
        {
            return await HandleRequestAsync<bool?, GetBudgetPlannerListResponse>(
                null,
                null,
                async () => new ResponseDto<GetBudgetPlannerListResponse> { Response = await _budgetPlannerService.GetBudgetPlannerList(), Status = true, StatuCode = 200 }
            );
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetPlannerDetailResponse>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GetBudgetPlannerDetailResponse>), 400)]
        public async Task<IActionResult> GetBudgetPlannerDetail(GetBudgetPlannerByIdRequest model)
        {
            return await HandleRequestAsync<GetBudgetPlannerByIdRequest, GetBudgetPlannerDetailResponse>(
                model,
                null,
                async () => new ResponseDto<GetBudgetPlannerDetailResponse> { Response = await _budgetPlannerService.GetBudgetPlannerDetail(model.BudgetPlannerId), Status = true, StatuCode = 200 }
            );
        }
    }
}
