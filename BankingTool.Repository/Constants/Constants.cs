namespace BankingTool.Repository
{
    public class Constants
    {
        public const int AccountType = 100;
        public const int DefaultDebitCard = 50000;
        public const int AccountMininumBalanceForSavingsAccount = 500;
    }
    public class AccountStatus
    {
        public const string Active = "Active";
        public const string InActive = "InActive";
        public const string Deactivated = "Deactivated";
        public const string NotInUse = "NotInUse";
    }
    public class CardType
    {
        public const string DebitCard = "DebitCard";
        public const string CreditCard = "CreditCard";
    }
    public class CreditScoreStatus
    {
        public const string Active = "Active";
        public const string InActive = "InActive";
        public const string Default = "Default";
    }
    public class TransactionType
    {
        public const string Debit = "Debit";
        public const string Credit = "Credit";
        public const string Pending = "Pending";
        public const string Returned = "Returned";
    }
    public static class AppClaimTypes
    {
        public const string FirstName = nameof(FirstName);
        public const string LastName = nameof(LastName);
        public const string UserId = nameof(UserId);
        public const string EmailId = nameof(EmailId);
        public const string RoleId = nameof(RoleId);
        public const string RoleLevel = nameof(RoleLevel);
        public const string Actions = nameof(Actions);
        public const string StaffId = nameof(StaffId);
        public const string CustomerId = nameof(CustomerId);
    }
}
