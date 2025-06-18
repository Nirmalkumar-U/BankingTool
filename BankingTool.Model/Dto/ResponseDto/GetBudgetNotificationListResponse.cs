namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetBudgetNotificationListResponse
    {
        public List<GetBudgetNotificationList> budgetNotificationList { get; set; }
    }
    public class GetBudgetNotificationList
    {
        public int BudgetNotificationId { get; set; }
        public string TransactionType { get; set; }
        public string TransactionCatagory { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string NotificationStatus { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
