using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum WalletStatusEnum
    {
        [EnumMember(Value = "Active")]
        Active,
        [EnumMember(Value = "InActive")]
        InActive,
    }
    public static class WalletStatusEnumMapping
    {
        public static string EnumToString(this WalletStatusEnum value)
        {
            return value switch
            {
                WalletStatusEnum.Active => "Active",
                WalletStatusEnum.InActive => "InActive",
                _ => throw new NotImplementedException(),
            };
        }
        public static WalletStatusEnum WalletStatusStringToEnum(this string value)
        {
            return value switch
            {
                "Active" => WalletStatusEnum.Active,
                "InActive" => WalletStatusEnum.InActive,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
