using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Enums;
using BankingTool.Model.Model;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class CommonService(IBankAccountRepository bankAccountRepository, IBudgetRepository budgetRepository, IWalletRepository walletRepository) : ICommonService
    {
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;
        private readonly IBudgetRepository _budgetRepository = budgetRepository;
        private readonly IWalletRepository _walletRepository = walletRepository;
        public async Task<(AccountTransactionsDto, List<ValidationResults>)> GetAccountDetailsToDebitOrCredit(bool isDebit, int? accountId, decimal amount, string description, DateTime transactionDate,
            TransactionTagEnum transactionTag = TransactionTagEnum.Transfer, TransactionCategoryEnum transactionCatagory = TransactionCategoryEnum.Transfer)
        {
            if (accountId == null)
            {
                return (null, new List<ValidationResults>() { new ValidationResults { PropertyPath = "Request.Account.Id", PropertyName = "Id", ErrorMessage = "Account Id is Required." } });
            }
            Account account = await _bankAccountRepository.GetAccount(accountId.Value);
            if (isDebit)
            {
                var validationResultBalance = ValidateBalance(account.Balance, amount);
                if (validationResultBalance != null)
                {
                    return (null, validationResultBalance);
                }
            }

            var lastTransaction = await _bankAccountRepository.GetLastTransaction(accountId.Value);
            if (lastTransaction != null && lastTransaction?.TransactionTime.Date > transactionDate.Date)
            {
                return (null, new List<ValidationResults>() { new ValidationResults { PropertyPath = "Request.Account.TransactionDate", PropertyName = "TransactionDate", ErrorMessage = "Transaction Date should be greater then last transaction date." } });
            }
            if (lastTransaction != null && lastTransaction.TransactionTime.Date == transactionDate.Date)
            {
                transactionDate = lastTransaction.TransactionTime;
                transactionDate = transactionDate.AddMinutes(5);
            }

            if (isDebit)
            {
                account.Balance = account.Balance - amount;
            }
            else
            {
                account.Balance = account.Balance + amount;
            }
            Transaction transaction = new()
            {
                Amount = amount,
                Description = description,
                TransactionTime = transactionDate,
                TransactionCategory = transactionCatagory.EnumToString(),
                TransactionTag = transactionTag.EnumToString()
            };
            TransactionDetail transactionDetails = new()
            {
                AccountId = account.AccountId,
                TransactionType = isDebit ? TransactionTypeEnum.Debit.EnumToString() : TransactionTypeEnum.Credit.EnumToString(),
                StageBalance = account.Balance,
                TransactionRole = isDebit ? TransactionRoleEnum.Sender.EnumToString() : TransactionRoleEnum.Receiver.EnumToString(),
            };
            var result = new AccountTransactionsDto
            {
                Account = account,
                Transaction = transaction,
                TransactionDetail = transactionDetails
            };
            return (result, null);
        }
        public async Task<(List<BudgetTransactionDto>, List<ValidationResults>)> GetBudgetAccountDetailsToDebitOrCredit(bool isDebit, int? accountId, decimal amount, string description,
            DateTime transactionDate, List<BudgetAccountDto> budgetAccounts, TransactionTagEnum transactionTag = TransactionTagEnum.Transfer, TransactionCategoryEnum transactionCatagory = TransactionCategoryEnum.Transfer)
        {
            Account account = accountId == null ? null : await _bankAccountRepository.GetAccount(accountId.Value);
            var validationResult = ValidateBudgetAccount(budgetAccounts, amount);
            if (validationResult != null)
            {
                return (null, validationResult);
            }
            List<BudgetTransactionDto> budgetAccountList = [];
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                foreach (var bAccount in budgetAccounts)
                {
                    BudgetTransaction lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                    if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                    {
                        return (null, new List<ValidationResults>() { new ValidationResults { PropertyPath = "Request.BudgetAccount.TransactionDate", PropertyName = "TransactionDate", ErrorMessage = "Transaction Date should be greater then last transaction date." } });
                    }
                    DateTime budgetTransactionDate = transactionDate;
                    if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction.TransactionTime.Date == budgetTransactionDate.Date)
                    {
                        budgetTransactionDate = lastBudgetAccountTransaction.TransactionTime;
                        budgetTransactionDate = budgetTransactionDate.AddMinutes(5);
                    }
                    BudgetAccount budgetAccount = await _budgetRepository.GetBudgetAccountById(bAccount.Id);
                    if (isDebit)
                    {
                        budgetAccount.Balance = budgetAccount.Balance - bAccount.Amount;
                    }
                    else
                    {
                        budgetAccount.Balance = budgetAccount.Balance + bAccount.Amount;
                    }
                    BudgetTransaction budgetTransaction = new BudgetTransaction()
                    {
                        Amount = bAccount.Amount,
                        Description = description,
                        TransactionTime = budgetTransactionDate,
                        TransactionCategory = transactionCatagory.EnumToString(),
                        TransactionTag = transactionTag.EnumToString()
                    };
                    BudgetTransactionDetail budgetTransactionDetails = new BudgetTransactionDetail()
                    {
                        TransactionType = isDebit ? TransactionTypeEnum.Debit.EnumToString() : TransactionTypeEnum.Credit.EnumToString(),
                        StageBalance = budgetAccount.Balance,
                        TransactionRole = isDebit ? TransactionRoleEnum.Sender.EnumToString() : TransactionRoleEnum.Receiver.EnumToString(),
                        AccountId = accountId == null ? 1 : account.AccountId,
                        BudgetAccountId = budgetAccount.BudgetAccountId
                    };

                    BudgetTransactionDto budgetTransactions = new BudgetTransactionDto()
                    {
                        BudgetAccount = budgetAccount,
                        BudgetTransaction = budgetTransaction,
                        BudgetTransactionDetail = budgetTransactionDetails
                    };
                    budgetAccountList.Add(budgetTransactions);
                }
            }
            return (budgetAccountList, null);
        }
        public async Task<(WalletTransactionDto, List<ValidationResults>)> GetWalletDetailsToDebitOrCredit(bool isDebit, int walletId, decimal amount, string description, DateTime transactionDate,
            TransactionCategoryEnum transactionCatagory = TransactionCategoryEnum.Transfer, TransactionTagEnum transactionTag = TransactionTagEnum.Transfer)
        {
            WalletAccount account = await _walletRepository.GetWalletAccount(walletId);
            if (isDebit)
            {
                var validationResultBalance = ValidateBalance(account.Balance, amount);
                if (validationResultBalance != null)
                {
                    return (null, validationResultBalance);
                }
            }
            var lastTransaction = await _walletRepository.GetLastWalletTransaction(walletId);
            if (lastTransaction != null && lastTransaction?.TransactionTime.Date > transactionDate.Date)
            {
                return (null, new List<ValidationResults>() { new ValidationResults { PropertyPath = "Request.Wallet.TransactionDate", PropertyName = "TransactionDate", ErrorMessage = "Transaction Date should be greater then last transaction date." } });

            }
            if (lastTransaction != null && lastTransaction.TransactionTime.Date == transactionDate.Date)
            {
                transactionDate = lastTransaction.TransactionTime;
                transactionDate = transactionDate.AddMinutes(5);
            }
            if (isDebit)
            {
                account.Balance -= amount;
            }
            else
            {
                account.Balance += amount;
            }

            WalletTransaction transaction = new()
            {
                Amount = amount,
                Description = description,
                TransactionTime = transactionDate,
                TransactionCategory = transactionCatagory.EnumToString(),
                TransactionTag = transactionTag.EnumToString()
            };
            WalletTransactionDetail transactionDetails = new()
            {
                WalletAccountId = account.WalletAccountId,
                TransactionType = isDebit ? TransactionTypeEnum.Debit.EnumToString() : TransactionTypeEnum.Credit.EnumToString(),
                StageBalance = account.Balance,
                TransactionRole = isDebit ? TransactionRoleEnum.Sender.EnumToString() : TransactionRoleEnum.Receiver.EnumToString(),
            };
            var result = new WalletTransactionDto
            {
                WalletAccount = account,
                WalletTransaction = transaction,
                WalletTransactionDetail = transactionDetails
            };
            return (result, null);
        }
        #region Private Method
        private static List<ValidationResults> ValidateBudgetAccount(List<BudgetAccountDto> budgetAccounts, decimal amount)
        {
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                bool isInValid = budgetAccounts.Any(x => x.Id == null);
                bool isAmountInValid = budgetAccounts.Any(x => x?.Amount == null || x.Amount == 0);
                decimal sumOfBudgetAccountAmount = budgetAccounts.Sum(x => x.Amount);
                if (sumOfBudgetAccountAmount > amount)
                {
                    return [new ValidationResults { PropertyPath = "Request.BudgetAccount.Amount", PropertyName = "Amount", ErrorMessage = "Sum of Budget Account Amount should be lesser equal to Transfer Amount." }];
                }
                if (isInValid)
                {
                    return [new ValidationResults { PropertyPath = "Request.BudgetAccount.Id", PropertyName = "BudgetAccountId", ErrorMessage = "BudgetAccountId is required." }];
                }
                if (isAmountInValid)
                {
                    return [new ValidationResults { PropertyPath = "Request.BudgetAccount.Amount", PropertyName = "Amount", ErrorMessage = "Amount is required." }];
                }
            }
            return null;
        }
        private static List<ValidationResults> ValidateBalance(decimal balance, decimal amount)
        {
            if (balance < amount)
            {
                return [new ValidationResults { PropertyPath = "Request.Amount", PropertyName = "Amount", ErrorMessage = "Amount should be lesser equal to Account Balance." }];
            }
            return null;
        }
        #endregion Private Method
    }
}
