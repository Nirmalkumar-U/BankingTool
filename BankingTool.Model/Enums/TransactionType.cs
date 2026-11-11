using System.Runtime.Serialization;

namespace BankingTool.Model
{
    public enum TransactionType
    {
        [EnumMember(Value = "Purchase")]
        Purchase = 1,

        [EnumMember(Value = "Payment")]
        Payment = 2,

        [EnumMember(Value = "Fee")]
        Fee = 3,

        [EnumMember(Value = "Interest")]
        Interest = 4,

        [EnumMember(Value = "Refund")]
        Refund = 5,

        [EnumMember(Value = "Reversal")]
        Reversal = 6
    }

    public static class TransactionTypeMapping
    {
        public static string EnumToString(this TransactionType value)
        {
            return value switch
            {
                TransactionType.Purchase => "Purchase",
                TransactionType.Payment => "Payment",
                TransactionType.Fee => "Fee",
                TransactionType.Interest => "Interest",
                TransactionType.Refund => "Refund",
                TransactionType.Reversal => "Reversal",
                _ => throw new NotImplementedException(),
            };
        }

        public static TransactionType StringToEnum(string value)
        {
            return value switch
            {
                "Purchase" => TransactionType.Purchase,
                "Payment" => TransactionType.Payment,
                "Fee" => TransactionType.Fee,
                "Interest" => TransactionType.Interest,
                "Refund" => TransactionType.Refund,
                "Reversal" => TransactionType.Reversal,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
