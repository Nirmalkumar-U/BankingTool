using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum NotificationStatusEnum
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Accepted")]
        Accepted,
        [EnumMember(Value = "Rejected")]
        Rejected,
    }
    public static class NotificationStatusEnumMapping
    {
        public static string EnumToString(this NotificationStatusEnum value)
        {
            return value switch
            {
                NotificationStatusEnum.Pending => "Pending",
                NotificationStatusEnum.Accepted => "Accepted",
                NotificationStatusEnum.Rejected => "Rejected",
                _ => throw new NotImplementedException(),
            };
        }
        public static NotificationStatusEnum NotificationStatusStringToEnum(this string value)
        {
            return value switch
            {
                "Pending" => NotificationStatusEnum.Pending,
                "Accepted" => NotificationStatusEnum.Accepted,
                "Rejected" => NotificationStatusEnum.Rejected,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
