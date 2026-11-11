namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class CreditCardDTO
    {
        public int CreditCardId { get; set; }
        public string MaskedCardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardType { get; set; }
        public string Status { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal AvailableCredit { get; set; }
        public string MaskedExpiryDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
