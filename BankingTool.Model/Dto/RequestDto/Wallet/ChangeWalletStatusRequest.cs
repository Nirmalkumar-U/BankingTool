using BankingTool.Model.Dto.RequestDtos;

namespace BankingTool.Model.Dto.RequestDto.Wallet
{
    public class ChangeWalletStatusRequestObject
    {
        public ChangeWalletStatusRequest Request { get; set; }
    }
    public class ChangeWalletStatusRequest
    {
        public ChangeWalletStatusRequestWallet Wallet { get; set; }
    }
    public class ChangeWalletStatusRequestWallet : RequestId
    {
    }
}
