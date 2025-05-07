using BankingTool.Model;
using BankingTool.Model.Dto.RequestDto.BankAccount;
using BankingTool.Model.Dto.Response;
using BankingTool.Model.Model;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class BankAccountService(IBankAccountRepository bankAccountRepository) : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;

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
                TransactionTime = DateTime.Now,
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
                    new DropDownListDto{ Name = "ToAccount", DropDown = await _bankAccountRepository.GetToAccountListOnWithoutCustomerId() }
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
                    new DropDownListDto{ Name = "FromAccount", DropDown = await _bankAccountRepository.GetFromAccountListByCustomerId() }
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
        public async Task<ResponseDto<bool>> TransferAmount(int fromAccountId, int toAccountId, int amount, string description)
        {
            Account fromAccount = await _bankAccountRepository.GetAccount(fromAccountId);
            Account toAccount = await _bankAccountRepository.GetAccount(toAccountId);
            fromAccount.Balance = fromAccount.Balance - amount;
            toAccount.Balance = toAccount.Balance + amount;
            Transaction transaction = new Transaction
            {
                TransactionTime = DateTime.Now,
                Amount = amount,
                Description = description,
                TransactionCategory = TransactionCatagory.Transfer
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
            _bankAccountRepository.TransferAmount(fromAccount, toAccount, transaction, fromTransactionDetail, toTransactionDetail);
            return new ResponseDto<bool>
            {
                Response = true,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> DepositAmount(int accountId, int amount, string description)
        {
            Account account = await _bankAccountRepository.GetAccount(accountId);
            account.Balance = account.Balance + amount;

            Transaction transaction = new()
            {
                Amount = amount,
                Description = description,
                TransactionTime = DateTime.Now,
                TransactionCategory = TransactionCatagory.Deposit
            };
            TransactionDetail transactionDetails = new()
            {
                AccountId = account.AccountId,
                TransactionType = TransactionType.Credit,
                StageBalance = account.Balance,
                TransactionRole = TransactionRole.Receiver,
            };
            _bankAccountRepository.CashTransfer(account, transaction, transactionDetails);
            return new ResponseDto<bool>
            {
                Response = true,
                Status = true,
                ValidationErrors = [],
                Errors = []
            };
        }
        public async Task<ResponseDto<bool>> CashWithdraw(int accountId, int amount, string description)
        {
            Account account = await _bankAccountRepository.GetAccount(accountId);
            account.Balance = account.Balance - amount;

            Transaction transaction = new()
            {
                Amount = amount,
                Description = description,
                TransactionTime = DateTime.Now,
                TransactionCategory = TransactionCatagory.Withdraw
            };
            TransactionDetail transactionDetails = new()
            {
                AccountId = account.AccountId,
                TransactionType = TransactionType.Debit,
                StageBalance = account.Balance,
                TransactionRole = TransactionRole.Sender,
            };
            _bankAccountRepository.CashTransfer(account, transaction, transactionDetails);
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
        #endregion Private
    }
}
