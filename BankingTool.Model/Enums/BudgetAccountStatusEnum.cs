using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum BudgetAccountStatusEnum
    {
        [EnumMember(Value ="Active")]
        Active,
        [EnumMember(Value ="InActive")]
        InActive,
    }
    public static class BudgetAccountStatusEnumMapping
    {
        public static string EnumToString(this BudgetAccountStatusEnum value)
        {
            return value switch
            {
                BudgetAccountStatusEnum.Active => "Active",
                BudgetAccountStatusEnum.InActive => "InActive",
                _ => throw new NotImplementedException(),
            };
        }
        public static BudgetAccountStatusEnum BudgetAccountStatusStringToEnum(this string value)
        {
            return value switch
            {
                "Active" => BudgetAccountStatusEnum.Active,
                "InActive" => BudgetAccountStatusEnum.InActive,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
