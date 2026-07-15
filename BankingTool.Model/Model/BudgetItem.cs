namespace BankingTool.Model.Model
{
    public class BudgetItem
    {
        public int BudgetItemId { get; set; }
        public int BudgetPlannerId { get; set; }
        public string BudgetName { get; set; }
        public decimal AllocatedAmount { get; set; }

        // Navigation property
        public BudgetPlanner BudgetPlanner { get; set; } = null!;
    }
}
