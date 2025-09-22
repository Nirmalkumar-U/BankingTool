using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Model;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class CommonService(IBankAccountRepository bankAccountRepository, IBudgetRepository budgetRepository, IWalletRepository walletRepository) : ICommonService
    {
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;
        private readonly IBudgetRepository _budgetRepository = budgetRepository;
        private readonly IWalletRepository _walletRepository = walletRepository;
        public async Task<(AccountTransactionsDto, List<ValidationResults>)> GetAccountDetailsToDebitOrCredit(bool isDebit, int? accountId, int amount, string description, DateTime transactionDate,
            string transactionTag = "Transfer", string transactionCatagory = "Transfer")
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
                TransactionCategory = transactionCatagory,
                TransactionTag = transactionTag
            };
            TransactionDetail transactionDetails = new()
            {
                AccountId = account.AccountId,
                TransactionType = isDebit ? TransactionType.Debit : TransactionType.Credit,
                StageBalance = account.Balance,
                TransactionRole = isDebit ? TransactionRole.Sender : TransactionRole.Receiver,
            };
            var result = new AccountTransactionsDto
            {
                Account = account,
                Transaction = transaction,
                TransactionDetail = transactionDetails
            };
            return (result, null);
        }
        public async Task<(List<BudgetTransactionDto>, List<ValidationResults>)> GetBudgetAccountDetailsToDebitOrCredit(bool isDebit, int? accountId, int amount, string description,
            DateTime transactionDate, List<BudgetAccountDto> budgetAccounts, string transactionTag = "Transfer", string transactionCatagory = "Transfer")
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
                        TransactionCategory = transactionCatagory,
                        TransactionTag = transactionTag
                    };
                    BudgetTransactionDetail budgetTransactionDetails = new BudgetTransactionDetail()
                    {
                        TransactionType = isDebit ? TransactionType.Debit : TransactionType.Credit,
                        StageBalance = budgetAccount.Balance,
                        TransactionRole = isDebit ? TransactionRole.Sender : TransactionRole.Receiver,
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
        public async Task<(WalletTransactionDto, List<ValidationResults>)> GetWalletDetailsToDebitOrCredit(bool isDebit, int walletId, int amount, string description, DateTime transactionDate,
            string transactionCatagory = "Transfer", string transactionTag = "Transfer")
        {
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
            WalletAccount account = await _walletRepository.GetWalletAccount(walletId);
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
                TransactionCategory = transactionCatagory,
                TransactionTag = transactionTag
            };
            WalletTransactionDetail transactionDetails = new()
            {
                WalletAccountId = account.WalletAccountId,
                TransactionType = isDebit ? TransactionType.Debit : TransactionType.Credit,
                StageBalance = account.Balance,
                TransactionRole = isDebit ? TransactionRole.Sender : TransactionRole.Receiver,
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
        private static List<ValidationResults> ValidateBudgetAccount(List<BudgetAccountDto> budgetAccounts, int amount)
        {
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                bool isInValid = budgetAccounts.Any(x => x.Id == null);
                bool isAmountInValid = budgetAccounts.Any(x => x?.Amount == null || x.Amount == 0);
                int sumOfBudgetAccountAmount = budgetAccounts.Sum(x => x.Amount);
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
        private static List<ValidationResults> ValidateBalance(int balance, int amount)
        {
            if (balance < amount)
            {
                return [new ValidationResults { PropertyPath = "Request.Account.Amount", PropertyName = "Amount", ErrorMessage = "Amount should be lesser equal to Account Balance." }];
            }
            return null;
        }
        #endregion Private Method
    }
}
