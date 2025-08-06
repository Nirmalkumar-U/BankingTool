using BankingTool.Model;
using BankingTool.Model.Model;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class WalletService(IWalletRespository walletRepository) : IWalletService
    {
        private readonly IWalletRespository _walletRepository = walletRepository;
        public async Task<ResponseDto<bool>> CreateWalletAccount(string walletName, int amount)
        {
            ResponseDto<bool> response = new()
            {
                Errors = [],
                ValidationErrors = [],
                Status = false,
            };
            var walletAccount = new WalletAccount()
            {
                WalletName = walletName,
                WalletNumber = await GetNewBudgetNumber(),
                Balance = amount,
                WalletStatus = "Active"
            };
            WalletTransaction walletTransaction = new()
            {
                Amount = amount,
                Description = "Initial Credit",
                TransactionTime = new DateTime(2025, 5, 6),
                TransactionCategory = TransactionCatagory.Deposit,
                TransactionTag = TransactionTag.Deposit
            };
            WalletTransactionDetail walletTransactionDetails = new()
            {
                TransactionType = TransactionType.Credit,
                StageBalance = amount,
                TransactionRole = TransactionRole.Receiver
            };

            var createdBudgerAccount = await _walletRepository.CreateWalletAccount(walletAccount, walletTransaction, walletTransactionDetails);

            response.Status = createdBudgerAccount;
            response.Response = createdBudgerAccount;
            response.StatuCode = createdBudgerAccount ? 200 : 400;
            if (!createdBudgerAccount)
                response.Errors.Add(new Errors { PropertyName = "Create Wallet Account", ErrorMessage = "Failed to create Wallet account" });
            response.Message = createdBudgerAccount ? "Wallet Account Created Successfully" : "Failed to create Wallet account";
            return response;
        }

        private async Task<string> GetNewBudgetNumber()
        {
            var lastWalletAccount = await _walletRepository.GetLastWalletAccount();
            return ((lastWalletAccount != null && long.TryParse(lastWalletAccount.WalletNumber, out long lastNumber)) ? lastNumber + 1 : 50000000).ToString();
        }
    }
}
