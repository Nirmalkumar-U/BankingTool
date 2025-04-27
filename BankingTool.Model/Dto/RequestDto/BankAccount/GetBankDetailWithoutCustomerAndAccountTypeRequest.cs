using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount;
public class GetBankDetailWithoutCustomerAndAccountTypeRequestObject
{
    public GetBankDetailWithoutCustomerAndAccountTypeRequest Request { get; set; }
}
public class GetBankDetailWithoutCustomerAndAccountTypeRequest
{
    public GetBankDetailWithoutCustomerAndAccountTypeRequestCustomer Customer { get; set; }
    public GetBankDetailWithoutCustomerAndAccountTypeRequestAccount Account { get; set; }
}
public class GetBankDetailWithoutCustomerAndAccountTypeRequestCustomer : RequestId
{

}
public class GetBankDetailWithoutCustomerAndAccountTypeRequestAccount
{
    public int AccountTypeId { get; set; }
}

