namespace BankingTool.Model.Dto.RequestDto.CreditCard
{
    public class ProcessCreditCardPaymentRequestDTO
    {
        public int CreditCardId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Fees { get; set; }
    }
}
