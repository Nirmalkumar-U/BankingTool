namespace BankingTool.Model.Model
{
    public class CalendarNote : BaseSchema
    {
        public int CalendarNoteId { get; set; }
        public int UserId { get; set; }

        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
