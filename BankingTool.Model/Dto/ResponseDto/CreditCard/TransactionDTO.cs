namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string MerchantName { get; set; }
        public string TransactionStatus { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
