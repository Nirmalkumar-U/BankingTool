using BankingTool.Model.Model;

namespace BankingTool.Model.Dto.InternalDto
{
    public class BudgetTransactionDto
    {
        public BudgetAccount BudgetAccount { get; set; }
        public BudgetTransaction BudgetTransaction { get; set; }
        public BudgetTransactionDetail BudgetTransactionDetail { get; set; }
    }
}
