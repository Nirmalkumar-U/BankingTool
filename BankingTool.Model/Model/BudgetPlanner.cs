namespace BankingTool.Model.Model
{
    public class BudgetPlanner
    {
        public int BudgetPlannerId { get; set; }
        public int UserId { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
        public decimal TotalBudget { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public Users User { get; set; }
        public ICollection<BudgetItem> BudgetItems { get; set; } = new List<BudgetItem>();
    }
}
