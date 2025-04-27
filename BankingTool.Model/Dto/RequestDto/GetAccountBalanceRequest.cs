namespace BankingTool.Model.Dto.RequestDtos
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
