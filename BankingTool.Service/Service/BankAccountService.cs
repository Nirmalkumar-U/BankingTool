using BankingTool.Model;
using BankingTool.Model.Dto.BankAccount;
using BankingTool.Repository;
using BankingTool.Repository.IRepository;
using BankingTool.Service.IService;

namespace BankingTool.Service.Service
{
    public class BankAccountService(IBankAccountRepository bankAccountRepository, ICommonRepository commonRepository) : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;
        private readonly ICommonRepository _commonRepository = commonRepository;

        public async Task<ResponseDto<CreateAccountInitialLoadDto>> GetCreateAccountInitialLoad()
        {
            return new ResponseDto<CreateAccountInitialLoadDto>
            {
                Result = new()
                {
                    AccountTypeList = await _bankAccountRepository.GetAccountTypeDropDown(),
                    BankList = await _bankAccountRepository.GetBankDetailsDropDown(),
                    CustomerList = await _bankAccountRepository.GetAllActiveCustomerDropDown()
                },
                Status = true,
                Message = []
            };
        }

        public async Task<ResponseDto<bool>> CreateAccount(CreateAccountDto model)
        {
            var response = new ResponseDto<bool>
            {
                Result = false,
                Status = true,
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
            int? accountId = _bankAccountRepository.InsertAccount(account);

            Transaction transaction = new()
            {
                TransactionType = TransactionType.Credit,
                Amount = Constants.AccountMininumBalanceForSavingsAccount,
                Description = "Initial Credit",
                AccountId = accountId.Value,
                TransactionTime = DateTime.Now
            };
            _ = _bankAccountRepository.InsertTransaction(transaction);

            await UpdatePrimaryAccountNumber(model.DoYouWantToChangeThisAccountToPrimaryAccount, account.AccountNumber, account.CustomerId);

            Card debitCard = new()
            {
                AccountId = accountId.Value,
                CardLimit = Constants.DefaultDebitCard,
                CardType = CardType.DebitCard,
                ExpireDate = DateTime.Now.AddMonths(48),
                CardNumber = await GetNewDebitCardNumber(),
                CVV = await GetNewDebitCVVNumber(),
            };
            _ = _bankAccountRepository.InsertCard(debitCard);
            if (model.CustomerWantCreditCard)
            {
                Card creditCard = new()
                {
                    AccountId = accountId.Value,
                    CardLimit = Constants.DefaultDebitCard,
                    CardType = CardType.CreditCard,
                    ExpireDate = DateTime.Now.AddMonths(48),
                    CardNumber = await GetNewCreditCardNumber(),
                    CVV = await GetNewCreditCVVNumber(),
                };
                _ = _bankAccountRepository.InsertCard(creditCard);
            }
            var IsAnyAccountForThisCustomer = false; //await _bankAccountRepository.IsAnyAccountForThisCustomer(account.CustomerId);
            if (IsAnyAccountForThisCustomer)
            {
                //TODO
            }
            else
            {
                CreditScore cardScore = new()
                {
                    CreditScoreValue = CalculateCreditScore(0, 0, 0.0, 0, 0),
                    CustomerId = account.CustomerId,
                    Description = null,
                    Status = CreditScoreStatus.Active,

                };
                _bankAccountRepository.InsertCreditScore(cardScore);
            }
            await _commonRepository.SaveTransaction();

            response.Result = true;
            response.Status = true;
            response.Message.Add("Account Created Successfully...");
            return response;
        }

        private async Task UpdatePrimaryAccountNumber(bool DoYouWantToChangeThisAccountToPrimaryAccount, string AccountNumber, int CustomerId)
        {
            bool isAnyAccountForThisCustomer = await _bankAccountRepository.IsAnyAccountForThisCustomer(CustomerId);

            if (isAnyAccountForThisCustomer && DoYouWantToChangeThisAccountToPrimaryAccount)
            {
                Customer customer = await _bankAccountRepository.GetCustomerByCustomerId(CustomerId);
                customer.PrimaryAccountNumber = AccountNumber;
                _bankAccountRepository.UpdateCustomer(customer);
            }
            else if (!isAnyAccountForThisCustomer)
            {
                Customer customer = await _bankAccountRepository.GetCustomerByCustomerId(CustomerId);
                customer.PrimaryAccountNumber = AccountNumber;
                _bankAccountRepository.UpdateCustomer(customer);
            }

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
