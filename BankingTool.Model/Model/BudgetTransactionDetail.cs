namespace BankingTool.Model.Model
{
    public class BudgetTransactionDetail
    {
        public int BudgetTransactionDetailId { get; set; }
        public int BudgetTransactionId { get; set; }
        public int AccountId { get; set; }
        public int BudgetAccountId { get; set; }
        public string TransactionRole { get; set; }
        public string TransactionType { get; set; }
        public decimal StageBalance { get; set; }

        // Optional navigation properties
        public virtual BudgetTransaction BudgetTransaction { get; set; }
        public virtual Account Account { get; set; }
        public virtual BudgetAccount BudgetAccount { get; set; }
    }
}
