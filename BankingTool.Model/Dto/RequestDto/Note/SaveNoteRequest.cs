using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Note
{
    public class SaveNoteRequestObject
    {
        public SaveNoteRequest Request { get; set; }
    }
    public class SaveNoteRequest
    {
        public SaveNoteRequestNote Note { get; set; }
    }
    public class SaveNoteRequestNote : RequestNullableId
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
