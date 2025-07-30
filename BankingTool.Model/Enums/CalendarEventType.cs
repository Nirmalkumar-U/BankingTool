namespace BankingTool.Model.Enums
{
    public enum CalendarEventType
    {
        Reminder,
        Event,
        Note,
        Holiday,
        Meeting
    }
    public static class CalenderEventTypeEnum
    {
        public static CalendarEventType MapCalendarEventType(string type)
        {
            var calendarEventType = CalendarEventType.Event;
            switch (type)
            {
                case "Reminder": calendarEventType= CalendarEventType.Reminder; break;
                case "Event": calendarEventType = CalendarEventType.Event; break;
                case "Note": calendarEventType = CalendarEventType.Note; break;
                case "Holiday": calendarEventType = CalendarEventType.Holiday; break;
                case "Meeting": calendarEventType = CalendarEventType.Meeting; break;
            }
            return calendarEventType;
        }
    }
}
