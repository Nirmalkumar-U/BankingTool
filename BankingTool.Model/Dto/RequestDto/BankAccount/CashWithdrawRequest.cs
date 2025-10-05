using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class CashWithdrawRequestObject
    {
        public CashWithdrawRequest Request { get; set; }
    }
    public class CashWithdrawRequest
    {
        public CashWithdrawRequestAccount Account { get; set; }
        public CashWithdrawRequestTransaction Transaction { get; set; }
        public List<CashWithdrawRequestBudgetAccount> BudgetAccount { get; set; }
    }
    public class CashWithdrawRequestAccount : RequestId
    {
    }
    public class CashWithdrawRequestBudgetAccount : RequestId
    {
        public decimal Amount { get; set; }
    }
    public class CashWithdrawRequestTransaction
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
