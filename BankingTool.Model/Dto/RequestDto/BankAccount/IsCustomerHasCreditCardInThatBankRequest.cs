using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class IsCustomerHasCreditCardInThatBankRequestObject
    {
        public IsCustomerHasCreditCardInThatBankRequest Request { get; set; }
    }
    public class IsCustomerHasCreditCardInThatBankRequest
    {
        public IsCustomerHasCreditCardInThatBankRequestCustomer Customer { get; set; }
        public IsCustomerHasCreditCardInThatBankRequestBank Bank { get; set; }
    }
    public class IsCustomerHasCreditCardInThatBankRequestCustomer : RequestId
    {
    }
    public class IsCustomerHasCreditCardInThatBankRequestBank : RequestId
    {
    }
}
