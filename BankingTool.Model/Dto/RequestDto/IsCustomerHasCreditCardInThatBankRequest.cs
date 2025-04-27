namespace BankingTool.Model.Dto.RequestDtos
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
