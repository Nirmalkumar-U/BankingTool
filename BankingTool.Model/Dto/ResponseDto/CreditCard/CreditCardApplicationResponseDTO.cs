using BankingTool.Model.Dto.BaseDto;

namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class CreditCardApplicationResponseDTO
    {
        public int ApplicationId { get; set; }
        public string Status { get; set; }
        public string CardHolderName { get; set; }
        public string CardType { get; set; }
        public CreditCardNumber CardNumber { get; set; } 
        public DateTime ApplicationDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? ApprovedLimit { get; set; }
        public decimal? CurrentLimit { get; set; }
    }
}
