using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum TransactionRoleEnum
    {
        [EnumMember(Value = "Sender")]
        Sender,
        [EnumMember(Value = "Receiver")]
        Receiver,
    }
    public static class TransactionRoleEnumMapping
    {
        public static string EnumToString(this TransactionRoleEnum value)
        {
            return value switch
            {
                TransactionRoleEnum.Sender => "Sender",
                TransactionRoleEnum.Receiver => "Receiver",
                _ => throw new NotImplementedException(),
            };
        }
        public static TransactionRoleEnum TransactionRoleStringToEnum(this string value)
        {
            return value switch
            {
                "Sender" => TransactionRoleEnum.Sender,
                "Receiver" => TransactionRoleEnum.Receiver,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
