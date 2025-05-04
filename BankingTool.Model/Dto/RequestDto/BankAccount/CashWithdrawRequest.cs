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
    }
    public class CashWithdrawRequestAccount : RequestId
    {
    }
    public class CashWithdrawRequestTransaction
    {
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
