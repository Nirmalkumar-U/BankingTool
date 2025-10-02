using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum AccountStatusEnum
    {
        [EnumMember(Value = "Active")]
        Active,
        [EnumMember(Value = "InActive")]
        InActive,
        [EnumMember(Value = "Deactivated")]
        Deactivated,
        [EnumMember(Value = "NotInUse")]
        NotInUse,
    }
    public static class AccountStatusEnumMapping
    {
        public static string EnumToString(this AccountStatusEnum value)
        {
            return value switch
            {
                AccountStatusEnum.Active => "Active",
                AccountStatusEnum.InActive => "InActive",
                AccountStatusEnum.Deactivated => "Deactivated",
                AccountStatusEnum.NotInUse => "NotInUse",
                _ => throw new NotImplementedException(),
            };
        }
        public static AccountStatusEnum AccountStatusStringToEnum(this string value)
        {
            return value switch
            {
                "Active" => AccountStatusEnum.Active,
                "InActive" => AccountStatusEnum.InActive,
                "Deactivated" => AccountStatusEnum.Deactivated,
                "NotInUse" => AccountStatusEnum.NotInUse,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
