using System.Runtime.Serialization;

namespace BankingTool.Model
{
    public enum TransactionStatus
    {
        [EnumMember(Value = "Pending")]
        Pending = 1,

        [EnumMember(Value = "Completed")]
        Completed = 2,

        [EnumMember(Value = "Reversed")]
        Reversed = 3,

        [EnumMember(Value = "Failed")]
        Failed = 4
    }

    public static class TransactionStatusMapping
    {
        public static string EnumToString(this TransactionStatus value)
        {
            return value switch
            {
                TransactionStatus.Pending => "Pending",
                TransactionStatus.Completed => "Completed",
                TransactionStatus.Reversed => "Reversed",
                TransactionStatus.Failed => "Failed",
                _ => throw new NotImplementedException(),
            };
        }

        public static TransactionStatus StringToEnum(string value)
        {
            return value switch
            {
                "Pending" => TransactionStatus.Pending,
                "Completed" => TransactionStatus.Completed,
                "Reversed" => TransactionStatus.Reversed,
                "Failed" => TransactionStatus.Failed,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
