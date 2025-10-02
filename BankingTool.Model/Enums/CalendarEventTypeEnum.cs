using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum CalendarEventTypeEnum
    {
        [EnumMember(Value = "Reminder")]
        Reminder,
        [EnumMember(Value = "Event")]
        Event,
        [EnumMember(Value = "Note")]
        Note,
        [EnumMember(Value = "Holiday")]
        Holiday,
        [EnumMember(Value = "Meeting")]
        Meeting
    }
    public static class CalenderEventTypeEnumMapping
    {
        public static CalendarEventTypeEnum StringToEnum(this string type)
        {
            return type switch
            {
                "Reminder" => CalendarEventTypeEnum.Reminder,
                "Event" => CalendarEventTypeEnum.Event,
                "Note" => CalendarEventTypeEnum.Note,
                "Holiday" => CalendarEventTypeEnum.Holiday,
                "Meeting" => CalendarEventTypeEnum.Meeting,
                _ => CalendarEventTypeEnum.Event,
            };
        }
        public static string CalendarEventTypeEnumToString(this CalendarEventTypeEnum type)
        {
            return type switch
            {
                CalendarEventTypeEnum.Reminder => "Reminder",
                CalendarEventTypeEnum.Event => "Event",
                CalendarEventTypeEnum.Note => "Note",
                CalendarEventTypeEnum.Holiday => "Holiday",
                CalendarEventTypeEnum.Meeting => "Meeting",
                _ => "Event",
            };
        }
    }
}
