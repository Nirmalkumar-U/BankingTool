using BankingTool.Model.Enums;

namespace BankingTool.Model.Dto.InternalDto
{
    public class CalendarEventDto
    {
        public int CalendarEventId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public CalendarEventType EventType { get; set; }
        public string Recurrence { get; set; }
        public string Duration { get; set; }
        public bool IsAllDay { get; set; }
    }
}
                               