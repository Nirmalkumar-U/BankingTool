using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class DepositAmountRequestObject
    {
        public DepositAmountRequest Request { get; set; }
    }
    public class DepositAmountRequest
    {
        public DepositAmountRequestAccount Account { get; set; }
        public DepositAmountRequestTransaction Transaction { get; set; }
    }
    public class DepositAmountRequestAccount : RequestId
    {
    }
    public class DepositAmountRequestTransaction
    {
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
