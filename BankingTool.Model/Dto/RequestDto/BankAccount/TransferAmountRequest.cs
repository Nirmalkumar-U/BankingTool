namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class TransferAmountRequestObject
    {
        public TransferAmountRequest Request { get; set; }
    }
    public class TransferAmountRequest
    {
        public TransferAmountRequestAccount Account { get; set; }
        public TransferAmountRequestTransaction Transaction { get; set; }
    }
    public class TransferAmountRequestAccount
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
    }
    public class TransferAmountRequestTransaction
    {
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
