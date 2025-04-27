using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class BankDropDownListRequestObject
    {
        public BankDropDownListRequest Request { get; set; }
    }
    public class BankDropDownListRequest
    {
        public BankDropDownListRequestCustomer Customer { get; set; }
    }
    public class BankDropDownListRequestCustomer : RequestId
    {
    }
}
