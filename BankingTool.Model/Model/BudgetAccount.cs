namespace BankingTool.Model.Model
{
    public class BudgetAccount : BaseSchema
    {
        public int BudgetAccountId { get; set; }
        public int CustomerId { get; set; }
        public string BudgetName { get; set; }
        public string BudgetNumber { get; set; }
        public string Balance { get; set; }
        public string BudgetStatus { get; set; }

        // Optional navigation property
        public virtual Customer Customer { get; set; }
        public virtual ICollection<BudgetTransactionDetail> BudgetTransactionDetail { get; set; }
    }
}
