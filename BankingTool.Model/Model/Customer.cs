namespace BankingTool.Model
{
    public class Customer : BaseSchema
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int CustomerLevel { get; set; }
        public string PrimaryAccountNumber { get; set; }
    }
}
