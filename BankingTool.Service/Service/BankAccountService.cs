using BankingTool.Model;
using BankingTool.Model.Dto.BaseDto;
using BankingTool.Model.Dto.InternalDto;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Dto.ResponseDto;
using BankingTool.Model.Model;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class BankAccountService(IBankAccountRepository bankAccountRepository, IBudgetRepository budgetRepository) : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;
        private readonly IBudgetRepository _budgetRepository = budgetRepository;

        public async Task<ResponseDto<GetDashBoardInitialLoadResponse>> GetDashBoardInitialLoad()
        {
            var accountList = await _bankAccountRepository.GetDashBoardInitialLoad();
            var totalBalance = accountList.Where(z => z.AccountType == "Saving Account").Sum(x => x.Balance);
            return new ResponseDto<GetDashBoardInitialLoadResponse>
            {
                DropDownList = [],
                Errors = [],
                Response = new GetDashBoardInitialLoadResponse { TotalBalance = totalBalance, DashBoardAccountList = accountList },
                StatuCode = 200,
                ValidationErrors = [],
                Status = true
            };
        }
        public async Task<ResponseDto<bool>> GetCreateAccountInitialLoad()
        {
            return new ResponseDto<bool>
            {
                Response = true,
                Errors = [],
                DropDownList = new List<DropDownListDto>
                {
                    new DropDownListDto{Name = "AccountType",DropDown = await _bankAccountRepository.GetAccountTypeDropDown()},
                    new DropDownListDto{Name = "Customer",DropDown = await _bankAccountRepository.GetAllActiveCustomerDropDown()}
                },
                StatuCode = 200,
                Status = true
            };
        }
        public async Task<ResponseDto<bool>> GetBankDetailsDropDownWithoutCustomerAndAccountType(int customerId, int accountTypeId)
        {
            return new ResponseDto<bool>
            {
                DropDownList = [new DropDownListDto { Name = "Bank", DropDown = await _bankAccountRepository.GetBankDetailsDropDownWithoutCustomerAndAccountType(customerId, accountTypeId) }],
                Response = true,
                StatuCode = 200,
                Status = true,
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> IsCustomerHasCreditCardInThatBank(int customerId, int bankId)
        {
            bool isCustomerHasCreditCardInThatBank = await _bankAccountRepository.IsCustomerHasCreditCardInThatBank(customerId, bankId);
            return new ResponseDto<bool>
            {
                Response = isCustomerHasCreditCardInThatBank,
                StatuCode = 200,
                ValidationErrors = [],
                Errors = [],
                Status = true
            };
        }
        public async Task<ResponseDto<bool>> CreateAccount(CreateAccountRequest model)
        {
            var response = new ResponseDto<bool>
            {
                Response = false,
                Status = false,
                Errors = [],
                ValidationErrors = []
            };
            if (model == null)
            {
                return response;
            }

            Account account = new()
            {
                AccountNumber = await GetNewAccountNumber(),
                AccountStatus = AccountStatus.Active,
                CustomerId = model.Customer.Id,
                AccountTypeId = model.Account.AccountTypeId,
                Balance = Constants.AccountMininumBalanceForSavingsAccount,
                BankId = model.Bank.Id
            };

            Transaction transaction = new()
            {
                Amount = Constants.AccountMininumBalanceForSavingsAccount,
                Description = "Initial Credit",
                TransactionTime = new DateTime(2025, 5, 6),
                TransactionCategory = TransactionCatagory.Deposit
            };
            TransactionDetail transactionDetails = new()
            {
                TransactionType = TransactionType.Credit,
                StageBalance = Constants.AccountMininumBalanceForSavingsAccount,
                TransactionRole = TransactionRole.Receiver,
            };

            var (customer, isUpdatePrimaryAccount) = await UpdatePrimaryAccountNumber(model.Account.DoYouWantToChangeThisAccountToPrimaryAccount, account.AccountNumber, account.CustomerId);

            Card debitCard = new()
            {
                CardLimit = Constants.DefaultDebitCard,
                CardType = CardType.DebitCard,
                ExpireDate = DateTime.Now.AddMonths(48),
                CardNumber = await GetNewDebitCardNumber(),
                CVV = await GetNewDebitCVVNumber(),
            };
            Card creditCard = null;
            if (model.Customer.CustomerWantCreditCard)
            {
                creditCard = new()
                {
                    CardLimit = Constants.DefaultDebitCard,
                    CardType = CardType.CreditCard,
                    ExpireDate = DateTime.Now.AddMonths(48),
                    CardNumber = await GetNewCreditCardNumber(),
                    CVV = await GetNewCreditCVVNumber(),
                };
            }
            var IsAnyAccountForThisCustomer = await _bankAccountRepository.IsAnyAccountForThisCustomer(account.CustomerId);
            CreditScore creditScore = null;
            if (IsAnyAccountForThisCustomer)
            {
                creditScore = await _bankAccountRepository.GetCreditScoreOfCustomer(account.CustomerId);
                creditScore.CreditScoreValue = CalculateCreditScore(0, 0, 0.0, 0, 0);//TODO
            }
            else
            {
                creditScore = new()
                {
                    CreditScoreValue = CalculateCreditScore(0, 0, 0.0, 0, 0),
                    CustomerId = model.Customer.Id,
                    Description = null,
                    Status = CreditScoreStatus.Active,
                };
            }

            bool isAccountCreated = _bankAccountRepository.CreateAccount(account, transaction, transactionDetails, debitCard, creditCard, creditScore, customer, model.Customer.CustomerWantCreditCard, IsAnyAccountForThisCustomer, isUpdatePrimaryAccount);
            if (isAccountCreated)
            {
                response.Response = true;
                response.StatuCode = 200;
                response.Status = true;
                response.Message = "Account Created Successfully...";
                return response;
            }
            else
            {
                response.Response = false;
                response.Status = false;
                response.StatuCode = 400;
                response.Errors.Add(new Errors { PropertyName = "Create Account", ErrorMessage = "Failed to create account" });
                response.Message = "Failed to create account...";
                return response;
            }
        }
        public async Task<ResponseDto<GetTransactionsListResponse>> TransactionsListForCustomer(int bankId, int accountTypeId, int customerId)
        {
            var (accountId, name, bankName, accountType) = await _bankAccountRepository.GetAccountIdByBankIdAndAccountTypeAndCustomerId(bankId, accountTypeId, customerId);
            var senderAccountList = await _bankAccountRepository.GetSenderAccountDropdown(accountId);
            var receiverAccountList = await _bankAccountRepository.GetReceiverAccountDropdown(accountId);
            var senderAccountDropDownList = senderAccountList.Select(x => new DropDownDto
            {
                Key = x.AccountId,
                Value = x.AccountDescription,
                IsSelected = false
            }).ToList();
            var receiverAccountDropDownList = receiverAccountList.Select(x => new DropDownDto
            {
                Key = x.AccountId,
                Value = x.AccountDescription,
                IsSelected = false
            }).ToList();
            return new ResponseDto<GetTransactionsListResponse>
            {
                Response = new GetTransactionsListResponse
                {
                    AccountInfo = await _bankAccountRepository.GetAccountInfo(accountId, accountType, name, bankName),
                    CardInfo = await _bankAccountRepository.GetCardInfoByAccountId(accountId, name, bankName),
                    TransactionsList = await _bankAccountRepository.GetTransactionByAccountId(accountId, null, null, null, null, null, null),
                },
                StatuCode = 200,
                Status = true,
                Errors = [],
                ValidationErrors = [],
                DropDownList = [
                        new DropDownListDto { Name = "SenderAccount", DropDown = senderAccountDropDownList },
                        new DropDownListDto { Name = "ReceiverAccount", DropDown = receiverAccountDropDownList },
                        new DropDownListDto { Name = "TransactionCategory", DropDown = await _bankAccountRepository.GetAllTransactionCategory() },
                        new DropDownListDto { Name = "TransactionTag", DropDown = await _bankAccountRepository.GetAllTransactionTag() },
                    ]
            };
        }
        public async Task<ResponseDto<GetTransactionsListResponse>> TransactionsList(TransactionListRequest request)
        {
            return new ResponseDto<GetTransactionsListResponse>
            {
                Response = new GetTransactionsListResponse
                {
                    TransactionsList = await _bankAccountRepository.GetTransactionByAccountId(request.Account.Id, request.Transaction.TransactionTag,
                    request.Transaction.TransactionFromDate, request.Transaction.TransactionToDate, request.Account.SenderAccountId,
                    request.Account.ReceiverAccountId, request.Transaction.TransactionCategory),
                },
                StatuCode = 200,
                Status = true,
                Errors = [],
                ValidationErrors = []
            };
        }
        public async Task<ResponseDto<bool>> GetAccountTypeDropDownListByCustomerIdAndBankId(int customerId, int bankId)
        {
            return new ResponseDto<bool>
            {
                DropDownList = [new DropDownListDto { Name = "AccountType", DropDown = await _bankAccountRepository.GetAccountTypeDropDownListByCustomerIdAndBankId(customerId, bankId) }],
                StatuCode = 200,
                Response = true,
                Status = true,
                Errors = [],
                ValidationErrors = []
            };
        }
        public async Task<ResponseDto<bool>> BankDropDownList()
        {
            return new ResponseDto<bool>
            {
                DropDownList = [new DropDownListDto { Name = "Bank", DropDown = await _bankAccountRepository.GetBankDropDownListByCustomerId() }],
                Response = true,
                StatuCode = 200,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> GetTransferAmountInitialLoad()
        {
            return new ResponseDto<bool>
            {
                Response = true,
                DropDownList = new List<DropDownListDto>
                {
                    new DropDownListDto{ Name = "FromAccount", DropDown = await _bankAccountRepository.GetFromAccountListByCustomerId() },
                    new DropDownListDto{ Name = "ToAccount", DropDown = await _bankAccountRepository.GetToAccountListOnWithoutCustomerId() },
                    new DropDownListDto{ Name = "TransactionTag", DropDown = await _bankAccountRepository.GetAllTransactionTag() },
                    new DropDownListDto{ Name = "BudgetAccount", DropDown = await _budgetRepository.GetBudgetAccountDropdown() }
                },
                StatuCode = 200,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> GetSelfTransferInitialLoad()
        {
            return new ResponseDto<bool>
            {
                Response = true,
                DropDownList = new List<DropDownListDto>
                {
                    new DropDownListDto{ Name = "FromAccount", DropDown = await _bankAccountRepository.GetFromAccountListByCustomerId() },
                    new DropDownListDto{ Name = "TransactionTag", DropDown = await _bankAccountRepository.GetAllTransactionTag() },
                    new DropDownListDto{ Name = "BudgetAccount", DropDown = await _budgetRepository.GetBudgetAccountDropdown() }
                },
                StatuCode = 200,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> GetToAccountListExcludedByAccountId(int accountId)
        {
            return new ResponseDto<bool>
            {
                Response = true,
                DropDownList = new List<DropDownListDto>
                {
                    new DropDownListDto{ Name = "ToAccount", DropDown = await _bankAccountRepository.GetToAccountListExcludedByAccountId(accountId) }
                },
                StatuCode = 200,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<int>> GetAccountBalance(int accountId)
        {
            return new ResponseDto<int>
            {
                Response = await _bankAccountRepository.GetAccountBalance(accountId),
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> TransferAmount(int fromAccountId, int toAccountId, int amount, string description, DateTime transactionDate,
            string transactionTag, List<TransferAmountRequestBudgetAccount> budgetAccounts)
        {
            var account = await _bankAccountRepository.GetAccount(fromAccountId);
            var validateBalance = ValidateBalance(account.Balance, amount);
            if (validateBalance != null)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = validateBalance,
                    Errors = [],
                    StatuCode = 400
                };
            }
            List<BudgetAccountDto> validateBudgetAccount = [.. budgetAccounts.Select(x => new BudgetAccountDto
            {
                Id = x.Id,
                Amount = x.Amount
            })];
            var validationResult = ValidateBudgetAccount(validateBudgetAccount, amount);
            if (validationResult != null)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = validationResult,
                    Errors = [],
                    StatuCode = 400
                };
            }
            var lastFromAccountTransaction = await _bankAccountRepository.GetLastTransaction(fromAccountId);
            var lastToAccountTransaction = await _bankAccountRepository.GetLastTransaction(toAccountId);
            if (lastFromAccountTransaction != null && lastFromAccountTransaction?.TransactionTime.Date > transactionDate.Date)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = [],
                    Errors = [new Errors { PropertyName = "TransferAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." }],
                    StatuCode = 400
                };
            }
            if (lastToAccountTransaction != null && lastToAccountTransaction?.TransactionTime.Date > transactionDate.Date)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = [],
                    Errors = [new Errors { PropertyName = "TransferAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." }],
                    StatuCode = 400
                };
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
            Account fromAccount = await _bankAccountRepository.GetAccount(fromAccountId);
            Account toAccount = await _bankAccountRepository.GetAccount(toAccountId);
            fromAccount.Balance = fromAccount.Balance - amount;
            toAccount.Balance = toAccount.Balance + amount;
            Transaction transaction = new Transaction
            {
                TransactionTime = transactionDate,
                Amount = amount,
                Description = description,
                TransactionCategory = TransactionCatagory.Transfer,
                TransactionTag = transactionTag
            };
            TransactionDetail fromTransactionDetail = new TransactionDetail
            {
                AccountId = fromAccountId,
                StageBalance = fromAccount.Balance,
                TransactionType = TransactionType.Debit,
                TransactionRole = TransactionRole.Sender
            };
            TransactionDetail toTransactionDetail = new TransactionDetail
            {
                AccountId = toAccountId,
                StageBalance = toAccount.Balance,
                TransactionType = TransactionType.Credit,
                TransactionRole = TransactionRole.Receiver
            };

            List<BudgetTransactionDto> budgetAccountList = [];
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                foreach (var bAccount in budgetAccounts)
                {
                    var lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                    if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                    {
                        return new ResponseDto<bool>
                        {
                            Response = false,
                            Status = false,
                            ValidationErrors = [],
                            Errors = [new Errors { PropertyName = "TransferAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." }]
                        };
                    }
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
                        TransactionCategory = TransactionCatagory.Transfer,
                        TransactionTag = transactionTag
                    };
                    var budgetTransactionDetails = new BudgetTransactionDetail()
                    {
                        TransactionType = TransactionType.Debit,
                        StageBalance = budgetAccount.Balance,
                        TransactionRole = TransactionRole.Sender,
                        AccountId = fromAccountId,
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

            var isCustomerAsBudgetAccount = await _budgetRepository.IsCustomerAsBudgetAccount(toAccount.CustomerId);
            BudgetNotification budgetNotification = null;
            if (isCustomerAsBudgetAccount)
            {
                budgetNotification = new BudgetNotification()
                {
                    AccountId = toAccountId,
                    Amount = amount,
                    Description = description,
                    TransactionDate = transactionDate,
                    TransactionCatagory = TransactionCatagory.Transfer,
                    TransactionTag = transactionTag,
                    TransactionRole = TransactionRole.Receiver,
                    TransactionType = TransactionType.Credit,
                    CustomerId = toAccount.CustomerId,
                    NotificationStatus = NotificationStatus.Pending
                };
            }

            var isSaved = _bankAccountRepository.TransferAmount(fromAccount, toAccount, transaction, fromTransactionDetail, toTransactionDetail,
                budgetAccountList, budgetNotification, isCustomerAsBudgetAccount);
            return new ResponseDto<bool>
            {
                Response = isSaved,
                Status = isSaved,
                Message = isSaved ? "Transfered Successfully." : "Transfer Failed.",
                ValidationErrors = [],
                Errors = isSaved ? [] : [new Errors { PropertyName = "TransferAmount", ErrorMessage = "Transfer Failed" }]
            };
        }
        public async Task<ResponseDto<bool>> DepositAmount(int accountId, int amount, string description, DateTime transactionDate, List<DepositAmountRequestBudgetAccount> budgetAccounts)
        {
            List<BudgetAccountDto> validateBudgetAccount = [.. budgetAccounts.Select(budgetAccounts => new BudgetAccountDto
            {
                Id = budgetAccounts.Id,
                Amount = budgetAccounts.Amount
            })];
            var validationResult = ValidateBudgetAccount(validateBudgetAccount, amount);
            if (validationResult != null)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = validationResult,
                    Errors = [],
                    StatuCode = 400
                };
            }

            var lastTransaction = await _bankAccountRepository.GetLastTransaction(accountId);
            if (lastTransaction != null && lastTransaction?.TransactionTime.Date > transactionDate.Date)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = [],
                    Errors = [new Errors { PropertyName = "DepositAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." }]
                };
            }
            if (lastTransaction != null && lastTransaction.TransactionTime.Date == transactionDate.Date)
            {
                transactionDate = lastTransaction.TransactionTime;
                transactionDate = transactionDate.AddMinutes(5);
            }
            Account account = await _bankAccountRepository.GetAccount(accountId);
            account.Balance = account.Balance + amount;

            Transaction transaction = new()
            {
                Amount = amount,
                Description = description,
                TransactionTime = transactionDate,
                TransactionCategory = TransactionCatagory.Deposit,
                TransactionTag = TransactionTag.Deposit
            };
            TransactionDetail transactionDetails = new()
            {
                AccountId = account.AccountId,
                TransactionType = TransactionType.Credit,
                StageBalance = account.Balance,
                TransactionRole = TransactionRole.Receiver,
            };

            List<BudgetTransactionDto> budgetAccountList = [];
            bool isBudgetAccountAttached = false;
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                isBudgetAccountAttached = true;
                foreach (var bAccount in budgetAccounts)
                {
                    var lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                    if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                    {
                        return new ResponseDto<bool>
                        {
                            Response = false,
                            Status = false,
                            ValidationErrors = [],
                            Errors = [new Errors { PropertyName = "DepositAmount", ErrorMessage = "Transaction Date should be greater then last transaction date." }]
                        };
                    }
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
                        TransactionCategory = TransactionCatagory.Deposit,
                        TransactionTag = TransactionTag.Deposit
                    };
                    var budgetTransactionDetails = new BudgetTransactionDetail()
                    {
                        TransactionType = TransactionType.Credit,
                        StageBalance = budgetAccount.Balance,
                        TransactionRole = TransactionRole.Receiver,
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

            _bankAccountRepository.CashTransfer(account, transaction, transactionDetails, isBudgetAccountAttached, budgetAccountList);
            return new ResponseDto<bool>
            {
                Response = true,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> CashWithdraw(int accountId, int amount, string description, DateTime transactionDate, List<CashWithdrawRequestBudgetAccount> budgetAccounts)
        {
            Account account = await _bankAccountRepository.GetAccount(accountId);
            var validationResultBalance = ValidateBalance(account.Balance, amount);
            if (validationResultBalance != null)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = validationResultBalance,
                    Errors = [],
                    StatuCode = 400
                };
            }
            List<BudgetAccountDto> validateBudgetAccount = [.. budgetAccounts.Select(budgetAccounts => new BudgetAccountDto
            {
                Id = budgetAccounts.Id,
                Amount = budgetAccounts.Amount
            })];
            var validationResult = ValidateBudgetAccount(validateBudgetAccount, amount);
            if (validationResult != null)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = validationResult,
                    Errors = [],
                    StatuCode = 400
                };
            }
            var lastTransaction = await _bankAccountRepository.GetLastTransaction(accountId);
            if (lastTransaction != null && lastTransaction?.TransactionTime.Date > transactionDate.Date)
            {
                return new ResponseDto<bool>
                {
                    Response = false,
                    Status = false,
                    ValidationErrors = [],
                    Errors = [new Errors { PropertyName = "CashWithdraw", ErrorMessage = "Transaction Date should be greater then last transaction date." }]
                };
            }
            if (lastTransaction != null && lastTransaction.TransactionTime.Date == transactionDate.Date)
            {
                transactionDate = lastTransaction.TransactionTime;
                transactionDate = transactionDate.AddMinutes(5);
            }
            account.Balance = account.Balance - amount;

            Transaction transaction = new()
            {
                Amount = amount,
                Description = description,
                TransactionTime = transactionDate,
                TransactionCategory = TransactionCatagory.Withdraw,
                TransactionTag = TransactionTag.Withdraw
            };
            TransactionDetail transactionDetails = new()
            {
                AccountId = account.AccountId,
                TransactionType = TransactionType.Debit,
                StageBalance = account.Balance,
                TransactionRole = TransactionRole.Sender,
            };

            List<BudgetTransactionDto> budgetAccountList = [];
            bool isBudgetAccountAttached = false;
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                isBudgetAccountAttached = true;
                foreach (var bAccount in budgetAccounts)
                {
                    BudgetTransaction lastBudgetAccountTransaction = await _budgetRepository.GetLastBudgetTransaction(bAccount.Id);
                    if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction?.TransactionTime.Date > transactionDate.Date)
                    {
                        return new ResponseDto<bool>
                        {
                            Response = false,
                            Status = false,
                            ValidationErrors = [],
                            Errors = [new Errors { PropertyName = "CashWithdraw", ErrorMessage = "Transaction Date should be greater then last transaction date." }]
                        };
                    }
                    DateTime budgetTransactionDate = transactionDate;
                    if (lastBudgetAccountTransaction != null && lastBudgetAccountTransaction.TransactionTime.Date == budgetTransactionDate.Date)
                    {
                        budgetTransactionDate = lastBudgetAccountTransaction.TransactionTime;
                        budgetTransactionDate = budgetTransactionDate.AddMinutes(5);
                    }
                    BudgetAccount budgetAccount = await _budgetRepository.GetBudgetAccountById(bAccount.Id);
                    budgetAccount.Balance = budgetAccount.Balance - bAccount.Amount;
                    BudgetTransaction budgetTransaction = new BudgetTransaction()
                    {
                        Amount = bAccount.Amount,
                        Description = description,
                        TransactionTime = budgetTransactionDate,
                        TransactionCategory = TransactionCatagory.Withdraw,
                        TransactionTag = TransactionTag.Withdraw
                    };
                    BudgetTransactionDetail budgetTransactionDetails = new BudgetTransactionDetail()
                    {
                        TransactionType = TransactionType.Debit,
                        StageBalance = budgetAccount.Balance,
                        TransactionRole = TransactionRole.Sender,
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

            _bankAccountRepository.CashTransfer(account, transaction, transactionDetails, isBudgetAccountAttached, budgetAccountList);
            return new ResponseDto<bool>
            {
                Response = true,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        #region Private
        private async Task<(Customer, bool)> UpdatePrimaryAccountNumber(bool DoYouWantToChangeThisAccountToPrimaryAccount, string AccountNumber, int CustomerId)
        {
            bool isAnyAccountForThisCustomer = await _bankAccountRepository.IsAnyAccountForThisCustomer(CustomerId);
            Customer customer = null;
            bool IsUpdatePrimaryAccountNumber = false;
            if (isAnyAccountForThisCustomer && DoYouWantToChangeThisAccountToPrimaryAccount)
            {
                IsUpdatePrimaryAccountNumber = true;
                customer = await _bankAccountRepository.GetCustomerByCustomerId(CustomerId);
                customer.PrimaryAccountNumber = AccountNumber;
            }
            else if (!isAnyAccountForThisCustomer)
            {
                IsUpdatePrimaryAccountNumber = true;
                customer = await _bankAccountRepository.GetCustomerByCustomerId(CustomerId);
                customer.PrimaryAccountNumber = AccountNumber;
            }
            return (customer, IsUpdatePrimaryAccountNumber);
        }
        private async Task<string> GetNewAccountNumber()
        {
            var lastAccount = await _bankAccountRepository.GetLastAccount();
            return ((lastAccount != null && long.TryParse(lastAccount.AccountNumber, out long lastNumber)) ? lastNumber + 1 : 9000000000000000).ToString();
        }
        private async Task<string> GetNewDebitCardNumber()
        {
            var lastCard = await _bankAccountRepository.GetLastCard(CardType.DebitCard);
            return ((lastCard != null && long.TryParse(lastCard.CardNumber, out long lastNumber)) ? lastNumber + 1 : 8000000000000000).ToString();
        }
        private async Task<string> GetNewCreditCardNumber()
        {
            var lastCard = await _bankAccountRepository.GetLastCard(CardType.CreditCard);
            return ((lastCard != null && long.TryParse(lastCard.CardNumber, out long lastNumber)) ? lastNumber + 1 : 7000000000000000).ToString();
        }
        private async Task<int> GetNewDebitCVVNumber()
        {
            var lastCard = await _bankAccountRepository.GetLastCard(CardType.DebitCard);
            return (lastCard != null) ? lastCard.CVV + 1 : 100;
        }
        private async Task<int> GetNewCreditCVVNumber()
        {
            var lastCard = await _bankAccountRepository.GetLastCard(CardType.CreditCard);
            return (lastCard != null) ? lastCard.CVV + 1 : 100;
        }
        private static int CalculateCreditScore(int onTimePayments, int totalPayments, double creditUtilization, int creditAge, int inquiries)
        {
            // Define base score
            int score = 300;

            // Payment history (35% weight)
            double paymentHistoryFactor = totalPayments > 0 ? (onTimePayments / (double)totalPayments) * 100 : 0;
            score += (int)(paymentHistoryFactor * 0.35);

            // Credit utilization (30% weight, should be below 30% for best scores)
            score += (int)((1 - Math.Min(creditUtilization, 1)) * 100 * 0.3);

            // Credit age (15% weight, longer is better)
            score += (creditAge * 2); // Assuming 2 points per year

            // Credit inquiries (10% weight, fewer is better)
            score -= inquiries * 5; // Deduct 5 points per inquiry

            // Ensure score stays within valid range (300 - 850)
            return Math.Clamp(score, 300, 850);
        }
        private static List<ValidationResults> ValidateBudgetAccount(List<BudgetAccountDto> budgetAccounts, int amount)
        {
            if (budgetAccounts != null && budgetAccounts.Count > 0)
            {
                bool isInValid = budgetAccounts.Any(x => x.Id == null);
                bool isAmountInValid = budgetAccounts.Any(x => x?.Amount == null || x.Amount == 0);
                int sumOfBudgetAccountAmount = budgetAccounts.Sum(x => x.Amount);
                if (sumOfBudgetAccountAmount > amount)
                {
                    return [new ValidationResults { PropertyPath = "Request.BudgetAccount.Asmount", PropertyName = "Amount", ErrorMessage = "Sum of Budget Account Amount should be lesser equal to Transfer Amount." }];
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
        #endregion Private
    }
}
