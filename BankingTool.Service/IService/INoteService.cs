using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model;

namespace BankingTool.Service.IService
{
    public interface INoteService
    {
        Task<ResponseDto<List<NoteDto>>> GetAllNotes();
    }
}
