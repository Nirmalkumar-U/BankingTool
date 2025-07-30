using BankingTool.Api.Filter;
using BankingTool.Model;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class CalenderController(ICalenderService calenderService,
        IValidatorService validatorService, ICacheProvider cacheProvider, IHttpContextAccessor httpContextAccessor) : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly ICalenderService _calenderService = calenderService;

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<CalendarEventDto>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<CalendarEventDto>>), 400)]
        public Task<IActionResult> GetCalendarEventList()
        {
            return HandleRequestAsync<bool?, List<CalendarEventDto>>(
                null,
                null,
                () => _calenderService.GetCalendarEventList()
            );
        }
    }
}
