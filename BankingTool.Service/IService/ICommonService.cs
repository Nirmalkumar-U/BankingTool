using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;

namespace BankingTool.Service.IService
{
    public interface ICommonService
    {
        Task<(AccountTransactionsDto, List<ValidationResults>)> GetAccountDetailsToDebitOrCredit(bool isDebit, int? accountId, int amount, string description, DateTime transactionDate,
           string transactionTag = "Transfer", string transactionCatagory = "Transfer");
        Task<(List<BudgetTransactionDto>, List<ValidationResults>)> GetBudgetAccountDetailsToDebitOrCredit(bool isDebit, int? accountId, int amount, string description,
            DateTime transactionDate, List<BudgetAccountDto> budgetAccounts, string transactionTag = "Transfer", string transactionCatagory = "Transfer");
        Task<(WalletTransactionDto, List<ValidationResults>)> GetWalletDetailsToDebitOrCredit(bool isDebit, int walletId, int amount, string description, DateTime transactionDate,
            string transactionCatagory = "Transfer", string transactionTag = "Transfer");
    }
}
