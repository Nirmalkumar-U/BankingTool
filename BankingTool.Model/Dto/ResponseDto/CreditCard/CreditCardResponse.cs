using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.ResponseDto.CreditCard
{
    public class CreditCardResponseObject
    {
        public CreditCardResponse Response { get; set; }
    }
    public class CreditCardResponseListObject
    {
        public List<CreditCardResponse> Responses { get; set; }
    }

    public class CreditCardResponse
    {
        public CreditCardResponseCreditCard CreditCard { get; set; }
        public CreditCardResponseAccount Account { get; set; }
    }

    public class CreditCardResponseCreditCard : RequestId
    {
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

    public class CreditCardResponseAccount : RequestId
    {
        public string AccountNumber { get; set; }
        public string AccountAbbreviation { get; set; }
        public string AccountDetail { get; set; }
    }

}
