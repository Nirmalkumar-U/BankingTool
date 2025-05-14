using BankingTool.Model.Model;

namespace BankingTool.Model
{
    public class Account : BaseSchema
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public int BankId { get; set; }
        public int Balance { get; set; }
        public string AccountNumber { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountStatus { get; set; }


        // Optional Navigation Properties
        public virtual Customer Customer { get; set; } 
        public virtual Bank Bank { get; set; } 
        public virtual ICollection<Card> Card { get; set; }
        public virtual ICollection<BudgetTransactionDetail> BudgetTransactionDetail { get; set; }
    }
}
