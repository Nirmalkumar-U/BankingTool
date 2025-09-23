using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Wallet
{
    public class GetWalletBalanceRequestObject
    {
        public GetWalletBalanceRequest Request { get; set; }
    }
    public class GetWalletBalanceRequest
    {
        public GetWalletBalanceRequestAccount Account { get; set; }
    }
    public class GetWalletBalanceRequestAccount : RequestId
    {
    }
}
