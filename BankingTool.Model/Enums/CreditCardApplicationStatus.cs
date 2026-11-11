using System.Runtime.Serialization;

namespace BankingTool.Model
{
    public enum CreditCardApplicationStatus
    {
        [EnumMember(Value = "Pending")]
        Pending = 1,

        [EnumMember(Value = "Approved")]
        Approved = 2,

        [EnumMember(Value = "Rejected")]
        Rejected = 3,

        [EnumMember(Value = "UnderReview")]
        UnderReview = 4,

        [EnumMember(Value = "Cancelled")]
        Cancelled = 5
    }

    public static class CreditCardApplicationStatusMapping
    {
        public static string EnumToString(this CreditCardApplicationStatus value)
        {
            return value switch
            {
                CreditCardApplicationStatus.Pending => "Pending",
                CreditCardApplicationStatus.Approved => "Approved",
                CreditCardApplicationStatus.Rejected => "Rejected",
                CreditCardApplicationStatus.UnderReview => "UnderReview",
                CreditCardApplicationStatus.Cancelled => "Cancelled",
                _ => throw new NotImplementedException(),
            };
        }

        public static CreditCardApplicationStatus StringToEnum(string value)
        {
            return value switch
            {
                "Pending" => CreditCardApplicationStatus.Pending,
                "Approved" => CreditCardApplicationStatus.Approved,
                "Rejected" => CreditCardApplicationStatus.Rejected,
                "UnderReview" => CreditCardApplicationStatus.UnderReview,
                "Cancelled" => CreditCardApplicationStatus.Cancelled,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
