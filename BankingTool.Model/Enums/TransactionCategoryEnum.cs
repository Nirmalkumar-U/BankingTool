using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum TransactionCategoryEnum
    {
        [EnumMember(Value = "Cash")]
        Cash,
        [EnumMember(Value = "Transfer")]
        Transfer,
        [EnumMember(Value = "Cheque")]
        Cheque,
        [EnumMember(Value = "Deposit")]
        Deposit,
        [EnumMember(Value = "Withdraw")]
        Withdraw,
    }
    public static class TransactionCatagoryEnumMapping
    {
        public static string EnumToString(this TransactionCategoryEnum value)
        {
            return value switch
            {
                TransactionCategoryEnum.Cash => "Cash",
                TransactionCategoryEnum.Transfer => "Transfer",
                TransactionCategoryEnum.Cheque => "Cheque",
                TransactionCategoryEnum.Deposit => "Deposit",
                TransactionCategoryEnum.Withdraw => "Withdraw",
                _ => throw new NotImplementedException(),
            };
        }
        public static TransactionCategoryEnum TransactionCatagoryStringToEnum(this string value)
        {
            return value switch
            {
                "Cash" => TransactionCategoryEnum.Cash,
                "Transfer" => TransactionCategoryEnum.Transfer,
                "Cheque" => TransactionCategoryEnum.Cheque,
                "Deposit" => TransactionCategoryEnum.Deposit,
                "Withdraw" => TransactionCategoryEnum.Withdraw,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
