using System.Runtime.Serialization;

namespace BankingTool.Model
{
    public enum FeeType
    {
        [EnumMember(Value = "Annual")]
        Annual = 1,

        [EnumMember(Value = "LatePay")]
        LatePay = 2,

        [EnumMember(Value = "OverLimit")]
        OverLimit = 3,

        [EnumMember(Value = "TransactionFee")]
        TransactionFee = 4,

        [EnumMember(Value = "InterestCharge")]
        InterestCharge = 5,

        [EnumMember(Value = "CashAdvance")]
        CashAdvance = 6,

        [EnumMember(Value = "BalanceTransfer")]
        BalanceTransfer = 7,

        [EnumMember(Value = "ReplacementCard")]
        ReplacementCard = 8
    }

    public static class FeeTypeMapping
    {
        public static string EnumToString(this FeeType value)
        {
            return value switch
            {
                FeeType.Annual => "Annual",
                FeeType.LatePay => "LatePay",
                FeeType.OverLimit => "OverLimit",
                FeeType.TransactionFee => "TransactionFee",
                FeeType.InterestCharge => "InterestCharge",
                FeeType.CashAdvance => "CashAdvance",
                FeeType.BalanceTransfer => "BalanceTransfer",
                FeeType.ReplacementCard => "ReplacementCard",
                _ => throw new NotImplementedException(),
            };
        }

        public static FeeType StringToEnum(string value)
        {
            return value switch
            {
                "Annual" => FeeType.Annual,
                "LatePay" => FeeType.LatePay,
                "OverLimit" => FeeType.OverLimit,
                "TransactionFee" => FeeType.TransactionFee,
                "InterestCharge" => FeeType.InterestCharge,
                "CashAdvance" => FeeType.CashAdvance,
                "BalanceTransfer" => FeeType.BalanceTransfer,
                "ReplacementCard" => FeeType.ReplacementCard,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
