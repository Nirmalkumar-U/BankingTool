using BankingTool.Model.Model;

namespace BankingTool.Model.Dto.InternalDto
{
    public class WalletTransactionDto
    {
        public WalletAccount WalletAccount { get; set; }
        public WalletTransaction WalletTransaction { get; set; }
        public WalletTransactionDetail WalletTransactionDetail { get; set; }
    }
}
