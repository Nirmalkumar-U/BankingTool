using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class GetToAccountListExcludedByAccountIdRequestObject
    {
        public GetToAccountListExcludedByAccountIdRequest Request { get; set; }
    }
    public class GetToAccountListExcludedByAccountIdRequest
    {
        public GetToAccountListExcludedByAccountIdRequestAccount Account { get; set; }
    }
    public class GetToAccountListExcludedByAccountIdRequestAccount : RequestId
    {
    }
}
