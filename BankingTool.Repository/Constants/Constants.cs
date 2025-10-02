namespace BankingTool.Repository
{
    public class Constants
    {
        public const int AccountType = 100;
        public const int DefaultDebitCard = 50000;
        public const int AccountMininumBalanceForSavingsAccount = 0;
    }

    public class CodeValueTypeCode
    {
        public const int AccountType = 100;
        public const int TransactionTag = 101;
        public const int TransactionCategory = 102;
    }

    public static class AppClaimTypes
    {
        public const string UserName = nameof(UserName);
        public const string LastName = nameof(LastName);
        public const string UserId = nameof(UserId);
        public const string EmailId = nameof(EmailId);
        public const string RoleId = nameof(RoleId);
        public const string RoleLevel = nameof(RoleLevel);
        public const string Actions = nameof(Actions);
        public const string StaffId = nameof(StaffId);
        public const string CustomerId = nameof(CustomerId);
        public const string ExpiresAt = nameof(ExpiresAt);
    }
}
