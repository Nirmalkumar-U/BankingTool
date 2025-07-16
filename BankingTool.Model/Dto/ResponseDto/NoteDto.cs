namespace BankingTool.Model.Dto.ResponseDto
{
    public class NoteDto
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
