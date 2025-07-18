using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Note
{
    public class DeleteNoteRequestObject
    {
        public DeleteNoteRequest Request { get; set; }
    }
    public class DeleteNoteRequest
    {
        public DeleteNoteRequestNote Note { get; set; }
    }
    public class DeleteNoteRequestNote : RequestId
    {
    }
}
