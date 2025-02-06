namespace BankingTool.Model
{
    public class Account : BaseSchema
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public int BankId { get; set; }
        public int Balance { get; set; }
        public string AccountNumber { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountStatus { get; set; }
    }
}
