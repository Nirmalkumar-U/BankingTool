using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.Note;

namespace BankingTool.Service.IService
{
    public interface INoteService
    {
        Task<ResponseDto<List<NoteDto>>> GetAllNotes();
        Task<ResponseDto<int?>> SaveNote(SaveNoteRequestNote model);
        Task<ResponseDto<bool>> DeleteNote(int noteId);
    }
}
