using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum CardTypeEnum
    {
        [EnumMember(Value = "DebitCard")]
        DebitCard,
        [EnumMember(Value = "CreditCard")]
        CreditCard,
    }
    public static class CardTypeEnumMapping
    {
        public static string EnumToString(this CardTypeEnum value)
        {
            return value switch
            {
                CardTypeEnum.DebitCard => "DebitCard",
                CardTypeEnum.CreditCard => "CreditCard",
                _ => throw new NotImplementedException(),
            };
        }
        public static CardTypeEnum CardTypeStringToEnum(this string value)
        {
            return value switch
            {
                "DebitCard" => CardTypeEnum.DebitCard,
                "CreditCard" => CardTypeEnum.CreditCard,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
