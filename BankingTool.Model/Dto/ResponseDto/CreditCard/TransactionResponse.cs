using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class TransactionResponseObject
    {
        public TransactionResponse Response { get; set; }
    }
    public class TransactionResponseListObject
    {
        public List<TransactionResponse> Response { get; set; }
    }
    public class TransactionResponse
    {
        public TransactionResponseTransaction Transaction { get; set; }
        public TransactionResponseCreditCard CreditCard { get; set; }
        public TransactionResponseAccount Account { get; set; }
    }
    public class TransactionResponseCreditCard : RequestId
    {
        public string CreditCardNumber { get; set; }
        public string CardDetail { get; set; }
        public string CardholderName { get; set; }
    }
    public class TransactionResponseAccount : RequestId
    {
    }
    public class TransactionResponseTransaction : RequestId
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string MerchantName { get; set; }
        public string TransactionStatus { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
