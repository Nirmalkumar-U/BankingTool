using System.Runtime.Serialization;

namespace BankingTool.Model.Enums
{
    public enum TransactionTagEnum
    {
        [EnumMember(Value = "Transfer")]
        Transfer,
        [EnumMember(Value = "Cheque")]
        Cheque,
        [EnumMember(Value = "Deposit")]
        Deposit,
        [EnumMember(Value = "Withdraw")]
        Withdraw,
        [EnumMember(Value = "Entertainment")]
        Entertainment,
        [EnumMember(Value = "NoTag")]
        NoTag,
        [EnumMember(Value = "Groceries")]
        Groceries,
        [EnumMember(Value = "Miscellaneous")]
        Miscellaneous,
        [EnumMember(Value = "SelfTransfer")]
        SelfTransfer,
        [EnumMember(Value = "Food")]
        Food,
        [EnumMember(Value = "Fuel")]
        Fuel,
        [EnumMember(Value = "BillPayment")]
        BillPayment,
        [EnumMember(Value = "Shopping")]
        Shopping,
        [EnumMember(Value = "Rent")]
        Rent,
        [EnumMember(Value = "Travel")]
        Travel,
        [EnumMember(Value = "Health")]
        Health,
        [EnumMember(Value = "Insurance")]
        Insurance,
        [EnumMember(Value = "Investment")]
        Investment,
        [EnumMember(Value = "Salary")]
        Salary,
        [EnumMember(Value = "Savings")]
        Savings
    }
    public static class TransactionTagEnumMapping
    {
        public static string EnumToString(this TransactionTagEnum value)
        {
            return value switch
            {
                TransactionTagEnum.Transfer => "Transfer",
                TransactionTagEnum.Cheque => "Cheque",
                TransactionTagEnum.Deposit => "Deposit",
                TransactionTagEnum.Withdraw => "Withdraw",
                TransactionTagEnum.Entertainment => "Entertainment",
                TransactionTagEnum.NoTag => "No Tag",
                TransactionTagEnum.Groceries => "Groceries",
                TransactionTagEnum.Miscellaneous => "Miscellaneous",
                TransactionTagEnum.SelfTransfer => "Self Transfer",
                TransactionTagEnum.Food => "Food",
                TransactionTagEnum.Fuel => "Fuel",
                TransactionTagEnum.BillPayment => "Bill Payment",
                TransactionTagEnum.Shopping => "Shopping",
                TransactionTagEnum.Rent => "Rent",
                TransactionTagEnum.Travel => "Travel",
                TransactionTagEnum.Health => "Health",
                TransactionTagEnum.Insurance => "Insurance",
                TransactionTagEnum.Investment => "Investment",
                TransactionTagEnum.Salary => "Salary",
                TransactionTagEnum.Savings => "Savings",
                _ => throw new NotImplementedException(),
            };
        }
        public static TransactionTagEnum TransactionTagStringToEnum(this string value)
        {
            return value switch
            {
                "Transfer" => TransactionTagEnum.Transfer,
                "Cheque" => TransactionTagEnum.Cheque,
                "Deposit" => TransactionTagEnum.Deposit,
                "Withdraw" => TransactionTagEnum.Withdraw,
                "Entertainment" => TransactionTagEnum.Entertainment,
                "No Tag" => TransactionTagEnum.NoTag,
                "Groceries" => TransactionTagEnum.Groceries,
                "Miscellaneous" => TransactionTagEnum.Miscellaneous,
                "Self Transfer" => TransactionTagEnum.SelfTransfer,
                "Food" => TransactionTagEnum.Food,
                "Fuel" => TransactionTagEnum.Fuel,
                "Bill Payment" => TransactionTagEnum.BillPayment,
                "Shopping" => TransactionTagEnum.Shopping,
                "Rent" => TransactionTagEnum.Rent,
                "Travel" => TransactionTagEnum.Travel,
                "Health" => TransactionTagEnum.Health,
                "Insurance" => TransactionTagEnum.Insurance,
                "Investment" => TransactionTagEnum.Investment,
                "Salary" => TransactionTagEnum.Salary,
                "Savings" => TransactionTagEnum.Savings,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
