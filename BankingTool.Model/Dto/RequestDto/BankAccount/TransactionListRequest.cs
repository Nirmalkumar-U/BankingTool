using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class TransactionListRequestObject
    {
        public TransactionListRequest Request { get; set; }
    }
    public class TransactionListRequest
    {
        public TransactionListRequestAccount Account { get; set; }
        public TransactionListRequestTransaction Transaction { get; set; }
    }
    public class TransactionListRequestAccount : RequestId
    {
        public int? SenderAccountId { get; set; }
        public int? ReceiverAccountId { get; set; }
    }
    public class TransactionListRequestTransaction
    {
        public string TransactionTag { get; set; }
        public DateTime? TransactionFromDate { get; set; }
        public DateTime? TransactionToDate { get; set; }
        public string TransactionCategory { get; set; }
    }
}
