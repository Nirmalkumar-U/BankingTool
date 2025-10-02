using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum TransactionTypeEnum
    {
        [EnumMember(Value = "Debit")]
        Debit,
        [EnumMember(Value = "Credit")]
        Credit
    }
    public static class TransactionTypeEnumMapping
    {
        public static string EnumToString(this TransactionTypeEnum value)
        {
            return value switch
            {
                TransactionTypeEnum.Debit => "Debit",
                TransactionTypeEnum.Credit => "Credit",
                _ => throw new NotImplementedException(),
            };
        }
        public static TransactionTypeEnum TransactionTypeStringToEnum(this string value)
        {
            return value switch
            {
                "Debit" => TransactionTypeEnum.Debit,
                "Credit" => TransactionTypeEnum.Credit,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
