namespace BankingTool.Model.Dto.RequestDtos;
public class GetBankDetailWithoutCustomerAndAccountTypeRequestObject
{
    public GetBankDetailWithoutCustomerAndAccountTypeRequest GetBankDetailWithoutCustomerAndAccountTypeRequest { get; set; }
}
public class GetBankDetailWithoutCustomerAndAccountTypeRequest
{
    public GetBankDetailWithoutCustomerAndAccountTypeRequestCustomer Customer { get; set; }
    public GetBankDetailWithoutCustomerAndAccountTypeRequestAccount Account { get; set; }
}
public class GetBankDetailWithoutCustomerAndAccountTypeRequestCustomer
{
    public int CustomerId { get; set; }

}
public class GetBankDetailWithoutCustomerAndAccountTypeRequestAccount
{
    public int AccountTypeId { get; set; }
}

