using System.Runtime.Serialization;

namespace BankingTool.Model
{
    public enum CreditCardStatus
    {
        [EnumMember(Value = "Pending")]
        Pending = 1,

        [EnumMember(Value = "Active")]
        Active = 2,

        [EnumMember(Value = "Blocked")]
        Blocked = 3,

        [EnumMember(Value = "Closed")]
        Closed = 4,

        [EnumMember(Value = "Expired")]
        Expired = 5
    }

    public static class CreditCardStatusMapping
    {
        public static string EnumToString(this CreditCardStatus value)
        {
            return value switch
            {
                CreditCardStatus.Pending => "Pending",
                CreditCardStatus.Active => "Active",
                CreditCardStatus.Blocked => "Blocked",
                CreditCardStatus.Closed => "Closed",
                CreditCardStatus.Expired => "Expired",
                _ => throw new NotImplementedException(),
            };
        }

        public static CreditCardStatus StringToEnum(string value)
        {
            return value switch
            {
                "Pending" => CreditCardStatus.Pending,
                "Active" => CreditCardStatus.Active,
                "Blocked" => CreditCardStatus.Blocked,
                "Closed" => CreditCardStatus.Closed,
                "Expired" => CreditCardStatus.Expired,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
