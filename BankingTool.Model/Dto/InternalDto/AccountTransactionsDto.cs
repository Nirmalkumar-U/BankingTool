using BankingTool.Model.Model;

namespace BankingTool.Model.Dto.InternalDto
{
    public class AccountTransactionsDto
    {
        public Account Account { get; set; }
        public Transaction Transaction { get; set; }
        public TransactionDetail TransactionDetail { get; set; }
    }
}
