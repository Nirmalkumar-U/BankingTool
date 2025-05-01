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
    }
    public class TransactionRole
    {
        public const string Sender = "Sender";
        public const string Receiver = "Receiver";
    }
    public class TransactionCatagory
    {
        public const string Cash = "Cash";
        public const string Transfer = "Transfer";
        public const string Cheque = "Cheque";
        public const string Deposit = "Deposit";
        public const string Withdraw = "Withdraw";
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
    }
    public static class ErrorMessage
    {
        public const string LoginFailed = "Login Failed.";
        public const string InternalServerError = "Internal Server Error.";
        public const string BadRequest = "Bad Request.";
        public const string Sucess = "Sucess.";
        public const string Created = "Created.";
        public const string MultiStatus = "Multi Status.";
        public const string NotFound = "Not Found.";
        public const string ValidationFailed = "Validation Failed.";
    }
}
