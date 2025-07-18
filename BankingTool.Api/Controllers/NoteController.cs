using BankingTool.Api.Filter;
using BankingTool.Api.Validators;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.Note;
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

        [HttpGet]
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
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<int?>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int?>), 400)]
        public Task<IActionResult> SaveNote(SaveNoteRequestObject model)
        {
            return HandleRequestAsync<SaveNoteRequestObject, int?>(
                model,
                ValidationRules.SaveNoteRequestRules,
                () => _noteService.SaveNote(model.Request.Note)
            );
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        public Task<IActionResult> DeleteNote(DeleteNoteRequestObject model)
        {
            return HandleRequestAsync<DeleteNoteRequestObject, bool>(
                model,
                ValidationRules.DeleteNoteRequestRules,
                () => _noteService.DeleteNote(model.Request.Note.Id)
            );
        }
    }
}
