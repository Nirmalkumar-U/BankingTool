using BankingTool.Model;
using BankingTool.Model.Dto.BankAccount;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class BankAccountService(IBankAccountRepository bankAccountRepository) : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;

        public async Task<ResponseDto<CreateAccountInitialLoadDto>> GetCreateAccountInitialLoad()
        {
            return new ResponseDto<CreateAccountInitialLoadDto>
            {
                Result = new()
                {
                    AccountTypeList = await _bankAccountRepository.GetAccountTypeDropDown(),
                    CustomerList = await _bankAccountRepository.GetAllActiveCustomerDropDown()
                },
                Status = true,
                Message = []
            };
        }
        public async Task<ResponseDto<List<DropDownDto>>> GetBankDetailsDropDownWithoutCustomerAndAccountType(int customerId, int accountTypeId)
        {
            return new ResponseDto<List<DropDownDto>>
            {
                Result = await _bankAccountRepository.GetBankDetailsDropDownWithoutCustomerAndAccountType(customerId, accountTypeId),
                Status = true,
                Message = []
            };
        }
        public async Task<ResponseDto<bool>> IsCustomerHasCreditCardInThatBank(int customerId, int bankId)
        {
            bool isCustomerHasCreditCardInThatBank = await _bankAccountRepository.IsCustomerHasCreditCardInThatBank(customerId, bankId);
            return new ResponseDto<bool>
            {
                Result = isCustomerHasCreditCardInThatBank,
                Status = isCustomerHasCreditCardInThatBank,
                Message = []
            };
        }
        public async Task<ResponseDto<bool>> CreateAccount(CreateAccountDto model)
        {
            var response = new ResponseDto<bool>
            {
                Result = false,
                Status = false,
                Message = []
            };
            if (model == null)
            {
                return response;
            }

            Account account = new()
            {
                AccountNumber = await GetNewAccountNumber(),
                AccountStatus = AccountStatus.Active,
                CustomerId = model.CustomerId,
                AccountTypeId = model.AccountTypeId,
                Balance = Constants.AccountMininumBalanceForSavingsAccount,
                BankId = model.BankId
            };

            Transaction transaction = new()
            {
                TransactionType = TransactionType.Credit,
                Amount = Constants.AccountMininumBalanceForSavingsAccount,
                Description = "Initial Credit",
                TransactionTime = DateTime.Now,
                StageBalance = Constants.AccountMininumBalanceForSavingsAccount
            };

            var (customer, isUpdatePrimaryAccount) = await UpdatePrimaryAccountNumber(model.DoYouWantToChangeThisAccountToPrimaryAccount, account.AccountNumber, account.CustomerId);

            Card debitCard = new()
            {
                CardLimit = Constants.DefaultDebitCard,
                CardType = CardType.DebitCard,
                ExpireDate = DateTime.Now.AddMonths(48),
                CardNumber = await GetNewDebitCardNumber(),
                CVV = await GetNewDebitCVVNumber(),
            };
            Card creditCard = null;
            if (model.CustomerWantCreditCard)
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
            var IsAnyAccountForThisCustomer = false; //await _bankAccountRepository.IsAnyAccountForThisCustomer(account.CustomerId);
            CreditScore cardScore = null;
            if (IsAnyAccountForThisCustomer)
            {
                //TODO
            }
            else
            {
                cardScore = new()
                {
                    CreditScoreValue = CalculateCreditScore(0, 0, 0.0, 0, 0),
                    CustomerId = model.CustomerId,
                    Description = null,
                    Status = CreditScoreStatus.Active,
                };
            }

            bool isAccountCreated = _bankAccountRepository.CreateAccount(account, transaction, debitCard, creditCard, cardScore, customer, model.CustomerWantCreditCard, IsAnyAccountForThisCustomer, isUpdatePrimaryAccount);
            if (isAccountCreated)
            {
                response.Result = true;
                response.Status = true;
                response.Message.Add("Account Created Successfully...");
                return response;
            }
            else
            {
                response.Result = false;
                response.Status = false;
                response.Message.Add("Failed to create account...");
                return response;
            }
        }
        public async Task<ResponseDto<GetTransactionsListDto>> TransactionsListForCustomer(int bankId, int accountTypeId, int customerId)
        {
            var (accountId, name, bankName, accountType) = await _bankAccountRepository.GetAccountIdByBankIdAndAccountTypeAndCustomerId(bankId, accountTypeId, customerId);
            return new ResponseDto<GetTransactionsListDto>
            {
                Message = [],
                Result = new GetTransactionsListDto
                {
                    AccountInfo = await _bankAccountRepository.GetAccountInfo(accountId, accountType, name, bankName),
                    CardInfo = await _bankAccountRepository.GetCardInfoByAccountId(accountId, name, bankName),
                    TransactionsList = await _bankAccountRepository.GetTransactionByAccountId(accountId),
                },
                Status = true
            };
        }
        public async Task<ResponseDto<List<DropDownDto>>> GetAccountTypeDropDownListByCustomerIdAndBankId(int customerId, int bankId)
        {
            return new ResponseDto<List<DropDownDto>>
            {
                Message = [],
                Result = await _bankAccountRepository.GetAccountTypeDropDownListByCustomerIdAndBankId(customerId, bankId),
                Status = true
            };
        }
        public async Task<ResponseDto<List<DropDownDto>>> BankDropDownList(int customerId)
        {
            return new ResponseDto<List<DropDownDto>>
            {
                Message = [],
                Result = await _bankAccountRepository.GetBankDropDownListByCustomerId(customerId),
                Status = true
            };
        }
        public async Task<ResponseDto<TransferAmountInitialLoadDto>> GetTransferAmountInitialLoad(int customerId)
        {
            return new ResponseDto<TransferAmountInitialLoadDto>()
            {
                Message = [],
                Result = new TransferAmountInitialLoadDto
                {
                    FromAccount = await _bankAccountRepository.GetFromAccountListByCustomerId(customerId),
                    ToAccount = await _bankAccountRepository.GetToAccountListOnWithoutCustomerId(customerId)
                },
                Status = true
            };
        }
        public async Task<ResponseDto<int>> GetAccountBalance(int accountId)
        {
            return new ResponseDto<int>
            {
                Message = [],
                Result = await _bankAccountRepository.GetAccountBalance(accountId),
                Status = true
            };
        }
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
            return (lastCard != null) ? lastCard.CVV + 1 : 001;
        }
        private async Task<int> GetNewCreditCVVNumber()
        {
            var lastCard = await _bankAccountRepository.GetLastCard(CardType.CreditCard);
            return (lastCard != null) ? lastCard.CVV + 1 : 001;
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
    }
}
