namespace BankingTool.Model.Model
{
    public class BudgetNotification : BaseSchema
    {
        public int BudgetNotificationId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string TransactionRole { get; set; }
        public string TransactionType { get; set; }
        public string TransactionTag { get; set; }
        public string TransactionCatagory { get; set; }
        public string Description { get; set; }
        public string NotificationStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

}
