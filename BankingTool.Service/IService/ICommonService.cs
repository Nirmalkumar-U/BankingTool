using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Enums;

namespace BankingTool.Service.IService
{
    public interface ICommonService
    {
        Task<(AccountTransactionsDto, List<ValidationResults>)> GetAccountDetailsToDebitOrCredit(bool isDebit, int? accountId, int amount, string description, DateTime transactionDate,
           TransactionTagEnum transactionTag = TransactionTagEnum.Transfer, TransactionCategoryEnum transactionCatagory = TransactionCategoryEnum.Transfer);
        Task<(List<BudgetTransactionDto>, List<ValidationResults>)> GetBudgetAccountDetailsToDebitOrCredit(bool isDebit, int? accountId, int amount, string description,
            DateTime transactionDate, List<BudgetAccountDto> budgetAccounts, TransactionTagEnum transactionTag = TransactionTagEnum.Transfer, TransactionCategoryEnum transactionCatagory = TransactionCategoryEnum.Transfer);
        Task<(WalletTransactionDto, List<ValidationResults>)> GetWalletDetailsToDebitOrCredit(bool isDebit, int walletId, int amount, string description, DateTime transactionDate,
            TransactionCategoryEnum transactionCatagory = TransactionCategoryEnum.Transfer, TransactionTagEnum transactionTag = TransactionTagEnum.Transfer);
    }
}
