using BankingTool.Model;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class NoteService(INoteRepository noteRepository) : INoteService
    {
        private readonly INoteRepository _noteRepository = noteRepository;

        public async Task<ResponseDto<List<NoteDto>>> GetAllNotes()
        {
            var notes = await _noteRepository.GetAllNotes();
            return new ResponseDto<List<NoteDto>>
            {
                DropDownList = [],
                Errors = [],
                Response = [.. notes.Select(x=> new NoteDto{
                    NoteId = x.NoteId,
                    Content = x.Content,
                    Title = x.Title,
                    ModifiedDate = x.ModifiedDate ?? x.CreatedDate
                })],
                StatuCode = 200,
                Status = true,
                ValidationErrors = []
            };
        }
    }
}

