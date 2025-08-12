using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Budget
{
    public class GetWalletTransactionListRequestObject
    {
        public GetWalletTransactionListRequest Request { get; set; }
    }

    public class GetWalletTransactionListRequest
    {
        public GetWalletTransactionListRequestWallet Wallet { get; set; }
    }

    public class GetWalletTransactionListRequestWallet : RequestId
    {
    }
}
