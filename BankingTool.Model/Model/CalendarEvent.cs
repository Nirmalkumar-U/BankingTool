using BankingTool.Model.Enums;

namespace BankingTool.Model.Model
{
    public class CalendarEvent : BaseSchema
    {
        public int CalendarEventId { get; set; }

        public int? UserId { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public CalendarEventType EventType { get; set; }

        public RecurrenceType? Recurrence { get; set; }
        public int? RecurrenceInterval { get; set; } 

        public EventPriority Priority { get; set; } = EventPriority.Normal;

        public bool IsAllDay { get; set; }
        public bool IsPublic { get; set; }
    }
}
