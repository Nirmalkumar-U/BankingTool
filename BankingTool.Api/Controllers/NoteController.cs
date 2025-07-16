using BankingTool.Api.Filter;
using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Service.IService;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace BankingTool.Api.Controllers
{
    [ApiController]
    [TokenAuthorization]
    [Route("api/[controller]/[action]")]
    public class NoteController(INoteService noteService,
        IValidatorService validatorService, ICacheProvider cacheProvider, IHttpContextAccessor httpContextAccessor) : HandleRequest(validatorService, cacheProvider, httpContextAccessor)
    {
        private readonly INoteService _noteService = noteService;

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<List<NoteDto>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<List<NoteDto>>), 400)]
        public Task<IActionResult> GetAllNotes()
        {
            return HandleRequestAsync<bool?, List<NoteDto>>(
                null,
                null,
                () => _noteService.GetAllNotes()
            );
        }
    }
}
