namespace BankingTool.Model.Model
{
    public class CalendarEvent : BaseSchema
    {
        public int CalendarEventId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string EventType { get; set; }
        public string Recurrence { get; set; }
        public string Duration { get; set; }
        public string Priority { get; set; } ="Normal";
        public bool IsAllDay { get; set; }
        public bool IsPublic { get; set; } = true;
    }
}
