using BankingTool.Model.Dto.BaseDto;

namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class CreditCardDTO
    {
        public int CreditCardId { get; set; }
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountAbbreviation { get; set; }
        public string AccountDetail { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal BalanceCredit { get; set; }
        public decimal AvailableCredit { get; set; }

        public string Status { get; set; }
        public string CardHolderName { get; set; }
        public string CardType { get; set; }
        public CreditCardNumber CardNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? ApprovedLimit { get; set; }
        public decimal? LimitUsed { get; set; }
        public decimal? CurrentLimit { get; set; }
    }
}
