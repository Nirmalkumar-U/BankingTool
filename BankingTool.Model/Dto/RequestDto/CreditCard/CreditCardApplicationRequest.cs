using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.CreditCard
{

    public class CreditCardApplicationRequestObject
    {
        public CreditCardApplicationRequest Request { get; set; }
    }
    public class CreditCardApplicationRequest
    {
        public CreditCardApplicationRequestAccount Account { get; set; }
        public CreditCardApplicationRequestTransaction Transaction { get; set; }
    }
    public class CreditCardApplicationRequestAccount : RequestId
    {
    }
    public class CreditCardApplicationRequestTransaction
    {
        public string CardType { get; set; }
        public decimal DesiredLimit { get; set; }
    }
}
