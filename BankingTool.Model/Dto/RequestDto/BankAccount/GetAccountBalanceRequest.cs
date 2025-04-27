using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.BankAccount
{
    public class GetAccountBalanceRequestObject
    {
        public GetAccountBalanceRequest Request { get; set; }
    }
    public class GetAccountBalanceRequest
    {
        public GetAccountBalanceRequestAccount Account { get; set; }
    }
    public class GetAccountBalanceRequestAccount : RequestId
    {
    }
}
