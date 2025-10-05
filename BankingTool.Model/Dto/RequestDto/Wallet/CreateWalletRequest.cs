namespace BankingTool.Model.Dto.RequestDto.Wallet
{
    public class CreateWalletRequestObject
    {
        public CreateWalletRequest Request { get; set; }
    }
    public class CreateWalletRequest
    {
        public CreateWalletRequestWallet Wallet { get; set; }
    }
    public class CreateWalletRequestWallet
    {
        public string WalletName { get; set; }
        public decimal Amount { get; set; }
    }
}
