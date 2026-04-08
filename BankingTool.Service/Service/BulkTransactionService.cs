using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.RequestDto.BulkTransaction;
using BankingTool.Model.Enums;
using BankingTool.Model.Model;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class BulkTransactionService(IBankAccountRepository bankAccountRepository, IBudgetRepository budgetRepository,
        DataContext dataContext) : EntityRepository<Account>(dataContext), IBulkTransactionService
    {
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;
        private readonly IBudgetRepository _budgetRepository = budgetRepository;
        public async Task<ResponseDto<bool>> BulkTransaction(BulkTransactionRequestObject model)
        {
            using var sqlTransaction = dataContext.Database.BeginTransaction();
            try
            {
                foreach (var request in model.Request)
                {
                    var budgetAccounts = request.BudgetAccount != null ? request.BudgetAccount.Select(x => new BudgetAccountDto
                    {
                        Id = x.Id,
                        Amount = x.Amount
                    }).ToList() : [];

                    //Get all details of transaction to save and validate before saving any data into database
                    var (transactions, validationResult, errors) = await GetTransactionDetailsOfTransactionToSave(request.TransactionCategory, request.Account.FromAccountId,
                        request.Account.ToAccountId, request.Transaction.Amount, request.Transaction.Description, request.Transaction.TransactionDate, request.Transaction.TransactionTag,
                        budgetAccounts, model.Request.Count());

                    //If any validation error or error exists then return without saving any data into database
                    if (validationResult.Count != 0 || errors.Count != 0)
                    {
                        sqlTransaction.Rollback();
                        return new ResponseDto<bool>
                        {
                            Response = false,
                            Status = false,
                            ValidationErrors = validationResult,
                            Errors = errors
                        };
                    }

                    //FromAccount
                    if (transactions.FromAccount != null)
                    {
                        UpdateAccount(transactions.FromAccount);
                    }
                    //ToAccount
                    if (transactions.ToAccount != null)
                    {
                        UpdateAccount(transactions.ToAccount);
                    }
                    //Transaction
                    int? transactionId = 0;
                    if (transactions.Transaction != null)
                    {
                        transactionId = InsertTransaction(transactions.Transaction);
                    }
                    //FromTransactionDetail
                    if (transactions.FromTransactionDetail != null && transactionId != null && transactionId != 0)
                    {
                        transactions.FromTransactionDetail.TransactionId = transactionId.Value;
                        InsertTransactionDetail(transactions.FromTransactionDetail);
                    }
                    //ToTransactionDetail
                    if (transactions.ToTransactionDetail != null && transactionId != null && transactionId != 0)
                    {
                        transactions.ToTransactionDetail.TransactionId = transactionId.Value;
                        InsertTransactionDetail(transactions.ToTransactionDetail);
                    }
                    //BudgetNotification
                    if (transactions.BudgetNotification != null)
                    {
                        InsertBudgetNotification(transactions.BudgetNotification);
                    }
                    //Budget
                    if (transactions.BudgetTransaction != null && transactions.BudgetTransaction.Count != 0)
                    {
                        foreach (var budgetAccount in transactions.BudgetTransaction)
                        {
                            UpdateBudgetAccount(budgetAccount.BudgetAccount);
                            var budgetTransactionId = InsertBudgetTransaction(budgetAccount.BudgetTransaction);
                            budgetAccount.BudgetTransactionDetail.BudgetTransactionId = budgetTransactionId.Value;
                            var budgetTransactionDetailId = InsertBudgetTransactionDetail(budgetAccount.BudgetTransactionDetail);
                        }
                    }

                }

                sqlTransaction.Commit();

                return new ResponseDto<bool>
                {
                    Response = true,
                    Status = true,
                    ValidationErrors = [],
                    Errors = []
                };
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = [],
                    Errors = [new Errors { PropertyName = "BulkTransaction", ErrorMessage = ex.Message }]
                };
            }
        }
        public async Task<(BulkTransactionDto, List<ValidationResults>, List<Errors>)> GetTransactionDetailsOfTransactionToSave(
            TransactionCategoryEnum transactionCategoryEnum, int? fromAccountId, int? toAccountId, decimal amount, string description,
            DateTime transactionDate, TransactionTagEnum transactionTag, List<BudgetAccountDto> budgetAccounts, int count)
        {
            List<ValidationResults> validationResults = [];
            List<Errors> errors = [];
            //Transfer Amount
            if (transactionCategoryEnum == TransactionCategoryEnum.Transfer)
            {
                var account = await _bankAccountRepository.GetAccount(fromAccountId.Value);
                //Validate Balance of From Account
                var validateBalance = ValidateBalance(account.Balance, amount);
                if (validateBalance != null)
                {
                    validationResults.AddRange(validateBalance);
                }
                //Validate Budget Account
                var validationResultBudgetAccount = ValidateBudgetAccount(budgetAccounts, amount);
                if (validationResultBudgetAccount != null)
                {
                    validationResults.AddRange(validationResultBudgetAccount);
                }
                //Validate Transaction Date should be greater than last transaction date
                var lastFromAccountTransaction = await _bankAccountRepository.GetLastTransaction(fromAccountId.Value);
                var lastToAccountTransaction = await _bankAccountRepository.GetLastTransaction(toAccountId.Value);
                if (lastFromAccountTransaction != null && lastFromAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                {
                    errors.Add(new Errors { PropertyName = "TransferAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." });
                }
                if (lastToAccountTransaction != null && lastToAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                {
                    errors.Add(new Errors { PropertyName = "TransferAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." });
                }
                if (budgetAccounts != null && budgetAccounts.Count > 0)
                {
                    foreach (var bAccount in budgetAccounts)
                    {
                        var lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                        if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                        {
                            errors.Add(new Errors { PropertyName = "TransferAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." });
                        }
                    }
                }
                //If any validation error or error exists then return without saving any data into database
                if (validationResults.Count != 0 || errors.Count != 0)
                {
                    return (null, validationResults, errors);
                }

                DateTime? latestTransactionDate = new[]
                {
                    lastFromAccountTransaction?.TransactionTime,
                    lastToAccountTransaction?.TransactionTime
                }.Max();

                if (latestTransactionDate != null && latestTransactionDate.Value.Date == transactionDate.Date)
                {
                    transactionDate = latestTransactionDate.Value;
                    transactionDate = transactionDate.AddMinutes(5);
                }
                //Get From Account and To Account details and prepare transaction, transaction details and budget transaction details to save into database
                Account fromAccount = await _bankAccountRepository.GetAccount(fromAccountId.Value);
                Account toAccount = await _bankAccountRepository.GetAccount(toAccountId.Value);
                fromAccount.Balance = fromAccount.Balance - amount;
                toAccount.Balance = toAccount.Balance + amount;
                Transaction transaction = new Transaction
                {
                    TransactionTime = transactionDate,
                    Amount = amount,
                    Description = description,
                    TransactionCategory = TransactionCategoryEnum.Transfer.EnumToString(),
                    TransactionTag = transactionTag.EnumToString()
                };
                TransactionDetail fromTransactionDetail = new TransactionDetail
                {
                    AccountId = fromAccountId.Value,
                    StageBalance = fromAccount.Balance,
                    TransactionType = TransactionTypeEnum.Debit.EnumToString(),
                    TransactionRole = TransactionRoleEnum.Sender.EnumToString()
                };
                TransactionDetail toTransactionDetail = new TransactionDetail
                {
                    AccountId = toAccountId.Value,
                    StageBalance = toAccount.Balance,
                    TransactionType = TransactionTypeEnum.Credit.EnumToString(),
                    TransactionRole = TransactionRoleEnum.Receiver.EnumToString()
                };
                //Get Budget Transaction and Budget Transaction details
                List<BudgetTransactionDto> budgetAccountList = [];
                if (budgetAccounts != null && budgetAccounts.Count > 0)
                {
                    foreach (var bAccount in budgetAccounts)
                    {
                        var lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                        var budgetTransactionDate = transactionDate;
                        if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction.TransactionTime.Date == budgetTransactionDate.Date)
                        {
                            budgetTransactionDate = lastBudgetAccountTransaction.TransactionTime;
                            budgetTransactionDate = budgetTransactionDate.AddMinutes(5);
                        }
                        var budgetAccount = await _budgetRepository.GetBudgetAccountById(bAccount.Id);
                        budgetAccount.Balance = budgetAccount.Balance - bAccount.Amount;
                        var budgetTransaction = new BudgetTransaction()
                        {
                            Amount = bAccount.Amount,
                            Description = description,
                            TransactionTime = budgetTransactionDate,
                            TransactionCategory = TransactionCategoryEnum.Transfer.EnumToString(),
                            TransactionTag = transactionTag.EnumToString()
                        };
                        var budgetTransactionDetails = new BudgetTransactionDetail()
                        {
                            TransactionType = TransactionTypeEnum.Debit.EnumToString(),
                            StageBalance = budgetAccount.Balance,
                            TransactionRole = TransactionRoleEnum.Sender.EnumToString(),
                            AccountId = fromAccountId.Value,
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
                //If Receiver account is customer as budget account then create notification for that customer
                var isCustomerAsBudgetAccount = await _budgetRepository.IsCustomerAsBudgetAccount(toAccount.CustomerId);
                BudgetNotification budgetNotification = null;
                if (isCustomerAsBudgetAccount)
                {
                    budgetNotification = new BudgetNotification()
                    {
                        AccountId = toAccountId.Value,
                        Amount = amount,
                        Description = description,
                        TransactionDate = transactionDate,
                        TransactionCatagory = TransactionCategoryEnum.Transfer.EnumToString(),
                        TransactionTag = transactionTag.EnumToString(),
                        TransactionRole = TransactionRoleEnum.Receiver.EnumToString(),
                        TransactionType = TransactionTypeEnum.Credit.EnumToString(),
                        CustomerId = toAccount.CustomerId,
                        NotificationStatus = NotificationStatusEnum.Pending.EnumToString()
                    };
                }
                return (new BulkTransactionDto
                {
                    FromAccount = fromAccount,
                    ToAccount = toAccount,
                    Transaction = transaction,
                    FromTransactionDetail = fromTransactionDetail,
                    ToTransactionDetail = toTransactionDetail,
                    BudgetTransaction = budgetAccountList,
                    BudgetNotification = budgetNotification,
                    TransactionCategory = transactionCategoryEnum
                }, validationResults, errors);
            }

            //Deposit and withdraw Amount
            else if (transactionCategoryEnum == TransactionCategoryEnum.Deposit || transactionCategoryEnum == TransactionCategoryEnum.Withdraw)
            {
                //Validate Budget Account
                var validationResult = ValidateBudgetAccount(budgetAccounts, amount);
                if (validationResult != null)
                {
                    validationResults.AddRange(validationResult);
                }
                //Validate Transaction Date should be greater than last transaction date
                var lastTransaction = await _bankAccountRepository.GetLastTransaction(toAccountId.Value);
                if (lastTransaction != null && lastTransaction?.TransactionTime.Date > transactionDate.Date)
                {
                    errors.Add(new Errors { PropertyName = "TransferAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." });
                }
                //Validate Transaction Date should be greater than last transaction date of budget account
                if (budgetAccounts != null && budgetAccounts.Count > 0)
                {
                    foreach (var bAccount in budgetAccounts)
                    {
                        var lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                        if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                        {
                            errors.Add(new Errors { PropertyName = "TransferAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." });
                        }
                    }
                }
                //If any validation error or error exists then return without saving any data into database
                if (validationResults.Count != 0 || errors.Count != 0)
                {
                    return (null, validationResults, errors);
                }

                if (lastTransaction != null && lastTransaction.TransactionTime.Date == transactionDate.Date)
                {
                    transactionDate = lastTransaction.TransactionTime;
                    transactionDate = transactionDate.AddMinutes(5);
                }
                //Get Account details and prepare transaction, transaction details and budget transaction details to save into database
                Account account = await _bankAccountRepository.GetAccount(toAccountId.Value);
                account.Balance = transactionCategoryEnum == TransactionCategoryEnum.Deposit ? account.Balance + amount : account.Balance - amount;

                Transaction transaction = new()
                {
                    Amount = amount,
                    Description = description,
                    TransactionTime = transactionDate,
                    TransactionCategory = transactionCategoryEnum.EnumToString(),
                    TransactionTag = transactionCategoryEnum == TransactionCategoryEnum.Deposit ? TransactionTagEnum.Deposit.EnumToString() : TransactionTagEnum.Withdraw.EnumToString()
                };
                TransactionDetail transactionDetails = new()
                {
                    AccountId = account.AccountId,
                    TransactionType = transactionCategoryEnum == TransactionCategoryEnum.Deposit ? TransactionTypeEnum.Credit.EnumToString() : TransactionTypeEnum.Debit.EnumToString(),
                    StageBalance = account.Balance,
                    TransactionRole = transactionCategoryEnum == TransactionCategoryEnum.Deposit ? TransactionRoleEnum.Receiver.EnumToString() : TransactionRoleEnum.Sender.EnumToString(),
                };

                List<BudgetTransactionDto> budgetAccountList = [];
                if (budgetAccounts != null && budgetAccounts.Count > 0)
                {
                    foreach (var bAccount in budgetAccounts)
                    {
                        var lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                        var budgetTransactionDate = transactionDate;
                        if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction.TransactionTime.Date == budgetTransactionDate.Date)
                        {
                            budgetTransactionDate = lastBudgetAccountTransaction.TransactionTime;
                            budgetTransactionDate = budgetTransactionDate.AddMinutes(5);
                        }
                        var budgetAccount = await _budgetRepository.GetBudgetAccountById(bAccount.Id);
                        budgetAccount.Balance = budgetAccount.Balance + bAccount.Amount;
                        var budgetTransaction = new BudgetTransaction()
                        {
                            Amount = bAccount.Amount,
                            Description = description,
                            TransactionTime = budgetTransactionDate,
                            TransactionCategory = transactionCategoryEnum.EnumToString(),
                            TransactionTag = transactionCategoryEnum == TransactionCategoryEnum.Deposit ? TransactionTagEnum.Deposit.EnumToString() : TransactionTagEnum.Withdraw.EnumToString()
                        };
                        var budgetTransactionDetails = new BudgetTransactionDetail()
                        {
                            TransactionType = transactionCategoryEnum == TransactionCategoryEnum.Deposit ? TransactionTypeEnum.Credit.EnumToString() : TransactionTypeEnum.Debit.EnumToString(),
                            StageBalance = budgetAccount.Balance,
                            TransactionRole = transactionCategoryEnum == TransactionCategoryEnum.Deposit ? TransactionRoleEnum.Receiver.EnumToString() : TransactionRoleEnum.Sender.EnumToString(),
                            AccountId = account.AccountId,
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
                return (new BulkTransactionDto
                {
                    FromAccount = null,
                    ToAccount = account,
                    Transaction = transaction,
                    FromTransactionDetail = null,
                    ToTransactionDetail = transactionDetails,
                    BudgetTransaction = budgetAccountList,
                    BudgetNotification = null,
                    TransactionCategory = transactionCategoryEnum
                }, validationResults, errors);
            }
            else
            {
                return (null, null, null);//TODO
            }
        }

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
                return [new ValidationResults { PropertyPath = "Request.Account.Amount", PropertyName = "Amount", ErrorMessage = "Amount should be lesser equal to Account Balance." }];
            }
            return null;
        }
        public int? InsertAccount(Account account)
        {
            account.CreatedBy = dataContext.UserEmail;
            account.CreatedDate = DateTime.Now;
            account.ModifiedBy = null;
            account.ModifiedDate = null;
            account.IsDeleted = false;
            var isInserted = Insert(account);
            return isInserted ? account.AccountId : null;
        }
        public void UpdateAccount(Account account)
        {
            account.ModifiedBy = dataContext.UserEmail;
            account.ModifiedDate = DateTime.Now;
            Update(account);
        }
        public int? InsertTransaction(Transaction transaction)
        {
            transaction.CreatedBy = dataContext.UserEmail;
            transaction.CreatedDate = DateTime.Now;
            transaction.ModifiedBy = null;
            transaction.ModifiedDate = null;
            transaction.IsDeleted = false;
            var isInserted = Insert(transaction);
            return isInserted ? transaction.TransactionId : null;
        }
        public void InsertTransactionDetail(TransactionDetail transactionDetail)
        {
            var isInserted = Insert(transactionDetail);
        }
        public void UpdateBudgetAccount(BudgetAccount model)
        {
            model.ModifiedBy = dataContext.UserEmail;
            model.ModifiedDate = DateTime.Now;
            Update(model);
        }
        public int? InsertBudgetTransaction(BudgetTransaction model)
        {
            model.CreatedBy = dataContext.UserEmail;
            model.CreatedDate = DateTime.Now;
            model.ModifiedBy = null;
            model.ModifiedDate = null;
            model.IsDeleted = false;
            var isInserted = Insert(model);
            return isInserted ? model.BudgetTransactionId : null;
        }
        public int? InsertBudgetTransactionDetail(BudgetTransactionDetail model)
        {
            var isInserted = Insert(model);
            return isInserted ? model.BudgetTransactionDetailId : null;
        }
        public int? InsertBudgetNotification(BudgetNotification model)
        {
            model.CreatedBy = dataContext.UserEmail;
            model.CreatedDate = DateTime.Now;
            model.ModifiedBy = null;
            model.ModifiedDate = null;
            model.IsDeleted = false;
            var isInserted = Insert(model);
            return isInserted ? model.BudgetNotificationId : null;
        }
    }
}
