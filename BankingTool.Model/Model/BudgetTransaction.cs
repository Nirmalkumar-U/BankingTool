namespace BankingTool.Model.Model
{
    public class BudgetTransaction : BaseSchema
    {
        public int BudgetTransactionId { get; set; }
        public string TransactionCategory { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionTime { get; set; }
        public string TransactionTag { get; set; }


        public virtual ICollection<BudgetTransactionDetail> BudgetTransactionDetail { get; set; }
    }
}
