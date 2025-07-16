namespace BankingTool.Model
{
    public class Note : BaseSchema
    {
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; }
    }
}
