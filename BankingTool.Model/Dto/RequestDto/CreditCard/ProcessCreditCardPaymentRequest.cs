using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.CreditCard
{

    public class ProcessCreditCardPaymentRequestObject
    {
        public ProcessCreditCardPaymentRequest Request { get; set; }
    }
    public class ProcessCreditCardPaymentRequest
    {
        public ProcessCreditCardPaymentRequestCreditCard CreditCard { get; set; }
        public ProcessCreditCardPaymentRequestTransaction Transaction { get; set; }
    }
    public class ProcessCreditCardPaymentRequestCreditCard : RequestId
    {
    }
    public class ProcessCreditCardPaymentRequestTransaction
    {
        public decimal PaymentAmount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Fees { get; set; }
    }
}
