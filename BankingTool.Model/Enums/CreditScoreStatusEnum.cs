using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum CreditScoreStatusEnum
    {
        [EnumMember(Value ="Active")]
        Active,
        [EnumMember(Value ="InActive")]
        InActive,
        [EnumMember(Value = "Default")]
        Default,
    }
    public static class CreditScoreEnumMapping
    {
        public static string EnumToString(this CreditScoreStatusEnum value)
        {
            return value switch
            {
                CreditScoreStatusEnum.Active => "Active",
                CreditScoreStatusEnum.InActive => "InActive",
                CreditScoreStatusEnum.Default => "Default",
                _ => throw new NotImplementedException(),
            };
        }
        public static CreditScoreStatusEnum CreditScoreStatusStringToEnum(this string value)
        {
            return value switch
            {
                "Active" => CreditScoreStatusEnum.Active,
                "InActive" => CreditScoreStatusEnum.InActive,
                "Default" => CreditScoreStatusEnum.Default,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
