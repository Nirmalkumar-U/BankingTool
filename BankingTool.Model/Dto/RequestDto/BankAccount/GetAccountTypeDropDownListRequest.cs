using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class GetAccountTypeDropDownListRequestObject
    {
        public GetAccountTypeDropDownListRequest Request { get; set; }
    }
    public class GetAccountTypeDropDownListRequest
    {
        public GetAccountTypeDropDownListRequestCustomer Customer { get; set; }
        public GetAccountTypeDropDownListRequestBank Bank { get; set; }
    }
    public class GetAccountTypeDropDownListRequestCustomer : RequestId
    {
    }
    public class GetAccountTypeDropDownListRequestBank : RequestId
    {
    }
}
