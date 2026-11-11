using System.Runtime.Serialization;

namespace BankingTool.Model
{
    public enum StatementStatus
    {
        [EnumMember(Value = "Generated")]
        Generated = 1,

        [EnumMember(Value = "Pending")]
        Pending = 2,

        [EnumMember(Value = "Paid")]
        Paid = 3,

        [EnumMember(Value = "PartiallyPaid")]
        PartiallyPaid = 4,

        [EnumMember(Value = "Overdue")]
        Overdue = 5
    }

    public static class StatementStatusMapping
    {
        public static string EnumToString(this StatementStatus value)
        {
            return value switch
            {
                StatementStatus.Generated => "Generated",
                StatementStatus.Pending => "Pending",
                StatementStatus.Paid => "Paid",
                StatementStatus.PartiallyPaid => "PartiallyPaid",
                StatementStatus.Overdue => "Overdue",
                _ => throw new NotImplementedException(),
            };
        }

        public static StatementStatus StringToEnum(string value)
        {
            return value switch
            {
                "Generated" => StatementStatus.Generated,
                "Pending" => StatementStatus.Pending,
                "Paid" => StatementStatus.Paid,
                "PartiallyPaid" => StatementStatus.PartiallyPaid,
                "Overdue" => StatementStatus.Overdue,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
