using BankingTool.Model.Enums;

namespace BankingTool.Model.Dto.ResponseDto
{
    public class GetWalletListResponse
    {
        public List<GetWalletList> WalletList { get; set; }
    }
    public class GetWalletList
    {
        public int WalletId { get; set; }
        public string WalletName { get; set; }
        public string WalletNumber { get; set; }
        public decimal Balance { get; set; }
        public WalletStatusEnum WalletStatus { get; set; }

    }
}
