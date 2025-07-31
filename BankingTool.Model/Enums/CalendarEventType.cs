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
        public static CalendarEventType MapCalendarEventTypeStringToEnum(string type)
        {
            var calendarEventType = CalendarEventType.Event;
            switch (type)
            {
                case "Reminder": calendarEventType = CalendarEventType.Reminder; break;
                case "Event": calendarEventType = CalendarEventType.Event; break;
                case "Note": calendarEventType = CalendarEventType.Note; break;
                case "Holiday": calendarEventType = CalendarEventType.Holiday; break;
                case "Meeting": calendarEventType = CalendarEventType.Meeting; break;
            }
            return calendarEventType;
        }
        public static string MapCalendarEventTypeEnumToString(CalendarEventType type)
        {
            var calendarEventType = "Event";
            switch (type)
            {
                case CalendarEventType.Reminder: calendarEventType = "Reminder"; break;
                case CalendarEventType.Event: calendarEventType = "Event"; break;
                case CalendarEventType.Note: calendarEventType = "Note"; break;
                case CalendarEventType.Holiday: calendarEventType = "Holiday"; break;
                case CalendarEventType.Meeting: calendarEventType = "Meeting"; break;
            }
            return calendarEventType;
        }
    }
}
