namespace BankingTool.Model
{
    public class Customer : BaseSchema
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int CustomerLevel { get; set; }
        public string PrimaryAccountNumber { get; set; }


        // Optional Navigation Property
        public virtual Users User { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual CreditScore CreditScore { get; set; }
    }
}
